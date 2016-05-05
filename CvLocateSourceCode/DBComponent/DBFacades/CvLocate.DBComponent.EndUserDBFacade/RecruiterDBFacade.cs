using AutoMapper;
using CvLocate.Common.CommonDto;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EndUserDTO.DTO;
using CvLocate.Common.EndUserDtoInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.DbInterface.DBEntities.Jobs;
using CvLocate.DBComponent.DbInterface.DBEntities.Recruiters;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.EndUserDBFacade
{
    public class RecruiterDBFacade : IRecruiterDBFacade
    {
        #region Members

        private IMapper mapper;

        #endregion


        public RecruiterDBFacade()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateRecruiterProfileCommand, UpdateRecruiterDBEntity>();
                cfg.CreateMap<RecruiterDBEntity, Recruiter>();
                cfg.CreateMap<JobDBEntity, Job>();
            });

            mapper = config.CreateMapper();

        }


        public RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query)
        {
            if (query == null || string.IsNullOrEmpty(query.RecruiterId))
                return null;
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterGetJobs(query);
        }

        public BaseResponse RecruiterCloseJob(CloseJobCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.JobId) || string.IsNullOrEmpty(command.Id))
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
            try
            {
                if (command == null || string.IsNullOrEmpty(command.Id) ||
                        string.IsNullOrEmpty(command.JobName) ||
                        string.IsNullOrEmpty(command.MandatoryRequirements))
                    return new RecruiterJobResponse(false) { ErrorMessage = "Id, Job name and Manadatory requirements are required" };
                IJobManager manager = JobManager.Instance;

                CreateJobDBEntity createJob = new CreateJobDBEntity()
                {
                    Name = command.JobName,
                    Owner = new List<Owner>() { new Owner() { RecruiterId = command.Id } },
                    Content = string.Empty,
                    MandatoryRequirements = command.MandatoryRequirements,
                    OptionalRequirements = command.OptionalRequirements,
                    Location = command.JobLocation,
                    Address = command.Address,
                    Status = JobStatus.Accepted,
                    SourceType = JobSourceType.Recruiter,
                    Source = string.Empty,
                    MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                    CreatedBy = command.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    StatusHistory = new List<BaseStatusHistory<JobStatus>>() { new BaseStatusHistory<JobStatus>() { Status = JobStatus.Accepted, Date = DateTime.Now } },
                    MatchingStatusHistory = new List<BaseStatusHistory<MatchingProcessStatus>>() { new BaseStatusHistory<MatchingProcessStatus>() { Status = MatchingProcessStatus.WaitingForMatching, Date = DateTime.Now } }
                };

                JobDBEntity newJob = manager.CreateJob(createJob);

                Job job = mapper.Map<JobDBEntity, Job>(newJob);

                return new RecruiterJobResponse(true) { Job = job };
            }
            catch (Exception ex)
            {
                return new RecruiterJobResponse(false) { ErrorMessage = "Failed create job. Orginal error: " + ex.ToString() };
            }
        }

        public RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.JobId) ||
                string.IsNullOrEmpty(command.Id) ||
                string.IsNullOrEmpty(command.JobName) ||
                string.IsNullOrEmpty(command.MandatoryRequirements))
                return new RecruiterJobResponse(false);
            IJobManager manager = JobManager.Instance;
            return manager.RecruiterUpdateJob(command);
        }

        public GetRecruiterProfileResponse GetRecruiterProfile(GetRecruiterProfileQuery query)
        {
            try
            {
                if (query == null)
                    return null;
                IRecruiterManager manager = RecruiterManager.Instance;
                RecruiterDBEntity recruiterEntity = manager.GetRecruiterById(query.RecruiterId);

                //convert from RecruiterDBEntity to Recruiter
                Recruiter rec = mapper.Map<RecruiterDBEntity, Recruiter>(recruiterEntity);

                return new GetRecruiterProfileResponse() { Recruiter = rec };

            }
            catch (Exception ex)
            {
                return new GetRecruiterProfileResponse() { ErrorMessage = "Failed get recruiter profile. Recruiter id: [" + query.RecruiterId + "]. Orginal error: " + ex.ToString() };
            }
        }

        public UpdateRecruiterProfileResponse UpdateRecruiterProfile(UpdateRecruiterProfileCommand command)
        {
            try
            {
                if (command == null ||
                        string.IsNullOrEmpty(command.Id) ||
                        string.IsNullOrEmpty(command.FirstName) ||
                         string.IsNullOrEmpty(command.LastName))
                    return new UpdateRecruiterProfileResponse(false);
                IRecruiterManager manager = RecruiterManager.Instance;
                
                //convert from UpdateRecruiterProfileCommand to UpdateRecruiterDBEntity
                UpdateRecruiterDBEntity entity = mapper.Map<UpdateRecruiterProfileCommand, UpdateRecruiterDBEntity>(command);

                RecruiterDBEntity recruiter = manager.UpdateRecruiterProfile(entity);

                //convert from RecruiterDBEntity to Recruiter
                Recruiter updatedRec = mapper.Map<RecruiterDBEntity, Recruiter>(recruiter);

                return new UpdateRecruiterProfileResponse(true) { Recruiter = updatedRec };

            }
            catch (Exception ex)
            {
                return new UpdateRecruiterProfileResponse(false) { ErrorMessage = "Failed update recruiter. Orginal error: " + ex.ToString() };
            }
        }
    }
}
