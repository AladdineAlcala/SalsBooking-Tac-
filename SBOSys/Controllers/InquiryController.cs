using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SBOSys.Models;
using SBOSys.ViewModel;
using System.Data.Entity;

namespace SBOSys.Controllers
{
    public class InquiryController : Controller
    {
        // GET: Inquiry

        private PegasusEntities _dbEntities;
        private CustomerBookingsViewModel cb=new CustomerBookingsViewModel();
        public InquiryController()
        {
            _dbEntities=new PegasusEntities();
        }
        [HttpGet]
        public ActionResult CustomerBookingHistory()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetCustomerBookings()
        {


            return PartialView("CustomerBookingsPartialView");
        }


        [HttpGet]
       // [ActionName("LoadBookingsByCustomer")]
        public ActionResult LoadBookingsByCustomer(int cusId)
        {
            List<CustomerBookingsViewModel> cusbookings = new List<CustomerBookingsViewModel>();

            try
            {
                cusbookings = cb.GetCusBookings().Where(x => x.cusId == cusId).ToList();


            }
            catch (Exception)
            {

                throw;
            }
            Thread.Sleep(3000);

            return Json(new { data = cusbookings }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookingScheduleIndex()
        {

            return View();

        }

        [HttpGet]
        public ActionResult GetBookingScheduleFilter()
        {
            return PartialView("GetBookingScheduleFilter_Partial");
        }

        public ActionResult GetBookingScheduleDataTableList(DateTime startDate, DateTime endDate,string filter)
        {

            List<CustomerBookingsViewModel> cusbookingSchedule = new List<CustomerBookingsViewModel>();

            try
            {

         
                if (filter == "serve")
                {
                    cusbookingSchedule = cb.GetCusBookings()
                        .Where(d => DbFunctions.TruncateTime(d.bookdatetime) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(d.bookdatetime) <= DbFunctions.TruncateTime(endDate) &&  d.isServe==true).ToList();


                }
                else if (filter == "unserve")
                {
                    cusbookingSchedule = cb.GetCusBookings()
                        .Where(d => DbFunctions.TruncateTime(d.bookdatetime) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(d.bookdatetime) <= DbFunctions.TruncateTime(endDate) && d.isServe==false).ToList();

                }
                else
                {
                    cusbookingSchedule = cb.GetCusBookings()
                        .Where(d => DbFunctions.TruncateTime(d.bookdatetime) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(d.bookdatetime) <= DbFunctions.TruncateTime(endDate)).ToList();

                }


            }
            catch (Exception)
            {

                throw;
            }
            Thread.Sleep(3000);


            return Json(new {data= cusbookingSchedule }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AccountsRecievableReportIndex()
        {
            return View();
        }

        public ActionResult SalesSummaryIndex()
        {
            return View();
        }

        public ActionResult RecievableprintOption(int? cusId,string selprintopt)
        {
            var pOption = new PrintOptionViewModel()
            {
                Id = Convert.ToInt32(cusId),
                selPrintOpt = "accnrecievesummary"

            };


            return View("~/Views/Shared/ReportContainer.cshtml",pOption);
        }


        public ActionResult GetSalesSummary()
        {

            return PartialView("_SalesSummaryListPartial");
        }

        public ActionResult GetSalesSummaryData(DateTime startDate, DateTime endDate)
        {
            IEnumerable<SalesSummaryViewModel> salessummarylist=new List<SalesSummaryViewModel>();
            var s=new SalesSummaryViewModel();
            try
            {
                salessummarylist=s.GetSalesSummary().ToList().Where(d => DbFunctions.TruncateTime(d.dateTrans) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(d.dateTrans) <= DbFunctions.TruncateTime(endDate)).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return Json(new {data=salessummarylist}, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CashSalesReportIndex()
        {

            return View();
        }

        public ActionResult CashReportView(DateTime datefrom,DateTime dateto)
        {
            var pOption = new PrintOptionViewModel()
            {
               
                selPrintOpt = "cashreport",
                dateFrom = Convert.ToDateTime(datefrom),
                dateTo = Convert.ToDateTime(dateto)

            };

            return View("~/Views/Shared/ReportContainer.cshtml", pOption);
        }
        protected override void Dispose(bool disposing)
        {
            _dbEntities.Dispose();
        }
    }
}