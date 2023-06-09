using System;
using System.Globalization;

namespace BlogApp.Core.Utilities.DateHelper
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
        
        public static string ToStringFormat(long dateTime)
        {
            var date = TimestamptToDate(dateTime);
            return date.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));
        }
    }
}

