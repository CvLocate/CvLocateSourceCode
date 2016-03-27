using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.EmailListener.Classes;
using CvLocate.Common;
using CvLocate.Common.Logging;

namespace CvLocate.EmailListener.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailListener.Classes.EmailListener listener = new Classes.EmailListener();
            List<MaillListenerDefinition> mailListener = new List<MaillListenerDefinition>();
            mailListener.Add(new MaillListenerDefinition()
            {
                ActionDefinition = new SaveAttachmentsActionDefinition(@"F:\Attachments"),
                EmailServer = new EmailServer() { Host = "imap.gmail.com", Port = 993, UserName = "cvlocatetest@gmail.com", Password = "cvlocate1qazx" }

            });


            listener.Initialize(mailListener, new CvLocateLogger());
            Console.ReadLine();
        }
    }
}
