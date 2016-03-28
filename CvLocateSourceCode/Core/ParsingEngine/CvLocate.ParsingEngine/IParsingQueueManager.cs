using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    public interface IParsingQueueManager
    {
        void Initialize(IParsingEngineDataWrapper dataWrapper);
        Candidate GetNextCandidate();
    }
}
