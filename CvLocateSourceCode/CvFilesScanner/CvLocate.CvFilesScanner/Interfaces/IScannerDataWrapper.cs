using CvLocate.Common.CommonDto.Enums;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Interfaces
{
    public interface IScannerDataWrapper
    {
        IList<FileType> GetSupportedFileTypes();
        BaseResult UploadScannedCvFile(UploadScannedCvFileCommand command);
        BaseResult DeleteScannedCvFile(DeleteScannedCvFileCommand command);
    }
}
