using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Response
{
    public class SignResponse : BaseResponse
    {
        public bool CanSignIn { get; set; }
        public string UserId { get; set; }
        public UserType UserType { get; set; }
    }
}
