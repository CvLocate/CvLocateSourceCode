using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Common.Logging
{
    public interface ICvLocateLogger
    {
        void Debug(string message);
        void DebugFormat(string message, params object[] args);
        void Info(string message);
        void InfoFormat(string message, params object[] args);
        void Warn(string message);
        void WarnFormat(string message, params object[] args);
        void Error(string message);
        void ErrorFormat(string message, params object[] args);
        void Fatal(string message);
        void FatalFormat(string message, params object[] args);
    }
 
}
