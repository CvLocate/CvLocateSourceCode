using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    public interface IParsingEngineDataWrapper
    {
        //a.	Get X top candidates cv files for parsing
        //i.	Take candidates with MatchingStatus: 'NotReadyForMatching'
        //ii.	Take first registered candidates (their RegisterStatus is 'Register')
        //iii.	Then take the not registered candidates ordered by updated date ascending
        //iv.	The X top parameter is taken from XTopCandidatessForParsingQueue column in Configuration table
        IList<Candidate> GetTopCandidatesForParsing();
    }
}
