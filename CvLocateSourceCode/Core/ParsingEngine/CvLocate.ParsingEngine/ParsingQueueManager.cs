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
        ConcurrentQueue<Candidate> _candidatesQueue;
        public void Initialize(IParsingEngineDataWrapper dataWrapper)
        {
            _dataWrapper = dataWrapper;
            _candidatesQueue = new ConcurrentQueue<Candidate>();
        }

        public Candidate GetNextCandidate()
        {
            Candidate candidate = null;
            if (!(_candidatesQueue.TryDequeue(out candidate)))
            {
                LoadNextCandidates();
                _candidatesQueue.TryDequeue(out candidate);
            }

            return candidate;
        }

        private void LoadNextCandidates()
        {
           IList<Candidate> nextCandidatesForParsing= _dataWrapper.GetTopCandidatesForParsing();
           foreach (var candidate in nextCandidatesForParsing)
           {
               _candidatesQueue.Enqueue(candidate);
           }
        }
    }
}
