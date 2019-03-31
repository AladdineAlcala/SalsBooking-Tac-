using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (User.IsInRole("admin")||(User.IsInRole("superadmin")))
            {
                return RedirectToAction("DashBoard", "Home");
            }

            return RedirectToAction("Index", "Events");
        }

        public ActionResult DashBoard()
        {
            ViewBag.FormTitle = "DashBoard";

            IndexViewModel indexView = new IndexViewModel();


            return View(indexView);
        }

    }
}