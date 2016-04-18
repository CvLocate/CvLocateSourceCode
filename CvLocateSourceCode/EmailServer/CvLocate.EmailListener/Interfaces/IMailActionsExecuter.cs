using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailActionsExecuter
    {
        void ExecuteMailActions(MailBox mailBox, List<IMailActionDefinition> mailActionsDefinitions);
    }
}
