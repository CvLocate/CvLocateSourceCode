using CvLocate.Common.CoreDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Query
{
    public class FindCandidateQuery : BaseCoreQuery
    {
        public FindCandidateBy FindByField { get; set; }
        public object FindByValue { get; set; }

        public FindCandidateQuery(FindCandidateBy findByField, object findByValue)
        {
            this.FindByField = findByField;
            this.FindByValue = findByValue;
        }
    }
}
