using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class EventsViewModel
    {
        private PegasusEntities _dbEntities=new PegasusEntities();
        private BookingsViewModel bookModel=new BookingsViewModel();
        private  ReservationViewModel resModel=new ReservationViewModel();

        public int EventId { get; set; }
        public Customer eventCustomer { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string eventLocation { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDatetime { get; set; }
        public string eventType { get; set; }
        public bool Allday { get; set; }



        public IEnumerable<EventsViewModel> GetAllBookingEvents()
        {
            List<BookingsViewModel> eventbookings = new List<BookingsViewModel>();

            eventbookings = bookModel.GetListofBookings().ToList();

            return (from e in eventbookings
                    select new EventsViewModel
                    {
                        EventId = e.trn_Id,
                        EventName = e.occasion,
                        EventDescription = "Venue: " + e.venue,
                        StartDateTime = e.startdate,
                        EndDatetime = e.enddate,
                        eventType = "booking",
                        Allday = false
                    }).ToList();


        }

        public IEnumerable<EventsViewModel> GetAllReservationEvents()
        {
            
            List<ReservationViewModel> eventreservations=new List<ReservationViewModel>();

            eventreservations = resModel.GetAll_Reservations().ToList();

            return (from e in eventreservations
                select new EventsViewModel()
                {
                    EventId =Convert.ToInt32(e.reservationId),
                    EventName = e.occasion,
                    
                    EventDescription = "Venue: " + e.eventVenue,
                    StartDateTime = e.reserveDate,
                    EndDatetime = e.reserveDate,
                    eventType = "reservation",
                    Allday = false

                }).ToList();
        }



        public IEnumerable<EventsViewModel> MixedEvents()
        {
            List<EventsViewModel> listofAllEvents=new List<EventsViewModel>();

            listofAllEvents.AddRange(this.GetAllBookingEvents());
            listofAllEvents.AddRange(this.GetAllReservationEvents());

            return listofAllEvents.ToList();

        }
        
    }
}