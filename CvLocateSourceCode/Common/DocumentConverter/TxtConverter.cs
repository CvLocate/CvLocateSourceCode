using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter
{
    public class TxtConverter : IDocumentConverter
    {
        const int HEBREW_ENCODING = 1255;
        public string ConvertFileToText(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.GetEncoding(HEBREW_ENCODING));
        }
    }
}
