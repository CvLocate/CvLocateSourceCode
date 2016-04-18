using CvLocate.Common.EndUserDTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.Response
{
    public class BaseWebsiteResponse
    {
        public EndUserError? Error { get; set; }
    }
}
