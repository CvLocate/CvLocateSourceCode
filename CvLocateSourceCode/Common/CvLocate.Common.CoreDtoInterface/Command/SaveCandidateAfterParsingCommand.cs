using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveCandidateAfterParsingCommand:BaseCommonCommand
    {
        public string CandidateId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public string CVFileId { get; set; }
    }
}
