using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.DBComponent.DbInterface;
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
        public BaseResult UploadScannedCvFile(UploadScannedCvFileCommand command)
        {
            if (command == null)
                return new BaseResult(false) { ErrorMessage = "Command cannot be null" };
            ICvFilesManager cvFilesManager = CvFilesManager.Instance;
            if (!cvFilesManager.CvFileExists(command.CvFileId))
                return new BaseResult(false) { ErrorMessage = "CvFile not found" };
            IFilesManager filesManager = FilesManager.Instance;
            if (command.Stream == null)
                return new BaseResult(false) { ErrorMessage = "Stream cannot be null" };
            string fileId = filesManager.UploadFile(command.Stream);
            cvFilesManager.UpdateCvFileUploaded(command.CvFileId, fileId);

            return new BaseResult(true);
        }

        public BaseResult DeleteScannedCvFile(DeleteScannedCvFileCommand command)
        {
            if (command == null)
                return new BaseResult(false) { ErrorMessage = "Command cannot be null" };
            ICvFilesManager cvFilesManager = CvFilesManager.Instance;
            if (!cvFilesManager.CvFileExists(command.CvFileId))
                return new BaseResult(false) { ErrorMessage = "CvFile not found" };
            cvFilesManager.UpdateCvFileDeleted(command.CvFileId, command.StatusReason, command.StatusReasonDetails);
            return new BaseResult(true);
        }
    }
}
