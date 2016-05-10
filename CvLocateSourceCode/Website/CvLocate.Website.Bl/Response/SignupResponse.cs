using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;

namespace CvLocate.Website.Bl.Response
{
    public class SignupResponse:BaseWebsiteResponse
    {
        public UserType UserType { get; set; }
        public string SessionId { get; set; }
    }
}
