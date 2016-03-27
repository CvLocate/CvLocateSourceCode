using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Query
{
    public class BaseRecruiterQuery : BaseQuery
    {
        public string RecruiterId { get; set; }
        public BaseRecruiterQuery(string recruiterId)
        {
            RecruiterId = recruiterId;
        }
    }
}
