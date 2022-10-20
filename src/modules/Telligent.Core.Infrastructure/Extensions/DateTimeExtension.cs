using TimeZoneConverter;

namespace Telligent.Core.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        private const string Utc8TimeZone = "Asia/Taipei";

        public static DateTime ToUtc8DateTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TZConvert.GetTimeZoneInfo(Utc8TimeZone));
        }

        /// <summary>
        /// 13 digits timestamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToMillisecondsTimestamp(this DateTime date)
        {
            var baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }

        /// <summary>
        /// 10 digits timestamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToSecondsTimestamp(this DateTime date)
        {
            var baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalSeconds;
        }
    }
}
