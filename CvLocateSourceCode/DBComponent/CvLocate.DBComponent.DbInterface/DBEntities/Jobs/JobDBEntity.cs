using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.DBEntities.Jobs
{
    public class JobDBEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MandatoryRequirements { get; set; }
        public string OptionalRequirements { get; set; }
        public Location Location { get; set; }
        public JobStatus Status { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
