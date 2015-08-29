using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TenTwenty.Common.Telemetry
{
    public interface ILog
    {
        void LogInfo(int id, string message, Guid correlationId, dynamic context, Exception exception = null);

        void LogVerbose(int id, string message, Guid correlationId, dynamic context, Exception exception = null);
        void LogWarning(int id, string message, Guid correlationId, dynamic context, Exception exception = null);

        void LogError(int id, string message, Guid correlationId, dynamic context, Exception exception = null);

        void LogCritical(int id, string message, Guid correlationId, dynamic context, Exception exception = null);
        
    }
}
