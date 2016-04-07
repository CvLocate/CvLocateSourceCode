using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.EmailServerDtoInterface.Command;

namespace CvLocate.DbInterface
{
    public interface ICvFilesManager
    {
        GetTopCvFilesForParsingResult GetTopCandidatesForParsing();
        BaseInsertResult UploadCvFile(UploadCvFileCommand command);
    }
}
