using Microsoft.Office.DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DocumentConverter
{
    public class DocxConverter : IDocumentConverter
    {
        public string ConvertFileToText(string filePath)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
            {
                MainDocumentPart mPart = doc.MainDocumentPart;

                using (StreamReader reader = new StreamReader(mPart.GetStream()))
                {
                    //string content = reader.ReadToEnd();

                    XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

                    XDocument xDocument = XDocument.Load(XmlReader.Create(reader));

                    XName rPr = w + "pPr";
                    XName p = w + "p";

                    string[] content = (from element in xDocument.Descendants(p)
                                        select
                                            element.Value == string.Empty ? "\n" : element.Value).ToArray();


                    return string.Join("\n", content);
                }
            }
        }
    }
}

