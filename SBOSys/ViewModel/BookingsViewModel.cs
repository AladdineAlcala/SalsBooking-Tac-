using SBOSys.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SBOSys.HtmlHelperClass;
using System.Data.Entity.Core;
using System.Web.Mvc;

namespace SBOSys.ViewModel
{
    public class BookingsViewModel
    {
        public int trn_Id { get; set; }
        public int? c_Id { get; set; }
        [Display(Name = "No. of Person:")]
        [Required(ErrorMessage = "No. of persons required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Pls. enter valid number format")]
        public int? noofperson { get; set; }
        [Display(Name = "Event:")]
        [Required(ErrorMessage = "Event/Ocassion Name Required")]
        public string occasion { get; set; }
        [Display(Name = "Venue:")]
        public string venue { get; set; }
        public int? typeofservice { get; set; }
        [Display(Name = "Date and Time:")]
        [Required(ErrorMessage = "Date Required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public int pId { get; set; }
        [Required(ErrorMessage = "Package Required")]
        public string packagename { get; set; }
        public bool? serve_status { get; set; }
        [Display(Name = "Event Motf:")]
        public string eventcolor { get; set; }
        [Display(Name = "Customer:")]
        [Required(ErrorMessage = "Customer Name Required")]
        public string fullname { get; set; }
        public int? serviceId { get; set; }
        public DateTime? transdate { get; set; }
        public bool apply_extendedAmount { get; set; }
        public IEnumerable<SelectListItem> Servicetype_ListItems { get; set; }
        public string selected_servicetype { get; set; }
        public int reservationId { get; set; }
        public List<BookingsViewModel> GetListofBookings()
        {
           var _entities=new PegasusEntities();

         //  _entities.Configuration.ProxyCreationEnabled = false;

            List<Booking> bookings = new List<Booking>();
            List<BookingsViewModel> bookingdetails=new List<BookingsViewModel>();

            try
            {
              bookings = (from c in _entities.Bookings select c).ToList();

                bookingdetails = (from b in bookings
                    select new BookingsViewModel
                    {
                        trn_Id = b.trn_Id,
                        c_Id = b.c_Id,
                        noofperson = b.noofperson,
                        occasion = b.occasion,
                        packagename = b.Package.p_descripton,
                        venue = b.venue,
                        typeofservice = b.typeofservice,
                        startdate = b.startdate,
                        enddate = b.enddate,
                        transdate = b.transdate,
                        serve_status = b.serve_stat,
                        eventcolor = b.eventcolor,
                        pId = Convert.ToInt32(b.p_id),
                        fullname = Utilities.getfullname(b.Customer.lastname, b.Customer.firstname, b.Customer.middle)
                    }).ToList();
            //}).Where(x=>x.serve_status==false).OrderBy(d => d.startdate).ToList();

            }
            catch (EntityCommandExecutionException eceex)
            {
                if (eceex.InnerException != null)
                {
                    throw eceex.InnerException;
                }
                throw;
            }


            return bookingdetails.ToList();


        }

        public IEnumerable<SelectListItem> GetServiceType_SelectListItems()
        {
            var _entities = new PegasusEntities();

            var servicetypeList = _entities.ServiceTypes.AsEnumerable().Select(x => new SelectListItem
            {
                Value = x.serviceId.ToString(),
                Text = x.servicetypedetails
            }).ToList();

            return new SelectList(servicetypeList, "Value", "Text");
        }
    }
}