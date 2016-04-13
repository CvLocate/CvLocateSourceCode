using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface
{
    public interface ICandidateManager
    {
        /// <summary>
        /// Check if email exists in candidates table
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <returns>If exists</returns>
        bool CandidateEmailExists(string email);

        /// <summary>
        /// Get candidate by email and password
        /// </summary>
        /// <param name="email">Email for check</param>
        /// <param name="password">The password</param>
        /// <returns>Founded id</returns>
        string GetCandidateByEmailAndPassword(string email, string password);

    }
}
