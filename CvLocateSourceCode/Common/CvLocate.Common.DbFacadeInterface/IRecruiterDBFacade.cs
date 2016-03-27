using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.DbFacadeInterface
{
    public interface IRecruiterDBFacade
    {
        
            //Get minimal details about the active or archive or all jobs belong to this recruiter
            RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query);
            //change the status of the job to closed
            BaseResponse RecruiterCloseJob(CloseJobCommand command);
            //get matchings list of specific job
            RecruiterGetJobMatchingsResponse RecruiterGetJobMatchings(RecruiterGetJobMatchingsQuery query);
            //change status of specific matching
            BaseResponse RecruiterChangeMatchingStatus(ChangeMatchingStatusCommand command);
            //get details about candidate that match to specific job
            RecruiterGetMatchCandidateResponse RecruiterGetMatchCandidate(RecruiterGetMatchCandidateQuery query);
            //get job details
            RecruiterJobResponse RecruiterGetJob(RecruiterGetJobQuery query);
            //create new job
            RecruiterJobResponse RecruiterCreateJob(CreateJobCommand command);
            //update existing job
            RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command);
            //get recruiter profile deatils
            GetRecruiterProfileResponse GetRecruiterProfile(GetRecruiterProfileQuery command);
            //update recruiter profile details
            UpdateRecruiterProfileResponse UpdateRecruiterProfile(UpdateRecruiterProfileCommand command);
        

    }
}
