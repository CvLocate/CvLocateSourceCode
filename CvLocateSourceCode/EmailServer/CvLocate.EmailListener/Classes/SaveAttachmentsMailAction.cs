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
        SaveAttachmentsActionDefinition _saveAttachmentsActionDefinition
        {
            get
            {
                return base.ActionDefinition as SaveAttachmentsActionDefinition;
            }
        }

        public SaveAttachmentsMailAction(MailMessage mail, SaveAttachmentsActionDefinition saveAttachmentsActionDefinition)
            : base(mail, saveAttachmentsActionDefinition)
        {
            
        }



        public override void DoAction()
        {
            this.Mail.Attachments.ToList().ForEach(attachment => SaveMailAttachment(attachment));
        }



        public void SaveMailAttachment(System.Net.Mail.Attachment attachment)
        {
            if (IsAllowAttachment(attachment))
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
        }

        private bool IsAllowAttachment(Attachment attachment)
        {
            return this._saveAttachmentsActionDefinition.AllowFileExtensions.Any(extension => attachment.Name.ToLower().EndsWith(extension.ToLower()));
        }


    }
}
