using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class BookingPaymentsViewModel
    {
        public int transId { get; set; }
        public BookingsViewModel Bookings { get; set; }
        public decimal t_amtBooking { get; set; }
        public IEnumerable<PaymentsViewModel> PaymentList { get; set; }

        public decimal Get_TotalAmountBook(int transId)
        {
            decimal totalAmount = 0;
            var  _dbcontext=new PegasusEntities();

           Booking b=new Booking();
           List<BookingAddon> addonsList=new List<BookingAddon>();


            try
            {
                decimal totalPackage_Amount = 0;
                decimal addons = 0;;

                b = _dbcontext.Bookings.ToList().FirstOrDefault(x => x.trn_Id == transId);


                if (b != null)
                {
                    totalPackage_Amount = Convert.ToDecimal(b.Package.p_amountPax) * Convert.ToInt32(b.noofperson);

                }

                addonsList = _dbcontext.BookingAddons.Where(x => x.trn_Id == transId).ToList();

                if (addonsList.Count > 0)
                {
                    addons = Convert.ToDecimal(addonsList.Sum(x => x.AddonAmount));
                }


                totalAmount = totalPackage_Amount + addons;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return totalAmount;
        }

        public IEnumerable<PaymentsViewModel> GetPaymentDetaiilsBooking(int transId)
        {

            var _dbcontext=new PegasusEntities();
            List<PaymentsViewModel> paylist=new List<PaymentsViewModel>();

            try
            {

                paylist = (from p in _dbcontext.Payments
                    where p.trn_Id == transId
                    select new PaymentsViewModel()
                    {
                        PayNo = p.payNo,
                        transId = p.trn_Id,
                        dateofPayment = p.dateofPayment,
                        particular = p.particular,
                        payType = p.payType,
                        amtPay = p.amtPay,
                        pay_means = p.pay_means,
                        notes = p.notes


                    }).OrderBy(x=>x.PayNo).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return paylist.ToList();
        }
    }
}