using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface
{
    public class Job
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MandatoryRequirements { get; set; }
        public string OptionalRequirements { get; set; }
        public Location Location { get; set; }
        public string Address { get; set; }
        public JobStatus Status { get; set; }
        public MatchingProcessStatus MatchingProcessStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByRecruiterId { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
