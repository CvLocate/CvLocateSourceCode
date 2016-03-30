using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveResultOfCandidateParsingCommand : BaseCommonCommand
    {
        public Candidate Candidate { get; set; }
        public string CvText { get; set; }
        public List<string> SeperatedCvTexts { get; set; }
    }
}
