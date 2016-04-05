using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Result
{

    public class GetTopCvFilesForParsingResult
    {
        public IList<CandidateCvFileForParsing> CandidateCvFilesForParsing { get; set; }

        public GetTopCvFilesForParsingResult(IList<CandidateCvFileForParsing> candidateCvFilesForParsing)
        {
            CandidateCvFilesForParsing = candidateCvFilesForParsing;
        }
        public GetTopCvFilesForParsingResult()
        {
            CandidateCvFilesForParsing = new List<CandidateCvFileForParsing>();
        }
    }
}
