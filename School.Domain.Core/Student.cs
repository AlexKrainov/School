using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Core
{
    public class Student
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Класс")]
        public int GradeID { get; set; }
        [Display(Name = "Имя")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [Required]
        public string LastName { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
