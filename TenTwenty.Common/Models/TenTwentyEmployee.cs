using Amazon.DynamoDBv2.DataModel;

namespace TenTwenty.Common.Models
{
    [DynamoDBTable("TenTwenty")]
    public class TenTwentyEmployee : TenTwentyModelBase
    {
        [DynamoDBProperty(AttributeName = "Name")]
        public string Name { get; set; }
    }
}