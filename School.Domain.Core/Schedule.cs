using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Core
{
    public class Schedule
    {
        public int ID { get; set; }
        public int GradeID { get; set; }
        public int TeacherID { get; set; }
        public int TimeID { get; set; }
        public DateTime Date { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual LessonTime Time { get; set; }
    }
}
