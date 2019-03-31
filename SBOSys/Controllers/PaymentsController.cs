using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class PaymentsController : Controller
    {


        private BookingsViewModel bookingsViewModel=new BookingsViewModel();
        private PaymentsViewModel paymentsViewModel=new PaymentsViewModel();
        private BookingPaymentsViewModel bookingPayments=new BookingPaymentsViewModel();
        private TransRecievablesViewModel tr = new TransRecievablesViewModel();
        private PaymentsViewModel pv = new PaymentsViewModel();
        private PegasusEntities _dbcontext;

        public PaymentsController()
        {
            _dbcontext = new PegasusEntities();
        }

        [HttpGet]
        public ActionResult GetBooking_Payments(int transactionId)
        {
            BookingPaymentsViewModel bookpay=new BookingPaymentsViewModel();

         //  BookingsViewModel bbViewModel=new BookingsViewModel();
            try
            {
                bookpay.transId = transactionId;

                bookpay.Bookings = bookingsViewModel.GetListofBookings().FirstOrDefault(x => x.trn_Id == transactionId);
                bookpay.t_amtBooking = bookingPayments.Get_TotalAmountBook(transactionId);
                bookpay.PaymentList = bookingPayments.GetPaymentDetaiilsBooking(transactionId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(bookpay);
        }

        public ActionResult LoadPaymentList(int transactionId)
        {
           List<PaymentsViewModel>listpayment=new List<PaymentsViewModel>();

            try
            {

                listpayment = paymentsViewModel.GetPaymentsList().Where(p=>p.transId==transactionId).ToList();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(new {data=listpayment }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add_PaymentPartialView(int transactionId)
        {
            PaymentsViewModel pay=new PaymentsViewModel();

            pay.transId = transactionId;
            pay.dateofPayment=DateTime.Now;
            pay.payType = 1;

            return PartialView(pay);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePayment(PaymentsViewModel payment)
        {
            if (!ModelState.IsValid) return PartialView("Add_PaymentPartialView",payment);

          //  bool success = false;

            var url = "";
            try
            {
                Payment newPayment=new Payment()
                {
                    trn_Id = payment.transId,
                    dateofPayment = payment.dateofPayment,
                    particular = payment.particular,
                    payType = payment.payType,
                    amtPay = payment.amtPay,
                    pay_means = payment.pay_means,
                    checkNo = payment.checkNo,
                    notes = payment.notes

                };

                _dbcontext.Payments.Add(newPayment);
                _dbcontext.SaveChanges();

                url = Url.Action("GetPaymentList", "Payments", new {transId = payment.transId });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {success = true,url=url}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RemovePayment(int pmtNo)
        {
            Payment paymt=new Payment();
            bool success = false;
            var url = "";
           
            try
            {
                paymt = _dbcontext.Payments.Find(pmtNo);

                int t_Id =Convert.ToInt32(paymt.trn_Id);

             
                    _dbcontext.Payments.Remove(paymt);
                    _dbcontext.SaveChanges();


                    url = Url.Action("GetPaymentList", "Payments", new { transId =t_Id});
                    success = true;
               
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {success=success,url=url}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Update_PaymentPartialView(int pymtId)
        {
            Payment pmt=new Payment();
            PaymentsViewModel pmtViewModel = new PaymentsViewModel();

           
                pmt = _dbcontext.Payments.FirstOrDefault(x => x.payNo == pymtId);

                if (pmt != null)
                {
                     pmtViewModel = new PaymentsViewModel()
                    {
                        PayNo = pmt.payNo,
                        transId = pmt.trn_Id,
                        dateofPayment = pmt.dateofPayment,
                        particular = pmt.particular,
                        payType = pmt.payType,
                        amtPay = pmt.amtPay,
                        pay_means = pmt.pay_means.Trim(),
                        checkNo = pmt.checkNo,
                        notes = pmt.notes

                    };

                }

            return PartialView(pmtViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_PaymentPartialView(PaymentsViewModel updatedPayment)
        {
            if (!ModelState.IsValid) return PartialView("Update_PaymentPartialView", updatedPayment);

            bool success = false;

            var url = "";
            try
            {

                Payment upPaymnt = new Payment()
                {
                    payNo = updatedPayment.PayNo,
                    trn_Id = updatedPayment.transId,
                    dateofPayment = updatedPayment.dateofPayment,
                    particular = updatedPayment.particular,
                    payType = updatedPayment.payType,
                    amtPay = updatedPayment.amtPay,
                    pay_means = updatedPayment.pay_means,
                    checkNo = updatedPayment.checkNo,
                    notes = updatedPayment.notes

                };

                _dbcontext.Payments.Attach(upPaymnt);
               _dbcontext.Entry(upPaymnt).State=EntityState.Modified;
                _dbcontext.SaveChanges();

                url = Url.Action("GetPaymentList", "Payments", new { transId = upPaymnt.trn_Id });
                success = true;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {success=success,url=url}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPaymentList(int transId)
        {
            try
            {
                var paytrans = new PaymentTransViewModel()
                {
                    transId = transId,
                    TransRecievables = tr.GetRecieveDetails().FirstOrDefault(t => t.transId == transId),
                    Payments = pv.GetPaymentsList().Where(p => p.transId == transId)
                };

                return PartialView("_PaymentsList", paytrans);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            
        }
        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }
    }
}