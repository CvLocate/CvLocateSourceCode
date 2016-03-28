using CvLocate.Common.CommonDto;

namespace CvLocate.Common.EndUserDtoInterface
{
    public class MatchingHeader
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public string CandidateId { get; set; }
        public int Mark { get; set; }
        public RecruiterMatchingStatus MatchingStatus { get; set; }
    }
}
