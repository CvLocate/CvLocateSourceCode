using CvLocate.DBComponent.CoreDBFacade;
using CvLocate.Common.DbFacadeInterface;
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
        ParsingEngineManager _parsingManager = null;

        public void InitializeCvLocateCore()
        {
            ICvLocateLogger parsingEngineLogger = new CvLocateLogger("parsingEngineLogger");
            ICvParser cvParser = new CvParser(parsingEngineLogger);
            ICoreDBFacade coreDbFacade = new CoreDBFacade();
            IParsingEngineDataWrapper dataWrapper = new ParsingEngineDataWrapper(coreDbFacade);
            IParsingQueueManager parsingQueueManager = new ParsingQueueManager(dataWrapper);

            this._parsingManager = new ParsingEngineManager(dataWrapper, parsingQueueManager, cvParser, parsingEngineLogger);
            this._parsingManager.Initialize();
        }

        public void Stop()
        {
            if (this._parsingManager != null)
            {
                this._parsingManager.Stop();
            }
        }
    }
}
