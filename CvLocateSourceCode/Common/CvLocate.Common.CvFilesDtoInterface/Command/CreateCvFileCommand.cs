using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CvFilesDtoInterface.Command
{
    public class CreateCvFileCommand 
    {
        public FileType Extension { get; set; }
        public CvSourceType SourceType { get; set; }
        public string Source { get; set; }
    }
}
