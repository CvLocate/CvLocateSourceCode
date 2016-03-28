using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.MongoDB.Managers;
using CvLocate.DbInterface;

namespace CvLocate.DBFacade
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
                        IRecruiterManager recManager = new RecruiterManager();
                        return recManager.SignUp(command);
                    }
                case UserType.JobSeeker:
                    break;
            }
            return null;
        }

        public SignResponse SignIn(SigninCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
