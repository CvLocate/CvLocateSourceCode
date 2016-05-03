using CvLocate.EmailListener.Enums;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public abstract class MailActionDefinition : IMailActionDefinition
    {
        public abstract MailActionType ActionType { get; }

        public abstract Func<IMailActionDefinition, MailBox, MailActionResult, bool> ActionDone{get;set;}
       
    }
}
