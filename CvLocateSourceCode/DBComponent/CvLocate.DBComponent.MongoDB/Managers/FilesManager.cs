using CvLocate.DBComponent.DbInterface.Managers;
using CvLocate.DBComponent.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.MongoDB.Managers
{
    public class FilesManager : IFilesManager
    {
        #region Members

        private MongoRepository<FileEntity> _filesRepository;

        #endregion

        #region Singletone Implementation

        private static FilesManager _instance;
        public static FilesManager Instance
        {
            get { return _instance ?? (_instance = new FilesManager()); }
        }

        private FilesManager()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["MongoCvFilesDBSettings"].ConnectionString;
            _filesRepository = new MongoRepository<FileEntity>(connection);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Upload new file
        /// </summary>
        /// <param name="fileStream">Stream of file</param>
        /// <returns>New file id</returns>
        public string UploadFile(byte[] fileStream)
        {
            FileEntity file = new FileEntity();
            file.File = fileStream;
            file = _filesRepository.Add(file);
            return file.Id;
        }

        #endregion

    }
}
