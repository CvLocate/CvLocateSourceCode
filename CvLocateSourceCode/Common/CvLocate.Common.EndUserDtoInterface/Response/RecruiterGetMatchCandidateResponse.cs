using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Response
{
    public class RecruiterGetMatchCandidateResponse
    {
        public string MatchingId { get; set; }
        public MatchCandidate Candidate { get; set; }
    }
}
