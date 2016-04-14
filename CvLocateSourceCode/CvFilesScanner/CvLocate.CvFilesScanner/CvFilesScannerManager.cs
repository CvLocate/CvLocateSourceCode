using CvLocate.Common.CommonDto.Enums;
using CvLocate.Common.Logging;
using CvLocate.CvFilesScanner.Entities;
using CvLocate.CvFilesScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.Utils;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using System.IO;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.CommonDto;

namespace CvLocate.CvFilesScanner
{
    public class CvFilesScannerManager : ICvFilesScannerManager
    {
        #region Members

        ICvLocateLogger _logger;
        IScannerDataWrapper _dataWrapper;
        ICvFilesFilesListener _cvFilesListener;
        ICvFileScanner _cvFileScanner;

        List<FileType> _supportedFileTypes;
        #endregion

        #region CTOR

        public CvFilesScannerManager(ICvFilesFilesListener cvFilesListener, IScannerDataWrapper dataWrapper,
            ICvLocateLogger logger, ICvFileScanner cvFileScanner)
        {
            this._logger = logger;
            this._dataWrapper = dataWrapper;
            this._cvFilesListener = cvFilesListener;
            this._cvFileScanner = cvFileScanner;
        }

        #endregion



        #region ICvFilesScannerManager Implementation

        public void Initialize()
        {
            this._logger.Info("Initilaize CV Files Scanner Manager");

            _supportedFileTypes = _dataWrapper.GetSupportedFileTypes().ToList();
            _cvFilesListener.OnNewFileCreated += _cvFilesListener_OnNewFileCreated;
            _cvFilesListener.Initialize();
        }

        public void Stop()
        {
            this._logger.Info("Stop CV Files Scanner Manager");
            if (this._cvFilesListener != null)
            {
                this._cvFilesListener.Stop();
            }
        }



        #endregion

        #region Private Methods


        void _cvFilesListener_OnNewFileCreated(object sender, FileCreatedEventArgs e)
        {

            NewCVFileCreated(e.FilePath);

        }

        private void NewCVFileCreated(string filePath)
        {
            try
            {
                _logger.DebugFormat("New file is found in scanner server: {0}", filePath);
                FileType? fileType = filePath.GetFileType();
                if (!IsFileTypeSupported(filePath, fileType))
                {
                    return;
                }

                ScanResult scanResult = _cvFileScanner.Scan(filePath, (FileType)fileType);
                if (scanResult.Succeed)
                {
                    ScanSucceed(filePath, scanResult);
                }
                else
                {
                    ScanFailed(filePath, scanResult);
                }

            }
            catch (Exception ex)
            {
                this._logger.ErrorFormat("File {0}: Failed in scanner process. Original Error: {1}", filePath, ex.ToString());
            }
        }

        private void ScanFailed(string filePath, ScanResult scanResult)
        {
            this._logger.DebugFormat("CV file {0}: scanning process is failed with reason: {1}, so delete this file record from DB", filePath, scanResult.ErrorMessage);
            DeleteScannedCvFileCommand command = new DeleteScannedCvFileCommand()
            {
                CvFileId = Path.GetFileNameWithoutExtension(filePath),
                StatusReasonDetails = scanResult.ErrorMessage,
                StatusReason = scanResult.IsSafeFile == false ? CvStatusReason.NotSafeFile : CvStatusReason.ScanningFailed
            };
            BaseResult result = _dataWrapper.DeleteScannedCvFile(command);
            string moveFileTo = null;
            if (result.Success)
            {
                this._logger.DebugFormat("CV file {0} is deleted from DB successfuly", filePath);
                if (scanResult.IsSafeFile)
                    moveFileTo = Properties.Settings.Default.ArchiveDirectoryForFailedScanFiles;
                else
                    moveFileTo = Properties.Settings.Default.ArchiveDirectoryForUnsafeFiles;
            }
            else
            {
                this._logger.ErrorFormat("CV file {0} is failed to delete from DB. Error message: {1}", filePath, result.ErrorMessage);
                moveFileTo = Properties.Settings.Default.ArchiveDirectoryForFailedDeletedFiles;
            }
            MoveFile(filePath, moveFileTo);
        }



        private void ScanSucceed(string filePath, ScanResult scanResult)
        {
            this._logger.DebugFormat("CV file {0}: scanning process is finished successfuly, so upload the file to DB.", filePath);
            UploadScannedCvFileCommand command = new UploadScannedCvFileCommand()
            {
                CvFileId = Path.GetFileNameWithoutExtension(filePath),
                FileEncoding = scanResult.Encoding,
                FileImage = scanResult.Image,
                Stream = scanResult.Stream,
                Text = scanResult.Text
            };

            BaseResult result = _dataWrapper.UploadScannedCvFile(command);

            if (result.Success)
            {
                this._logger.DebugFormat("CV file {0} is uploaded successfuly", filePath);
                DeleteFile(filePath);
            }
            else
            {
                this._logger.ErrorFormat("CV file {0} is failed to upload. Error message: {1}", filePath, result.ErrorMessage);
                MoveFile(filePath, Properties.Settings.Default.ArchiveDirectoryForFailedUploadFiles);
            }

        }

        private void MoveFile(string filePath, string targetPath)
        {
            try
            {
                string targetFullPath=Path.Combine(targetPath,Path.GetFileName(filePath));
                File.Move(filePath,targetFullPath );
                if (File.Exists(targetFullPath))
                {
                    this._logger.DebugFormat("File {0} is moved to {1}", filePath, targetPath);
                }
                else
                {
                    this._logger.ErrorFormat("File {0} is failed to moved to {1}", filePath, targetPath);
                }
            }
            catch (Exception ex)
            {
                this._logger.ErrorFormat("File {0} failed to be moved to {1}. Origional error: {2}", filePath, targetPath, ex.Message);
            }
        }
        private void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                this._logger.DebugFormat("File {0} is deleted successfuly", filePath);
            }
            catch (Exception ex)
            {
                this._logger.ErrorFormat("File {0} failed to be deleted. Origional error: {1}", filePath, ex.Message);
            }
        }

        private bool IsFileTypeSupported(string filePath, FileType? fileType)
        {
            if (fileType == null || !_supportedFileTypes.Contains((FileType)fileType))
            {
                this._logger.WarnFormat("File {0} is of type that isn't supported by system", filePath);
                MoveFile(filePath, Properties.Settings.Default.ArchiveDirectoryForUnsupportedFiles);
                return false;
            }
            return true;
        }
        #endregion
    }
}
