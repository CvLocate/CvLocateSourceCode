using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CommonDto
{
    public enum ParsingProcessStatus
    {
        WaitingForParsing,
        CannotDeciphered,
        Duplicate,
        Parsed
    }
}
