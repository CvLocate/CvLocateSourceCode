using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class CreateJobCommand : BaseRecruiterCommand
    {
        public string JobName { get; set; }
        public string MandatoryRequirements { get; set; }
        public string OptionalRequirements { get; set; }
        public Location JobLocation { get; set; }
        public string Address { get; set; }

        public CreateJobCommand(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
