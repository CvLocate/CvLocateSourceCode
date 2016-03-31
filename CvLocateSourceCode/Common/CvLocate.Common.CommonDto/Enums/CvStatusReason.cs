using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CommonDto
{
    public enum CvStatusReason
    {
        None,
        Duplicate,
        ParsingFailed,
        OtherEmailThenCandidate,
        ReplacedByCandidate
    }
}
