using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.Common.EndUserDtoInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface
{
    public class Recruiter
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }
        public string CompanyName { get; set; }
        public RecruiterSourceType SourceType { get; set; }
        public string Source { get; set; }
        public RecruiterRegisterStatus RegisterStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory> RegisterStatusHistory { get; set; }
    }
}
