using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class BaseRecruiterCommand : BaseCommand
    {
        public string Id { get; set; }
        public BaseRecruiterCommand(string recruiterId)
        {
            Id = recruiterId;
        }
    }
}
