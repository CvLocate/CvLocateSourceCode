using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Interfaces
{
    public interface ICvFilesScannerManager
    {
        void Initialize();
        void Stop();
    }
}
