using System;
using Amazon.DynamoDBv2.DataModel;
using TenTwenty.Common.Converters;

namespace TenTwenty.Common.Models
{
    [DynamoDBTable("TenTwenty")]
    public class TenTwentyModelBase
    {
        [DynamoDBHashKey(AttributeName = "AgencyId", Converter = typeof (GuidConverter))]
        public Guid AgencyId { get; set; }

        [DynamoDBRangeKey(AttributeName = "InstanceId")]
        public string InstanceId { get; set; }

        [DynamoDBProperty(AttributeName = "CreatedTime", Converter = typeof (DateTimeOffsetConverter))]
        public DateTimeOffset CreatedTime { get; set; }

        [DynamoDBProperty(AttributeName = "ModifiedTime", Converter = typeof (DateTimeOffsetConverter))]
        public DateTimeOffset ModifiedTime { get; set; }

        [DynamoDBVersion(AttributeName = "Version")]
        public long? Version { get; set; }
    }
}