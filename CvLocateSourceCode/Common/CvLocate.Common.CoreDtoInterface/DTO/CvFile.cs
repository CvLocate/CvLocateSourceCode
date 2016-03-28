using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class CvFile
    {
        public string Id { get; set; }
        public byte[] Stream { get; set; }
        public string Extention { get; set; }
    }
}
