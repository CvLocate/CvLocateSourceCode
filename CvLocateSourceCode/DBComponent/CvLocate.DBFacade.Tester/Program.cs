using CvLocate.Common.CommonDto;
using CvLocate.Common.EndUserDtoInterface.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBFacade.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDBFacade dbFacade = new UserDBFacade();
            dbFacade.SignUp(new SignUpCommand() { Email = "rachelifff@gmail.com", Password = "1234567", UserType = UserType.Recruiter });
            Console.ReadKey();
        }
    }
}
