using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TenTwenty.Common.Utility;

namespace TenTwenty.Common.Telemetry
{
    public class Log : ILog
    {
        private readonly TraceSource traceSource = new TraceSource("TenTwenty");

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LogCritical(int id, string message, Guid correlationId, dynamic context, Exception exception = null)
        {
            this.WriteLog(id, TraceEventType.Critical, message, correlationId, context.ToString(), exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LogError(int id, string message, Guid correlationId, dynamic context, Exception exception = null)
        {
            this.WriteLog(id, TraceEventType.Error, message, correlationId, context.ToString(), exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LogInfo(int id, string message, Guid correlationId, dynamic context, Exception exception = null)
        {
            this.WriteLog(id, TraceEventType.Information, message, correlationId, context.ToString(), exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LogWarning(int id, string message, Guid correlationId, dynamic context, Exception exception = null)
        {
            this.WriteLog(id, TraceEventType.Warning, message, correlationId, context.ToString(), exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void LogVerbose(int id, string message, Guid correlationId, dynamic context, Exception exception = null)
        {
            this.WriteLog(id, TraceEventType.Verbose, message, correlationId, context.ToString(), exception);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void WriteLog(int id, TraceEventType eventType, string message, Guid correlationId, string context,
            Exception exception)
        {
            long filetime;
            TimeExtensions.GetSystemTimePreciseAsFileTime(out filetime);
            // string stackTrace = new StackTrace(4, true).ToString();

            var dateTime = DateTime.FromFileTimeUtc(filetime).ToString("HH:mm:ss.ffff");

            string output;
            if (exception == default(Exception))
            {
                output = string.Format("Time={0}, Message={1}, CorrelationId={2}, Context={3}", dateTime, message,
                    correlationId, context);
            }
            else
            {
                output = string.Format("Time={0}, Message={1}, CorrelationId={2}, Context={3}, Exception={4}", dateTime,
                    message, correlationId, context, exception);
            }

            output = output.Replace(Environment.NewLine, string.Empty);
            traceSource.TraceEvent(eventType, id, output);
        }
    }
}