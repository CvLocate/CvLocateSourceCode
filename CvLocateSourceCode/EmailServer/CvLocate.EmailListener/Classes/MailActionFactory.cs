using CvLocate.EmailListener.Enums;
using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class MailActionFactory : IMailActionFactory
    {
        public IMailAction Create (MailMessage mail,IMailActionDefinition mailActionDefinition)
        {
            if (mailActionDefinition == null)
                return null;
            switch (mailActionDefinition.ActionType)
            {
                case MailActionType.SaveAttachments:
                    {
                        if (mailActionDefinition is SaveAttachmentsActionDefinition)
                            return new SaveAttachmentsMailAction(mail,(SaveAttachmentsActionDefinition)mailActionDefinition);
                        else
                            throw new Exception("Cannot create MailAction. ActionType is SaveAttachments but mailActionDefinition isn't SaveAttachmentsActionDefinition");
                    }
                default:
                    return null;
            }
        }
    }
}
