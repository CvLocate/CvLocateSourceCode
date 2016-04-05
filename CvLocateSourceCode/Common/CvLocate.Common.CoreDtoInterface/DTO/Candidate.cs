using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;
using System.IO;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class Candidate
    {
        public string Id { get; set; }
        public long CandidateNum { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CVFileId { get; set; }
        public MatchingProcessStatus MatchingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            string result = string.Format("Id: {0}, CandidateNum: {1}, Email: {2}, Name: {3}, CvFileId: {4}, MatchingStatus: {5}, CreatedDate: {6}, UpdatedAt: {7}",
                Id,CandidateNum,Email,Name,CVFileId,MatchingStatus,CreatedAt,UpdatedAt);
            return result;
        }
    }
}
