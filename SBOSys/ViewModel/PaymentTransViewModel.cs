using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBOSys.ViewModel
{
    public class PaymentTransViewModel
    {
        public int transId { get; set; }
        public TransRecievablesViewModel TransRecievables { get; set; }
        public IEnumerable<PaymentsViewModel> Payments { get; set; }



    }
}