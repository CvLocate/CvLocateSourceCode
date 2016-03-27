using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.DTO
{
    public class BaseStatusHistory
    {
        public Enum Status { get; set; }
        public DateTime Date { get; set; }
    }
}
