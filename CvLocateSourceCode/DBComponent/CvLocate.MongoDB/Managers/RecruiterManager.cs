using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Enums;
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
    public class RecruiterManager : IRecruiterManager
    {
        #region Members

        private MongoRepository<RecruiterEntity> _recruitersRepository;

        #endregion

        #region Ctor

        public RecruiterManager()
        {
            _recruitersRepository = new MongoRepository<RecruiterEntity>();
        }

        #endregion

        #region Public Methods

        public SignResponse SignUp(SignUpCommand command)
        {
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
                 //RegisterStatusHistory = new List<BaseStatusHistory>() { new BaseStatusHistory() { Status = RecruiterRegisterStatus.Register, Date = DateTime.Now } },
                 FriendlyId = _recruitersRepository.GetNextId<RecruiterEntity>()
             });

            return new SignResponse()
            {
                CanSignIn = true,
                UserId = newEntity.Id,
                UserType = CvLocate.Common.EndUserDtoInterface.Enums.UserType.Recruiter
            };
        }

        #endregion

    }
}
