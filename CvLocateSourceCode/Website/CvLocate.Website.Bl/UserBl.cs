using AutoMapper;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Common.Logging;
using CvLocate.Website.Bl.Interfaces;
using CvLocate.Website.Bl.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           SignResponse dataResponse = this._dataWrapper.SignUp(command);

           if (dataResponse.Error != null)
           {
               _logger.WarnFormat("Failed to signup with following details: {0}. Error Message: {1}", command.ToString(), dataResponse.ErrorMessage);
           }
           else
           {
               _logger.InfoFormat("Signup with following details: {0}", command.ToString());
           }

           SignupResponse response = Mapper.Map<SignResponse, SignupResponse>(dataResponse);
           return response;

        }
    }
}
