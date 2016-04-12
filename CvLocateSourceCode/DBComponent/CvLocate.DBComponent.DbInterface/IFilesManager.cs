using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface
{
    public interface IFilesManager
    {
        /// <summary>
        /// Upload new file
        /// </summary>
        /// <param name="fileStream">Stream of file</param>
        /// <returns>New file id</returns>
        string UploadFile(byte[] fileStream);
    }
}
