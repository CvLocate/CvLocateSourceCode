using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.DbInterface;
using CvLocate.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBFacade
{
    public class EmailServerDBFacade : IEmailServerDBFacade
    {
        public BaseInsertResult UploadCvFile(UploadCvFileCommand command)
        {
            if (command == null ||
                command.Stream == null ||
                string.IsNullOrEmpty(command.FileName))
                return new BaseInsertResult(false);
            ICvFilesManager manager = CvFilesManager.Instance;
            return manager.UploadCvFile(command);
        }
    }
}
