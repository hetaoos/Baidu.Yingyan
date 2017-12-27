using System;

namespace Baidu.Yingyan
{
    /// <summary>
    /// 日期扩展
    /// </summary>
    public static class DateTimeExtension
    {
        private static readonly DateTime REFERENCE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Unix 时间戳转换（秒）
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static ulong ToUtcTicks(this DateTime dt)
        {
            return (ulong)(dt.ToUniversalTime() - REFERENCE).TotalSeconds;
        }

        /// <summary>
        /// Unix 时间戳转换（秒）
        /// </summary>
        /// <param name="ticks">The ticks.</param>
        /// <returns></returns>
        public static DateTime FromUtcTicks(this ulong ticks)
        {
            return REFERENCE.AddSeconds(ticks).ToLocalTime();
        }

        /// <summary>
        /// Unix 时间戳转换（秒）
        /// </summary>
        /// <param name="ticks">The ticks.</param>
        /// <returns></returns>
        public static DateTime FromUtcTicks(this long ticks)
        {
            return REFERENCE.AddSeconds(ticks).ToLocalTime();
        }

        /// <summary>
        /// Unix 时间戳转换（毫秒）
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static ulong ToUtcTicks_ms(this DateTime dt)
        {
            return (ulong)(dt.ToUniversalTime() - REFERENCE).TotalSeconds;
        }

        /// <summary>
        /// Unix 时间戳转换（毫秒）
        /// </summary>
        /// <param name="Ticks_ms">The ticks ms.</param>
        /// <returns></returns>
        public static DateTime FromUtcTicks_ms(this ulong Ticks_ms)
        {
            return REFERENCE.AddMilliseconds(Ticks_ms).ToLocalTime();
        }

        /// <summary>
        /// FUnix 时间戳转换（毫秒）
        /// </summary>
        /// <param name="Ticks_ms">The ticks ms.</param>
        /// <returns></returns>
        public static DateTime FromUtcTicks_ms(this long Ticks_ms)
        {
            return REFERENCE.AddMilliseconds(Ticks_ms).ToLocalTime();
        }
    }
}