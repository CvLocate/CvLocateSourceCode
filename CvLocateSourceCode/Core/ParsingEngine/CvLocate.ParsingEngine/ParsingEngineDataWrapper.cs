using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
   public  class ParsingEngineDataWrapper:IParsingEngineDataWrapper
    {
        public IList<CandidateCvFileForParsing> GetTopCandidatesForParsing()
        {
            throw new NotImplementedException();
        }

        public ParsingEngineConfiguration GetParsingEngineConfiguration()
        {
            throw new NotImplementedException();
        }

        public CvFileForParsing GetCandidateCvFile(string p)
        {
            throw new NotImplementedException();
        }

        public void SaveResultOfCandidateParsing(SaveResultOfCandidateParsingCommand saveCommand)
        {
            throw new NotImplementedException();
        }

        public FindCandidateResult FindCandidate(FindCandidateQuery findCandidateQuery)
        {
            throw new NotImplementedException();
        }
    }
}
