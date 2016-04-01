using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    public class CvParsedData
    {
        public string Text { get; set; }
        public List<string> SeperatedTexts { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
