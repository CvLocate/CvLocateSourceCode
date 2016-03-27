using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface
{
    public class MatchCandidate
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public object Cv { get; set; }//TBD how get the cv
        public long CandidateNum { get; set; }
    }
}
