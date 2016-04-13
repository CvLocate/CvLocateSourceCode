using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class RequiredFieldsNullOrEmptyException : BaseMongoException
    {
        public RequiredFieldsNullOrEmptyException(string[] fieldsNames) :
            base("Required fields (" + string.Join(",", fieldsNames) + ") are null or empty")
        {

        }
    }
}
