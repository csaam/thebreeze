using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TenTwenty.Common.Utility
{
    public static class TimeExtensions
    {
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void GetSystemTimePreciseAsFileTime(out long fileTime);

        public static DateTimeOffset GetPreciseUTCNow()
        {
            long fileTime;
            TimeExtensions.GetSystemTimePreciseAsFileTime(out fileTime);
            return new DateTimeOffset(DateTime.FromFileTimeUtc(fileTime));

        }
    }
}
