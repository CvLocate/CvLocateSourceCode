using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.DBComponent.DbInterface;
using CvLocate.DBComponent.DbInterface.Managers;
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
        #region Members

        private ICvFilesManager _cvFilesManager;

        #endregion

        #region Ctor

        public EmailServerDBFacade()
        {
            _cvFilesManager = CvFilesManager.Instance;
        }

        #endregion

        #region Public Methods

        public BaseInsertResult CreateCvFile(CreateCvFileCommand command)
        {
            if (command == null)
                return new BaseInsertResult(false) { ErrorMessage = "Command cannot be null" };
            
            string newId = _cvFilesManager.CreateCvFile(command.Extension, command.SourceType, command.Source);
            return new BaseInsertResult(true) { Id = newId };
        }

        #endregion
    }
}
