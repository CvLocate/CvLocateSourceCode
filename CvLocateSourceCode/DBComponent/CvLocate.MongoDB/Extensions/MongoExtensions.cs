using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB
{
    public class MongoExtensions
    {
        #region Singletone Implementation

        private static MongoExtensions _instance;
        public static MongoExtensions Instance
        {
            get { return _instance ?? (_instance = new MongoExtensions()); }
        }

        private MongoExtensions()
        {
        }

        #endregion

        #region Public Methods

        public string UploadFile(byte[] fileBytes, string fileName)
        {
            if (fileBytes == null || string.IsNullOrEmpty(fileName))
                return null;
            var database = GetFilesDatabase();

            //TODO throw exception with error type
            if (database.GridFS.Exists(fileName))
                return null;

            Stream stream = new MemoryStream(fileBytes);

            var gridFsInfo = database.GridFS.Upload(stream, fileName);
            return gridFsInfo.Name;
        }

        public byte[] DownloadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            var database = GetFilesDatabase();
            var file = database.GridFS.FindOne(fileName);
            if (file == null)
                return null;
            using (var stream = file.OpenRead())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                return bytes;
            }
        }

        public void RemoveFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            var database = GetFilesDatabase();
            database.GridFS.Delete(fileName);
        }

        #endregion

        #region Private Methods

        private MongoDatabase GetFilesDatabase()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
            MongoClient client = new MongoClient(connection);
            var server = client.GetServer();

            if (server.DatabaseExists("CvLocateFilesDB"))
                return server.GetDatabase("CvLocateFilesDB");

            MongoDatabase db = new MongoDatabase(server, "CvLocateFilesDB", new MongoDatabaseSettings());
            return db;
        }

        #endregion
    }
}
