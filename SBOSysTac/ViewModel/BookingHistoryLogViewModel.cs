using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBOSysTac.ViewModel
{
    public class BookingHistoryLogViewModel
    {

        public int TransId { get; set; }
        public DateTime Logdate { get; set; }
        public string Log_operation { get; set; }
        public List<BookingEvents> BookingEventList { get; set; }

        public BookingHistoryLogViewModel()
        {
            BookingEventList = new List<BookingEvents>();
        }

    }


    public class BookingEvents
    {
      
        public int eventNo { get; set; }
        public int transId { get; set; }
        public string eventDetails { get; set; }
    }
}