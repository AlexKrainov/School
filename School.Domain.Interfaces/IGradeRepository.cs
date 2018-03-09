using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> GetGradeList();
        Grade GetGradeByID(int id);
        bool Create(Grade book);
        void Update(Grade book);
        void Delete(int id);
        void Save();
    }
}
