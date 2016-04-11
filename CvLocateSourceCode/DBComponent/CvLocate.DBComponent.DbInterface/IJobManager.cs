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
    public interface IJobManager
    {
        RecruiterJobResponse RecruiterCreateJob(CreateJobCommand command);
        RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command);
        RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query);
        RecruiterJobResponse RecruiterGetJob(RecruiterGetJobQuery query);
        BaseResponse RecruiterCloseJob(CloseJobCommand command);
    }
}
