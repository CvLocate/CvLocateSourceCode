using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CvLocate.Common.CommonDto;
using System.Threading.Tasks;

namespace CvLocate.Common.CvFilesScannerDtoInterface.Command
{
    public class DeleteScannedCvFileCommand:BaseCommand
    {
        public string CvFileId { get; set; }
        public CvStatusReason StatusReason { get; set; }
        public string StatusReasonDetails { get; set; }
    }
}
