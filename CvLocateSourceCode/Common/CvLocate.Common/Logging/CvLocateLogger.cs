using log4net.Core;
using System.Reflection;



namespace CvLocate.Common.Logging
{
    public class CvLocateLogger : ICvLocateLogger
    {
        private static readonly ILogger defaultLogger;
        private ILogger logger;

        static CvLocateLogger()
        {
            defaultLogger =
              LoggerManager.GetLogger(Assembly.GetCallingAssembly(), "defaultLogger");
            log4net.Config.XmlConfigurator.Configure(defaultLogger.Repository);
        }

        public CvLocateLogger(string loggerName = null)
        {
            if (!string.IsNullOrWhiteSpace(loggerName))
            {
                logger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), loggerName);
                log4net.Config.XmlConfigurator.Configure(logger.Repository);

            }
        }
        public void Debug(string message)
        {
            ILogger currentLogger = logger == null ? defaultLogger : logger;
            if (currentLogger.IsEnabledFor(Level.Debug))
            {
                currentLogger.Log(typeof(CvLocateLogger), Level.Debug, message, null);
            }
        }
        public void DebugFormat(string message, params object[] args)
        {
            Debug(string.Format(message, args));
        }
        public void Info(string message)
        {
            ILogger currentLogger = logger == null ? defaultLogger : logger;
            if (currentLogger.IsEnabledFor(Level.Info))
            {
                currentLogger.Log(typeof(CvLocateLogger), Level.Info, message, null);
            }
        }
        public void InfoFormat(string message, params object[] args)
        {
            Info(string.Format(message, args));
        }
        public void Error(string message)
        {
            ILogger currentLogger = logger == null ? defaultLogger : logger;
            if (currentLogger.IsEnabledFor(Level.Error))
            {
                currentLogger.Log(typeof(CvLocateLogger), Level.Error, message, null);
            }
        }
        public void ErrorFormat(string message, params object[] args)
        {
            Error(string.Format(message, args));
        }
        public void Warn(string message)
        {
            ILogger currentLogger = logger == null ? defaultLogger : logger;
            if (currentLogger.IsEnabledFor(Level.Warn))
            {
                currentLogger.Log(typeof(CvLocateLogger), Level.Warn, message, null);
            }
        }
        public void WarnFormat(string message, params object[] args)
        {
            Warn(string.Format(message, args));
        }
        public void Fatal(string message)
        {
            ILogger currentLogger = logger == null ? defaultLogger : logger;
            if (currentLogger.IsEnabledFor(Level.Fatal))
            {
                currentLogger.Log(typeof(CvLocateLogger), Level.Fatal, message, null);
            }
        }
        public void FatalFormat(string message, params object[] args)
        {
            Fatal(string.Format(message, args));
        }
    }
}
