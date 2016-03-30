using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;
using System.IO;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class Candidate
    {
        public string Id { get; set; }
        public long CandidateNum { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CVFileId { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
