using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSysTac.Models;

namespace SBOSysTac.ViewModel
{
    public class BookingPaymentsViewModel
    {
        public int transId { get; set; }
        public decimal t_amtBooking { get; set; }
        public decimal t_addons { get; set; }
        public decimal cateringdiscount { get; set; }
        public decimal generaldiscount { get; set; }
        public decimal locationextcharge { get; set; }
        public BookingsViewModel Bookings { get; set; }
        public IEnumerable<PaymentsViewModel> PaymentList { get; set; }



        private TransactionDetailsViewModel transdetails=new TransactionDetailsViewModel();

        //get totalPackageAmount
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

                // get transaction discount 
                totalAmount = totalPackage_Amount + addons;

                var discount = this.getBookingTransDiscount(transId, totalAmount);

                if (discount > 0)
                {

                    totalAmount = totalAmount - discount;

                    //totalAmount = (totalPackage_Amount + addons) - discount;
                }


                var hasLocationExtendedCharge = transdetails.Get_extendedAmountLoc(transId);

                if (hasLocationExtendedCharge > 0)
                {
                    totalAmount = totalAmount + (hasLocationExtendedCharge * Convert.ToInt32(b.noofperson));
                }

                var hasCateringdiscounted = transdetails.getCateringdiscount(Convert.ToInt32(b.noofperson));


                if (hasCateringdiscounted > 0)
                {
                    totalAmount = totalAmount - (hasCateringdiscounted * Convert.ToInt32(b.noofperson));
                }


                

                //var belowminpax = transdetails.GetBelowMinPaxAmount(Convert.ToInt32(b.noofperson));

                //if (belowminpax > 0)
                //{
                //    var belowminpaxAmt = belowminpax * Convert.ToInt32(b.noofperson);

                //    totalAmount = totalAmount + belowminpaxAmt;
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



            return totalAmount;
        }

      


        public decimal GetCateringDiscount(int transid)
        {
            var _dbcontext = new PegasusEntities();
            Booking bookings = new Booking();
            decimal discountedAmount = 0;


            bookings = _dbcontext.Bookings.FirstOrDefault(x => x.trn_Id == transid);

            if (bookings != null)
            {
                var noofpax = bookings.noofperson;

                var amount = transdetails.getCateringdiscount(Convert.ToInt32(noofpax));
                discountedAmount =Convert.ToDecimal(amount * noofpax);
            }

            return discountedAmount;

        }

        public decimal getTotalAddons(int transId)
        {
            var _dbcontext = new PegasusEntities();
            var addonsList = _dbcontext.BookingAddons.Where(x => x.trn_Id == transId).ToList();

            
               return(Convert.ToDecimal(addonsList.Sum(x => x.AddonAmount)));
            
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


        public decimal getBookingTransDiscount(int transId,decimal amountdue)
        {
            decimal discountedAmount = 0;
            var _dbcontext = new PegasusEntities();


            try
            {
                var discount = (from bd in _dbcontext.Book_Discount
                    join tdisc in _dbcontext.Discounts on bd.disc_Id equals tdisc.disc_Id
                    select new
                    {
                        trans_Id = bd.trn_Id,
                        discountType = tdisc.disctype,
                        discount = tdisc.discount1
                    }).ToList();



                var discountDetails = discount.FirstOrDefault(x => x.trans_Id == transId);

                if (discountDetails != null)
                {
                    //decimal discAmt = 0;

                    if (discountDetails.discountType == "percentage")
                    {

                        var percentagedisc = discountDetails.discount / 100;

                        discountedAmount = amountdue * Convert.ToDecimal(percentagedisc);

                    }
                    else
                    {
                        discountedAmount = Convert.ToDecimal(discountDetails.discount);
                    }


                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Convert.ToDecimal(discountedAmount);

        }
    }
}