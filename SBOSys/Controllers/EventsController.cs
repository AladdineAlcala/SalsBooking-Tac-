using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events

        private EventsViewModel events=new EventsViewModel();
        private CustomerBookingsViewModel cb=new CustomerBookingsViewModel();
        public ActionResult Index()
        {
            ViewBag.FormTitle = "Calendar Events Index";

            return View();
        }
        

        [HttpGet]
        //[OutputCache(Duration = 60,VaryByParam = "none",Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetAllEvents()
        {
            List<EventsViewModel> listofevents=new List<EventsViewModel>();

            listofevents = events.MixedEvents().ToList();

            return Json(listofevents,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get_AllBookingEvents()
        {
            List<EventsViewModel> listofevents = new List<EventsViewModel>();

            listofevents = events.GetAllBookingEvents().ToList();

            return Json(listofevents, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get_AllReservationEvents()
        {
            List<EventsViewModel> listofevents = new List<EventsViewModel>();

            listofevents = events.GetAllReservationEvents().ToList();

            return Json(listofevents, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetAllEventsDay(DateTime? eventdate)
        {
          //  List<CustomerBookingsViewModel> custbookingList=new List<CustomerBookingsViewModel>();
          
                EventSelectionViewModel eventsel = new EventSelectionViewModel()
                {
                    eventdateselected =Convert.ToDateTime(eventdate),
                    eventlist = cb.GetCusBookings().Where(d => d.bookdatetime.Value.Date == eventdate.Value.Date).OrderByDescending(x => x.bookdatetime.Value.ToShortTimeString()).ToList()
                 };

             
         

            return PartialView("GetEventsDay", eventsel);
        }

    }
}