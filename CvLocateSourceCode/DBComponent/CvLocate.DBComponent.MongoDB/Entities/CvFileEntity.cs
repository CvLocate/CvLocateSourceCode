using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.DTO;

namespace CvLocate.DBComponent.MongoDB.Entities
{
    [CollectionName("CvFiles")]
    public class CvFileEntity:Entity
    {
        public string CandidateId { get; set; }
        public string FileStreamName { get; set; }
        public string Extension { get; set; }
        public CvSourceType SourceType { get; set; }
        public string Source { get; set; }
        public CvFileStatus Status { get; set; }
        public CvStatusReason StatusReason { get; set; }
        public string StatusReasonDetails { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }
        public string Text { get; set; }
        public List<string> SeperatedTexts { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory<ParsingProcessStatus>> ParsingStatusHistory { get; set; }

    }
}
