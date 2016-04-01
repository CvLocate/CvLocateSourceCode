using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.Logging;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
#if ZVIKA
            ZvikaTester.TestCvParser();
#endif

#if CHEVI
            CheviTester.TestParsingEngineManager();
#endif

            Console.ReadLine();
        }
    }
  
}
