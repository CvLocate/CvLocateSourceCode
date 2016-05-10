using CvLocate.EmailListener.Enums;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class SaveAttachmentsActionDefinition:MailActionDefinition
    {
        public override MailActionType ActionType
        {
            get { return MailActionType.SaveAttachments; }
        }

        public override Func<IMailActionDefinition, MailBox, MailActionResult, bool> ActionDone { get; set; }

        public string TargetFolder { get; set; }
        public List<string> AllowFileExtensions { get; set; }

        public SaveAttachmentsActionDefinition(string targetFolder, List<string> allowFileExtensions)
        {
            this.TargetFolder = targetFolder;
            this.AllowFileExtensions = allowFileExtensions;
        }

        
    }
}
