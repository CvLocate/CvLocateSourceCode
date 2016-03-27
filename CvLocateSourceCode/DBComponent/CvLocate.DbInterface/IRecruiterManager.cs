﻿using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DbInterface
{
    public interface IRecruiterManager
    {
        SignResponse SignUp(SignUpCommand command);
    }
}
