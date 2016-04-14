using CvLocate.Common.CommonDto.Enums;
using CvLocate.CvFilesScanner.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Interfaces
{
    public interface ICvFileScanner
    {
        ScanResult Scan(string filePath, FileType fileType); 
    }
}
