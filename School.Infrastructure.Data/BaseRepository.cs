using System;
using System.Collections.Generic;
using System.Text;

namespace School.Infrastructure.Data
{
    public class BaseRepository : IDisposable
    {
        protected SchoolContext db;
        private bool disposed = false;

        public BaseRepository(SchoolContext db)
        {
            this.db = db;
        }

        public virtual void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
                if (disposing == true)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
