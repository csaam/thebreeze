using System.Collections.Generic;

namespace TenTwenty.Common.Models
{
    public class TenTwentyResultSet<T> where T : TenTwentyModelBase
    {
        public IEnumerable<T> Results { get; set; }
        public string ContinuationToken { get; set; }
        public bool IsComplete { get; set; }
    }
}