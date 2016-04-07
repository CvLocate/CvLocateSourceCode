using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class CvFileForParsing : CvFile
    {
        public byte[] Stream { get; set; }

        public string Extension { get; set; }

        public override string ToString()
        {
            string result = base.ToString();
            result += string.Format(" Extension: {0}", Extension);
            return result;
        }

    }
}
