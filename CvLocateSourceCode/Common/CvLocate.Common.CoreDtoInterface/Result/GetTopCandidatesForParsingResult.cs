using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Result
{

    public class GetTopCandidatesForParsingResult
    {
        public IList<CandidateCvFileForParsing> CandidateCvFilesForParsing { get; set; }

        public GetTopCandidatesForParsingResult(IList<CandidateCvFileForParsing> candidateCvFilesForParsing)
        {
            CandidateCvFilesForParsing = candidateCvFilesForParsing;
        }
    }
}
