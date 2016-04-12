using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class RequiredFieldsNullOrEmptyExcepstion : Exception
    {
        public RequiredFieldsNullOrEmptyExcepstion(string[] fieldNames) :
            base("Required fields (" + fieldNames + ") are null or empty")
        {
            
        }
    }
}
