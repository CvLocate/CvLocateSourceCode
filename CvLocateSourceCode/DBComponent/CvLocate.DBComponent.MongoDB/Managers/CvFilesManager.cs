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
            _cvFilesRepository = new MongoRepository<CvFileEntity>();
        }

        #endregion

        public GetTopCvFilesForParsingResult GetTopCandidatesForParsing()
        {
            var files = _cvFilesRepository.Where(cvFile => cvFile.ParsingStatus == Common.CommonDto.ParsingProcessStatus.WaitingForParsing);
            if (files == null)
                return null;

            files.OrderByDescending(file => file.UpdatedAt);
            files.Take(10);
            List<CandidateCvFileForParsing> cvFilesForParsing = new List<CandidateCvFileForParsing>();
            files.ToList().ForEach(fileEntity => 
            {
                //convert from CvFileEntity to CvFileForParsing
                AutoMapper.Mapper.CreateMap<CvFileEntity, CvFileForParsing>();
                CvFileForParsing cvFile = AutoMapper.Mapper.Map<CvFileEntity, CvFileForParsing>(fileEntity);
                //download file stream by file name
                cvFile.Stream = MongoExtensions.Instance.DownloadFile(fileEntity.FileStreamName);
                //get candidate from candidate table by CandidateId
                //Candidate candidate = CandidateManager.Instance.GetCandidateById(fileEntity.CandidateId);
                //cvFilesForParsing.Add(new CandidateCvFileForParsing() { CvFile = cvFile, Candidate = candidate });
            });
            return null;
        }

        public BaseInsertResult UploadCvFile(UploadCvFileCommand command)
        {
            if (command == null)
                return null;
            CvFileEntity fileEntity = new CvFileEntity();
            fileEntity.Extension = command.Extension;
            fileEntity.Source = command.Source;
            fileEntity.SourceType = command.SourceType;
            fileEntity.FileStreamName = MongoExtensions.Instance.UploadFile(command.Stream, command.FileName);
            _cvFilesRepository.Add(fileEntity);
            return new BaseInsertResult(true) { Id = fileEntity.Id };
        }
    }
}
