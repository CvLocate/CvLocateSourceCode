using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Entities
{
    public class FileCreatedEventArgs:EventArgs
    {
        public string FilePath { get; set; }

        public FileCreatedEventArgs(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
