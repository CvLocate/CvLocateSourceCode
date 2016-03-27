using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Query
{
    public class RecruiterGetJobMatchingsQuery:BaseRecruiterQuery
    {
        public string JobId { get; set; }

        public RecruiterGetJobMatchingsQuery(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
