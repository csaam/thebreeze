using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TenTwenty.Api;
using TenTwenty.Common.Models;

namespace TenTwenty.API.Controllers
{
    [RoutePrefix("api/v1/agencies/{agencyId}/employees")]
    public class EmployeesController : ControllerBase<TenTwentyEmployee>
    {
        [MetadataActionFilter]
        [HttpGet]
        [Route("{instanceId}")]
        public Task<TenTwentyEmployee> GetAsync(Guid agencyId, string instanceId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedGetAsync(agencyId, instanceId, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpGet]
        [Route("")]
        public Task<TenTwentyResultSet<TenTwentyEmployee>> GetAsync(Guid agencyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedGetAsync(agencyId, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpPut]
        [Route("")]
        public Task<TenTwentyEmployee> PutAsync([FromBody] TenTwentyEmployee employee,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedPutAsync(employee, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpPost]
        [Route("")]
        public Task<TenTwentyEmployee> PostAsync(Guid agencyId, [FromBody] TenTwentyEmployee employee,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedPostAsync(agencyId, employee, cancellationToken);
        }

        [MetadataActionFilter]
        [HttpDelete]
        [Route("{instanceId}")]
        public Task DeleteAsync(Guid agencyId, string instanceId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ProtectedDeleteAsync(agencyId, instanceId, cancellationToken);
        }
    }
}