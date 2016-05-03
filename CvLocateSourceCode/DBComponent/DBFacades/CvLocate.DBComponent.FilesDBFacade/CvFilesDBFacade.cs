using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.CvFilesDtoInterface.Command;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.MongoDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.CvFilesDBFacade
{
    public class CvFilesDBFacade:ICvFilesDBFacade
    {
        private ICvFilesManager _cvFilesManager;

        public CvFilesDBFacade()
        {
            _cvFilesManager = CvFilesManager.Instance;
        }
        public BaseInsertResult CreateCvFile(CreateCvFileCommand command)
        {
            try
            {
                if (command == null)
                    return new BaseInsertResult(false) { ErrorMessage = "Command cannot be null" };

                string newId = _cvFilesManager.CreateCvFile(command.Extension, command.SourceType, command.Source);
                return new BaseInsertResult(true) { Id = newId };
            }
            catch (Exception ex)
            {
                return new BaseInsertResult(false) { ErrorMessage = "Failed too create new cv file. Orginal error: " + ex.ToString() };
            }
        }
    }
}
