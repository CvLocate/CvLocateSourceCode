using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class BaseMongoException : Exception
    {
        public BaseMongoException(string message)
            : base(message)
        {

        }
    }
}
