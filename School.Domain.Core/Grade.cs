using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace School.Domain.Core
{
    public class Grade
    {
        public int ID { get; set; }

        [Display(Name = "Номер класса")]
        public int Number { get; set; }

        [Column(TypeName = "varchar(1)")]
        [Display(Name = "Буква класса")]
        public string Symbol { get; set; }
    }
}
