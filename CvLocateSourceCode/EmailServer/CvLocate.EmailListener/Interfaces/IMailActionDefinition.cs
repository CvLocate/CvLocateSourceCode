using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.EmailListener.Enums;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailActionDefinition
    {
        MailActionType ActionType { get; } 
    }
}
