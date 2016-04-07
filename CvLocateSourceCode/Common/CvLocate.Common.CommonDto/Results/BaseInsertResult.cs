using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CommonDto.Results
{
    public class BaseInsertResult
    {
        public string Id { get; set; }
        public bool Inserted { get; set; }

        public BaseInsertResult(bool inserted)
        {
            Inserted = inserted;
        }
    }
}
