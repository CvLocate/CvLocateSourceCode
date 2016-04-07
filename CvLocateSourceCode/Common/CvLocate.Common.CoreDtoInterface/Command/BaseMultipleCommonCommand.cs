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

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var command in Commands)
            {
                result += string.Format("Command {0}: {1}\n",command.GetType().Name,command.ToString());
            }
            return result;
        }
    }
}
