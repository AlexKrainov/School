using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Business.PresentationModel
{
    public class SelectListGarde
    {
        private List<Grade> grades { get; set; }
        public SelectListGarde(List<Grade> grades)
        {
            this.grades = grades;
        }

        public SelectList GetSelectList()
        {
            var gradesList = grades
              .OrderBy(x => x.Number)
              .OrderBy(x => x.Symbol)
              .Select(x => new { ID = x.ID, Value = string.Format("{0}'{1}'", x.Number, x.Symbol) });
            return new SelectList(gradesList, "ID", "Value"); ;
        }

    }
}
