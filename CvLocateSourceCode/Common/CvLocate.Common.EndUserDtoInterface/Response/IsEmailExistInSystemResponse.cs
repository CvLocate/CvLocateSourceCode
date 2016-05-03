using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Response
{
    public class IsEmailExistInSystemResponse:BaseResponse
    {
        public bool EmailExistInSystem { get; set; }
    }
}
