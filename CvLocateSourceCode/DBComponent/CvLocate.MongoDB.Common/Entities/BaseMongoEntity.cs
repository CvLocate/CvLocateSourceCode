using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB.Common.Entities
{
    public class BaseMongoEntity : Entity
    {
        public int FriendlyId { get; set; }
    }
}
