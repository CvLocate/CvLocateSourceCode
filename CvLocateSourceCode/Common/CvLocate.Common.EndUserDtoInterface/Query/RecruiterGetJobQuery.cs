using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;

namespace CvLocate.Common.EndUserDtoInterface.Query
{
    public class RecruiterGetJobQuery:BaseRecruiterQuery
    {
        public string JobId { get; set; }

        public RecruiterGetJobQuery(string recruiterId)
            : base(recruiterId)
        {

        }

    }
}
