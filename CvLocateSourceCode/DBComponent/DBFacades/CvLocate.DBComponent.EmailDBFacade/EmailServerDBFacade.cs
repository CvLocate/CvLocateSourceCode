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

       

        #endregion

        #region Ctor

        public EmailServerDBFacade()
        {
        }

        #endregion

        #region Public Methods

       

        #endregion





        public Common.EmailServerDtoInterface.Result.GetEmailServerConfigurationResult GetEmailServerConfiguration()
        {
            throw new NotImplementedException();
        }

        List<Common.CommonDto.Enums.FileType> IEmailServerDBFacade.GetSupportedFileTypes()
        {
            throw new NotImplementedException();
        }
    }
}
