using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectByID(int id);
        void Create(Subject subject);
        void Update(Subject subject);
        void Delete(int id);
        void Save();
    }
}
