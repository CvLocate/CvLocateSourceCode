using CvLocate.Common.CommonDto.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.Utils
{
    public static class Extentions
    {
        public static string StringsListToString(this List<string> strings)
        {
            string result = string.Empty;
            foreach (var str in strings)
            {
                result =result + "," + str;
            }
            return result.TrimStart(',');
        }
        public static FileType? GetFileType(this FileInfo file)
        {
            FileType? fileType = null;
            switch (file.Extension.ToLower())
            {
                case ".docx":
                    fileType = FileType.Docx;
                    break;
                case ".doc":
                    fileType = FileType.Doc;
                    break;
                case ".pdf":
                    fileType = FileType.Pdf;
                    break;
                case ".txt":
                    fileType = FileType.Txt;
                    break;
                case ".rtf":
                    fileType = FileType.Rtf;
                    break;
            }
            return fileType;
        }
    }
}
