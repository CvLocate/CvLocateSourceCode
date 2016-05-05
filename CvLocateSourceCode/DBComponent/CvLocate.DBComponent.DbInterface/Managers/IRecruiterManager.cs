using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.DbInterface.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Managers
{
    public interface IRecruiterManager
    {
        /// <summary>
        /// Create new record in Recruiters table
        /// </summary>
        /// <param name="email">Required - email of recuiter</param>
        /// <param name="password">Required  - password</param>
        /// <returns>New recruiter id</returns>
        string CreateRecruiter(string email, string password);

        /// <summary>
        /// Get recruiter by email and password
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <param name="password">The password</param>
        /// <returns>Founded id</returns>
        string GetRecruiterByEmailAndPassword(string email, string password);

        /// <summary>
        /// Check if email not exists in recruiters table
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <returns>If exists</returns>
        bool RecruiterEmailExists(string email);

        /// <summary>
        /// Update existing recruiter 
        /// </summary>
        /// <param name="recruiterDBEntity">Recruiter details</param>
        /// <returns>Updated recruiter</returns>
        RecruiterDBEntity UpdateRecruiterProfile(UpdateRecruiterDBEntity recruiterDBEntity);

        /// <summary>
        /// Get recruiter by id
        /// </summary>
        /// <param name="recruiterId">The id</param>
        /// <returns>Recruiter details</returns>
        RecruiterDBEntity GetRecruiterById(string recruiterId);
    }
}
