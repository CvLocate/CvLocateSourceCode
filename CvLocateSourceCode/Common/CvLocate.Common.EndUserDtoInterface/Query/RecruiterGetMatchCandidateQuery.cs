using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;

namespace CvLocate.Common.EndUserDtoInterface.Query
{
    public class RecruiterGetMatchCandidateQuery:BaseRecruiterQuery
    {
        public string MatchingId { get; set; }

        public RecruiterGetMatchCandidateQuery(string recruiterId)
            : base(recruiterId)
        {

        }

    }
}
