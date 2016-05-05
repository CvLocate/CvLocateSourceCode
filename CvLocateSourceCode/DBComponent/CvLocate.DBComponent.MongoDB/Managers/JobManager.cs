using AutoMapper;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDTO.DTO;
using CvLocate.Common.EndUserDtoInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.DbInterface.DBEntities.Jobs;
using CvLocate.DBComponent.DbInterface.Exceptions;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CvLocate.DBComponent.MongoDB.Managers
{
    public class JobManager : IJobManager
    {
        #region Singletone Implementation

        private static JobManager _instance;
        public static JobManager Instance
        {
            get { return _instance ?? (_instance = new JobManager()); }
        }

        private JobManager()
        {
            _jobRepository = new MongoRepository<JobEntity>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateJobDBEntity, JobEntity>();
                cfg.CreateMap<JobEntity, JobDBEntity>();
            });

            mapper = config.CreateMapper();

        }

        #endregion

        #region Members

        private MongoRepository<JobEntity> _jobRepository;
        private IMapper mapper;

        #endregion

        #region Public Methods

        /// <summary>
        /// Create new job for specific recruiter
        /// </summary>
        /// <param name="jobDBEntity">Job details</param>
        /// <returns>New job</returns>
        public JobDBEntity CreateJob(CreateJobDBEntity jobDBEntity)
        {
            if (jobDBEntity == null)
                throw new NullObjectException(typeof(CreateJobDBEntity).Name);

            JobEntity jobEntity = mapper.Map<CreateJobDBEntity, JobEntity>(jobDBEntity);
            jobEntity.FriendlyId = RepositoryExtensions.GetNextId<JobEntity>(_jobRepository);
            
            JobEntity newJob = _jobRepository.Add(jobEntity);
            JobDBEntity newJobDBEntity = mapper.Map<JobEntity, JobDBEntity>(newJob);
            // if job was created by recruiter, get name of recruiter
            if (jobEntity.SourceType == JobSourceType.Recruiter)
            {
            newJobDBEntity.CreatedByName = RecruiterManager.Instance.GetRecruiterNameById(jobDBEntity.CreatedBy);
            }

            return newJobDBEntity;
        }

        public RecruiterJobResponse UpdateJob(UpdateJobCommand command)
        {
            if (command == null)
                return new RecruiterJobResponse(false);
            //get jobEntity by id
            JobEntity jobEntity = _jobRepository.GetById(command.JobId);
            //check the recruiter id is the owner
            if (!CheckRecruiterIsJobOwner(jobEntity, command.Id))
                return new RecruiterJobResponse(false);

            //check if there is changes that affect on matching process
            if (command.JobName != jobEntity.Name ||
                command.MandatoryRequirements != jobEntity.MandatoryRequirements ||
                jobEntity.OptionalRequirements != command.OptionalRequirements)
            {
                jobEntity.MatchingStatus = MatchingProcessStatus.WaitingForMatching;
                if (jobEntity.MatchingStatusHistory == null)
                    jobEntity.MatchingStatusHistory = new List<BaseStatusHistory<MatchingProcessStatus>>();
                jobEntity.MatchingStatusHistory.Add(new BaseStatusHistory<MatchingProcessStatus>() { Status = MatchingProcessStatus.WaitingForMatching, Date = DateTime.Now });
            }

            jobEntity.UpdatedAt = DateTime.Now;

            //convert from UpdateJobCommand to JobEntity for update other fields
            AutoMapper.Mapper.CreateMap<UpdateJobCommand, JobEntity>();
            jobEntity = AutoMapper.Mapper.Map(command, jobEntity);

            //update entity
            jobEntity = _jobRepository.Update(jobEntity);

            AutoMapper.Mapper.CreateMap<JobEntity, Job>();
            Job job = AutoMapper.Mapper.Map<JobEntity, Job>(jobEntity);

            // if job was created by recruiter, get name of recruiter
            if (jobEntity.SourceType == JobSourceType.Recruiter)
            {
                job.CreatedByName = RecruiterManager.Instance.GetRecruiterNameById(jobEntity.CreatedBy);
            }
            return new RecruiterJobResponse(true) { Job = job };
        }

        public RecruiterGetJobsResponse RecruiterGetJobs(RecruiterGetJobsQuery query)
        {
            if (query == null)
                return null;

            //search for jobs that reruiter id is from owners
            var jobs = _jobRepository.Where(job => job.Owner != null && job.Owner.Any(owner => owner.RecruiterId == query.RecruiterId));

            if (jobs == null)
                return new RecruiterGetJobsResponse() { ActiveJobs = null };

            List<JobEntity> filterJobs = null;

            //filter list by JobState
            switch (query.JobState)
            {
                case JobState.All:
                    filterJobs = jobs.ToList();
                    break;
                case JobState.Active:
                    {
                        var activeJobs = jobs.Where(job => job.Status == JobStatus.Accepted);
                        if (activeJobs != null)
                            filterJobs = activeJobs.ToList();
                    }
                    break;
                case JobState.Archive:
                    {
                        var archiveJobs = jobs.Where(job => job.Status == JobStatus.Closed);
                        if (archiveJobs != null)
                            filterJobs = archiveJobs.ToList();
                    }
                    break;
            }
            if (filterJobs == null)
                return new RecruiterGetJobsResponse() { ActiveJobs = null };

            //order list by created date
            filterJobs = filterJobs.OrderByDescending(job => job.CreatedAt).ToList();

            List<JobHeader> jobsHeader = new List<JobHeader>();

            //convert list from JobEntity to JobHeader
            AutoMapper.Mapper.CreateMap<JobEntity, JobHeader>();
            filterJobs.ForEach(job => jobsHeader.Add(AutoMapper.Mapper.Map<JobHeader>(job)));

            return new RecruiterGetJobsResponse() { ActiveJobs = jobsHeader };
        }

        public RecruiterJobResponse RecruiterGetJob(RecruiterGetJobQuery query)
        {
            if (query == null)
                return new RecruiterJobResponse(false);
            JobEntity jobEntity = _jobRepository.GetById(query.JobId);
            if (!CheckRecruiterIsJobOwner(jobEntity, query.RecruiterId))
                return new RecruiterJobResponse(false);
            AutoMapper.Mapper.CreateMap<JobEntity, Job>();
            Job job = AutoMapper.Mapper.Map<JobEntity, Job>(jobEntity);

            UpdateJobRecruiterName(jobEntity, job);

            return new RecruiterJobResponse(true) { Job = job };
        }

        public BaseResponse RecruiterCloseJob(CloseJobCommand command)
        {
            if (command == null)
                return null;
            JobEntity jobEntity = _jobRepository.GetById(command.JobId);
            if (!CheckRecruiterIsJobOwner(jobEntity, command.Id))
                return null;
            jobEntity.Status = JobStatus.Closed;
            _jobRepository.Update(jobEntity);
            return new BaseResponse();
        }

        #endregion

        #region Private Methods

        private void UpdateJobRecruiterName(JobEntity jobEntity, Job job)
        {
            // if job was created by recruiter, get name of recruiter
            if (jobEntity.SourceType == JobSourceType.Recruiter)
            {
                job.CreatedByName = RecruiterManager.Instance.GetRecruiterNameById(jobEntity.CreatedBy);
            }
        }

        private bool CheckRecruiterIsJobOwner(JobEntity jobEntity, string recruiterId)
        {
            if (jobEntity == null || jobEntity.Owner == null ||
                !jobEntity.Owner.Exists(owner => owner.RecruiterId == recruiterId))
                return false;
            return true;

        }

        #endregion
    }
}
