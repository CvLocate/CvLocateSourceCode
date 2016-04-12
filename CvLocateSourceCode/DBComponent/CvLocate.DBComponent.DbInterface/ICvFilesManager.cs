using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.CommonDto.Results;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Enums;

namespace CvLocate.DBComponent.DbInterface
{
    public interface ICvFilesManager
    {
        /// <summary>
        /// Update existing file to uploaded state
        /// </summary>
        /// <param name="cvFileId">CvFile id</param>
        /// <param name="fileId">File id from Files table</param>
        void UpdateCvFileUploaded(string cvFileId, string fileId);

        /// <summary>
        /// Check if CvFile exists
        /// </summary>
        /// <param name="cvFileId">CvFile id</param>
        /// <returns>If exists</returns>
        bool CvFileExists(string cvFileId);

        /// <summary>
        /// Update existing file to deleted state
        /// </summary>
        /// <param name="cvFileId">existing cvFile id</param>
        /// <param name="statusReason">status reason</param>
        /// <param name="statusReasonDetails">status reason details</param>
        void UpdateCvFileDeleted(string cvFileId, CvStatusReason statusReason, string statusReasonDetails);

        /// <summary>
        /// Create new record in CvFiles table
        /// </summary>
        /// <param name="extension">Extension type</param>
        /// <param name="sourceType">Source type</param>
        /// <param name="source">Source string</param>
        /// <returns>New record id</returns>
        string CreateCvFile(FileType extension, CvSourceType sourceType, string source);
    }
}
