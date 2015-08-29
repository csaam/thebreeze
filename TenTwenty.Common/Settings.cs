using Amazon.DynamoDBv2;
using Amazon.Geo;

namespace TenTwenty.Common
{
    public class Settings
    {
        public static GeoDataManagerConfiguration GeoDataManagerConfiguration
        {
            get
            {
                return new GeoDataManagerConfiguration(AmazonDynamoDBClient, "Location")
                {
                    HashKeyAttributeName = "HashKey",
                    RangeKeyAttributeName = "InstanceId",
                    GeohashAttributeName = "GeoHash",
                    GeoJsonAttributeName = "GeoJson",
                    GeohashIndexName = "GeorHash-Index",
                    HashKeyLength = 6
                };
            }
        }

        public static string AgenciesRangeKeyValue
        {
            get { return "Agency"; }
        }

        public static AmazonDynamoDBClient AmazonDynamoDBClient
        {
            get { return new AmazonDynamoDBClient(); }
        }

        public static GeoDataManager GeoDataManager
        {
            get { return new GeoDataManager(GeoDataManagerConfiguration); }
        }
    }
}