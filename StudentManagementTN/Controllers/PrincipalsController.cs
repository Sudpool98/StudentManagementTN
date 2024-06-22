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

        public ActionResult Login()
        {
            try
            {
                if ((int)Session["logged_in"] == 1)     //If Login Page is accessed while already logged in, redirect to logout action.
                    return RedirectToAction("Logout");  

                return View();
            }
            catch (NullReferenceException) { return View(); }    

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Principal principal)
        {
            var L1 = from pcpl in db.Principals orderby pcpl.Id select pcpl;    //Get All

            if (ModelState.IsValid)
            {
                foreach (Principal principallist in L1)
                {
                    if (principallist.Username == principal.Username && principallist.Password == principal.Password)   
                    {
                        Session["logged_in"] = 1;
                        Session["id"] = principal.Id;
                        Session["user"] = 1;    //user implies type of user, 1 is Principal.

                        return RedirectToAction("Portal");
                    }

                }
                ModelState.AddModelError("password", "Username or Password Incorrect!");
            }
            return View(principal);
        }

        public ActionResult Logout()
        {
            Session["logged_in"] = 0;
            Session["id"] = 0;
            Session["user"] = 0;

            return RedirectToAction("Login");
        }

        //Retreives content for Principal Portal and returns the view
        public ActionResult Portal()
        {
            try     //Before first login Session is NULL, and will throw exception when Session value is checked.
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)    //Only show Principal portal if  Principal is logged in, else redirect to login page
                {
                    var students = db.Students.Include(s => s.ClassDivision).Include(s => s.EduStatus); //Get list of ALL Students
                    List<string> excellentlist = new List<string>();
                    List<string> averagelist = new List<string>();
                    List<string> poorlist = new List<string>();

                    //Traverse list of students and store Combined(Class+Division) of students with Excellent, Average and Poor status separately.
                    foreach (var student in students)
                    {
                        if (student.EduStatus.Rank == 1)
                            excellentlist.Add(student.ClassDivision.Combined);
                        if (student.EduStatus.Rank == 3)
                            averagelist.Add(student.ClassDivision.Combined);
                        if (student.EduStatus.Rank == 4)
                            poorlist.Add(student.ClassDivision.Combined);
                    }

                    //Applying groupby on the lists to get the repeating elements together
                    //Then "Order by Count Descending" in each group, hence bringing the group with most recurring element at the top.
                    ViewBag.Excellent = excellentlist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                    ViewBag.Average = averagelist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                    ViewBag.Poor = poorlist.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                    return View();
                }
                return RedirectToAction("Login");
            }
            catch (NullReferenceException) { return RedirectToAction("Login"); }
        }

    }
}