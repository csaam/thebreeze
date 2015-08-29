using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace TenTwenty.Common.Models
{
    public class TenTwentyPhone: TenTwentyModelBase
    {
        public string  PhoneNumber { get; set; }

        [DynamoDBProperty(AttributeName = "Latitude")]
        public double Latitude { get; set; }

        [DynamoDBProperty(AttributeName = "Longitude")]
        public double Longitude { get; set; }
    }
}
