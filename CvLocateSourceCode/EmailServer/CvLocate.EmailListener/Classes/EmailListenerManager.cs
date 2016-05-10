using CvLocate.Common.Logging;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class EmailListenerManager : IEmailListenerManager
    {
        #region Members

        private IMailActionsExecuter _mailActionsExecuter;
        IEmailListener _emailListener;
        private ICvLocateLogger _logger;
        private Dictionary<MailBox, List<IMailActionDefinition>> _listenersDefinitions;

        #endregion

        #region Ctor

        public EmailListenerManager(IMailActionsExecuter mailActionsExecuter, IEmailListener emailListener, ICvLocateLogger logger)
        {
            this._logger = logger;
            this._mailActionsExecuter = mailActionsExecuter;
            this._emailListener = emailListener;
            this._listenersDefinitions = new Dictionary<MailBox, List<IMailActionDefinition>>();
            this._emailListener.NewMailReceived += OnNewMailReceived;
        }


        #endregion

        #region IEmailListenerManager Implementation

        public void AddListener(IList<MailBox> mailBoxes, List<IMailActionDefinition> mailActionsDefinitions)
        {
            foreach (var mailBox in mailBoxes)
            {
                _listenersDefinitions.Add(mailBox, mailActionsDefinitions);
                this._emailListener.Connect(mailBox);
            }
        }

        public void StopAllIsteners()
        {
            throw new NotImplementedException();
        }

        public void StopListeners(IList<MailBox> mailBoxes)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        void OnNewMailReceived(object sender, NewMailRecievedEventArgs e)
        {
            try
            {
                this._logger.DebugFormat("New mail recieved. MailBox Deatils: {0}\nMail Details: {1}", e.SourceMailBox.ToString(), e.Mail.ToFormattedString());

                if (!this._listenersDefinitions.ContainsKey(e.SourceMailBox))
                {
                    this._logger.WarnFormat("Recieve OnNewMailReceived event but this mailbox ({0}) isn't defined for listen on.", e.SourceMailBox.ToString());
                    return;
                }

                List<IMailActionDefinition> actions = this._listenersDefinitions[e.SourceMailBox];
                this._mailActionsExecuter.ExecuteMailActions(e.SourceMailBox,e.Mail, actions);
            }
            catch (Exception ex)
            {
                this._logger.ErrorFormat("Error occurred on process new mail.\nMail details: ({0}).\nMailBox details: {1}.\nOriginal Exception: {2}",
                    e.SourceMailBox.ToString(), e.Mail.ToFormattedString(), ex.ToString());
            }
        }


        #endregion





        
    }
}
