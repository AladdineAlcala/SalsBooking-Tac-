using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class TransactionDetailsViewModel
    {
        public int transactionId { get; set; }
        public Customer Customer { get; set; }
        public Booking Booking_Trans { get; set; }
        public Package Package_Trans { get; set; }
        public Decimal TotaAddons { get; set; }
        public Decimal book_discounts { get; set; }
        public Decimal TotaBelowMinPax { get; set; }
        public Decimal TotaDp { get; set; }
        public Decimal extLocAmount { get; set; }
        public Decimal Fullpaymnt { get; set; }


        private PegasusEntities _dbEntities = new PegasusEntities();

        public IEnumerable<TransactionDetailsViewModel> GetTransactionDetails()
        {

            List<TransactionDetailsViewModel> _list = new List<TransactionDetailsViewModel>();


            // _dbEntities.Configuration.ProxyCreationEnabled = false;

            try
            {


                _list = (from b in _dbEntities.Bookings
                    join c in _dbEntities.Customers on b.c_Id equals c.c_Id
                    join p in _dbEntities.Packages on b.p_id equals p.p_id
                    select new TransactionDetailsViewModel()
                    {
                        transactionId = b.trn_Id,
                        Booking_Trans = b,
                        Customer = c,
                        Package_Trans = p

                    }).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return _list;

        }

        public decimal GetTotalBookingAmount(int transId)
        {
            decimal totalpackage = 0;
            decimal totalAddons = 0;
            decimal package = 0;

            try
            {
                var booktrans = _dbEntities.Bookings.FirstOrDefault(t => t.trn_Id == transId);
                if (booktrans != null)
                {
                    var pax = booktrans.noofperson;
                    var heads = booktrans.Package.p_amountPax;
                    package = Convert.ToDecimal(heads) * Convert.ToDecimal(pax);

                }

                var addonslist = _dbEntities.BookingAddons.Where(x => x.trn_Id == transId).ToList();

                totalAddons = addonslist.Sum(y => Convert.ToDecimal(y.AddonAmount));

                totalpackage = package + totalAddons;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return totalpackage;
        }



        public decimal GetBelowMinPaxAmount(int noofPax)
        {

            decimal amt_added = 0;
            _dbEntities.Configuration.ProxyCreationEnabled = false;
            //List<PackagesRangeBelowMin> packagerangebelowmin=new List<PackagesRangeBelowMin>();
            PackagesRangeBelowMin packagerangebelowmin = new PackagesRangeBelowMin();

            packagerangebelowmin =
                _dbEntities.PackagesRangeBelowMins.FirstOrDefault(x => x.pMax >= noofPax && x.pMin <= noofPax);


            if (packagerangebelowmin != null)
            {
                amt_added = Convert.ToDecimal(packagerangebelowmin.Amt_added);
            }

            return amt_added;
        }

        public decimal GetTotalDownPayment(int transId)
        {
            decimal dpAmount = 0;

            try
            {

                dpAmount = Convert.ToDecimal(_dbEntities.Payments.Where(p => p.trn_Id == transId && p.payType == 0)
                    .Sum(x => x.amtPay));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return dpAmount;

        }


        public decimal Get_extendedAmountLoc(int transId)
        {
            decimal extAmt = 0;

            try
            {
                var list = (from b in _dbEntities.Bookings
                    join p in _dbEntities.Packages on b.p_id equals p.p_id
                    join pa in _dbEntities.PackageAreaCoverages on p.p_id equals pa.p_id
                    where b.trn_Id == transId
                    select new
                    {
                        extLocAmount = pa.ext_amount

                    }).FirstOrDefault();


                if (list != null)
                {
                    extAmt = Convert.ToDecimal(list.extLocAmount);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return extAmt;
        }

        public decimal GetFullPayment(int transId)
        {
            decimal fp = 0;

            try
            {
                fp = Convert.ToDecimal(_dbEntities.Payments.Where(p => p.trn_Id == transId)
                    .Sum(x => x.amtPay));

            }
            catch (Exception)
            {

                throw;
            }

            return fp;
        }


        public decimal GetTotalPaymentByTrans(int transId)
        {
            decimal totaPayment = 0;
            try
            {
                totaPayment = Convert.ToDecimal(_dbEntities.Payments.Where(t => t.trn_Id == transId)
                    .Sum(x => x.amtPay));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return totaPayment;
        }


        public decimal Get_bookingDiscountbyTrans(int transId, decimal subtotal)
        {
            decimal totaldisc = 0;

            try
            {

                var hasdiscount = _dbEntities.Book_Discount.FirstOrDefault(x => x.trn_Id == transId);

                if (hasdiscount != null)
                {
                    var bookdiscount = _dbEntities.Discounts.FirstOrDefault(d => d.disc_Id == hasdiscount.disc_Id);

                    if (bookdiscount != null)
                    {
                        if (bookdiscount.disctype == "percentage")
                        {
                            var percentage = bookdiscount.discount1 / 100;

                            totaldisc = subtotal * Convert.ToDecimal(percentage);

                        }
                        else
                        {
                            totaldisc = Convert.ToDecimal(bookdiscount.discount1);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return totaldisc;
        }


        public int get_totalNoofMainMenus_on_booking(int transId)
        {
            int i = 0;

            try
            {
                var noofMainMenu = (from b in _dbEntities.Bookings
                    join pb in _dbEntities.PackageBodies on b.p_id equals pb.p_id
                    where b.trn_Id == transId
                    select new
                    {
                        noofmainmenu = pb.mainCourse
                    }).FirstOrDefault();

                if (noofMainMenu != null) i = Convert.ToInt32(noofMainMenu.noofmainmenu);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }

            return i;
        }


        public bool isSelectedMenuMainCourse(string menuId)
        {

            var courseMenu = (from m in _dbEntities.Menus
                join cc in _dbEntities.CourseCategories on m.CourserId equals cc.CourserId
                where m.menuid == menuId
                select new
                {
                    ismainMenu = cc.Main_Bol
                }).FirstOrDefault();



            return Convert.ToBoolean(courseMenu.ismainMenu);
        }

    }
}