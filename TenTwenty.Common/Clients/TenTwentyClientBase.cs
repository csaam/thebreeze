using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TenTwenty.Common.Models;

namespace TenTwenty.Common.Clients
{
    public abstract class TenTwentyClientBase<T> where T : TenTwentyModelBase, new()
    {
        public TenTwentyClientBase(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        protected Uri BaseAddress { private set; get; }
        protected abstract string Controller { get; }

        protected HttpClient CreateClient()
        {
            var client = new HttpClient {BaseAddress = BaseAddress};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public async Task<T> CreateAsync(T value, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = CreateClient())
            {
                using (
                    var response =
                        await client.SendAsync(CreateRequestMessage(HttpMethod.Post, value), cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<T>(cancellationToken);
                }
            }
        }

        public async Task<T> UpdateAsync(T value, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = CreateClient())
            {
                using (
                    var response =
                        await client.SendAsync(CreateRequestMessage(HttpMethod.Put, value), cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<T>(cancellationToken);
                }
            }
        }

        protected async Task ProtectedDeleteAsync(Guid agencyId, string instanceId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = CreateClient())
            {
                using (
                    var response =
                        await
                            client.SendAsync(
                                CreateRequestMessage(HttpMethod.Delete,
                                    new T {AgencyId = agencyId, InstanceId = instanceId}),
                                cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        protected async Task<T> ProtectedGetAsync(Guid agencyId, string instanceId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = CreateClient())
            {
                using (
                    var response =
                        await
                            client.SendAsync(
                                CreateRequestMessage(HttpMethod.Get,
                                    new T {AgencyId = agencyId, InstanceId = instanceId}),
                                cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<T>(cancellationToken);
                }
            }
        }

        protected async Task<TenTwentyResultSet<T>> ProtectedGetAsync(Guid agencyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = CreateClient())
            {
                using (
                    var response =
                        await
                            client.SendAsync(
                                CreateRequestMessage(HttpMethod.Get, new T {AgencyId = agencyId}),
                                cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<TenTwentyResultSet<T>>(cancellationToken);
                }
            }
        }

        protected virtual HttpRequestMessage CreateRequestMessage(HttpMethod method, T value)
        {
            var uriBuilder = new UriBuilder(this.BaseAddress);
            ObjectContent<T> context = null;
            switch (method.Method)
            {
                case "POST":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}/{1}",
                        value.AgencyId, Controller);
                    context = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
                    break;
                case "PUT":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}/{1}",
                        value.AgencyId,
                        Controller);
                    context = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
                    break;
                case "DELETE":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}/{1}/{2}",
                        value.AgencyId,
                        Controller, value.InstanceId);
                    break;
                case "GET":
                    uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "api/v1/agencies/{0}/{1}/{2}",
                        value.AgencyId,
                        Controller, value.InstanceId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(method.Method, method.Method,
                        "Method must be a valid HTTP/REST action.");
            }
            var request = new HttpRequestMessage(method, uriBuilder.Uri);
            if (context != null)
            {
                request.Content = context;
            }
            request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
            return request;
        }
    }
}