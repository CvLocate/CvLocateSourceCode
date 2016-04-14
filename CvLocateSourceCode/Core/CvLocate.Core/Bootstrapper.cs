using CvLocate.DBComponent.CoreDBFacade;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.Logging;
using CvLocate.ParsingEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace CvLocate.Core
{
    public class Bootstrapper
    {
        IParsingEngineManager _parsingManager = null;

        public void InitializeCvLocateCore()
        {
            var container = new Container();
            container.RegisterSingleton<ICvLocateLogger>(new CvLocateLogger("parsingEngineLogger"));
            container.Register<ICoreDBFacade, CoreDBFacade>(Lifestyle.Singleton);
            container.Register<IParsingEngineDataWrapper, ParsingEngineDataWrapper>(Lifestyle.Singleton);
            container.Register<IParsingQueueManager, ParsingQueueManager>(Lifestyle.Singleton);
            container.Register<ICvParser, CvParser>(Lifestyle.Transient);
            container.Register<IParsingEngineManager, ParsingEngineManager>(Lifestyle.Singleton);

            this._parsingManager = container.GetInstance<IParsingEngineManager>();
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
