using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Implementations
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public static class DateUtility
    {
        public static string DateTimeToIsoyd(DateTime dt)
        {
            var str = new StringBuilder();
            str.Append(dt.Year.ToString("0000"));
            str.Append("-");
            str.Append(dt.DayOfYear.ToString("000"));
            str.Append("T");
            str.Append(dt.Hour.ToString("00"));
            str.Append(":");
            str.Append(dt.Minute.ToString("00"));
            str.Append(":");
            str.Append(dt.Second.ToString("00"));
            str.Append(".");
            str.Append(dt.Millisecond.ToString("000"));
            return str.ToString();
        }

        public static DateTime IsoYDToDateTime(string isoYD)
        {
            var time = DateTime.MinValue;
            if (String.IsNullOrEmpty(isoYD)) {return time;}
            int sep1 = isoYD.IndexOf("-", StringComparison.Ordinal);
            int sep2 = isoYD.IndexOf("T", StringComparison.Ordinal);
            int sep3 = isoYD.IndexOf(":", StringComparison.Ordinal);
            int sep4 = isoYD.IndexOf(":", sep3 + 1, StringComparison.Ordinal);
            int year = Int32.Parse(isoYD.Substring(0, sep1));
            int dayOfYear = Int32.Parse(isoYD.Substring(sep1 + 1, sep2 - (sep1 + 1)));
            int hour = Int32.Parse(isoYD.Substring(sep2 + 1, sep3 - (sep2 + 1)));
            int minute = 0;
            double second = 0;
            if (sep4 > 0)
            {
                minute = Int32.Parse(isoYD.Substring(sep3 + 1, sep4 - (sep3 + 1)));
                string secondString = isoYD.Substring(sep4 + 1);
                if (secondString.Length > 0)
                {
                    second = Double.Parse(secondString, NumberStyles.Float, CultureInfo.InvariantCulture);
                }
            }

            if (hour >= 24)
            {
                hour -= 24;
                dayOfYear += 1;
            }

            time = new DateTime(year, 1, 1);
            time = time.AddDays(dayOfYear - 1);
            time = time.AddHours(hour).AddMinutes(minute).AddTicks((long)(second * 10000000));

            return time;
        }
    }
}
