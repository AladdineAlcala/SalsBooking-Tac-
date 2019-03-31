using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class AccnRecieveSummaryViewModel
    {
        public int cusId { get; set; }
        public string cusname { get; set; }
        public int transId { get; set; }
        public DateTime transDate { get; set; }
        public DateTime duedate { get; set; }
        public int daysOdd { get; set; }
        public decimal balance { get; set; }

        public IEnumerable<AccnRecieveSummaryViewModel> GetAllAccnRecieve()
        {
            List<AccnRecieveSummaryViewModel> listAccn=new List<AccnRecieveSummaryViewModel>();
            var _dbentities=new PegasusEntities();
            var bookingPayments = new BookingPaymentsViewModel();
            var transdetails = new TransactionDetailsViewModel();

            try
            {

                var bookings = (from booking in _dbentities.Bookings select booking).ToList();

                listAccn = (from b in bookings let daydue = Convert.ToDateTime(b.transdate).AddDays(30)
                    select new AccnRecieveSummaryViewModel()
                    {
                        cusId =Convert.ToInt32(b.c_Id),
                        cusname = Utilities.getfullname(b.Customer.lastname,b.Customer.firstname,b.Customer.middle),
                        transId = b.trn_Id,
                        transDate =Convert.ToDateTime(b.transdate),
                        duedate = daydue,
                        daysOdd =Convert.ToInt32(DateTime.Now.Subtract(daydue).Days) <0?0: Convert.ToInt32(DateTime.Now.Subtract(daydue).Days),
                        balance = bookingPayments.Get_TotalAmountBook(b.trn_Id) -
                                  transdetails.GetTotalPaymentByTrans(b.trn_Id)

                    }).Where(x=>x.balance>0).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return listAccn;
        }
    }
}