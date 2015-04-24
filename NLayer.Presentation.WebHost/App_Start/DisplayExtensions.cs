using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NLayer.Presentation.WebHost
{
    public static class DisplayExtensions
    {
        public static string Display(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static string Display(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.Display() : "";
        }

        public static string DisplayDate(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd");
        }

        public static string DisplayDate(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.DisplayDate() : "";
        }

        public static string DisplayDateHourMinute(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd HH:mm");
        }

        public static string DisplayDateHourMinute(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.DisplayDateHourMinute() : "";
        }

        public static string Display(this Decimal value)
        {
            return value.ToString("#,##0.00");
        }

        public static string Display(this Decimal? value)
        {
            return value.HasValue ? value.Value.Display() : "";
        }
    }
}