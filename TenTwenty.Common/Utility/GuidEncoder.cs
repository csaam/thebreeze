using System;

namespace TenTwenty.Common.Utility
{
    public static class GuidEncoder
    {
        public static string Encode(Guid value)
        {
            var bytes = value.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        public static Guid Decode(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return new Guid(bytes);
        }
    }
}