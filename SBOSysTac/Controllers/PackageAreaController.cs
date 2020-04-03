using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSysTac.Models;
using SBOSysTac.ViewModel;

namespace SBOSysTac.Controllers
{
    public class PackageAreaController : Controller
    {
        // GET: PackageArea
        private PegasusEntities _dbcontext;
        private PackageAreaLocationViewModel packageAreaLocation=new PackageAreaLocationViewModel();

        public PackageAreaController()
        {
            _dbcontext=new PegasusEntities();

        }

        public ActionResult GetAreas(string query)
        {
            var areaList = packageAreaLocation.GetSelect2AreaViewModels().Where(x =>x.text.ToLower().Contains(query.ToLower())).ToList();

            return Json(new {areaList}, JsonRequestBehavior.AllowGet);

        }


        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }

    }
}