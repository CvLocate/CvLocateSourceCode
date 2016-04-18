using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Website.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.DataWrapper
{
    public class UserDataWrapper:IUserDataWrapper
    {
        private IUserDBFacade _userDbFacade;
        public UserDataWrapper(IUserDBFacade userDbFacade)
        {
            this._userDbFacade = userDbFacade;
        }

        public SignResponse SignUp(SignUpCommand command)
        {
            return this._userDbFacade.SignUp(command);
        }
    }
}
