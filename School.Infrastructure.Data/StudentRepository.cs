using Microsoft.EntityFrameworkCore;
using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(SchoolContext db)
            : base(db)
        {
        }
        public void Create(Student student)
        {
            db.Students.Add(student);
        }

        public void Delete(int id)
        {
            Student student = db.Students.Find(id);

            if (student != null)
            {
                db.Students.Remove(student);
            }
        }

        public Student GetStudentByID(int id)
        {
            return db.Students.Include(x => x.Grade).FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Student> GetStudents()
        {
            return db.Students.Include(x => x.Grade).ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
        }
    }
}
