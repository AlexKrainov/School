using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Domain.Core
{
    public class Subject
    {
        public int ID { get; set; }
        public Nullable<int> TeacherID { get; set; }
        [Required]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }

        public virtual Teacher Teacher { get; set; }

    }
}
