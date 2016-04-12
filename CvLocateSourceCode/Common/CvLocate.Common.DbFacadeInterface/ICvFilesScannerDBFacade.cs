using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.DbFacadeInterface
{
    public interface ICvFilesScannerDBFacade
    {
        BaseResult UploadScannedCvFile(UploadScannedCvFileCommand command);
        BaseResult DeleteScannedCvFile(DeleteScannedCvFileCommand command);
    }
}
