using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveCvFileCommand:BaseCommonCommand
    {
        public CvFile CvFile { get; set; }

        public override string ToString()
        {
            string result = "Save CV file with the following details:\n";
            result += this.CvFile.ToString();
            return result;
        }
    }
}
