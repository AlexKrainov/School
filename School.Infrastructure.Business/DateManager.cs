using School.Domain.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace School.Infrastructure.Business
{
    public class DateManager
    {
        private int year;
        private int week;
        /// <summary>
        /// В формате 2018-W11
        /// </summary>
        /// <param name="yearAndWeek"></param>
        public DateManager(string yearAndWeek)
        {
            ParseWeek(yearAndWeek);
        }

        public DatesOfWeek GetDatesOfWeek()
        {
            DatesOfWeek dates = new DatesOfWeek();

            dates.MondayDate = FirstDateOfWeekISO8601();
            dates.TuesdayDate = dates.MondayDate.AddDays(1);
            dates.WednesdayDate = dates.MondayDate.AddDays(2);
            dates.ThursdayDate = dates.MondayDate.AddDays(3);
            dates.FridayDate = dates.MondayDate.AddDays(4);

            return dates;
        }

        private void ParseWeek(string value)
        {
            string[] s = value.Split("-W");

            year = int.Parse(s[0]);
            week = int.Parse(s[1]);
        }

        private DateTime FirstDateOfWeekISO8601()
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = week;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}
