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

        public GetTopCvFilesForParsingResult GetTopCvFilesForParsing()
        {
            throw new NotImplementedException();
        }

        public GetParsingEngineConfigurationResult GetParsingEngineConfiguration()
        {
            throw new NotImplementedException();
        }

        public FindCandidateResult FindCandidate(Common.CoreDtoInterface.Query.FindCandidateQuery findCandidateQuery)
        {
            throw new NotImplementedException();
        }

        public void SaveResultOfCandidateParsing(Common.CoreDtoInterface.Command.SaveResultOfCandidateParsingCommand saveCommand)
        {
            throw new NotImplementedException();
        }
    }
}
