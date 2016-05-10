using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.CvFilesDtoInterface.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.DbFacadeInterface
{
    public interface ICvFilesDBFacade
    {
        BaseInsertResult CreateCvFile(CreateCvFileCommand command);
    }
}
