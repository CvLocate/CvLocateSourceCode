using CvLocate.Common.CommonDto;
using CvLocate.DBComponent.DbInterface;
using CvLocate.DBComponent.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.EndUserDtoInterface;
using MongoDB.Bson;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.DBComponent.DbInterface.Exceptions;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.DbInterface.DBEntities;
using AutoMapper;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.DBComponent.DbInterface.DBEntities.Recruiters;

namespace CvLocate.DBComponent.MongoDB.Managers
{
    public class RecruiterManager : IRecruiterManager
    {
        #region Singletone Implementation

        private static RecruiterManager _instance;
        public static RecruiterManager Instance
        {
            get { return _instance ?? (_instance = new RecruiterManager()); }
        }

        private RecruiterManager()
        {
            _recruitersRepository = new MongoRepository<RecruiterEntity>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RecruiterEntity, RecruiterDBEntity>();
                cfg.CreateMap<UpdateRecruiterDBEntity, RecruiterEntity>();
            });

            mapper = config.CreateMapper();
        }

        #endregion

        #region Members

        private MongoRepository<RecruiterEntity> _recruitersRepository;
        private IMapper mapper;
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Check if email not exists in recruiters table
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <returns>If exists</returns>
        public bool RecruiterEmailExists(string email)
        {
            return _recruitersRepository.Exists(rec => rec.Email == email);
        }

        /// <summary>
        /// Create new record in Recruiters table
        /// </summary>
        /// <param name="email">Required - email of recuiter</param>
        /// <param name="password">Required  - password</param>
        /// <returns>New recruiter id</returns>
        public string CreateRecruiter(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new RequiredFieldsNullOrEmptyException(new string[]{"email", "password"});

            RecruiterEntity newEntity = _recruitersRepository.Add(new RecruiterEntity()
             {
                 Email = email,
                 Password = password,
                 Gender = Gender.Unknown,
                 RegisterStatus = RecruiterRegisterStatus.Register,
                 SourceType = RecruiterSourceType.System,
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now,
                 RegisterStatusHistory = new List<BaseStatusHistory<RecruiterRegisterStatus>>() { new BaseStatusHistory<RecruiterRegisterStatus>() { Status = RecruiterRegisterStatus.Register, Date = DateTime.Now } },
             });

            return newEntity.Id;
        }

        /// <summary>
        /// Get recruiter by email and password
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <param name="password">The password</param>
        /// <returns>Founded id</returns>
        public string GetRecruiterByEmailAndPassword(string email, string password)
        {
            RecruiterEntity entity = _recruitersRepository.FirstOrDefault(rec => rec.Email == email && rec.Password == password);
            if (entity == null)
                throw new SignInException(email);
            return entity.Id;
        }

        /// <summary>
        /// Update existing recruiter 
        /// </summary>
        /// <param name="recruiterDBEntity">Recruiter details</param>
        /// <returns>Updated recruiter</returns>
        public RecruiterDBEntity UpdateRecruiterProfile(UpdateRecruiterDBEntity recruiterDBEntity)
        {
            if (recruiterDBEntity == null)
                throw new NullObjectException(typeof(RecruiterDBEntity).Name);

            RecruiterEntity recEntity = _recruitersRepository.GetById(recruiterDBEntity.Id);

            if (recEntity == null)
                throw new MongoEntityNotFoundException(recruiterDBEntity.Id, "recruiters");

            
            recEntity = mapper.Map(recruiterDBEntity, recEntity);
            
            //update UpdatedAt for current date
            recEntity.UpdatedAt = DateTime.Now;
            //update entity
            recEntity = _recruitersRepository.Update(recEntity);

            RecruiterDBEntity recruiter = mapper.Map<RecruiterEntity, RecruiterDBEntity>(recEntity);

            return recruiter;
        }

        /// <summary>
        /// Get recruiter by id
        /// </summary>
        /// <param name="recruiterId">The id</param>
        /// <returns>Recruiter details</returns>
        public RecruiterDBEntity GetRecruiterById(string recruiterId)
        {
            RecruiterEntity recEntity = _recruitersRepository.GetById(recruiterId);

            if (recEntity == null)
                throw new MongoEntityNotFoundException(recruiterId, "recruiters");
            
            RecruiterDBEntity recruiter = mapper.Map<RecruiterEntity, RecruiterDBEntity>(recEntity);

            return recruiter;
        }

        public string GetRecruiterNameById(string recruiterId)
        {
            RecruiterEntity recEntity = _recruitersRepository.GetById(recruiterId);
            if (recEntity != null)
                return string.Join(" ", recEntity.FirstName, recEntity.LastName);
            return null;
        }

        #endregion
    }
}
