using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School.Infrastructure.Business.PresentationModel
{
    public class SelectListSubject
    {
        private List<Subject> subjects { get; set; }
        public SelectListSubject(List<Subject> subjects)
        {
            this.subjects = subjects;
        }

        public SelectList GetSelectList()
        {
            var subjectList = subjects
              .OrderBy(x => x.Name)
              .Select(x => new { ID = x.ID, Value = x.Name });
            return new SelectList(subjectList, "ID", "Value");
        }

    }
}
