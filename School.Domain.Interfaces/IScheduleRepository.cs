using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> GetSchedules(DateTime start, int gradeID);
        Schedule GetStudentByID(int id);
        int Create(Schedule schedule);
        void Update(Schedule schedule);
        void Delete(int id);
        void Save();

        bool IsBusyTeacher(Schedule schedule);
    }
}
