using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace TenTwenty.Common.Converters
{
    public class DateTimeOffsetConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            var dateTimeOffset = (DateTimeOffset) value;

            return new Primitive(dateTimeOffset.UtcTicks.ToString(), true);
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var ticks = entry.AsLong();
            return new DateTimeOffset(ticks, DateTimeOffset.UtcNow.Offset);
        }
    }
}