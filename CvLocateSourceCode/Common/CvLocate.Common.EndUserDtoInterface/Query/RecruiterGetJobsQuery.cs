using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.EndUserDtoInterface.Enums;

namespace CvLocate.Common.EndUserDtoInterface.Query
{
    public class RecruiterGetJobsQuery:BaseRecruiterQuery
    {
        public JobState JobState { get; set; }
        public RecruiterGetJobsQuery(string recruiterId):base(recruiterId)
        {

        }

    }
}
