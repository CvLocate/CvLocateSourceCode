using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.DbInterface;
using CvLocate.DBComponent.DbInterface.Exceptions;
using CvLocate.DBComponent.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.MongoDB.Managers
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

        /// <summary>
        /// Get candidate by email and password
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <param name="password">The password</param>
        /// <returns>Founded id</returns>
        public string GetCandidateByEmailAndPassword(string email, string password)
        {
            CandidateEntity entity = _candidateRepository.FirstOrDefault(can => can.Email == email && can.Password == password);
            if (entity == null)
                throw new SignInException(email);
            return entity.Id;
        }

        /// <summary>
        /// Check if email exists in candidates table
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <returns>If exists</returns>
        public bool CandidateEmailExists(string email)
        {
            return _candidateRepository.Exists(can => can.Email == email);
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
