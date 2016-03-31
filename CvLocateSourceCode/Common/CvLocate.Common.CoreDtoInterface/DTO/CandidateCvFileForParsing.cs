using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.DTO
{
    public class CandidateCvFileForParsing
    {
        public CvFileForParsing CvFile { get; set; }
        public Candidate Candidate { get; set; }
    }
}
