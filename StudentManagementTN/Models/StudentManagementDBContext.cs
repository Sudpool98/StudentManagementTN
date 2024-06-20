using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace StudentManagementTN.Models
{
    public class StudentManagementDBContext : DbContext
    {
        protected StudentManagementDBContext() : base("StudentManagementConnection")
        {

        }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<ClassDivision> ClassDivisions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EduStatus> EduStatuses { get; set; }
    }
}