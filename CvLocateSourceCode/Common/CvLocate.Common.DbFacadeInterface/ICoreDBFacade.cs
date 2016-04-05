using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.DbFacadeInterface
{
    public interface ICoreDBFacade
    {
        //•	Return just X records when the X top parameter is taken from XTopCandidatessForParsingQueue column in Configuration table
        //•	Take just records with ParsingStatus =  'WaitingForParsing'
        //•	The order is done by UpdatedAt field. First take the oldest recorded.
        //•	In future: Take first the files of registered candidates
        GetTopCvFilesForParsingResult GetTopCvFilesForParsing();

        GetParsingEngineConfigurationResult GetParsingEngineConfiguration();

        FindCandidateResult FindCandidate(FindCandidateQuery findCandidateQuery);

        void SaveResultOfCandidateParsing(SaveResultOfCandidateParsingCommand saveCommand);
    }
}
