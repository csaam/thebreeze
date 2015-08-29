using System;
using System.Threading;

namespace TenTwenty.Common.Telemetry
{
    public static class Metadata
    {
        
        public static Guid CorrelationId { get; set; }

        public static TimeSpan TimeOffset { get; set; }

        public static DateTimeOffset CallStartTime{ get; set; }
    }
}