using CvLocate.DBComponent.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.DBComponent.DbInterface;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Enums;
using CvLocate.DBComponent.DbInterface.Exceptions;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.DbInterface.DBEntities;

namespace CvLocate.DBComponent.MongoDB.Managers
{
    public class CvFilesManager : ICvFilesManager
    {
        #region Members

        private MongoRepository<CvFileEntity> _cvFilesRepository;

        #endregion

        #region Singletone Implementation

        private static CvFilesManager _instance;
        public static CvFilesManager Instance
        {
            get { return _instance ?? (_instance = new CvFilesManager()); }
        }

        private CvFilesManager()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["MongoCvFilesDBSettings"].ConnectionString;
            _cvFilesRepository = new MongoRepository<CvFileEntity>(connection);
        }

        #endregion

        //public GetTopCvFilesForParsingResult GetTopCandidatesForParsing()
        //{
        //    var files = _cvFilesRepository.Where(cvFile => cvFile.ParsingStatus == Common.CommonDto.ParsingProcessStatus.WaitingForParsing);
        //    if (files == null)
        //        return null;

        //    files.OrderByDescending(file => file.UpdatedAt);
        //    files.Take(10);
        //    List<CandidateCvFileForParsing> cvFilesForParsing = new List<CandidateCvFileForParsing>();
        //    files.ToList().ForEach(fileEntity =>
        //    {
        //        //convert from CvFileEntity to CvFileForParsing
        //        //AutoMapper.Mapper.CreateMap<CvFileEntity, CvFileForParsing>();
        //        //CvFileForParsing cvFile = AutoMapper.Mapper.Map<CvFileEntity, CvFileForParsing>(fileEntity);
        //        //download file stream by file name
        //        //cvFile.Stream = MongoExtensions.Instance.DownloadFile(fileEntity.FileStreamName);
        //        //get candidate from candidate table by CandidateId
        //        //Candidate candidate = CandidateManager.Instance.GetCandidateById(fileEntity.CandidateId);
        //        //cvFilesForParsing.Add(new CandidateCvFileForParsing() { CvFile = cvFile, Candidate = candidate });
        //    });
        //    return null;
        //}

        #region Public Methods

        /// <summary>
        /// Update existing file to uploaded state
        /// </summary>
        /// <param name="cvFileId">CvFile id</param>
        /// <param name="fileId">File id from Files table</param>
        public void UpdateCvFileUploaded(CvFileDBEntity cvFile)
        {
            if (cvFile == null)
                throw new BaseMongoException("CvFile is null");
            CvFileEntity cvFileEntity = GetCvFileById(cvFile.CvFileId);
            if (cvFileEntity == null)
                throw new MongoEntityNotFoundException(cvFile.CvFileId, "CvFiles");

            AutoMapper.Mapper.CreateMap<CvFileDBEntity, CvFileEntity>();
            cvFileEntity = AutoMapper.Mapper.Map(cvFile, cvFileEntity);
            
            cvFileEntity.UpdatedAt = DateTime.Now;
            _cvFilesRepository.Update(cvFileEntity);
        }

        /// <summary>
        /// Update existing file to deleted state
        /// </summary>
        /// <param name="cvFileId">existing cvFile id</param>
        /// <param name="statusReason">status reason</param>
        /// <param name="statusReasonDetails">status reason details</param>
        public void UpdateCvFileDeleted(string cvFileId, CvStatusReason statusReason, string statusReasonDetails)
        {
            CvFileEntity cvFile = GetCvFileById(cvFileId);
            if (cvFile == null)
                throw new MongoEntityNotFoundException(cvFileId, "CvFiles");

            cvFile.Status = CvFileStatus.Deleted;
            cvFile.StatusReason = statusReason;
            cvFile.StatusReasonDetails = statusReasonDetails;
            cvFile.UpdatedAt = DateTime.Now;
            _cvFilesRepository.Update(cvFile);
        }

        /// <summary>
        /// Check if CvFile exists
        /// </summary>
        /// <param name="cvFileId">CvFile id</param>
        /// <returns>If exists</returns>
        public bool CvFileExists(string cvFileId)
        {
            return _cvFilesRepository.Exists(cvFile => cvFile.Id == cvFileId);
        }

        /// <summary>
        /// Create new record in CvFiles table
        /// </summary>
        /// <param name="extension">Extension type</param>
        /// <param name="sourceType">Source type</param>
        /// <param name="source">Source string</param>
        /// <returns>New record id</returns>
        public string CreateCvFile(FileType extension, CvSourceType sourceType, string source)
        {
            CvFileEntity cvFile = new CvFileEntity()
            {
                Extension = extension,
                SourceType = sourceType,
                Source = source,
                Status = CvFileStatus.New,
                ParsingStatus = ParsingProcessStatus.NotReadyForParsing,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            cvFile = _cvFilesRepository.Add(cvFile);
            return cvFile.Id;
        }

        #endregion

        #region Private Methods

        private CvFileEntity GetCvFileById(string fileId)
        {
            return _cvFilesRepository.GetById(fileId);
        }

        #endregion
    }
}
