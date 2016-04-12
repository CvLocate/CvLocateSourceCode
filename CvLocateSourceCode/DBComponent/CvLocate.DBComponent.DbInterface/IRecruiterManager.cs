using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface
{
    public interface IRecruiterManager
    {
        SignResponse SignUp(SignUpCommand command);
        SignResponse SignIn(SigninCommand command);
        UpdateRecruiterProfileResponse UpdateRecruiterProfile(UpdateRecruiterProfileCommand command);
        GetRecruiterProfileResponse GetRecruiterProfile(GetRecruiterProfileQuery query);



        /// <summary>
        /// Check if email not exists in recruiters table
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <returns>If exists</returns>
        bool CheckEmailNotExists(string email);
    }
}
