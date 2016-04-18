using CvLocate.EmailListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener.Interfaces
{
    public interface IEmailListener
    {
        void Connect(MailBox mailBox);
        void Disconnect(MailBox mailBox);
        event EventHandler<NewMailRecievedEventArgs> NewMailReceived;
    }
}
