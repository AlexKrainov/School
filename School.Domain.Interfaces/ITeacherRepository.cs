using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetTeachers();
        Teacher GetTeacherByID(int id);
        void Create(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(int id);
        void Save();
    }
}
