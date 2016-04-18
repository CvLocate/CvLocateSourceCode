using CvLocate.EmailListener.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public class MailBox 
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseSSL { get; set; }

        public override string ToString()
        {
            return string.Format("Host:{0}, Port:{1}, UserName: {2}", Host, Port, UserName);
        }
    }
}
