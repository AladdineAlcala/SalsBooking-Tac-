﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSysTac.HtmlHelperClass;
using SBOSysTac.Models;

namespace SBOSysTac.ViewModel
{
    public class RefundsViewModel
    {
        //public int Ref_Id { get; set; }
        public int TransId { get; set; }
        public int cId { get; set; }
        public bool isCancelled { get; set; }
        public bool hasrefundEntry { get; set; }
        public string CustomerName { get; set; }
        public DateTime Eventdate { get; set; }
        public string EventLocation { get; set; }
        public decimal PaymemntAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public string Status { get; set; }

       
        private PegasusEntities dbentities = new PegasusEntities();
        private BookingPaymentsViewModel bookingPayments = new BookingPaymentsViewModel();
    

        public IEnumerable<RefundsViewModel> GetAllRefundsList()
        {

            List<RefundsViewModel> listRefunds = new List<RefundsViewModel>();

            try
            {
                IEnumerable<Booking> bookings = (from booking in dbentities.Bookings select booking).ToList();


                listRefunds = (from b in bookings
                    select new
                    {
                        _tId = b.trn_Id,
                        _trEvtDate = b.startdate,
                        _cusId = b.c_Id,
                        _cusfullname = Utilities.getfullname(b.Customer.lastname, b.Customer.firstname, b.Customer.middle),
                        _occasion = b.occasion,
                        _evtVenue=b.venue,
                        _iscancelledbooking = b.is_cancelled,
                        _serveStat=b.serve_stat,
                        _tpackageAmt = bookingPayments.Get_TotalAmountBook(b.trn_Id),
                        _totapayment = (from p in dbentities.Payments select p).Where(s => s.trn_Id == b.trn_Id).Sum(x => x.amtPay),
                        _hasrefundEntry=(from rp in dbentities.Refunds select rp).Any(x => x.trn_Id==b.trn_Id),
                        _stat=" "

                    //}).ToList().Where(t=> t._serveStat==false || t._iscancelledbooking==true) .Select(book => new RefundsViewModel()
                      }).ToList().Where(t=>t._serveStat == false).Select(book => new RefundsViewModel()
                      {
                        TransId = book._tId,
                        cId = (int) book._cusId,
                        CustomerName =book._cusfullname,
                        EventLocation = book._evtVenue,
                        Eventdate = (DateTime)book._trEvtDate,
                        isCancelled =(bool) book._iscancelledbooking,
                        PaymemntAmount = Convert.ToDecimal(book._totapayment),
                        RefundAmount = book._totapayment > book._tpackageAmt?Convert.ToDecimal(book._totapayment - book._tpackageAmt):0,
                        //RefundAmount =book._iscancelledbooking==false? Convert.ToDecimal(book._totapayment - book._tpackageAmt): Convert.ToDecimal(book._totapayment),
                        hasrefundEntry = book._hasrefundEntry,
                        Status = book._stat
                    }).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

            

            return listRefunds;

        }


           

        
    }
}