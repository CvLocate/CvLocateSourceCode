using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Response
{
    public class RecruiterJobResponse : BaseResponse
    {
        public bool CanExecute { get; set; }
        public Job Job { get; set; }

        public RecruiterJobResponse(bool canExecute)
        {
            CanExecute = canExecute;
        }
    }
}
