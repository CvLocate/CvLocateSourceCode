using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentConverter
{
    public class DocConverter : IDocumentConverter
    {

        public string ConvertFileToText(string filePath)
        {
            StringBuilder result = new StringBuilder();
            new FilterLibrary.FilterCode().GetTextFromDocument(filePath, ref result);
            return result.ToString();

        }
    }
}
