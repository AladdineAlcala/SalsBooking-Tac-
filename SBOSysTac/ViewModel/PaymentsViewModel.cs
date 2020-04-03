using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSysTac.Models;
using System.ComponentModel.DataAnnotations;

namespace SBOSysTac.ViewModel
{
    public class PaymentsViewModel
    {
        public string PayNo { get; set; }
        public int? transId { get; set; }
        public DateTime? dateofPayment { get; set; }
        [Display(Name = "Particular:")]
        [Required(ErrorMessage = "Reference Required ")]
        public string particular { get; set; }
        [Display(Name = "Payment Type:")]
        [Required(ErrorMessage = "Payment Type Required")]
        public int? payType { get; set; }

        [Required(ErrorMessage = "Payment Amount Required")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Amount Format; Max. Two Decimal Points.")]
        [Range(1, 9999999999999999.99,ErrorMessage = "Amount Entered Is Not in Range or cannot accept 0 values")]
        public decimal? amtPay { get; set; }
        [Display(Name = "Payment Means:")]
        [Required(ErrorMessage = "Payment Means Required")]
        public string pay_means { get; set; }
        [Display(Name = "Check No:")]
        public string checkNo { get; set; }
        [Display(Name = "Notes:")]
        public string notes { get; set; }

       

        private PegasusEntities _dbcontext = new PegasusEntities();
        public IEnumerable<PaymentsViewModel> GetPaymentsList()
        {

            List<PaymentsViewModel> paymentslist = new List<PaymentsViewModel>();

            try
            {

                paymentslist = (from p in _dbcontext.Payments
             
                    select new PaymentsViewModel()
                    {
                        PayNo = p.payNo,
                        transId = p.trn_Id,
                        dateofPayment = p.dateofPayment,
                        particular = p.particular,
                        payType = p.payType,
                        amtPay = p.amtPay,
                        pay_means = p.pay_means,
                        checkNo = p.checkNo,
                        notes = p.notes 


                    }).OrderBy(x=>x.dateofPayment).ToList();



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return paymentslist.ToList();
        }


    }
}
