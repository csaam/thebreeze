using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TenTwenty.Api;
using TenTwenty.Common.Models;

namespace TenTwenty.API.Controllers
{
    [RoutePrefix("api/v1/agencies/{agencyId}/phones")]
    public class PhoneController : ControllerBase<TenTwentyPhone>
    {
        [MetadataActionFilter]
        [HttpGet]
        [Route("{id}")]
        public Task<TenTwentyPhone> GetAsync(Guid agencyId, string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedGetAsync(agencyId, id, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpGet]
        [Route("")]
        public Task<TenTwentyResultSet<TenTwentyPhone>> GetAsync(Guid agencyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedGetAsync(agencyId, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpPut]
        [Route("")]
        public Task<TenTwentyPhone> PutAsync([FromBody] TenTwentyPhone employee,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedPutAsync(employee, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpPost]
        [Route("")]
        public Task<TenTwentyPhone> PostAsync(Guid agencyId, [FromBody] TenTwentyPhone employee,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedPostAsync(agencyId, employee, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid agencyId, string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedDeleteAsync(agencyId, id, cancellationToken);
        }
    }
}