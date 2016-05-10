using CvLocate.Common.CommonDto;
using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.Utils;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveParsedCvFileCommand:BaseCommonCommand
    {
        public CvFile CvFile { get; set; }
        public List<string> SeperatedTexts { get; set; }

        public SaveParsedCvFileCommand()
        {
            SeperatedTexts = new List<string>();
        }

        public override string ToString()
        {
            string result = "Save parsed CV file with the following details:\n";
            result += this.CvFile.ToString();
            result += string.Format("\nSeperatedTexts:{0}", this.SeperatedTexts.StringsListToString());
            return result;
        }
    }
}
