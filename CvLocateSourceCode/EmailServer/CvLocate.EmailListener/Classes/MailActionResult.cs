using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.EmailListener
{
    public enum Result
    {
        Success,
        Failed
    }

    public class MailActionResult
    {
        public Result Status { get; set; }
        public string Message { get; set; }
    }
}
