using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.DTO;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CvLocate.DBComponent.MongoDB.Entities
{
    [CollectionName("Recruiters")]
    public class RecruiterEntity : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string ImageId { get; set; }
        public string CompanyName { get; set; }
        public RecruiterSourceType SourceType { get; set; }
        public string Source { get; set; }
        public RecruiterRegisterStatus RegisterStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BaseStatusHistory<RecruiterRegisterStatus>> RegisterStatusHistory { get; set; }
    }
}
