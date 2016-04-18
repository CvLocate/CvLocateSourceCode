using CvLocate.Common.EndUserDTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Response
{
    public class BaseResponse
    {
        public string ErrorMessage { get; set; }
        public EndUserError? Error { get; set; }
    }
}
