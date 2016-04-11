using CvLocate.Common.Logging;
using CvLocate.CvFilesScanner.Entities;
using CvLocate.CvFilesScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class CvFilesListener : ICvFilesFilesListener
    {
        #region Members

        ICvLocateLogger _logger;
        List<DirectoryInfo> _sourceFolders;
        List<FileSystemWatcher> _watchers;

        #endregion

        #region CTOR

        public CvFilesListener(ICvLocateLogger logger)
        {
            this._logger = logger;
        }

        #endregion

        #region ICvFilesFilesListener Members

        public event EventHandler<FileCreatedEventArgs> OnNewFileCreated;

        public void Initialize()
        {
            this._logger.Info("Initialize CV Files Listener");

            FindSourceFolders();

            StartWatchers();

            NotifyAboutExistingFiles();
        }

        public void Stop()
        {
            this._logger.Info("Stop CV Files Listener");

            StopWatchers();
        }




        #endregion

        #region Private Methods

        private void NotifyAboutExistingFiles()
        {
            if (this._sourceFolders == null)
                return;

            _logger.Info("Start find existing files");

            foreach (var folder in this._sourceFolders)
            {
                NotifyAboutExistingFiles(folder);
            }
        }

        private void NotifyAboutExistingFiles(DirectoryInfo sourceFolder)
        {
            FileInfo[] files = sourceFolder.GetFiles("*", SearchOption.TopDirectoryOnly);

            if (files.Count() == 0)
                return;

            this._logger.InfoFormat("{0} files found in folder {1}", files.Count(), sourceFolder.FullName);
            foreach (var file in files)
            {
                RaiseOnCreatedFileEvent(file.FullName);
            }
        }

        private void RaiseOnCreatedFileEvent(string filePath)
        {
            if (this.OnNewFileCreated != null)
                this.OnNewFileCreated(this, new FileCreatedEventArgs(filePath));
        }

        private void StopWatchers()
        {
            if (_watchers == null)
                return;

            _logger.Info("Stop watchers");

            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
            _watchers = null;
        }

        private void StartWatchers()
        {

            if (_sourceFolders == null)
                return;

            _logger.Info("Start watchers");

            _watchers = new List<FileSystemWatcher>();

            foreach (var folder in _sourceFolders)
            {
                _watchers.Add(StartFolderWatcher(folder.FullName));
            }

        }

        private List<DirectoryInfo> GetFolders(DirectoryInfo folder)
        {
            List<DirectoryInfo> subFolders = new List<DirectoryInfo>() { folder };
            foreach (var subFolder in folder.GetDirectories())
            {
                subFolders.AddRange(GetFolders(subFolder));
            }
            return subFolders;
        }

        private void FindSourceFolders()
        {
            string sourceFolder = Properties.Settings.Default.CvFilesDirectory;
            if (string.IsNullOrWhiteSpace(sourceFolder))
            {
                _logger.Error("Source folder isn't defined in configuration file (CvFilesDirectory setting)");
                return;
            }
            else if (!Directory.Exists(sourceFolder))
            {
                _logger.ErrorFormat("Source folder {0} doesn't exist", sourceFolder);
                return;
            }

            this._sourceFolders = new List<DirectoryInfo>();
            this._sourceFolders.AddRange(GetFolders(new DirectoryInfo(sourceFolder)));

            _logger.InfoFormat("{0} folders are found for CV files listening", this._sourceFolders.Count);

        }

        private FileSystemWatcher StartFolderWatcher(string folderPath)
        {
            _logger.InfoFormat("start watch on folder {0}", folderPath);
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = folderPath;
            watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;

            watcher.Created += watcher_Created;

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            return watcher;
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.ChangeType == WatcherChangeTypes.Created)
                {
                    RaiseOnCreatedFileEvent(e.FullPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }


        #endregion
    }
}
