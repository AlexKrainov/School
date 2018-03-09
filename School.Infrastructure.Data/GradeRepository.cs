using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class GradeRepository : BaseRepository, IGradeRepository
    {
        public GradeRepository(SchoolContext db) : base(db)
        { }
        public bool Create(Grade grade)
        {
            bool have = db.Grades.Any(x => x.Number == grade.Number && x.Symbol == grade.Symbol);

            if (have == false)
            {
                db.Grades.Add(grade);
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            Grade grade = db.Grades.Find(id);

            if (grade != null)
            {
                db.Grades.Remove(grade);
            }
        }

        public Grade GetGradeByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Grade> GetGradeList()
        {
            return db.Grades.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Grade grade)
        {
            Grade oldGrade = db.Grades.FirstOrDefault(x => x.Number == grade.Number && x.Symbol == grade.Symbol);

            if (oldGrade == null)
            {
                db.Entry(grade).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
    }
}
