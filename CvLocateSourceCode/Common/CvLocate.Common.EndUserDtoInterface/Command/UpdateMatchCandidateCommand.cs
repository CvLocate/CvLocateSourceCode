using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class UpdateMatchCandidateCommand : BaseRecruiterCommand
    {
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }

        public UpdateMatchCandidateCommand(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
