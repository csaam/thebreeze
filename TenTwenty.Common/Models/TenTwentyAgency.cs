using Amazon.DynamoDBv2.DataModel;

namespace TenTwenty.Common.Models
{
    [DynamoDBTable("TenTwenty")]
    public class TenTwentyAgency : TenTwentyModelBase
    {
        [DynamoDBProperty(AttributeName = "Latitude")]
        public double Latitude{ get; set; }

        [DynamoDBProperty(AttributeName = "Longitude")]
        public double Longitude { get; set; }

        [DynamoDBProperty(AttributeName = "Name")]
        public string Name { get; set; }

        [DynamoDBProperty(AttributeName = "Description")]
        public string Description { get; set; }
    }
}