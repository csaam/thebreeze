using System.Web.Http.ExceptionHandling;
using TenTwenty.Common.Telemetry;

namespace TenTwenty.API
{
    public class TenTwentyExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            ILog log = new Log();

            var logContext = new
            {
                context.Request.RequestUri,
                Content = context.Request.Content.ReadAsStringAsync(),
                HttpMethod = context.Request.Method
            };

            log.LogError(LogTags.EmptyTag, context.Exception.Message, Metadata.CorrelationId, logContext,
                context.Exception);
        }
    }
}