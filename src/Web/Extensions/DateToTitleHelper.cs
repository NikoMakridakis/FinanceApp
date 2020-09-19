using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class DateToTitleHelper
    {
        public static string GetTitleFromDate(this DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            string title = $"{month} {year}";

            return title;
        }
    }
}
