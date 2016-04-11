using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface;
using CvLocate.Common.EndUserDtoInterface.DTO;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.EndUserDTO.DTO;


namespace CvLocate.DBComponent.MongoDB.Entities
{
    [CollectionName("Jobs")]
    public class JobEntity : BaseMongoEntity
    {
        public string Name { get; set; }
        public List<Owner> Owner { get; set; }
        public string Content { get; set; }
        public string MandatoryRequirements { get; set; }
        public string OptionalRequirements { get; set; }
        public Location Location { get; set; }
        public string Address { get; set; }
        public double SearchRadius { get; set; }
        public JobStatus Status { get; set; }
        public JobSourceType SourceType { get; set; }
        public string Source { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory<JobStatus>> StatusHistory { get; set; }
        public List<BaseStatusHistory<MatchingProcessStatus>> MatchingStatusHistory { get; set; }

    }
}
