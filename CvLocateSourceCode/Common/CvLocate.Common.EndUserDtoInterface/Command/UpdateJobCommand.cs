using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class UpdateJobCommand : BaseRecruiterCommand
    {
        public string JobId { get; set; }
        public string JobName { get; set; }
        public string MandatoryRequirements { get; set; }
        public string OptionalRequirements { get; set; }
        public Location Location { get; set; }
        public string Address { get; set; }

        public UpdateJobCommand(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
