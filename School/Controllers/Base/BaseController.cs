using Microsoft.AspNetCore.Mvc;
using School.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Controllers.Base
{
    public class BaseController : Controller
    {
        protected SchoolContext db;

        public BaseController(SchoolContext db)
        {
            this.db = db;
        }
    }
}
