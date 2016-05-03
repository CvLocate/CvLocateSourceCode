using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailActionFactory
    {
        IMailAction Create(MailMessage mail,IMailActionDefinition mailActionDefinition);
    }
}
