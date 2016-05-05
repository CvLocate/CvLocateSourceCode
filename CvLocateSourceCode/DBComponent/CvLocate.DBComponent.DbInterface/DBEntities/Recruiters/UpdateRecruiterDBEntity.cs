using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.DBEntities.Recruiters
{
    public class UpdateRecruiterDBEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }
        public string CompanyName { get; set; }
    }
}
