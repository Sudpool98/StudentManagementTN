using StudentManagementTN.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementTN.Controllers
{
    public class PrincipalsController : Controller
    {
        private StudentManagementDBContext db = new StudentManagementDBContext();

        public ActionResult Portal()
        {
            var students = db.Students.Include(s => s.ClassDivision).Include(s => s.EduStatus);
            List<string> excellentlist = new List<string>();
            List<string> averagelist = new List<string>();
            List<string> poorlist = new List<string>();
            foreach (var student in students)
            {
                if(student.EduStatus.Rank == 1)
                    excellentlist.Add(student.ClassDivision.Combined);
                if (student.EduStatus.Rank == 3)
                    averagelist.Add(student.ClassDivision.Combined);
                if (student.EduStatus.Rank == 4)
                    poorlist.Add(student.ClassDivision.Combined);
            }
            ViewBag.Excellent = excellentlist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            ViewBag.Average = averagelist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            ViewBag.Poor = poorlist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}