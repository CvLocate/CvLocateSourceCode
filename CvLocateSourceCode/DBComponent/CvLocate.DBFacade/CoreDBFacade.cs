using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBFacade
{
    public class CoreDBFacade:ICoreDBFacade
    {
        public GetTopCandidatesForParsingResult GetTopCandidatesForParsing()
        {
            //return new GetTopCandidatesForParsingResult(n
            throw new NotImplementedException();
        }

        public CvLocate.Common.CoreDtoInterface.Result.GetParsingEngineConfigurationResult GetParsingEngineConfiguration()
        {
            throw new NotImplementedException();
        }

        public CvLocate.Common.CoreDtoInterface.Result.FindCandidateResult FindCandidate(CvLocate.Common.CoreDtoInterface.Query.FindCandidateQuery findCandidateQuery)
        {
            throw new NotImplementedException();
        }

        public void SaveResultOfCandidateParsing(CvLocate.Common.CoreDtoInterface.Command.SaveResultOfCandidateParsingCommand saveCommand)
        {
            throw new NotImplementedException();
        }
    }
}
