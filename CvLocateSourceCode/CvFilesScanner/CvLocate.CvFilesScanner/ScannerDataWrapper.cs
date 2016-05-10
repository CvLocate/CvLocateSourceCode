using CvLocate.Common.CommonDto.Enums;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.CvFilesScanner.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class ScannerDataWrapper:IScannerDataWrapper
    {
        ICvFilesScannerDBFacade _cvFilesScannerDBFacade;
        public ScannerDataWrapper(Container container)
        {
            _cvFilesScannerDBFacade = container.GetInstance<ICvFilesScannerDBFacade>();
        }
        public IList<FileType> GetSupportedFileTypes()//todo change to get from db
        {
            return new List<FileType>() { FileType.Doc, FileType.Docx, FileType.Pdf, FileType.Rtf, FileType.Txt };
        }

        public BaseResult UploadScannedCvFile(UploadScannedCvFileCommand command)
        {
            return _cvFilesScannerDBFacade.UploadScannedCvFile(command);
        }

        public BaseResult DeleteScannedCvFile(DeleteScannedCvFileCommand command)
        {
            return _cvFilesScannerDBFacade.DeleteScannedCvFile(command);
        }
    }
}
