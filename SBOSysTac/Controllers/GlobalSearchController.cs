using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSysTac.ViewModel;

namespace SBOSysTac.Controllers
{
    public class GlobalSearchController : Controller
    {

        public ActionResult SearchIndex(string globalFilter, string globalsearchString, int? page)
        {


            if (globalsearchString != null)
            {
                page = 1;
            }
            else
            {
                globalsearchString = globalFilter;

            }

            ViewBag.GlobalFilter = globalsearchString;




            return View();

        }


    }
}