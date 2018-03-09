using School.Domain.Core;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Data
{
    public class LessonTimeRepository : BaseRepository, ILessonTimeRepository
    {
        public LessonTimeRepository(SchoolContext db) : base(db)
        {
            if (db.LessonTimes.Count() == 0)
            {
                db.LessonTimes.Add(new LessonTime { Time = "8-9" });
                db.LessonTimes.Add(new LessonTime { Time = "9-10" });
                db.LessonTimes.Add(new LessonTime { Time = "10-11" });
                db.LessonTimes.Add(new LessonTime { Time = "11-12" });
                db.LessonTimes.Add(new LessonTime { Time = "12-13" });
                db.LessonTimes.Add(new LessonTime { Time = "13-14" });
            }
        }

        public IEnumerable<LessonTime> GetLessonTime()
        {
            return db.LessonTimes;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
