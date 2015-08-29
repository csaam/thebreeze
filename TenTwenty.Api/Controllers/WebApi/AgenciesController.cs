using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Amazon.DynamoDBv2.DataModel;
using TenTwenty.Api;
using TenTwenty.Common;
using TenTwenty.Common.Models;
using TenTwenty.Common.Utility;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace TenTwenty.API.Controllers
{
    [SwaggerResponse(HttpStatusCode.BadRequest)]
    [System.Web.Http.RoutePrefix("api/v1/agencies")]
    public class AgenciesController : ApiController
    {
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<TenTwentyAgency>))]
        [MetadataActionFilter]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public async Task<IEnumerable<TenTwentyAgency>> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            AsyncSearch<TenTwentyAgency> search = context.ScanAsync<TenTwentyAgency>(Enumerable.Empty<ScanCondition>(), null);
            List<TenTwentyAgency> result = await search.GetRemainingAsync(cancellationToken);
            return result;
        }

        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<TenTwentyAgency>))]
        [MetadataActionFilter]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{instanceId:guid}")]
        public async Task<TenTwentyAgency> GetAsync(Guid instanceId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            return
                await
                    context.LoadAsync<TenTwentyAgency>(instanceId.ToString(), Settings.AgenciesRangeKeyValue,
                        cancellationToken);
        }

        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(TenTwentyAgency))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TenTwentyAgency))]
        [MetadataActionFilter]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("")]
        public async Task<TenTwentyAgency> PutAsync([FromBody] TenTwentyAgency tenTwentyAgency,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var now = TimeExtensions.GetPreciseUTCNow();
            tenTwentyAgency.ModifiedTime = now;
            var context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await context.SaveAsync(tenTwentyAgency, cancellationToken);
            return tenTwentyAgency;
        }

        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(TenTwentyAgency))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TenTwentyAgency))]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [MetadataActionFilter]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("")]
        public async Task<TenTwentyAgency> PostAsync([FromBody] TenTwentyAgency tenTwentyAgency,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var now = TimeExtensions.GetPreciseUTCNow();
            tenTwentyAgency.AgencyId = Guid.NewGuid();
            tenTwentyAgency.CreatedTime = tenTwentyAgency.ModifiedTime = now;
            tenTwentyAgency.InstanceId = Settings.AgenciesRangeKeyValue;
            var context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await context.SaveAsync(tenTwentyAgency, cancellationToken);
            return tenTwentyAgency;
        }

        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TenTwentyAgency))]
        [MetadataActionFilter]
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("{instanceId:guid}")]
        public async Task DeleteAsync(Guid instanceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await
                context.DeleteAsync<TenTwentyAgency>(instanceId.ToString(), Settings.AgenciesRangeKeyValue,
                    cancellationToken);
        }
    }
}