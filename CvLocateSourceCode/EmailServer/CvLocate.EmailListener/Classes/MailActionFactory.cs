using CvLocate.EmailListener.Enums;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class MailActionFactory : IMailActionFactory
    {
        public IMailAction Create (IMailActionDefinition mailActionDefinition)
        {
            if (mailActionDefinition == null)
                return null;
            switch (mailActionDefinition.ActionType)
            {
                case MailActionType.SaveAttachments:
                    {
                        if(mailActionDefinition is SaveAttachmentsActionDefinition)
                            return new SaveAttachmentsMailAction((SaveAttachmentsActionDefinition)mailActionDefinition);
                        return new SaveAttachmentsMailAction(new SaveAttachmentsActionDefinition(string.Empty));
                    }
                default:
                    return null;
            }
        }
    }
}
