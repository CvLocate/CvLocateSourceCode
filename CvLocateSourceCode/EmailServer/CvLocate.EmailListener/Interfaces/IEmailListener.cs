using CvLocate.Common.Logging;
using System.Collections.Generic;


namespace CvLocate.EmailListener.Interfaces
{
    public interface IEmailListener
    {
        void Initialize(IEnumerable<IMaillListenerDefinition> mailActionsDefinitions, ICvLocateLogger logger);
    }
}
