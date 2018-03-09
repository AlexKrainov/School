using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Core.ViewModel
{
    public class DatesOfWeek
    {
        public string Monday { get { return "Понедельник"; } }
        public string Tuesday { get { return "Вторник"; } }
        public string Wednesday { get { return "Среда"; } }
        public string Thursday { get { return "Четверг"; } }
        public string Friday { get { return "Пятница"; } }

        public DateTime MondayDate { get; set; }
        public DateTime TuesdayDate { get; set; }
        public DateTime WednesdayDate { get; set; }
        public DateTime ThursdayDate { get; set; }
        public DateTime FridayDate { get; set; }

    }
}
