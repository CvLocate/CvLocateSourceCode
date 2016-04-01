using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
   public  class ParsingEngineDataWrapper:IParsingEngineDataWrapper
    {
       ICoreDBFacade _coreDbFacade;

       public ParsingEngineDataWrapper(ICoreDBFacade coreDbFacade)
       {
           this._coreDbFacade = coreDbFacade;
       }

        public IList<CandidateCvFileForParsing> GetTopCandidatesForParsing()
        {
            return _coreDbFacade.GetTopCandidatesForParsing().CandidateCvFilesForParsing;
        }

        public ParsingEngineConfiguration GetParsingEngineConfiguration()
        {
            return _coreDbFacade.GetParsingEngineConfiguration().Configuration;
        }

        public void SaveResultOfCandidateParsing(SaveResultOfCandidateParsingCommand saveCommand)
        {
            _coreDbFacade.SaveResultOfCandidateParsing(saveCommand);
        }

        public FindCandidateResult FindCandidate(FindCandidateQuery findCandidateQuery)
        {
            return _coreDbFacade.FindCandidate(findCandidateQuery);
        }
    }
}
