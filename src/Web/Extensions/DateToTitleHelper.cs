using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class DateToTitleHelper
    {
        public static string GetTitleFromDate(this DateTime date)
        {
            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            int year = date.Year;
            string title = $"{month} {year}";

            return title;
        }
    }
}
