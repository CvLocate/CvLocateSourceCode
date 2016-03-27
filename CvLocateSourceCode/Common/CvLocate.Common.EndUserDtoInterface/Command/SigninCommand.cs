using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class SigninCommand : BaseCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
