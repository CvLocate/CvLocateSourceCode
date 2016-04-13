using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.DBComponent.DbInterface;
using CvLocate.DBComponent.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.EmailServerDBFacade
{
    public class EmailServerDBFacade : IEmailServerDBFacade
    {
        public BaseInsertResult CreateCvFile(CreateCvFileCommand command)
        {
            if (command == null)
                return new BaseInsertResult(false) { ErrorMessage = "Command cannot be null" };
            
            ICvFilesManager cvFilesManager = CvFilesManager.Instance;
            string newId = cvFilesManager.CreateCvFile(command.Extension, command.SourceType, command.Source);
            return new BaseInsertResult(true) { Id = newId };
        }
    }
}
