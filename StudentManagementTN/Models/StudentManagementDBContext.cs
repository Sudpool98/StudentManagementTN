using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class StudentManagementDBContext : DbContext
    {
        protected StudentManagementDBContext() : base("StudentManagementConnection")
        {

        }
    }
}