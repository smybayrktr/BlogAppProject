using System;
using System.Globalization;

namespace BlogApp.Core.Utilities
{
    public class DateHelper
    {
        public static long DateToTimestampt(DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeMilliseconds();
        }

        public static DateTime TimestamptToDate(long timestampt)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(timestampt).ToLocalTime();
            return dateTime;
        }

        public static string ToDDMMMMYYYYhhddmmStringFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd MMMM yyyy hh:mm:ss", new CultureInfo("tr-TR"));
        }

        public static string ToDDMMMMYYYYStringFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
        }

        public static string ToHHMMDDMMYYYYStringFormat(DateTime dateTime)
        {
            return dateTime.ToString("HH.mm dd.MM.yyyy", new CultureInfo("tr-TR"));
        }
        public static string ToHHMMStringFormat(DateTime dateTime)
        {
            return dateTime.ToString("HH.mm", new CultureInfo("tr-TR"));
        }
    }
}

