using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    internal static class  Extensions
    {
        internal static string ToFormattedString(this MailMessage mailMessage)
        {
            string result = string.Format("Subject: {0}, From: {1}, AttachmentsCount: {2}",
                mailMessage.Subject, mailMessage.Sender, mailMessage.Attachments.Count);
            return result;
        }
    }
}
