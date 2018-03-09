using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Domain.Core
{
    public class Teacher
    {
        public Teacher()
        {
            this.Subjects = new HashSet<Subject>();
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
        public virtual IEnumerable<Subject> Subjects { get; set; }
    }
}
