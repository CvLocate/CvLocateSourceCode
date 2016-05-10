using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.EmailListener.Enums;
using CvLocate.EmailListener;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailActionDefinition
    {
        MailActionType ActionType { get; }
        Func<IMailActionDefinition, MailBox, MailActionResult,bool> ActionDone { get; set; }
    }
}
