using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common;
using CvLocate.Common.Logging;
using CvLocate.EmailListener.Interfaces;
using SimpleInjector;

namespace CvLocate.EmailListener.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.RegisterSingleton<ICvLocateLogger>(new CvLocateLogger("emailListenerLogger"));
            container.Register<IMailActionFactory, MailActionFactory>(Lifestyle.Singleton);
            container.Register<IMailActionsExecuter, MailActionsExecuter>(Lifestyle.Singleton);
            container.Register<IEmailListener, EmailListener>(Lifestyle.Singleton);
            container.Register<IEmailListenerManager, EmailListenerManager>(Lifestyle.Singleton);

            List<MailBox> mailBoxes = new List<MailBox>();
            mailBoxes.Add(new MailBox() { Host = "imap.gmail.com", Port = 993, UserName = "cvlocatetest@gmail.com", Password = "cvlocate1qazx" });

            List<IMailActionDefinition> mailActionsDefinitions = new List<IMailActionDefinition>();
            var actionDefinition = new SaveAttachmentsActionDefinition(@".\Attachments", new List<string>() {".pdf",".doc",".docx",".txt",".rtf" });
            actionDefinition.ActionDone = OnSaveAttachmentsActionDone;
            mailActionsDefinitions.Add(actionDefinition);


            IEmailListenerManager emailListenerManager = container.GetInstance<IEmailListenerManager>();
            emailListenerManager.AddListener(mailBoxes, mailActionsDefinitions);

            Console.ReadLine();
        }

        private static bool OnSaveAttachmentsActionDone(IMailActionDefinition doneAction, MailBox sourceMailBox, MailActionResult result)
        {
            return true;
        }
    }
}
