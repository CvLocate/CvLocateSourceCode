using CvLocate.EmailListener.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IMailAction
    {
        #region Properties

        IMailActionDefinition ActionDefinition { get; }

        IEmailServer Email { get; set; }

        MailActionResult Result { get; set; }

        #endregion

        #region Methods

        void DoAction();

        #endregion

    }
}
