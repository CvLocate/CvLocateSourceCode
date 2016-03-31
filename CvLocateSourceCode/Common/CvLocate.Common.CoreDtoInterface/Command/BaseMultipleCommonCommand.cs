using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.CoreDtoInterface.Command
{
    public class BaseMultipleCommonCommand:BaseCommonCommand
    {
        public IList<BaseCommonCommand> Commands { get; set; }

        public BaseMultipleCommonCommand()
        {
            Commands = new List<BaseCommonCommand>();
        }
    }
}
