using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class EduStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}