using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface ILessonTimeRepository
    {
        IEnumerable<LessonTime> GetLessonTime();
        void Save();
    }
}
