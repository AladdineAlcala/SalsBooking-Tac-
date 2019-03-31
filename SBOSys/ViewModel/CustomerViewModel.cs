using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class CustomerViewModel
    {
        public int customerId { get; set; }
        public string fullname { get; set; }

        public List<CustomerViewModel> getCustomer()
        {
           var _entities=new PegasusEntities();

            List<Customer> customers =new List<Customer>();
            customers=(from c in _entities.Customers select c).ToList();

            var customerlist = from l in customers
                               select new CustomerViewModel()
                {
                    customerId =(int) l.c_Id,
                    fullname = Utilities.getfullname(l.lastname,l.firstname,l.middle)
                };

            return customerlist.ToList();
        }

        public string get_CustomerFullname(int cusId)
        {
            
            string full = String.Empty;

            var _dbcontext=new PegasusEntities();
            try
            {


                var customer = _dbcontext.Customers.Find(cusId);

                if (customer != null)
                {

                    full = Utilities.getfullname(customer.lastname, customer.firstname, customer.lastname);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return full;
        }
    }
}