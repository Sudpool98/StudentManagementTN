using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Address { get; set; }
        public int Classid { get; set; }
        public virtual ClassDivision ClassDivision { get; set; }
    }
}