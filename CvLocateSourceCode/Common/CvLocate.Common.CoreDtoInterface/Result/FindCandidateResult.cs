using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Result
{
    public class FindCandidateResult
    {
        public Candidate Candidate { get; set; }
        public CvFile CvFile { get; set; }
    }
}
