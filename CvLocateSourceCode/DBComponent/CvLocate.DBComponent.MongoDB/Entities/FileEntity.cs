using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.MongoDB.Entities
{
    [CollectionName("Files")]
    public class FileEntity : Entity
    {
        public byte[] File { get; set; }
    }
}
