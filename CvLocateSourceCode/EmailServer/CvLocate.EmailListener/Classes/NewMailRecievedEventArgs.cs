using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class NewMailRecievedEventArgs:EventArgs
    {
        public MailMessage Mail { get; set; }
        public MailBox SourceMailBox { get; set; }

        public NewMailRecievedEventArgs(MailMessage mail, MailBox sourceMailBox)
        {
            this.Mail = mail;
            this.SourceMailBox = sourceMailBox;
        }
    }
}
