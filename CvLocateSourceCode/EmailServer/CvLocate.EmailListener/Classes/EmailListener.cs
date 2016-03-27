using CvLocate.Common.Logging;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Classes
{
    public class EmailListener : IEmailListener
    {
        #region Members

        private MailActionFactory _mailActionFactory;

        #endregion

        #region Ctor

        public EmailListener()
        {
            _mailActionFactory = new MailActionFactory();
        }

        #endregion

        #region Public Methods

        public void Initialize(IEnumerable<IMaillListenerDefinition> mailActionsDefinitions, ICvLocateLogger logger)
        {
            if (mailActionsDefinitions == null || logger == null)
                return;

            mailActionsDefinitions.ToList().ForEach(actionDef => 
            {
                try
                {
                    IMailAction action = _mailActionFactory.Create(actionDef.ActionDefinition);
                    if (action == null)
                    {
                        logger.Error("[EmailListener.Initialize] error: action cannot be null.");
                        return;
                    }
                    action.Email = actionDef.EmailServer;
                    action.DoAction();
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat(ex.ToString());
                }
            });
        }

        #endregion
    }
}
