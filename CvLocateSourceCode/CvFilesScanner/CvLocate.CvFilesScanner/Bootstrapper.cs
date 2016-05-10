using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.Logging;
using CvLocate.CvFilesScanner.Interfaces;
using CvLocate.DBComponent.CvFilesDBFacade;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.CvFilesScanner
{
    public class Bootstrapper
    {
        ICvFilesScannerManager _cvFilesScannerManager;

        public void Initialize()
        {
            var container = new Container();
            container.RegisterSingleton<ICvLocateLogger>(new CvLocateLogger("scannerEngineLogger"));
            container.Register<ICvFilesFilesListener, CvFilesListener>(Lifestyle.Singleton);
            container.Register<ICvFilesScannerDBFacade, CvFilesScannerDBFacade>(Lifestyle.Singleton);
            container.Register<IScannerDataWrapper, ScannerDataWrapper>(Lifestyle.Singleton);
            container.Register<IDocumentConverterFactory, DocumentConverterFactory>(Lifestyle.Transient);
            container.Register<ICvFileScanner, CvFileScanner>(Lifestyle.Transient);
            container.Register<ICvFilesScannerManager, CvFilesScannerManager>(Lifestyle.Singleton);
            this._cvFilesScannerManager = container.GetInstance<ICvFilesScannerManager>();
            this._cvFilesScannerManager.Initialize();
        }
        public void Stop()
        {
            this._cvFilesScannerManager.Stop();
        }
    }
}
