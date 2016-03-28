using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class ChangeMatchingStatusCommand : BaseRecruiterCommand
    {
        public string MatchingId { get; set; }
        public RecruiterMatchingStatus NewMatchingStatus { get; set; }

        public ChangeMatchingStatusCommand(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
