using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.CommonDto;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB.Entities
{
    [CollectionName("Candidates")]
    public class CandidateEntity : BaseMongoEntity
    {
        public string Id { get; set; }
        public long CandidateNum { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CVFileId { get; set; }
        public CandidateSourceType SourceType { get; set; }
        public string Source { get; set; }
        public CandidateRegisterStatus RegisterStatus { get; set; }
        public CandidateSearchJobStatus SearchJobStatus { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory<CandidateRegisterStatus>> RegisterStatusHistory { get; set; }
        public List<BaseStatusHistory<CandidateSearchJobStatus>> SearchJobStatusHistory { get; set; }
        public List<BaseStatusHistory<MatchingProcessStatus>> MatchingStatusHistory { get; set; }
    }
}
