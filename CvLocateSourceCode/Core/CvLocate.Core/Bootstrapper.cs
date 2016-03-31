using CvLocate.Common.Logging;
using CvLocate.ParsingEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Core
{
    public class Bootstrapper
    {
        ParsingEngineManager parsingManager = null;

        public void InitializeCvLocateCore()
        {
            ICvLocateLogger parsingEngineLogger = new CvLocateLogger("parsingEngineLogger");
            ICvParser cvParser = new CvParser(parsingEngineLogger);
            IParsingEngineDataWrapper dataWrapper = new ParsingEngineDataWrapper();
            IParsingQueueManager parsingQueueManager = new ParsingQueueManager(dataWrapper);

            ParsingEngineManager parsingManager = new ParsingEngineManager(dataWrapper, parsingQueueManager, cvParser, parsingEngineLogger);
            parsingManager.Initialize();
        }

        public void Stop()
        {
            if (parsingManager != null)
            {
                parsingManager.Stop();
            }
        }
    }
}
