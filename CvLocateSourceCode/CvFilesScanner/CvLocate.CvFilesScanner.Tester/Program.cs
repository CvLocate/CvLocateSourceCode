using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bootstrapper bootstrap = new Bootstrapper();
                bootstrap.Initialize();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
