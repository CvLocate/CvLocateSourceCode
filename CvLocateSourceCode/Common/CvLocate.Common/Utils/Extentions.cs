using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.Utils
{
    public static class Extentions
    {
        public static string StringsListToString(this List<string> strings)
        {
            string result = string.Empty;
            foreach (var str in strings)
            {
                result =result + "," + str;
            }
            return result.TrimStart(',');
        }
    }
}
