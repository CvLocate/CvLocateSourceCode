using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Entities
{
    internal class ScanResult
    {
        public bool Succeed { get; set; }
        public bool IsSafeFile { get; set; }
        public string ErrorMessage { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public byte[] Stream { get; set; }
        public string Encoding { get; set; }
    }
}
