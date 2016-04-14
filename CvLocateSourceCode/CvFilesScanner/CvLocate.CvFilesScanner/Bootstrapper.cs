using CvLocate.Common.Logging;
using CvLocate.CvFilesScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class Bootstrapper
    {
        ICvFilesScannerManager _cvFilesScannerManager;
        
        public void Initialize()
        {
            ICvLocateLogger _logger = new CvLocateLogger("scannerEngineLogger");
            ICvFilesFilesListener cvFilesFilesListener = new CvFilesListener(_logger);
            IScannerDataWrapper dataWrapper = new ScannerDataWrapper();
            IDocumentConverterFactory documentConverterFactory = new DocumentConverterFactory();
            ICvFileScanner cvFileScanner = new CvFileScanner(documentConverterFactory);
            this._cvFilesScannerManager = new CvFilesScannerManager(cvFilesFilesListener, dataWrapper, _logger, cvFileScanner);
           this._cvFilesScannerManager.Initialize();
        }
        public void Stop()
        {
            this._cvFilesScannerManager.Stop();
        }
    }
}
