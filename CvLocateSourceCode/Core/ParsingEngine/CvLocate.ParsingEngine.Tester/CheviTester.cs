using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.Logging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine.Tester
{
    static class CheviTester
    {
        public static void TestParsingEngineManager()
        {
            var container = new Container();
            container.RegisterSingleton<ICvLocateLogger>(new CvLocateLogger("parsingEngineLogger"));
            container.Register<ICoreDBFacade, MockCoreDBFacade>(Lifestyle.Singleton);
            container.Register<IParsingEngineDataWrapper, ParsingEngineDataWrapper>(Lifestyle.Singleton);
            container.Register<IParsingQueueManager, ParsingQueueManager>(Lifestyle.Singleton);
            container.Register<ICvParser, MockCvParser>(Lifestyle.Transient);
            container.Register<IParsingEngineManager, ParsingEngineManager>(Lifestyle.Singleton);

            IParsingEngineManager parsingManager = container.GetInstance<IParsingEngineManager>();
            parsingManager.Initialize();
        }
    }
}
