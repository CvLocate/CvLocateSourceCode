using CvLocate.Common.CommonDto.Enums;
using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.EmailServerDtoInterface.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.DbFacadeInterface
{
    public interface IEmailServerDBFacade
    {
        IList<FileType> GetSupportedFileTypes();

    }
}
