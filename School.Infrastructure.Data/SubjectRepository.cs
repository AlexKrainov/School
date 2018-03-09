using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        public SubjectRepository(SchoolContext db)
            : base(db)
        {
        }
        public void Create(Subject subject)
        {
            db.Subjects.Add(subject);
        }

        public void Delete(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject != null)
            {
                db.Subjects.Remove(subject);
            }
        }

        public Subject GetSubjectByID(int id)
        {
            return db.Subjects.Find(id);
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return db.Subjects;
        }

        public IEnumerable<Subject> GetSubjectsWithoutTeacher()
        {
            return db.Subjects.Where(x => x.TeacherID == null);
        }

        public IEnumerable<Subject> GetSubjectsByTeacher(int id)
        {
            return GetSubjectsWithoutTeacher().Union(db.Subjects.Where(x => x.TeacherID == id));
        }


        public void Save()
        {
           var r = db.SaveChanges();
        }

        public void Update(Subject subject)
        {
            db.Entry(subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateByTeacherID(int teacherID, int[] id_s)
        {
            //Устанавливаем новое значение
            List<Subject> updated = db.Subjects
                .Where(x => id_s.Contains(x.ID) == true).ToList();
            for (int i = 0; i < updated.Count(); i++)
            {
                updated[i].TeacherID = teacherID;
                Update(updated.ElementAt(i));
            }
            //удаляем ссылки на предметы у данного учителя
            List<Subject> notUpdated = db.Subjects
                .Where(x => x.TeacherID == teacherID && id_s.Contains(x.ID) == false).ToList();

            for (int i = 0; i < notUpdated.Count(); i++)
            {
                notUpdated[i].TeacherID = null;
                Update(notUpdated.ElementAt(i));
            }

        }
    }
}
