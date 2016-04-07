using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EmailServerDtoInterface.Command
{
    public class UploadCvFileCommand : BaseEmailServerCommand
    {
        public byte[] Stream { get; set; }
        public string Extension { get; set; }
        public CvSourceType SourceType { get; set; }
        public string Source { get; set; }
        //unique field, maybe join email with firstName or id or ...
        public string FileName { get; set; }
    }
}
