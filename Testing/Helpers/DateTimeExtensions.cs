using System;

namespace Codell.Pies.Testing.Helpers
{
    public static class DateTimeExtensions
    {
        public static TimeSpan Hours(this int duration)
        {
            return DateTime.Now.AddHours(duration).Subtract(DateTime.Now);
        }

        public static DateTime TruncateToSeconds(this DateTime source)
        {
            return new DateTime(source.Ticks - (source.Ticks % TimeSpan.TicksPerSecond), source.Kind);
        }

        public static DateTime IgnoreTime(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, source.Day);
        }

        public static DateTime IgnoreSeconds(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, source.Day, source.Hour, source.Minute, 0);
        }
    }
}