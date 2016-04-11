using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.Logging;

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
            ICvLocateLogger parsingEngineLogger = new CvLocateLogger("parsingEngineLogger");
            ICvParser cvParser = new MockCvParser(parsingEngineLogger);
            ICoreDBFacade coreDbFacade = new MockCoreDBFacade();
            IParsingEngineDataWrapper dataWrapper = new ParsingEngineDataWrapper(coreDbFacade);
            IParsingQueueManager parsingQueueManager = new ParsingQueueManager(dataWrapper);

            ParsingEngineManager parsingManager = new ParsingEngineManager(dataWrapper, parsingQueueManager, cvParser, parsingEngineLogger);
            parsingManager.Initialize();
        }
    }
}
