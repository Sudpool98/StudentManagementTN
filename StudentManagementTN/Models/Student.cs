﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [ForeignKey("EduStatus")]
        public int Edustatusid { get; set; }
        [Required]
        [ForeignKey("ClassDivision")]
        public int Classid { get; set; }

        public virtual ClassDivision ClassDivision { get; set; }
        public virtual EduStatus EduStatus { get; set; }
    }
}