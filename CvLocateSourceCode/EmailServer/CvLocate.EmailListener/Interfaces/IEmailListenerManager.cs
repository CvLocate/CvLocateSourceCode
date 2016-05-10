using CvLocate.Common.Logging;
using CvLocate.EmailListener;
using System.Collections.Generic;


namespace CvLocate.EmailListener.Interfaces
{
    public interface IEmailListenerManager
    {
        void AddListener(IList<MailBox> mailBoxes, List<IMailActionDefinition> mailActionsDefinitions);
        void StopAllIsteners();
        void StopListeners(IList<MailBox> mailBoxes);
    }
}
