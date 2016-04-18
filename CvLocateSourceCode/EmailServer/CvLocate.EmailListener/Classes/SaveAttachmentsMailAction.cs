using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Net.Mail;
using System.IO;


namespace CvLocate.EmailListener
{
    public class SaveAttachmentsMailAction : MailAction
    {
        SaveAttachmentsActionDefinition _saveAttachmentsActionDefinition;

        public override IMailActionDefinition ActionDefinition
        {
            get { return _saveAttachmentsActionDefinition; }
        }

        public override void DoAction()
        {
            EmailListener.Instance.NewMailReceived += Instance_NewMailReceived;
            EmailListener.Instance.Connect(Email);
        }

        void Instance_NewMailReceived(MailMessage message)
        {
            if (message == null)
                return;
            message.Attachments.ToList().ForEach(attachment => SaveMailAttachment(attachment));
        }

        public void SaveMailAttachment(System.Net.Mail.Attachment attachment)
        {
            byte[] allBytes = new byte[attachment.ContentStream.Length];
            int bytesRead = attachment.ContentStream.Read(allBytes, 0, (int)attachment.ContentStream.Length);

            //save files in attchments folder
            string destinationFile = _saveAttachmentsActionDefinition.TargetFolder;
            destinationFile += "\\" + attachment.Name;

            BinaryWriter writer = new BinaryWriter(new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
            writer.Write(allBytes);
            writer.Close();
        }

        public SaveAttachmentsMailAction(SaveAttachmentsActionDefinition saveAttachmentsActionDefinition)
        {
            _saveAttachmentsActionDefinition = saveAttachmentsActionDefinition;
        }
    }
}
