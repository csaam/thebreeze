using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace TenTwenty.Common.Converters
{
    public class GuidConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            return new Guid(entry.AsString());
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
            {
                return null;
            }
            return new Primitive(value.ToString());
        }
    }
}