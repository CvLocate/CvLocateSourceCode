using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveCandidateAfterParsingCommand : BaseCommonCommand
    {
        public string CandidateId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public string CVFileId { get; set; }
        public byte[] CVFileImage { get; set; }

        public override string ToString()
        {
            string result = "Save candidate after parsing with the following details:\n";
            result += string.Format("CandidateId: {0}, Email:{1}, Name: {2}, MatchingStatus: {3}, CvFileId: {4}, FileImage length: {5}",
                CandidateId, Email, Name, MatchingStatus, CVFileId, CVFileImage == null ? 0 : CVFileImage.Length);
            return result;
        }
    }
}
