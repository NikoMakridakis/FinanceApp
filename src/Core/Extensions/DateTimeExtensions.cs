using System;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentDaysLeftInMonth(this DateTime todaysDate)
        {
            todaysDate.ToLocalTime();
            int year = todaysDate.Year;
            int dayOfTheMonth = todaysDate.Month;
            int totalDaysInMonth = DateTime.DaysInMonth(year, dayOfTheMonth);
            int daysLeftInMonth = totalDaysInMonth - dayOfTheMonth;

            return daysLeftInMonth;
        }
    }
}
