using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentConverter
{
    public interface IDocumentConverter
    {
        string ConvertFileToText(string filePath);
    }
}
