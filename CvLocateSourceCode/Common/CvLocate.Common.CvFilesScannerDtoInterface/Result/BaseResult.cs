using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CvFilesScannerDtoInterface.Result
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResult(bool success)
        {
            Success = success;
        }
    }
}
