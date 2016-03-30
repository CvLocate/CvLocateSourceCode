using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DbInterface;
using CvLocate.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBFacade
{
    public class RecruiterDBFacade : IRecruiterDBFacade
    {
        public RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query)
        {
            if (query == null || string.IsNullOrEmpty(query.RecruiterId))
                return null;
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterGetJobs(query);
        }

        public BaseResponse RecruiterCloseJob(CloseJobCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.JobId) || string.IsNullOrEmpty(command.RecruiterId))
                return null;
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterCloseJob(command);
        }

        public Common.EndUserDtoInterface.Response.RecruiterGetJobMatchingsResponse RecruiterGetJobMatchings(Common.EndUserDtoInterface.Query.RecruiterGetJobMatchingsQuery query)
        {
            throw new NotImplementedException();
        }

        public Common.EndUserDtoInterface.Response.BaseResponse RecruiterChangeMatchingStatus(Common.EndUserDtoInterface.Command.ChangeMatchingStatusCommand command)
        {
            throw new NotImplementedException();
        }

        public Common.EndUserDtoInterface.Response.RecruiterGetMatchCandidateResponse RecruiterGetMatchCandidate(Common.EndUserDtoInterface.Query.RecruiterGetMatchCandidateQuery query)
        {
            throw new NotImplementedException();
        }

        public RecruiterJobResponse RecruiterGetJob(RecruiterGetJobQuery query)
        {
            if (query == null || string.IsNullOrEmpty(query.RecruiterId)
                || string.IsNullOrEmpty(query.JobId))
                return null;
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterGetJob(query);
        }

        public RecruiterJobResponse RecruiterCreateJob(CreateJobCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.RecruiterId) ||
                string.IsNullOrEmpty(command.JobName) ||
                string.IsNullOrEmpty(command.MandatoryRequirements))
                return new RecruiterJobResponse(false);
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterCreateJob(command);
        }

        public RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.JobId) ||
                string.IsNullOrEmpty(command.RecruiterId) ||
                string.IsNullOrEmpty(command.JobName) ||
                string.IsNullOrEmpty(command.MandatoryRequirements))
                return new RecruiterJobResponse(false);
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterUpdateJob(command);
        }

        public GetRecruiterProfileResponse GetRecruiterProfile(GetRecruiterProfileQuery query)
        {
            if (query == null)
                return null;
            IRecruiterManager manager = RecruiterManager.Instance;
            return manager.GetRecruiterProfile(query);
        }

        public UpdateRecruiterProfileResponse UpdateRecruiterProfile(UpdateRecruiterProfileCommand command)
        {
            if (command == null || 
                string.IsNullOrEmpty(command.RecruiterId) ||
                string.IsNullOrEmpty(command.FirstName) ||
                 string.IsNullOrEmpty(command.LastName))
                return new UpdateRecruiterProfileResponse(false);
            IRecruiterManager manager = RecruiterManager.Instance;
            return manager.UpdateRecruiterProfile(command);
        }
    }
}
