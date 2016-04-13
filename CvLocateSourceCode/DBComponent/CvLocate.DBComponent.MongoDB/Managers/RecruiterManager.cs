using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Response;
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
        }

        #endregion

        #region Members

        private MongoRepository<RecruiterEntity> _recruitersRepository;

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

        public UpdateRecruiterProfileResponse UpdateRecruiterProfile(UpdateRecruiterProfileCommand command)
        {
            if (command == null)
                return new UpdateRecruiterProfileResponse(false);

            RecruiterEntity recEntity = _recruitersRepository.GetById(command.RecruiterId);
            recEntity.Gender = command.Gender;
            recEntity.FirstName = command.FirstName;
            recEntity.LastName = command.LastName;
            recEntity.CompanyName = command.CompanyName;
            
            //upload image
            if (command.Image != null)
            {
                //check if there is old image in DB. if true, remove old image
                if (!string.IsNullOrEmpty(recEntity.ImageId))
                {
                    MongoExtensions.Instance.RemoveFile(recEntity.ImageId);
                }
                string imageName = "[ImageOf]:[" + recEntity.Email + "]";
                string imageId = MongoExtensions.Instance.UploadFile(command.Image, imageName);
                recEntity.ImageId = imageId;
            }
            //update UpdatedAt for current date
            recEntity.UpdatedAt = DateTime.Now;
            //update entity
            recEntity = _recruitersRepository.Update(recEntity);

            //download image
            byte[] imageBytes = MongoExtensions.Instance.DownloadFile(recEntity.ImageId);

            //convert from RecruiterEntity to Recruiter
            AutoMapper.Mapper.CreateMap<RecruiterEntity, Recruiter>();
            Recruiter recruiter = AutoMapper.Mapper.Map<RecruiterEntity, Recruiter>(recEntity);
            recruiter.Image = imageBytes;

            return new UpdateRecruiterProfileResponse(true) { Recruiter = recruiter };
        }

        public GetRecruiterProfileResponse GetRecruiterProfile(GetRecruiterProfileQuery query)
        {
            if (query == null)
                return null;
            RecruiterEntity recEntity = _recruitersRepository.GetById(query.RecruiterId);
            if (recEntity != null)
            {
                AutoMapper.Mapper.CreateMap<RecruiterEntity, Recruiter>();
                Recruiter recruiter = AutoMapper.Mapper.Map<RecruiterEntity, Recruiter>(recEntity);
                byte[] imageBytes = MongoExtensions.Instance.DownloadFile(recEntity.ImageId);
                recruiter.Image = imageBytes;
                return new GetRecruiterProfileResponse() { Recruiter = recruiter };
            }
            return null;
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
