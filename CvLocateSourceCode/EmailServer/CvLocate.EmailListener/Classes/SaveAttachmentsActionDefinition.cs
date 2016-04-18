using CvLocate.EmailListener.Enums;
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

        public string TargetFolder { get; set; }

        public SaveAttachmentsActionDefinition(string targetFolder)
        {
            TargetFolder = targetFolder;
        }
    }
}
