using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EmailServerDtoInterface.Command
{
    public class CreateCvFileCommand : BaseEmailServerCommand
    {
        public FileType Extension { get; set; }
        public CvSourceType SourceType { get; set; }
        public string Source { get; set; }
    }
}
