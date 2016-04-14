using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.CommonDto.Enums;
using CvLocate.Common.CommonDto.Entities;

namespace CvLocate.DBComponent.MongoDB.Entities
{
    [CollectionName("CvFiles")]
    public class CvFileEntity : Entity
    {
        public string CandidateId { get; set; }
        public string FileId { get; set; }
        public FileType Extension { get; set; }
        public byte[] FileImage { get; set; }
        public CvSourceType SourceType { get; set; }
        public string Source { get; set; }
        public CvFileStatus Status { get; set; }
        public CvStatusReason StatusReason { get; set; }
        public string StatusReasonDetails { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }
        public string Text { get; set; }
        public TextEncoding FileEncoding { get; set; }
        public List<string> SeperatedTexts { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory<ParsingProcessStatus>> ParsingStatusHistory { get; set; }

    }
}
