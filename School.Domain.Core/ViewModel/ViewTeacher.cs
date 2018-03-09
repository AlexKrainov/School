using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Domain.Core.ViewModel
{
    public class ViewTeacher
    {
        public ViewTeacher()
        {
            SubjectsIDs = default(int[]);
        }
        public ViewTeacher(Teacher teacher)
        {
            ID = teacher.ID;
            FirstName = teacher.FirstName;
            MiddleName = teacher.MiddleName;
            LastName = teacher.LastName;
        }
        public int ID { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Предметы")]
        public int[] SubjectsIDs { get; set; }

        public virtual SelectList SubjectsSelectList { get; set; }

    }
}
