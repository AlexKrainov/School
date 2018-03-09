using Microsoft.EntityFrameworkCore;
using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        public ScheduleRepository(SchoolContext db)
            : base(db)
        {
        }

        public int Create(Schedule schedule)
        {
            Schedule oldSchedule = db.Schedules.FirstOrDefault(x =>
               x.GradeID == schedule.GradeID &&
               x.TimeID == schedule.TimeID &&
               x.Date == schedule.Date);// Находим соответсвия в базе (класс, вермя дата)

            if (oldSchedule != null)
            {
                oldSchedule.TeacherID = schedule.TeacherID;
                return oldSchedule.ID;
            }
            else
            {
                db.Schedules.Add(schedule);
                Save();
                return schedule.ID;
            }
        }

        /// <summary>
        /// Проверяем занят ли учитель на это время другим классом
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeID"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public bool IsBusyTeacher(Schedule newSchedule)
        {
            Schedule schedule = db.Schedules.FirstOrDefault(x =>
            x.Date == newSchedule.Date &&
            x.TimeID == newSchedule.TimeID &&
            x.TeacherID == newSchedule.TeacherID);

            return schedule == null ? false : true;
        }

        public void Delete(int id)
        {
            var schedule = db.Schedules.Find(id);
            if (schedule != null)
            {
                db.Schedules.Remove(schedule);
            }
        }

        public IEnumerable<Schedule> GetSchedules(DateTime monday, int gradeID)
        {
            DateTime friday = monday.AddDays(4);
            return db.Schedules
                .Include(x => x.Teacher).Include(x => x.Teacher.Subjects)
                .Where(x => x.GradeID == gradeID
                && (x.Date >= monday && x.Date <= friday));
        }

        public Schedule GetStudentByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Schedule schedule)
        {
            throw new NotImplementedException();
        }
    }
}
