using CvLocate.CvFilesScanner.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Interfaces
{
    public interface ICvFilesFilesListener
    {
        event EventHandler<FileCreatedEventArgs> OnNewFileCreated;
        void Initialize();
        void Stop();
    }
}
