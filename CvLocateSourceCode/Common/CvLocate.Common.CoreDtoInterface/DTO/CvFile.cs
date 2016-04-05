using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class CvFile
    {
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public CandidateSourceType SourceType { get; set; }
        public string Source { get; set; }
        public CvFileStatus Status { get; set; }
        public CvStatusReason StatusReason { get; set; }
        public string StatusReasonDetails { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public CvFile()
        {

        }

        public CvFile(CvFile otherCvFile)
        {
            this.Id = otherCvFile.Id;
            this.CandidateId = otherCvFile.CandidateId;
            this.SourceType = otherCvFile.SourceType;
            this.Source = otherCvFile.Source;
            this.Status = otherCvFile.Status;
            this.StatusReason = otherCvFile.StatusReason;
            this.StatusReasonDetails = otherCvFile.StatusReasonDetails;
            this.ParsingStatus = otherCvFile.ParsingStatus;
            this.CreatedDate = otherCvFile.CreatedDate;
            this.UpdatedDate = otherCvFile.UpdatedDate;
        }
        public override string ToString()
        {
            string result = string.Format("Id: {0}, CandidateId:{1}, ParsingStatus: {2}, Status: {3}, StatusReason:{4}, StatusReasonDetails: {5},\n"+
            " CreatedDate: {6}, UpdatedDate: {7}, SourceType: {8}, Source: {9}",
            Id,CandidateId,ParsingStatus,Status,StatusReason,StatusReasonDetails,CreatedDate,UpdatedDate,SourceType,Source);
            return result;
        }
    }
}
