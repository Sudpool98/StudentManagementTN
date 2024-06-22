using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementTN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if ((int)Session["logged_in"] == 1)     //If Home Page is accessed while already logged in, logout.
                {
                    Session["logged_in"] = 0;
                    Session["id"] = 0;
                    Session["user"] = 0;
                }
                return View();
            }
            catch (NullReferenceException) { return View(); }
        }
    }
}