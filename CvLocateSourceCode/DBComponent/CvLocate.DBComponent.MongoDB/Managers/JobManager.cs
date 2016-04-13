using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDTO.DTO;
using CvLocate.Common.EndUserDtoInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Query;
using CvLocate.Common.EndUserDtoInterface.Response;
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
        }

        #endregion

        #region Members

        private MongoRepository<JobEntity> _jobRepository;

        #endregion

        #region Public Methods

        public RecruiterJobResponse RecruiterCreateJob(CreateJobCommand command)
        {
            if (command == null)
                return new RecruiterJobResponse(false);
            JobEntity jobEntity = new JobEntity()
            {
                Name = command.JobName,
                FriendlyId = RepositoryExtensions.GetNextId<JobEntity>(_jobRepository),
                Owner = new List<Owner>() { new Owner() { RecruiterId = command.RecruiterId } },
                Content = string.Empty,
                MandatoryRequirements = command.MandatoryRequirements,
                OptionalRequirements = command.OptionalRequirements,
                Location = command.JobLocation,
                Address = command.Address,
                Status = JobStatus.Accepted,
                SourceType = JobSourceType.Recruiter,
                Source = string.Empty,
                MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                CreatedBy = command.RecruiterId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StatusHistory = new List<BaseStatusHistory<JobStatus>>() { new BaseStatusHistory<JobStatus>() { Status = JobStatus.Accepted, Date = DateTime.Now } },
                MatchingStatusHistory = new List<BaseStatusHistory<MatchingProcessStatus>>() { new BaseStatusHistory<MatchingProcessStatus>() { Status = MatchingProcessStatus.WaitingForMatching, Date = DateTime.Now } }
            };

            jobEntity = _jobRepository.Add(jobEntity);
            AutoMapper.Mapper.CreateMap<JobEntity, Job>();
            Job job = AutoMapper.Mapper.Map<JobEntity, Job>(jobEntity);
            // if job was created by recruiter, get name of recruiter
            if (jobEntity.SourceType == JobSourceType.Recruiter)
            {
                job.CreatedByName = RecruiterManager.Instance.GetRecruiterNameById(jobEntity.CreatedBy);
            }
            return new RecruiterJobResponse(true) { Job = job };
        }

        public RecruiterJobResponse RecruiterUpdateJob(UpdateJobCommand command)
        {
            if (command == null)
                return new RecruiterJobResponse(false);
            //get jobEntity by id
            JobEntity jobEntity = _jobRepository.GetById(command.JobId);
            //check the recruiter id is the owner
            if (!CheckRecruiterIsJobOwner(jobEntity,command.RecruiterId))
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
            if (!CheckRecruiterIsJobOwner(jobEntity, command.RecruiterId))
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
