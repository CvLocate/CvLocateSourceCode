using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class MongoEntityNotFoundException : Exception
    {
        public MongoEntityNotFoundException(string entityId, string collectionName) :
            base("MongoEntity with id [" + entityId + "] not found in " + collectionName + " collection")
        {

        }
    }
}
