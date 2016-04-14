using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Entities;
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
        public string Text { get; set; }
        public TextEncoding Encoding { get; set; }
        public byte[] FileImage { get; set; }

        public override string ToString()
        {
            string result = base.ToString();
            result += string.Format("\nEncoding: {0}, Text: {1}",this.Encoding, this.Text);
            return result;
        }

    }
}
