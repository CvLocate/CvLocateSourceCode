using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface
{
    public class JobHeader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
    }
}
