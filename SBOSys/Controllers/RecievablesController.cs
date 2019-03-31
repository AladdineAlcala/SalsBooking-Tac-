using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
   
    public class RecievablesController : Controller
    {
        private PegasusEntities _dbEntities;

        private TransRecievablesViewModel tr=new TransRecievablesViewModel();
        public RecievablesController()
        {
            _dbEntities=new PegasusEntities();
        }

        // GET: Recievables
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult GetPaymentListView()
        {

            return PartialView("GetPaymentListView");

        }



        [HttpGet]
        public ActionResult LoadBookingsByCustomer(int cusId)
        {
            List<TransRecievablesViewModel> recievablesList=new List<TransRecievablesViewModel>();

            try
            {

                recievablesList = tr.GetAllRecievables().Where(x => x.cusId == cusId).ToList();

                // recievablesList = (from r in list where r.cusId == cusId select r) as List<TransRecievablesViewModel>;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {data= recievablesList }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
           _dbEntities.Dispose();
        }
    }
}