using CvLocate.Common.DbFacadeInterface;
using CvLocate.Website.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl.DataWrapper
{
    public class RecruiterDataWrapper : IRecruiterDataWrapper
    {
        private IRecruiterDBFacade _recruiterDbFacade;
        public RecruiterDataWrapper(IRecruiterDBFacade recruiterDbFacade)
        {
            this._recruiterDbFacade = recruiterDbFacade;
        }

    }
}
