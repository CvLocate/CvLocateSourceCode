using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.Exceptions
{
    public class SignInException : BaseMongoException
    {
        public SignInException(string email)
            : base("Cannot find entity with email:" + email + " or password is uncorrect")
        {

        }
    }
}
