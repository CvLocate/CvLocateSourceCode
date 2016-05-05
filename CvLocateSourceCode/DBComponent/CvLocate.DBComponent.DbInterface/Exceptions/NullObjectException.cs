using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class NullObjectException : BaseMongoException
    {
        public NullObjectException(string objectType)
            : base("Object is null. Object type: " + objectType)
        {

        }
    }
}
