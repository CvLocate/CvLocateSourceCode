using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public abstract class MailAction : IMailAction
    {
        public abstract IMailActionDefinition ActionDefinition { get; }

        public IEmailServer Email { get; set; }

        public MailActionResult Result { get; set; }

        public abstract void DoAction();
    }
}
