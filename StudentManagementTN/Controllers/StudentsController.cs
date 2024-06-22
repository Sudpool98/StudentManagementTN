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
    public class StudentsController : Controller
    {
        private StudentManagementDBContext db = new StudentManagementDBContext();

        public ActionResult Login()
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
        public ActionResult Login(StudentLogin student)      //Same logic as Principal Login
        {
            var L1 = from stu in db.Students orderby stu.Id select stu;    //Get All

            if (ModelState.IsValid)
            {
                foreach (Student studentlist in L1)
                {
                    if (studentlist.Username == student.Username && studentlist.Password == student.Password)
                    {
                        Session["logged_in"] = 1;
                        Session["id"] = studentlist.Id;
                        Session["user"] = 3;    //user implies type of user, 3 is Student.

                        return RedirectToAction("Portal");
                    }

                }
                ModelState.AddModelError("Password", "Username or Password Incorrect!");
            }
            return View(student);
        }

        public ActionResult Logout()
        {
            Session["logged_in"] = 0;
            Session["id"] = 0;
            Session["user"] = 0;

            return RedirectToAction("Login");
        }

        public ActionResult Portal()
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 3)
                {
                    Student student = db.Students.Find((int)Session["id"]);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);
                }
                return RedirectToAction("Login");
            }
            catch (NullReferenceException) { return RedirectToAction("Login"); }
        }


        //All Actions below are part of Teacher Management which can only be accessed by Principal, hence Principal login is checked.
        // GET: Students
        public ActionResult Index()
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    var students = db.Students.Include(s => s.ClassDivision).Include(s => s.EduStatus);
                    return View(students.ToList());
                }
                return RedirectToAction("Login","Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // GET: Students/Details/5
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
                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            try
            {
                if ((int)Session["logged_in"] == 1 && (int)Session["user"] == 1)
                {
                    ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined");
                    ViewBag.Edustatusid = new SelectList(db.EduStatuses, "Id", "Status");
                    return View();
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Name,Address,Edustatusid,Classid")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", student.Classid);
            ViewBag.Edustatusid = new SelectList(db.EduStatuses, "Id", "Status", student.Edustatusid);
            return View(student);
        }

        // GET: Students/Edit/5
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
                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", student.Classid);
                    ViewBag.Edustatusid = new SelectList(db.EduStatuses, "Id", "Status", student.Edustatusid);
                    return View(student);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Name,Address,Edustatusid,Classid")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Classid = new SelectList(db.ClassDivisions, "Id", "Combined", student.Classid);
            ViewBag.Edustatusid = new SelectList(db.EduStatuses, "Id", "Status", student.Edustatusid);
            return View(student);
        }

        // GET: Students/Delete/5
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
                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);
                }
                return RedirectToAction("Login", "Principals");
            }
            catch (NullReferenceException) { return RedirectToAction("Login", "Principals"); }
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
