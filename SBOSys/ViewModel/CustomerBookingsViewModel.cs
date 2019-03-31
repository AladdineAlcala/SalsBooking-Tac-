using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.Models;
using System.ComponentModel.DataAnnotations;
using SBOSys.HtmlHelperClass;

namespace SBOSys.ViewModel
{
    public class CustomerBookingsViewModel
    {
        public int transId { get; set; }
        public int? cusId { get; set; }
        public string cusfullname { get; set; }
        public string occasion { get; set; }
        public string venue { get; set; }

        [DisplayFormat(DataFormatString = "{0: MMM-dd-yyyy hh:mm}")]
        public DateTime? bookdatetime { get; set; }
        public string package { get; set; }
        public decimal packageDue { get; set; }
        public bool isServe { get; set; }

        private BookingPaymentsViewModel bookingPayments = new BookingPaymentsViewModel();
        public IEnumerable<CustomerBookingsViewModel> GetCusBookings()
        {
            var _dbEntities=new PegasusEntities();


        List<CustomerBookingsViewModel> lst=new List<CustomerBookingsViewModel>();

            try
            {
                List<Booking> listbookings = new List<Booking>();
                listbookings = (from b in _dbEntities.Bookings select b).ToList();

                lst=(from l in listbookings
                    select new CustomerBookingsViewModel()
                    {
                        transId = l.trn_Id,
                        cusId = l.c_Id,
                        cusfullname= Utilities.getfullname_nonreverse(l.Customer.lastname, l.Customer.firstname, l.Customer.middle),
                        occasion = l.occasion,
                        venue = l.venue,
                        bookdatetime = l.startdate,
                        package = l.Package.p_descripton,
                        packageDue = bookingPayments.Get_TotalAmountBook(l.trn_Id),
                        isServe =Convert.ToBoolean(l.serve_stat)

                    }).ToList();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return lst;

        }
    }
}