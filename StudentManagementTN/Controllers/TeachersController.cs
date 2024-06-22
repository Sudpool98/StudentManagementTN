using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagementTN.Models;

namespace StudentManagementTN.Controllers
{
    public class TeachersController : Controller
    {
        private StudentManagementDBContext db = new StudentManagementDBContext();

        public ActionResult Login()     //Same logic as Principal Login
        {
            try
            {
                if ((int)Session["logged_in"] == 1)     
                    return RedirectToAction("Logout");

                return View();
            }
            catch (NullReferenceException) { return View(); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TeacherLogin teacher)      //Same logic as Principal Login
        {
            var L1 = from teach in db.Teachers orderby teach.Id select teach;    //Get All

            if (ModelState.IsValid)
            {
                foreach (Teacher teacherlist in L1)
                {
                    if (teacherlist.Username == teacher.Username && teacherlist.Password == teacher.Password)
                    {
                        Session["logged_in"] = 1;
                        Session["id"] = teacherlist.Id;
                        Session["user"] = 2;    //user implies type of user, 2 is Teacher.

                        return RedirectToAction("Portal");
                    }

                }
                ModelState.AddModelError("Password", "Username or Password Incorrect!");
            }
            return View(teacher);
        }

        public ActionResult Portal()
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 2)
                {
                    Teacher teacher = db.Teachers.Find((int)Session["id"]);
                    if (teacher == null)
                    {
                        return HttpNotFound();
                    }
                    return View(teacher);
                }
                return RedirectToAction("Login");
            }
            catch (NullReferenceException) { return RedirectToAction("Login"); }
        }

        public ActionResult Logout()
        {
            Session["logged_in"] = 0;
            Session["id"] = 0;
            Session["user"] = 0;

            return RedirectToAction("Login");
        }

        //All Actions below are part of Teacher Management which can only be accessed by Principal, hence Principal login is checked.
        // GET: Teachers
        public ActionResult Index()
        {
            try     
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    var teachers = db.Teachers.Include(t => t.ClassDivision);
                    return View(teachers.ToList());
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Teacher teacher = db.Teachers.Find(id);
                    if (teacher == null)
                    {
                        return HttpNotFound();
                    }
                    return View(teacher);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined");
                    return View();
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Name,Contact,Address,Classid")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", teacher.Classid);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Teacher teacher = db.Teachers.Find(id);
                    if (teacher == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", teacher.Classid);
                    return View(teacher);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Name,Contact,Address,Classid")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", teacher.Classid);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Teacher teacher = db.Teachers.Find(id);
                    if (teacher == null)
                    {
                        return HttpNotFound();
                    }
                    return View(teacher);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
