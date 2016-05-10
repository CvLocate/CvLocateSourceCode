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

namespace CvLocate.EmailListener
{
    public class EmailListener : IEmailListener
    {
        #region Members

        Dictionary<ImapClient, MailBox> _clients; //todo change to concurrent dictionary

        #endregion

        #region CTOR

        public EmailListener()
        {
            this._clients = new Dictionary<ImapClient, MailBox>();
        }

        #endregion

        #region IEmailListener Implementation

        public event EventHandler<NewMailRecievedEventArgs> NewMailReceived;

        public void Connect(MailBox mailBox)
        {
            ImapClient client = new ImapClient(mailBox.Host, mailBox.Port, mailBox.UserName, mailBox.Password, AuthMethod.Login, true);

            // Make sure our server actually supports IMAP IDLE.
            if (!client.Supports("IDLE"))
                throw new Exception("This server does not support IMAP IDLE");
            this._clients.Add(client, mailBox);

            // Our event handler will be called whenever a new message is received
            // by the server.

            client.NewMessage += Client_NewMessage;

        }
        public void Disconnect(MailBox mailBox)
        {
            if (this._clients.ContainsValue(mailBox))
            {
                ImapClient client = this._clients.First(clnt => clnt.Value == mailBox).Key;
                client.NewMessage -= Client_NewMessage;
                this._clients.Remove(client);
            }
        }

        #endregion

        #region Private Methods

        void Client_NewMessage(object sender, IdleMessageEventArgs e)
        {
            ImapClient client = sender as ImapClient;
            if (client == null)
                return;

            MailBox sourceMailBox = this._clients.ContainsKey(client) ? this._clients[client] : null;
            if (sourceMailBox == null)
                return;

            MailMessage message = client.GetMessage(e.MessageUID);

            if (NewMailReceived != null)
                NewMailReceived(this, new NewMailRecievedEventArgs(message, sourceMailBox));
        }

        #endregion
    }
}
