using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CvLocate.EmailListener.Interfaces;
using System.Threading.Tasks;
using S22.Imap;

namespace CvLocate.EmailListener.Classes
{
    public delegate void GetMailDelegate(MailMessage message);
    public class EmailOperations
    {
        #region Singletone Implementation

        private static EmailOperations _instance;
        public static EmailOperations Instance
        {
            get { return _instance ?? (_instance = new EmailOperations()); }
        }

        private EmailOperations()
        {
        }

        #endregion

        public event GetMailDelegate NewMailReceived;

        #region Public Methods

        public void Connect(IEmailServer email)
        {
            ImapClient Client = new ImapClient(email.Host, email.Port, email.UserName, email.Password, AuthMethod.Login, true);

            // Make sure our server actually supports IMAP IDLE.
            if (!Client.Supports("IDLE"))
                throw new Exception("This server does not support IMAP IDLE");

            // Our event handler will be called whenever a new message is received
            // by the server.
            Client.NewMessage += Client_NewMessage;

        }

        #endregion

        #region Private Methods

        void Client_NewMessage(object sender, IdleMessageEventArgs e)
        {
            ImapClient client = sender as ImapClient;
            if (client == null)
                return;
            MailMessage message = client.GetMessage(e.MessageUID);
            if (NewMailReceived != null)
                NewMailReceived(message);
        }

        #endregion
    }
}
