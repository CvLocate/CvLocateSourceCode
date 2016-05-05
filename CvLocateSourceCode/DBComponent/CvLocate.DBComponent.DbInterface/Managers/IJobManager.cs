using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.DbInterface.DBEntities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Managers
{
    public interface IJobManager
    {
        RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command);
        RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query);
        RecruiterJobResponse RecruiterGetJob(RecruiterGetJobQuery query);
        BaseResponse RecruiterCloseJob(CloseJobCommand command);

        /// <summary>
        /// Create new job for specific recruiter
        /// </summary>
        /// <param name="jobDBEntity">Job details</param>
        /// <returns>New job</returns>
        JobDBEntity CreateJob(CreateJobDBEntity jobDBEntity);
    }
}
