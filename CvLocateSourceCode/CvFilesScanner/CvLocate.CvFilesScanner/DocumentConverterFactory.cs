using CvLocate.Common.CommonDto.Enums;
using CvLocate.CvFilesScanner.Interfaces;
using DocumentConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class DocumentConverterFactory : IDocumentConverterFactory
    {
        public IDocumentConverter GetDocumentConverter(FileType fileType)
        {
            IDocumentConverter documentConverter = null;
            switch (fileType)
            {
                case FileType.Docx:
                    documentConverter= new DocxConverter();
                    break;
                case FileType.Doc:
                    documentConverter= new DocConverter();
                    break;
                case FileType.Pdf:
                    documentConverter= new PdfConverter();
                    break;
                case FileType.Rtf:
                    documentConverter= new RtfConverter();
                    break;
                case FileType.Txt:
                    documentConverter= new TxtConverter();
                    break;
            }
            return documentConverter;
        }
    }
}
