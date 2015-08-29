using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TenTwenty.Common.Models;

namespace TenTwenty.Common.Clients
{
    public class EmployeesClient: TenTwentyClientBase<TenTwentyEmployee>
    {
        public EmployeesClient(Uri baseAddress):base(baseAddress)
        {
        }

        protected override string Controller
        {
            get { return "employees"; }
        }
         public Task DeleteAsync(Guid agencyId, string instanceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.ProtectedDeleteAsync(agencyId, instanceId, cancellationToken);
        }

        public Task<TenTwentyEmployee> GetAsync(Guid agencyId, string instanceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.ProtectedGetAsync(agencyId, instanceId, cancellationToken);
        }

        public Task<TenTwentyResultSet<TenTwentyEmployee>> GetAsync(Guid agencyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.ProtectedGetAsync(agencyId, cancellationToken);
        }
    }
}