using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int id);
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
        void Save();
    }
}
