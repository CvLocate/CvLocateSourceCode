using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.DbInterface;
using CvLocate.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB.Managers
{
    public class CandidateManager:ICandidateManager
    {
        
        private MongoRepository<CandidateEntity> _candidateRepository;

        public CandidateManager()
        {
            _candidateRepository = new MongoRepository<CandidateEntity>();
        }

        public void SignIn(string email, string password)
        {
            
        }

        public string InsertCandidate(Candidate candidate)
        {
            //todo add convertion between candidate and candidateentity
            //CandidateEntity newCandidate = _candidateRepository.Add(candidate);
            return null;// newCandidate.Id;
        }

        public void UpdateCandidate(CandidateEntity candidate)
        {
            _candidateRepository.Update(candidate);
        }

        public void GetCandidate(string id)
        {
            _candidateRepository.First(candidate => candidate.Id == id);
        }
    }
}
