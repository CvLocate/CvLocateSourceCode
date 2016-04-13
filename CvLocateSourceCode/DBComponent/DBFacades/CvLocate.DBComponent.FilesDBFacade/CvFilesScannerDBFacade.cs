using AutoMapper;
using CvLocate.Common.CommonDto;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.DBComponent.DbInterface;
using CvLocate.DBComponent.DbInterface.DBEntities;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.CvFilesDBFacade
{
    public class CvFilesScannerDBFacade : ICvFilesScannerDBFacade
    {
        #region Members

        private ICvFilesManager _cvFilesManager;
        private IFilesManager _filesManager;

        #endregion

        #region Ctor

        public CvFilesScannerDBFacade()
        {
            _cvFilesManager = CvFilesManager.Instance;
            _filesManager = FilesManager.Instance;
        }

        #endregion

        #region Public Methods

        public BaseResult UploadScannedCvFile(UploadScannedCvFileCommand command)
        {
            if (command == null)
                return new BaseResult(false) { ErrorMessage = "Command cannot be null" };
            if (command.Stream == null)
                return new BaseResult(false) { ErrorMessage = "Stream cannot be null" };

            if (!_cvFilesManager.CvFileExists(command.CvFileId))
                return new BaseResult(false) { ErrorMessage = "CvFile not found" };

            //upload stream to Files table
            string fileId = _filesManager.UploadFile(command.Stream);

            //convert from UploadScannedCvFileCommand to CvFileDBEntity
            Mapper.CreateMap<UploadScannedCvFileCommand, CvFileDBEntity>();
            CvFileDBEntity cvFile = Mapper.Map<UploadScannedCvFileCommand, CvFileDBEntity>(command);
            
            //update other fields
            cvFile.FileId = fileId;
            cvFile.Status = CvFileStatus.Scanned;
            cvFile.ParsingStatus = ParsingProcessStatus.WaitingForParsing;

            _cvFilesManager.UpdateCvFileUploaded(cvFile);

            return new BaseResult(true);
        }

        public BaseResult DeleteScannedCvFile(DeleteScannedCvFileCommand command)
        {
            if (command == null)
                return new BaseResult(false) { ErrorMessage = "Command cannot be null" };
            if (!_cvFilesManager.CvFileExists(command.CvFileId))
                return new BaseResult(false) { ErrorMessage = "CvFile not found" };
            _cvFilesManager.UpdateCvFileDeleted(command.CvFileId, command.StatusReason, command.StatusReasonDetails);
            return new BaseResult(true);
        }

        #endregion
    }
}
