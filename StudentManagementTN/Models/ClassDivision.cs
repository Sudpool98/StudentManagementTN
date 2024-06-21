using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class ClassDivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,Range(1,12)]
        public int Classno { get; set; }
        [Column(TypeName = "char")]
        [Required, MaxLength(1)]
        public string Division { get; set; }
        [Required, MaxLength (3)]
        public string Combined { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}