using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Owin.Security.Twitter.Messages;
using Microsoft.Practices.Unity;
using TenTwenty.Api.App_Start;
using TenTwenty.Common.Telemetry;
using TenTwenty.Common.Utility;

namespace TenTwenty.Api
{
    public class MetadataActionFilterAttribute: ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Metadata.CorrelationId = Guid.Empty;
            Metadata.TimeOffset = TimeSpan.MaxValue;
            Metadata.CallStartTime = TimeExtensions.GetPreciseUTCNow();
            foreach (var header in actionContext.Request.Headers)
            {
                if (header.Key == "CorrelationId")
                {
                    Guid correlationId;
                    if (Guid.TryParse(header.Value.FirstOrDefault(), out correlationId))
                    {
                        Metadata.CorrelationId = correlationId;
                    }
                }
                else if (header.Key == "TimeOffset")
                {
                    TimeSpan timeOffset;
                    if (TimeSpan.TryParse(header.Value.FirstOrDefault(), out timeOffset))
                    {
                        Metadata.TimeOffset = timeOffset;
                    }
                }
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            TimeSpan duration = TimeExtensions.GetPreciseUTCNow() - Metadata.CallStartTime;
            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            ILog log = container.Resolve<ILog>();

            var context = new
            {
                RequestUri = actionExecutedContext.Request.RequestUri,
                Content = actionExecutedContext.Request.Content.ReadAsStringAsync(),
                CallDuration = duration,
                HttpMethod = actionExecutedContext.Request.Method,
                MethodName = ((System.Web.Http.Controllers.ReflectedHttpActionDescriptor)actionExecutedContext.ActionContext.ActionDescriptor).MethodInfo.ToString(),
            };

            if (actionExecutedContext.Exception != null)
            {
                log.LogError(LogTags.EmptyTag, "Exception Invoking action", Metadata.CorrelationId, context, actionExecutedContext.Exception);
            }
            else
            {
                log.LogInfo(LogTags.EmptyTag, "Call completed successfully.", Metadata.CorrelationId, context);
            }
        }
    }
}
