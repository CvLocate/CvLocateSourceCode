using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class CloseJobCommand : BaseRecruiterCommand
    {
        public string JobId { get; set; }
        public CloseJobCommand(string recruiterId):base(recruiterId)
        {

        }
    }
}
