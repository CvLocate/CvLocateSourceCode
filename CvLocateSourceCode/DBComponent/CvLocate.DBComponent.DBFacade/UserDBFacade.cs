using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.DBComponent.MongoDB.Managers;
using CvLocate.DBComponent.DbInterface;

namespace CvLocate.DBComponent.DBFacade
{
    public class UserDBFacade : IUserDBFacade
    {
        public SignResponse SignUp(SignUpCommand command)
        {
            if (command == null)
                return null;
            switch (command.UserType)
            {
                case UserType.Recruiter:
                    {
                        IRecruiterManager recManager = RecruiterManager.Instance;
                        return recManager.SignUp(command);
                    }
                case UserType.JobSeeker:
                    break;
            }
            return null;
        }

        public SignResponse SignIn(SigninCommand command)
        {
            if (command == null)
                return null;
            SignResponse response = RecruiterManager.Instance.SignIn(command);
            if (response == null || response.CanSignIn == false)
                response = CandidateManager.Instance.SignIn(command);
            return response;
        }
    }
}
