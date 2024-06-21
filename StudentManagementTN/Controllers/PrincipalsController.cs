using StudentManagementTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementTN.Controllers
{
    public class PrincipalsController : Controller
    {
        private StudentManagementDBContext db = new StudentManagementDBContext();

        // GET: Principals
        public ActionResult Portal()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}