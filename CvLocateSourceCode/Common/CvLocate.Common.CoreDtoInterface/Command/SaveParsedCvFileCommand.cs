using CvLocate.Common.CommonDto;
using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class SaveParsedCvFileCommand:BaseCommonCommand
    {
        public CvFile CvFile { get; set; }
        public string Text { get; set; }
        public List<string> SeperatedTexts { get; set; }
    }
}
