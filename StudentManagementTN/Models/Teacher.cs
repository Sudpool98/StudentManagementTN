using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Index("IX_UsernameUniqueKey", 1, IsUnique = true)]
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Index("IX_ContactUniqueKey", 1, IsUnique = true)]
        [Required, MaxLength(10)]
        public string Contact { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [ForeignKey("ClassDivision")]
        public int Classid { get; set; }

        public virtual ClassDivision ClassDivision { get; set; }
    }
}