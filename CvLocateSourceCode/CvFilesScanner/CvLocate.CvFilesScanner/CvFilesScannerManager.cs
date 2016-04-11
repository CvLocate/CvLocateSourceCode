using CvLocate.Common.Logging;
using CvLocate.CvFilesScanner.Entities;
using CvLocate.CvFilesScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class CvFilesScannerManager : ICvFilesScannerManager
    {
        #region Members

        ICvLocateLogger _logger;
        IScannerDataWrapper _dataWrapper;
        ICvFilesFilesListener _cvFilesListener;

        #endregion

        #region CTOR

        public CvFilesScannerManager(ICvFilesFilesListener cvFilesListener, IScannerDataWrapper dataWrapper, ICvLocateLogger logger)
        {
            this._logger = logger;
            this._dataWrapper = dataWrapper;
            this._cvFilesListener = cvFilesListener;
        }

        #endregion



        #region ICvFilesScannerManager Implementation

        public void Initialize()
        {
            this._logger.Info("Initilaize CV files listener");

            _cvFilesListener.OnNewFileCreated += _cvFilesListener_OnNewFileCreated;
            _cvFilesListener.Initialize();
        }

        void _cvFilesListener_OnNewFileCreated(object sender, FileCreatedEventArgs e)
        {
            try
            {
                NewCVFileCreated(e.FilePath);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex.ToString()); 
            }
        }

        private void NewCVFileCreated(string filePath)
        {
            
        }

        public void Stop()
        {
            this._logger.Info("Stop CV files listener");
            if (this._cvFilesListener != null)
            {
                this._cvFilesListener.Stop();
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
