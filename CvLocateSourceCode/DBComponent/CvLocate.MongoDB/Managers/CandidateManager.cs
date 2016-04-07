using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Response;
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
    public class CandidateManager : ICandidateManager
    {
        #region Singletone Implementation

        private static CandidateManager _instance;
        public static CandidateManager Instance
        {
            get { return _instance ?? (_instance = new CandidateManager()); }
        }

        private CandidateManager()
        {
            _candidateRepository = new MongoRepository<CandidateEntity>();
        }

        #endregion

        #region Members

        private MongoRepository<CandidateEntity> _candidateRepository;

        #endregion

        #region Public Methods

        public SignResponse SignUp(SignUpCommand command)
        {
            throw new NotImplementedException();
        }

        public SignResponse SignIn(SigninCommand command)
        {
            return new SignResponse() { CanSignIn = false };
        }

        public Candidate GetCandidateById(string id)
        {
            CandidateEntity candidateEntity = _candidateRepository.GetById(id);
            AutoMapper.Mapper.CreateMap<CandidateEntity,Candidate>();
            return AutoMapper.Mapper.Map<CandidateEntity, Candidate>(candidateEntity);
        }

        #endregion
    }
}
