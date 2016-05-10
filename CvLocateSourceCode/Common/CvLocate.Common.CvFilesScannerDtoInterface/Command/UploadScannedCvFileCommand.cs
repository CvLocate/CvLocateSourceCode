using CvLocate.Common.CommonDto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CvFilesScannerDtoInterface.Command
{
    public class UploadScannedCvFileCommand : BaseCommand
    {
        public string CvFileId { get; set; }
        public byte[] Stream { get; set; }
        public string Text { get; set; }
        public byte[] FileImage { get; set; }
        public TextEncoding FileEncoding { get; set; }
    }
}
