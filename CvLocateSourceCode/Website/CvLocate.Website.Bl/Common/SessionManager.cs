using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace CvLocate.Website.Bl.Common
{
    public class SessionManager
    {
        #region Singleton
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<SessionManager> _instance = new Lazy<SessionManager>(() => new SessionManager());

        // private to prevent direct instantiation.
        private SessionManager()
        {
        }

        // accessor for instance
        public static SessionManager Instance
        {
            get
            {

                return _instance.Value;
            }
        }

        #endregion

        #region Public Methods

        public bool AuthenticateUser(string userEmail, string sessionId)
        {
            try
            {
                SessionData session = GetSessionBySessionId(sessionId);

                if (session == null || session.ExpireTime < DateTime.UtcNow)
                    return false;


                //update expire time
                RefreshExpireTime(session);

                SetPrincipal(sessionId);

                return true;
            }
            catch (Exception ex)
            {
                //logger.WriteError(ex);
                return false;
            }
        }

        private void RefreshExpireTime(SessionData session)
        {
            long timesToAdd = 2000000;//todo replace to parameter
            session.ExpireTime = DateTime.UtcNow.AddMilliseconds(timesToAdd);
        }



        public string CreateSession(string userId, UserType userType, string email)
        {
            string sessionId = Guid.NewGuid().ToString();

            //Save in current principal
            SetPrincipal(sessionId);

            SessionData session = new SessionData()
            {
                UserId = userId,
                UserEmail = email,
                UserType = userType,
                SessionId = sessionId,
                ValidFrom = DateTime.UtcNow
            };

            RefreshExpireTime(session);

            AddSession(session);
            //add session also to DB to enable scalibility to many servers
            return sessionId;
        }

        public SessionData GetCurrentSession()
        {
            Dictionary<String, SessionData> sessions = GetSessions();
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                var sessionId = HttpContext.Current.User.Identity.Name;
                if (sessions != null && sessionId != "" && sessions.ContainsKey(sessionId))
                {
                    return sessions[sessionId];
                }
            }
            throw new ArgumentNullException("the sessionId is null in cache manager");
        }

        public SessionData GetSessionBySessionId(string sessionId)//just for AuthenticationHandler
        {
            Dictionary<String, SessionData> sessions = GetSessions();
            if (sessions != null && sessionId != "")
            {
                if (sessions.ContainsKey(sessionId))
                    return sessions[sessionId];
            }
            return null;
        }

        public void RemoveSessionBySessionId(string sessionId)
        {
            Dictionary<String, SessionData> sessions = GetSessions();
            if (sessions != null && sessionId != "")
            {
                sessions.Remove(sessionId);
            }

        }


        public void Initilize()
        {
            CheckExpireTimeSessions();
        }

        #endregion

        #region Privates
        private void RemoveExpiredSessions()
        {

            Dictionary<String, SessionData> sessions = GetSessions();
            if (sessions != null && sessions.Count != 0)
            {
                var keysToRemove = sessions.Where(item => item.Value.ExpireTime.AddMinutes(5) < DateTime.UtcNow)
                        .Select(item => item.Key)
                        .ToArray();
                foreach (var key in keysToRemove)
                {
                    sessions.Remove(key);
                }
            }

        }

        private void AddSession(SessionData session)
        {
            Dictionary<String, SessionData> sessions = GetSessions();
            if (sessions != null)
                sessions[session.SessionId] = session;
            else
            {
                sessions = new Dictionary<String, SessionData>();
                sessions.Add(session.SessionId, session);
            }
            CacheManager.Instance.SaveInCache(CacheItem.Sessions, sessions);
        }

        private Dictionary<String, SessionData> GetSessions()
        {
            return CacheManager.Instance.GetCacheItem(CacheItem.Sessions) as Dictionary<String, SessionData>;
        }

        private void CheckExpireTimeSessions()
        {
            // Dynamically create new timer
            System.Timers.Timer timScheduledTask = new System.Timers.Timer();

            long spanTime = 20000;//todo change to setting - Angular1BaseConfigurationSection.Settings.ExpireCache.Milliseconds;

            // Timer interval is set in miliseconds,
            // In this case, we'll run a task every minute
            timScheduledTask.Interval = spanTime;
            timScheduledTask.Enabled = true;
            // Add handler for Elapsed event
            timScheduledTask.Elapsed +=
            new ElapsedEventHandler(RemoveExpiredSessions);
            timScheduledTask.Stop();
            timScheduledTask.Start();
        }

        private void RemoveExpiredSessions(object sender, System.Timers.ElapsedEventArgs e)
        {
            RemoveExpiredSessions();
        }

        private void SetPrincipal(string sessionId)
        {
            var role = string.Empty;
            var identity = new GenericIdentity(sessionId);
            var principal = new GenericPrincipal(identity, new string[] { role });
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        #endregion
    }

}
