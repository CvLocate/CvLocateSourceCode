using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Website.Bl;
using CvLocate.Website.Bl.Interfaces;
using CvLocate.Website.Bl.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CvLocate.Website.Conrollers
{
    public class UsersController : ApiController
    {
        IUserBl _userBl;
        public UsersController()
        {
            this._userBl = Bootstrapper.Instance.Container.GetInstance<IUserBl>();
        }

        [HttpPost]
        [ActionName("signUp")]
        public SignupResponse SignUp(SignUpCommand command)
        {
           return  this._userBl.SignUp(command);
        }
    }
}
