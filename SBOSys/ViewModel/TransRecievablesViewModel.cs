using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class TransRecievablesViewModel
    {
        public int transId { get; set; }
        public int cusId { get; set; }
        public DateTime transDate { get; set; }
        public DateTime bookdatetime { get; set; }
        public string cusfullname { get; set; }
        public string occasion { get; set; }
        public string venue { get; set; }
        public string packagedetails { get; set; }
        public decimal? p_amountperPax { get; set; }
        public decimal totalPackageAmount { get; set; }
        public decimal totalPayment { get; set; }
        public decimal balance { get; set; }


        private PegasusEntities db_entities = new PegasusEntities();
        private BookingPaymentsViewModel bookingPayments = new BookingPaymentsViewModel();
        private TransactionDetailsViewModel transdetails = new TransactionDetailsViewModel();


        public IEnumerable<TransRecievablesViewModel> GetAllRecievables()
        {
            List<TransRecievablesViewModel> recievable_list=new List<TransRecievablesViewModel>();
            try
            {

                IEnumerable<Booking> bookings=(from booking in db_entities.Bookings select booking).ToList();

                recievable_list = (from b in bookings
                    select new TransRecievablesViewModel()
                    {


                        transId = b.trn_Id,
                        transDate = Convert.ToDateTime(b.transdate),
                        bookdatetime = Convert.ToDateTime(b.startdate),
                        cusId = Convert.ToInt32(b.Customer.c_Id),
                        cusfullname = Utilities.getfullname(b.Customer.lastname, b.Customer.firstname,
                            b.Customer.middle),
                        occasion = b.occasion,
                        venue=b.venue,
                        packagedetails = b.Package.p_descripton,
                        p_amountperPax = b.Package.p_amountPax,
                        totalPackageAmount = bookingPayments.Get_TotalAmountBook(b.trn_Id),
                        totalPayment = transdetails.GetTotalPaymentByTrans(b.trn_Id),
                        balance = bookingPayments.Get_TotalAmountBook(b.trn_Id) -
                                  transdetails.GetTotalPaymentByTrans(b.trn_Id)

                    }).Where(x => x.balance > 0).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



            return recievable_list.ToList();
        }


        public IEnumerable<TransRecievablesViewModel> GetRecieveDetails()
        {
            List<TransRecievablesViewModel> recievable_list = new List<TransRecievablesViewModel>();
            try
            {
                IEnumerable<Booking> bookings = (from booking in db_entities.Bookings select booking).ToList();

                recievable_list = (from b in bookings
                    select new TransRecievablesViewModel()
                    {


                        transId = b.trn_Id,
                        transDate = Convert.ToDateTime(b.transdate),
                        bookdatetime = Convert.ToDateTime(b.startdate),
                        cusId = Convert.ToInt32(b.Customer.c_Id),
                       
                        //cusfullname = Utilities.getfullname(b.Customer.lastname, b.Customer.firstname,
                        //    b.Customer.middle),
                        //occasion = b.occasion,
                        //venue = b.venue,
                        //packagedetails = b.Package.p_descripton,
                        //p_amountperPax = b.Package.p_amountPax,
                        totalPackageAmount = bookingPayments.Get_TotalAmountBook(b.trn_Id)

                    }).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return recievable_list.ToList();
        }

        

    }
}