using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSysTac.Models;

namespace SBOSysTac.ViewModel
{
    public class CustomerBookingHistoryViewModel
    {
        public int TransId { get; set; }
        public Customer Customer { get; set; }
    }
}