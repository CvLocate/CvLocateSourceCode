using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailActionsExecuter
    {
        void ExecuteMailActions(MailBox mailBox,MailMessage mail, List<IMailActionDefinition> mailActionsDefinitions);
    }
}
