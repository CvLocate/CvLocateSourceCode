using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.Common
{
    public class SessionData
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public UserType UserType { get; set; }
        public string SessionId { get; set; }

        public DateTime ExpireTime { get; set; }
        public DateTime ValidFrom { get; set; }

        


    }
}
