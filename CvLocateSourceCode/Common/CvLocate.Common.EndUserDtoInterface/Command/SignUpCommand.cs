using CvLocate.Common.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.EndUserDtoInterface.Command
{
    public class SignUpCommand : BaseCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        public override string ToString()
        {
            return string.Format("Email: {0}, Password: {1}, UserType: {2}",this.Email,this.Password,this.UserType);
        }
    }

   
}
