using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class UpdateRecruiterProfileCommand : BaseRecruiterCommand
    {
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public byte[] Image { get; set; }

        public UpdateRecruiterProfileCommand(string recruiterId)
            : base(recruiterId)
        {

        }
    }
}
