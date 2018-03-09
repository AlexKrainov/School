using Microsoft.EntityFrameworkCore;
using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class TeacherRepository : BaseRepository, ITeacherRepository
    {

        public TeacherRepository(SchoolContext db)
            : base(db)
        {
        }
        public void Create(Teacher teacher)
        {
            db.Theachers.Add(teacher);
        }

        public void Delete(int id)
        {
            Teacher teacher = db.Theachers.Find(id);
            if (teacher != null)
            {
                if (db.Subjects.Any(x => x.TeacherID == id))
                {
                    List<Subject> subjects = db.Subjects.Where(x => x.TeacherID == id).ToList();
                    for (int i = 0; i < subjects.Count; i++)
                    {
                        subjects[i].TeacherID = null;
                    }
                }
                db.Theachers.Remove(teacher);
            }
        }

        public Teacher GetTeacherByID(int id)
        {
            return db.Theachers.Include(x => x.Subjects).FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return db.Theachers.Include(x => x.Subjects).Where(x => x.Subjects.Count() != 0).ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            db.Entry(teacher).State = EntityState.Modified;
        }
    }
}
