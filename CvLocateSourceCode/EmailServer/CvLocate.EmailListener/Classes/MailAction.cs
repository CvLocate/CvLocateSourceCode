using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public abstract class MailAction : IMailAction
    {
        public MailMessage Mail { get; set; }

        public IMailActionDefinition ActionDefinition { get; set; }

        public MailActionResult Result { get; set; }

        public abstract void DoAction();

        public MailAction(MailMessage mail, IMailActionDefinition actionDefinition)
        {
            if (mail == null )
                throw new ArgumentNullException("mail");
            if (actionDefinition == null)
                throw new ArgumentNullException("actionDefinition");

            this.Mail = mail;
            this.ActionDefinition = actionDefinition;
        }
    }
}
