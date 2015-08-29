using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using TenTwenty.Common.Models;

namespace TenTwenty.Common.Clients
{
    public class AgenciesClient : TenTwentyClientBase<TenTwentyAgency>
    {
        public AgenciesClient(Uri baseAddress) : base(baseAddress)
        {
        }

        protected override string Controller
        {
            get { return "agencies"; }
        }

        protected override HttpRequestMessage CreateRequestMessage(HttpMethod method, TenTwentyAgency value)
        {
            var uriBuilder = new UriBuilder(BaseAddress);
            ObjectContent<TenTwentyAgency> context = null;
            switch (method.Method)
            {
                case "POST":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies");
                    context = new ObjectContent<TenTwentyAgency>(value, new JsonMediaTypeFormatter());
                    break;
                case "PUT":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies");
                    context = new ObjectContent<TenTwentyAgency>(value, new JsonMediaTypeFormatter());
                    break;
                case "DELETE":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}", value.AgencyId);
                    break;
                case "GET":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}", value.AgencyId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(method.Method, method.Method,
                        "Method must be a valid HTTP/REST action.");
            }
            HttpRequestMessage request = new HttpRequestMessage(method, uriBuilder.Uri);
            if (context != null)
            {
                request.Content = context;
            }
            request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
            return request;
        }

        public Task DeleteAsync(Guid agencyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedDeleteAsync(agencyId, Settings.AgenciesRangeKeyValue, cancellationToken);
        }

        public Task<TenTwentyAgency> GetAsync(Guid agencyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedGetAsync(agencyId, Settings.AgenciesRangeKeyValue, cancellationToken);
        }
    }
}