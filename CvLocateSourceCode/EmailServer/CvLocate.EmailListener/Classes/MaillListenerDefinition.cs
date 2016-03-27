using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Classes
{
    public class MaillListenerDefinition : IMaillListenerDefinition
    {
        public IEmailServer EmailServer { get; set; }

        public IMailActionDefinition ActionDefinition { get; set; }
    }
}
