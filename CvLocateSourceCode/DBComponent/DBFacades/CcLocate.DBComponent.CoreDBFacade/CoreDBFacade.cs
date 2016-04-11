using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcLocate.DBComponent.CoreDBFacade
{
    public class CoreDBFacade : ICoreDBFacade
    {

        public GetTopCvFilesForParsingResult GetTopCvFilesForParsing()
        {
            throw new NotImplementedException();
        }

        public GetParsingEngineConfigurationResult GetParsingEngineConfiguration()
        {
            throw new NotImplementedException();
        }

        public FindCandidateResult FindCandidate(FindCandidateQuery findCandidateQuery)
        {
            throw new NotImplementedException();
        }

        public void SaveResultOfCandidateParsing(SaveResultOfCandidateParsingCommand saveCommand)
        {
            throw new NotImplementedException();
        }
    }
}