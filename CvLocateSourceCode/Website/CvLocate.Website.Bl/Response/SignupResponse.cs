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
        public string UserId { get; set; }
        public UserType UserType { get; set; }
    }
}
