using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class DiscountController : Controller
    {


        private PegasusEntities _dbEntities;
        private DiscountCodeDetailsViewModel dc=new DiscountCodeDetailsViewModel();
        public DiscountController()
        {
            _dbEntities=new PegasusEntities();
        }
        // GET: Discount
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateNewDiscount()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewDiscount(DiscountCodeDetailsViewModel newdiscountviewmodel)
        {
            if (!ModelState.IsValid) return PartialView("Add_PaymentPartialView", newdiscountviewmodel);

            try
            {
                var newdiscount = new Discount()
                {
                    discCode = newdiscountviewmodel.discCode,
                    disctype = newdiscountviewmodel.disctype,
                    discount1 = newdiscountviewmodel.discount_amt,
                    discStartdate = newdiscountviewmodel.discStartdate,
                    discEnddate = newdiscountviewmodel.discEnddate

                };

                _dbEntities.Discounts.Add(newdiscount);
                _dbEntities.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {success=true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDiscountList()
        {

            List<DiscountCodeDetailsViewModel> discountslist =new List<DiscountCodeDetailsViewModel>();
            discountslist=dc.getAllListofDiscounts().ToList();

            return Json(new {data= discountslist }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveDiscount(int discountId)
        {
            try
            {
                Discount deletedItem = _dbEntities.Discounts.Find(discountId);

                if (deletedItem != null)
                {
                    _dbEntities.Discounts.Remove(deletedItem);
                    _dbEntities.SaveChanges();
                }
              

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {success=true }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _dbEntities.Dispose();
        }
    }
}