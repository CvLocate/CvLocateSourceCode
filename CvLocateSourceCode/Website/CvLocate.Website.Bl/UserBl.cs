using AutoMapper;
using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Common.Logging;
using CvLocate.Website.Bl.Common;
using CvLocate.Website.Bl.Interfaces;
using CvLocate.Website.Bl.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CvLocate.Website.Bl
{
    public class UserBl : IUserBl
    {
        IUserDataWrapper _dataWrapper;
        ICvLocateLogger _logger;

        public UserBl(IUserDataWrapper dataWrapper,ICvLocateLogger logger)
        {
            this._dataWrapper = dataWrapper;
            this._logger = logger;
        }
        public SignupResponse SignUp(SignUpCommand command)
        {
            SignupResponse result = new SignupResponse();
            result.UserType = command.UserType;

           SignResponse response = this._dataWrapper.SignUp(command);

           if (response.Error != null)
           {
               _logger.WarnFormat("Failed to signup with following details: {0}. Error Message: {1}", command.ToString(), response.ErrorMessage);
               result.Error = response.Error;
           }
           else
           {
               _logger.InfoFormat("Signup with following details: {0}", command.ToString());
               result.SessionId = SessionManager.Instance.CreateSession(response.UserId,response.UserType,command.Email);
           }

           //SignupResponse result = Mapper.Map<SignResponse, SignupResponse>(response);
           return result;

        }

       
    }
}
