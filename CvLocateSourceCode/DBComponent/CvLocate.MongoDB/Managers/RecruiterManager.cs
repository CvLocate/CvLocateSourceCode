using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DbInterface;
using CvLocate.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.EndUserDtoInterface;
using MongoDB.Bson;
using CvLocate.Common.EndUserDtoInterface.Query;

namespace CvLocate.MongoDB.Managers
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

        public SignResponse SignUp(SignUpCommand command)
        {
            if (command == null)
                return new SignResponse() { CanSignIn = false };
            if (_recruitersRepository.Exists(rec => rec.Email == command.Email))
                return new SignResponse() { CanSignIn = false };
            RecruiterEntity newEntity = _recruitersRepository.Add(new RecruiterEntity()
             {
                 Email = command.Email,
                 Password = command.Password,
                 Gender = Gender.Unknown,
                 RegisterStatus = RecruiterRegisterStatus.Register,
                 SourceType = RecruiterSourceType.System,
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now,
                 RegisterStatusHistory = new List<BaseStatusHistory<RecruiterRegisterStatus>>() { new BaseStatusHistory<RecruiterRegisterStatus>() { Status = RecruiterRegisterStatus.Register, Date = DateTime.Now } },
             });

            return new SignResponse()
            {
                CanSignIn = true,
                UserId = newEntity.Id,
                UserType = CvLocate.Common.CommonDto.UserType.Recruiter
            };
        }

        public SignResponse SignIn(SigninCommand command)
        {
            SignResponse cantSigninResponse = new SignResponse() { CanSignIn = false };
            if (command == null || _recruitersRepository.Count() == 0)
                return cantSigninResponse;
            RecruiterEntity recuriter = _recruitersRepository.FirstOrDefault(rec => rec.Email == command.Email && rec.Password == command.Password);
            if (recuriter == null)
                return cantSigninResponse;
            return new SignResponse() { CanSignIn = true, UserId = recuriter.Id, UserType = UserType.Recruiter };
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
