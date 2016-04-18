using CvLocate.Common.Logging;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class MailActionsExecuter : IMailActionsExecuter
    {
        IMailActionFactory _mailActionFactory;
        ICvLocateLogger _logger;

        public MailActionsExecuter(IMailActionFactory mailActionFactory, ICvLocateLogger logger)
        {
            this._mailActionFactory = mailActionFactory;
            this._logger = logger;
        }

        public void ExecuteMailActions(MailBox mailBox, List<IMailActionDefinition> mailActionsDefinitions)
        {
            foreach (var actionDefinition in mailActionsDefinitions)
            {
                IMailAction doneAction = ExecuteMailActions(actionDefinition);
                bool continueExecute = actionDefinition.ActionDone(actionDefinition, mailBox, doneAction.Result);
                if (!continueExecute)
                {
                    break;
                }
            }
        }

        private IMailAction ExecuteMailActions(IMailActionDefinition actionDefinition)
        {
            IMailAction action = this._mailActionFactory.Create(actionDefinition);
            action.DoAction();
            return action;
        }
    }
}
