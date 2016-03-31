using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class CvFile
    {
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public CandidateSourceType SourceType { get; set; }
        public string Source { get; set; }
        public CvFileStatus Status { get; set; }
        public CvStatusReason StatusReason { get; set; }
        public string StatusReasonDetails { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
