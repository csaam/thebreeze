using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using TenTwenty.Common;
using TenTwenty.Common.Models;
using TenTwenty.Common.Utility;
using TenTwenty.Common.Telemetry;

namespace TenTwenty.API.Controllers
{
    public abstract class ControllerBase<T> : ApiController
        where T : TenTwentyModelBase, new()
    {
        private string prefix = typeof (T).Name + "_";

        protected async Task<T> ProtectedGetAsync(Guid agencyId, string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            return await context.LoadAsync<T>(agencyId.ToString(), id, cancellationToken);   
        }



        protected async Task<TenTwentyResultSet<T>> ProtectedGetAsync(Guid agencyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            AsyncSearch<T> result = context.QueryAsync<T>(agencyId.ToString(), QueryOperator.BeginsWith, new[] { prefix });

            TenTwentyResultSet<T> resultSet = new TenTwentyResultSet<T>();
            resultSet.Results = await result.GetRemainingAsync(cancellationToken);
            resultSet.ContinuationToken = null;
            resultSet.IsComplete = true;
            return resultSet;
        }

        
        public async Task<T> ProtectedPutAsync([FromBody] T employee,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DateTimeOffset now = TimeExtensions.GetPreciseUTCNow();
            employee.ModifiedTime = now;
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await context.SaveAsync(employee, cancellationToken);
            return employee;

        }


        protected async Task<T> ProtectedPostAsync(Guid agencyId, [FromBody] T employee, CancellationToken cancellationToken = default(CancellationToken))
        {
            DateTimeOffset now = TimeExtensions.GetPreciseUTCNow();
            employee.AgencyId = agencyId;
            employee.CreatedTime = employee.ModifiedTime = now;
            employee.InstanceId = prefix + Guid.NewGuid().ToString();
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await context.SaveAsync(employee, cancellationToken);
            return employee;
        }


        protected async Task ProtectedDeleteAsync(Guid agencyId, string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            DynamoDBContext context = new DynamoDBContext(Settings.AmazonDynamoDBClient);
            await context.DeleteAsync<T>(agencyId.ToString(), id, cancellationToken);
        }
    }
    
}