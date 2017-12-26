using System;

namespace Baidu.Yingyan
{
    public static class DateTimeExtension
    {
        private static readonly DateTime REFERENCE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static ulong ToUtcTicks(this DateTime dt)
        {
            return (ulong)(dt.ToUniversalTime() - REFERENCE).TotalSeconds;
        }

        public static DateTime FromUtcTicks(this ulong ticks)
        {
            return REFERENCE.AddSeconds(ticks).ToLocalTime();
        }

        public static DateTime FromUtcTicks(this long ticks)
        {
            return REFERENCE.AddSeconds(ticks).ToLocalTime();
        }

        public static ulong ToUtcTicks_ms(this DateTime dt)
        {
            return (ulong)(dt.ToUniversalTime() - REFERENCE).TotalSeconds;
        }

        public static DateTime FromUtcTicks_ms(this ulong Ticks_ms)
        {
            return REFERENCE.AddMilliseconds(Ticks_ms).ToLocalTime();
        }

        public static DateTime FromUtcTicks_ms(this long Ticks_ms)
        {
            return REFERENCE.AddMilliseconds(Ticks_ms).ToLocalTime();
        }
    }
}