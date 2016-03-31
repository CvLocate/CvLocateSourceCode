using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CvLocate.ParsingEngine
{
    public class ParsingQueueManager : IParsingQueueManager
    {
        IParsingEngineDataWrapper _dataWrapper;
        ConcurrentQueue<CandidateCvFileForParsing> _candidatesQueue;


        public ParsingQueueManager(IParsingEngineDataWrapper dataWrapper)
        {
            _dataWrapper = dataWrapper;
            _candidatesQueue = new ConcurrentQueue<CandidateCvFileForParsing>();
        }


        public CandidateCvFileForParsing GetNextCandidate()
        {
            CandidateCvFileForParsing candidate = null;
            if (!(_candidatesQueue.TryDequeue(out candidate)))
            {
                LoadNextCandidates();
                _candidatesQueue.TryDequeue(out candidate);
            }

            return candidate;
        }

        private void LoadNextCandidates()
        {
            IList<CandidateCvFileForParsing> nextCandidatesForParsing = _dataWrapper.GetTopCandidatesForParsing();
            foreach (var candidate in nextCandidatesForParsing)
            {
                _candidatesQueue.Enqueue(candidate);
            }
        }
    }
}
