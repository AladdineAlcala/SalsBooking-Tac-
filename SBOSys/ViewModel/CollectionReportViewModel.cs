using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class CollectionReportViewModel
    {
        public int transId { get; set; }
        public DateTime payDate { get; set; }
        public string customer { get; set; }
        public string occassion { get; set; }
        public int noofPax { get; set; }
        public string reference { get; set; }
        public string paymeans { get; set; }
        public string checkdetails { get; set; }
        public string notes { get; set; }
        public decimal AmountperPax { get; set; }
        public DateTime eventDate { get; set; }
        public Decimal PayAmt { get; set; }
        public string recievedBy { get; set; }


       private PegasusEntities dbEntities=new PegasusEntities();

        public IEnumerable<CollectionReportViewModel> GetAllCollection()
        {
            List<CollectionReportViewModel> list=new List<CollectionReportViewModel>();


            try
            {

                var bookinglist = (from b in dbEntities.Bookings select b).ToList();

                list = (from item in bookinglist
                        join p in dbEntities.Payments on item.trn_Id equals p.trn_Id
                        join pp in dbEntities.Packages on item.p_id equals pp.p_id
                        join c in dbEntities.Customers on item.c_Id equals c.c_Id 
                        select new CollectionReportViewModel()
                        {
                            transId = item.trn_Id,
                            payDate = Convert.ToDateTime(p.dateofPayment),
                            customer = Utilities.getfullname_nonreverse(c.lastname,c.firstname,c.middle),
                            occassion = item.occasion,
                            eventDate = Convert.ToDateTime(item.startdate),
                            reference = p.particular,
                            paymeans = p.pay_means,
                            checkdetails = p.checkNo,
                            notes = p.notes,
                            noofPax = Convert.ToInt32(item.noofperson),
                            AmountperPax =Convert.ToDecimal(pp.p_amountPax),
                            PayAmt = Convert.ToDecimal(p.amtPay)
                          
                       
                        }).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return list;
        }

    }
}