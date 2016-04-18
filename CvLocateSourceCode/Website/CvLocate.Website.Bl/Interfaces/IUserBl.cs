using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Website.Bl.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.Interfaces
{
    public interface IUserBl
    {
        SignupResponse SignUp(SignUpCommand command);
    }
}
