using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using SBOSys.HtmlHelperClass;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class BookingsController : Controller
    {

        private PegasusEntities _dbcontext;
        private BookingsViewModel booking=new BookingsViewModel();
        private PackageBookingViewModel packageBook=new PackageBookingViewModel();
        private MainMenuListViewModel mainmenulistviewmodel=new MainMenuListViewModel();
        private AddonsViewModel addonsviewmodel=new AddonsViewModel();
        private TransactionDetailsViewModel transactionDetails=new TransactionDetailsViewModel();
     

        public BookingsController()
        {
            _dbcontext=new PegasusEntities();
        }

        // GET: Bookings
        public ActionResult Index()
        {
            ViewBag.FormTitle = "Bookings & Events Details";

            return View();
        }


        [HttpGet]
        public ActionResult LoadBookings()
        {
            var bookings = booking.GetListofBookings().Where(s=>s.serve_status==false).OrderBy(d => d.startdate);

            return Json(new {data=bookings}, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult CreateBooking()
        {
            ViewBag.FormTitle = "Create New Bookings";

            var newBooking = new BookingsViewModel
            {
                transdate = DateTime.Now,
                startdate = DateTime.Now,
                Servicetype_ListItems = booking.GetServiceType_SelectListItems()
            };

            return View(newBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(BookingsViewModel bookingViewModel)
        {
            if (!ModelState.IsValid) return View(bookingViewModel);

            bool success = false;

          //  _dbcontext.Configuration.ProxyCreationEnabled = false;

            int transactionId = 0;

            try
            {

                var newBooking = new Booking
                {
                    c_Id = bookingViewModel.c_Id,
                    noofperson = bookingViewModel.noofperson,
                    occasion = bookingViewModel.occasion,
                    venue = bookingViewModel.venue,
                    typeofservice = bookingViewModel.serviceId ?? 2,
                    startdate = bookingViewModel.startdate,
                    enddate = bookingViewModel.startdate,
                    transdate = bookingViewModel.transdate,
                    p_id = bookingViewModel.pId,
                    apply_extendedAmount = bookingViewModel.apply_extendedAmount,
                    serve_stat =false
                    

                };
                _dbcontext.Bookings.Add(newBooking);
                _dbcontext.SaveChanges();

                transactionId=newBooking.trn_Id;

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Json(new {success=success, trnsId=transactionId},JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPackageId(int? transactionId)
        {
            var booking=new Booking();
            var is_success = false;

            booking = _dbcontext.Bookings.Find(transactionId);

            if (booking.p_id != null)
            {
                is_success = true;
            }

            return Json(new {success= is_success }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetPackageonBooking(int? transId)
        {
             PackageBookingViewModel pBooking=new PackageBookingViewModel();
             BookMenusViewModel bm = new BookMenusViewModel();

             pBooking = packageBook.GetBookingDetails().FirstOrDefault(x => x.transactionId==transId);


            return View(pBooking);
        }


        [HttpGet]
        public ActionResult GetPackageBookingDetails(int transId)
        {
            TransactionDetailsViewModel _transDetails = new TransactionDetailsViewModel();
            try
            {

                _transDetails = transactionDetails.GetTransactionDetails().FirstOrDefault(x => x.transactionId.Equals(transId));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return View(_transDetails);
        }

        [HttpGet]
        public ActionResult getPartialView_AmountDue(int transId)
        {
            List<BookingAddon> addonslist = new List<BookingAddon>();
            TransactionDetailsViewModel _transDetails = new TransactionDetailsViewModel();

            try
            {
                //var transId = transModel.transactionId;
                _transDetails = transactionDetails.GetTransactionDetails().FirstOrDefault(x => x.transactionId.Equals(transId));

               

                decimal packageTotal = 0;
                decimal addonsTotal = 0;
                decimal belowminPax = 0;
                decimal extendedLocationAmount = 0;
                decimal dpAmount = 0;
                decimal fpAmount = 0;
                decimal bookdiscountAmount = 0;


                var packageAmount = _transDetails.Package_Trans.p_amountPax;
                int no_of_pax = Convert.ToInt32(_transDetails.Booking_Trans.noofperson);


                addonslist = _dbcontext.BookingAddons.Where(x => x.trn_Id == transId).ToList();
                addonsTotal = addonslist.Sum(y => Convert.ToDecimal(y.AddonAmount));
                extendedLocationAmount = transactionDetails.Get_extendedAmountLoc(transId);
                belowminPax = transactionDetails.GetBelowMinPaxAmount(no_of_pax);
                dpAmount = transactionDetails.GetTotalDownPayment(transId);
                fpAmount = transactionDetails.GetFullPayment(transId);

                packageTotal = Convert.ToDecimal(packageAmount) * no_of_pax;

                var subtotal = (packageTotal + addonsTotal + extendedLocationAmount + belowminPax);

                //get discount information
                bookdiscountAmount = transactionDetails.Get_bookingDiscountbyTrans(transId, subtotal);


                _transDetails.TotaAddons = addonsTotal;
                _transDetails.extLocAmount = extendedLocationAmount;
                _transDetails.TotaBelowMinPax = belowminPax;
                _transDetails.TotaDp = dpAmount;
                _transDetails.Fullpaymnt = fpAmount;
                _transDetails.book_discounts = bookdiscountAmount;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return PartialView("_BookingsAmountDuePartial", _transDetails);
        }



        [HttpGet]
        public ActionResult GetListofBookMenus(int transactionId)
        {
            PackageBookingViewModel pBooking = new PackageBookingViewModel();
            BookMenusViewModel bm = new BookMenusViewModel();

            pBooking.BookMenuses = bm.LisofMenusBook().Where(x => x.transId.Equals(transactionId)).ToList();
            pBooking.transactionId = transactionId;


            return PartialView("_ListofBookMenus",pBooking);
        }




        [HttpGet]
        public ActionResult GetListofAddons(int transId)
        {

            // ViewBag.transId = transid;

            PackageBookingViewModel pBooking = new PackageBookingViewModel();
            AddonsViewModel addonsViewModel=new AddonsViewModel();

            pBooking.AddOns = addonsViewModel.ListofAddons().Where(x => x.TransId.Equals(transId)).OrderBy(o=>o.No).ToList();

            pBooking.transactionId = transId;


            return PartialView("_GetListofAddons",pBooking);

        }

        public ActionResult GetListofCourse(int transactionId)
        {

            ViewBag.transId = transactionId;

            return PartialView("_GetListofMainCourse");
        }


        public ActionResult LoadListMenus()
        {
            var listofmainmenu = mainmenulistviewmodel.ListofMainMenu().ToList();


            return Json(new {data=listofmainmenu }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddMenusToPackage(int transacId,string menuId)
        {

            bool isRecordExist =false;
            string _message = "";
            string menu_details = "";

            var url = "";

            if (transacId>0 && menuId != null)
            {
                try
                {

                    Book_Menus isExistMenu =
                        _dbcontext.Book_Menus.FirstOrDefault(x => x.trn_Id == transacId && x.menuid == menuId);

                    var menu = _dbcontext.Menus.FirstOrDefault(x => x.menuid == menuId);

                    if (menu != null)
                    {
                        menu_details = menu.menu_name;
                    }


                    if (isExistMenu == null)
                    {


                        //---- Check Total count of main menu per transaction -------

                        TransactionDetailsViewModel td = new TransactionDetailsViewModel();

                        BookMenusViewModel bm = new BookMenusViewModel();

                        var totalmainmenus = td.get_totalNoofMainMenus_on_booking(transacId);

                        //get no of selected main menus
                        var intSelectedmainmenus = bm.get_totalselectedMainMenus(transacId);

                        //------ test total no of main menus per selected menus

                        var bookMenu = new Book_Menus()
                        {
                            trn_Id = transacId,
                            menuid = menuId
                        };

                        //=====check if selected course is a main menu
                        if (td.isSelectedMenuMainCourse(menuId) == true)
                        {

                            if (intSelectedmainmenus < totalmainmenus)
                            {

                           

                                _dbcontext.Book_Menus.Add(bookMenu);
                                _dbcontext.SaveChanges();

                                
                            }

                            else
                            {
                                isRecordExist = true;
                                _message = "Main menus exceed on required no..";

                            }
                        }
                        else
                        {
                       

                            _dbcontext.Book_Menus.Add(bookMenu);
                            _dbcontext.SaveChanges();

                           
                        }


                        url = Url.Action("GetListofBookMenus", "Bookings", new { transactionId = transacId });



                    }
                    else
                    {
                        isRecordExist = true;
                        _message = menu_details + " already in the list";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }


            return Json(new { isRecordExist= isRecordExist,message = _message,url= url}, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddOnsInformation(int transactionId)
        {

            AddonsViewModel addons = new AddonsViewModel
            {
                TransId = transactionId
            };

            return PartialView("_AddOnsInformation",addons);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAddons(AddonsViewModel addons)
        {
            if (!ModelState.IsValid) return PartialView("_AddOnsInformation", addons);


            var _addons = new BookingAddon
            {
               trn_Id = addons.TransId,
               Addondesc = addons.AddonsDescription,
               Note = addons.AddonNote,
               AddonAmount = addons.AddonAmount
            };

            _dbcontext.BookingAddons.Add(_addons);
            _dbcontext.SaveChanges();

            var ReturnUrl = Url.Action("GetListofAddons", "Bookings", new { transId=addons.TransId });

            return Json(new {success=true,url=ReturnUrl }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveMenufromListofBookings(int bookmenuNo)
        {

            bool success = true;
            var returnUrl = "";

            Book_Menus bm=new Book_Menus();

            try
            {
                bm = _dbcontext.Book_Menus.Find(bookmenuNo);

                var transactionId = bm.trn_Id;

                if (bm != null)
                {
                    _dbcontext.Book_Menus.Remove(bm);
                    _dbcontext.SaveChanges();

                returnUrl = Url.Action("GetListofBookMenus", "Bookings", new {transactionId = transactionId});

                }
                else
                {
                    success = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Json(new {success=success,url=returnUrl }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RemoveAddons(int addonNo)
        {


            BookingAddon ba=new BookingAddon();
            int transactionId;
            string returnUrl=String.Empty;
            bool success = true;

            try
            {
                ba = _dbcontext.BookingAddons.Find(addonNo);

                if (ba!= null)
                {
                    transactionId = Convert.ToInt32(ba.trn_Id);


                    _dbcontext.BookingAddons.Remove(ba);
                    _dbcontext.SaveChanges();


                    returnUrl= Url.Action("GetListofAddons", "Bookings", new { transId = transactionId });
                }

                else
                {
                    success = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Json(new {success=success,url=returnUrl}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditBooking(int transId)
        {

            Booking editBooking=new Booking();
            BookingsViewModel bookingviewmodel=new BookingsViewModel();
            CustomerViewModel cus=new CustomerViewModel();

            editBooking = _dbcontext.Bookings.Find(transId);

            if (editBooking != null)
            {
                bookingviewmodel.trn_Id = editBooking.trn_Id;
                bookingviewmodel.transdate = Convert.ToDateTime(editBooking.transdate);
                bookingviewmodel.c_Id = editBooking.c_Id;
                bookingviewmodel.noofperson = editBooking.noofperson;
                bookingviewmodel.occasion = editBooking.occasion;
                bookingviewmodel.venue = editBooking.venue;
                bookingviewmodel.serviceId = editBooking.typeofservice;
                bookingviewmodel.startdate = editBooking.startdate;
                bookingviewmodel.serve_status = editBooking.serve_stat;
                bookingviewmodel.eventcolor = editBooking.eventcolor;
                bookingviewmodel.pId =Convert.ToInt32(editBooking.p_id);
                bookingviewmodel.fullname = cus.get_CustomerFullname(Convert.ToInt32(editBooking.c_Id));

                bookingviewmodel.Servicetype_ListItems = booking.GetServiceType_SelectListItems();

                bookingviewmodel.selected_servicetype = _dbcontext.ServiceTypes
                    .Where(x => x.serviceId == editBooking.typeofservice).Select(s => s.servicetypedetails).Single();

                if (editBooking.p_id != null)
                {
                    bookingviewmodel.packagename = _dbcontext.Packages.Where(x => x.p_id == editBooking.p_id)
                        .Select(p => p.p_descripton).Single();
                }

             

            }

            return View(bookingviewmodel);
        }






        [HttpPost,ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooking(BookingsViewModel bookingViewModel)
        {
            if (!ModelState.IsValid) return View(bookingViewModel);

            //  _dbcontext.Configuration.ProxyCreationEnabled = false;
            bool success = false;

            var editedBooking = new Booking
            {
                trn_Id = bookingViewModel.trn_Id,
                c_Id = bookingViewModel.c_Id,
                noofperson = bookingViewModel.noofperson,
                occasion = bookingViewModel.occasion,
                venue = bookingViewModel.venue,
                typeofservice = bookingViewModel.serviceId ?? 2,
                startdate = bookingViewModel.startdate,
                enddate = bookingViewModel.startdate,
                eventcolor = bookingViewModel.eventcolor,
                transdate =bookingViewModel.transdate,

                p_id = bookingViewModel.pId,
                serve_stat = bookingViewModel.serve_status

            };
            try
            {

                _dbcontext.Bookings.Attach(editedBooking);
                _dbcontext.Entry(editedBooking).State = EntityState.Modified;
                _dbcontext.SaveChanges();


                success = true;

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }


            return Json(new { success = success }, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult RemoveBooking(int transId)
        {
            bool success = false;

            Booking booking=new Booking();

            BookingAddon bookaddons=new BookingAddon();

            booking = _dbcontext.Bookings.Find(transId);
            IEnumerable<Book_Menus> bookmenustransList = _dbcontext.Book_Menus.Where(x => x.trn_Id == transId);
            IEnumerable<BookingAddon> bookingAddons = _dbcontext.BookingAddons.Where(x => x.trn_Id == transId);

            try
            {
                if (booking != null)
                {
                    _dbcontext.Bookings.Remove(booking);

                    _dbcontext.Book_Menus.RemoveRange(bookmenustransList);
                    _dbcontext.BookingAddons.RemoveRange(bookingAddons);

                    _dbcontext.SaveChanges();

                    success = true;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }

            return Json(new {success = success}, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ServeBookingStatus(int transactionNo)
        {
            var booking = _dbcontext.Bookings.FirstOrDefault(x => x.trn_Id == transactionNo);
            bool success = false;

            var datenow = DateTime.Now;

            try
            {
                if (booking!=null)
                {
                    if (booking.startdate <= datenow)
                    {
                        booking.serve_stat = true;

                        _dbcontext.Bookings.Attach(booking);
                        _dbcontext.Entry(booking).Property(x => x.serve_stat).IsModified = true;
                        _dbcontext.SaveChanges();

                        success = true;
                    }
                    else
                    {
                        return Json(new {success = false},JsonRequestBehavior.AllowGet);
                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(new {success=success}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintContractOption(int transId)
        {
            var p = new PrintOptionViewModel()
            {
                Id = transId,
            };
            return View(p);

        }

        public ActionResult PrintContract(int transId, string selprintopt)
        {
            var pOption = new PrintOptionViewModel()
            {
                Id = transId,
                selPrintOpt = selprintopt

            };

           return View("~/Views/Shared/ReportContainer.cshtml", pOption);

        }

        public ActionResult PrintDistribution(int transId)
        {
            var pOption = new PrintOptionViewModel()
            {
                Id = transId,
                selPrintOpt ="distribution"

            };


            return View("~/Views/Shared/ReportContainer.cshtml",pOption);
        }

        [HttpGet]
        public ActionResult AddBookingDiscount(int transactionId)
        {
            DiscountCodeViewModel dc = new DiscountCodeViewModel();

            ViewBag.HeadTitle = "Create New Discount Code";

            var discountView = new DiscountCodeViewModel()
            {
                transId = transactionId,
                DiscountSelectlist =dc.GetListofDiscount()
            };

            return PartialView("_AddBookingDiscount", discountView);
        }


        [HttpPost, ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBookingDiscount(DiscountCodeViewModel newdiscount)
        {


            if (!ModelState.IsValid) return PartialView("_AddBookingDiscount", newdiscount);

            string url;
            try
            {
                var bookDiscount = new Book_Discount()
                {
                    trn_Id = newdiscount.transId,
                    disc_Id = newdiscount.discountCode,
                    userid = User.Identity.GetUserId()

                };

                _dbcontext.Book_Discount.Add(bookDiscount);
                _dbcontext.SaveChanges();

                url = @Url.Action("getPartialView_AmountDue", "Bookings", new {transId = newdiscount.transId});

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

              
           
            return Json(new {success=true,url=url }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDiscountDetails(int discountId)
        {

            var discountdetails = (from d in _dbcontext.Discounts where d.disc_Id==discountId
                select new
                {
                    discType=d.disctype,
                    discountAmount=d.discount1,
                    discStart=d.discStartdate,
                    discEnd=d.discEnddate
                }).FirstOrDefault();

            return Json(new {discountdetails}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookReservation(int reservationId)
        {
            var reservationinfo = _dbcontext.Reservations.Find(reservationId);

            var customer = _dbcontext.Customers.FirstOrDefault(x => x.c_Id == reservationinfo.c_Id);
            BookingsViewModel bookingviewModel = new BookingsViewModel();

            if (reservationinfo != null)
            {
               bookingviewModel = new BookingsViewModel()
                {
                    transdate = DateTime.Now,
                    c_Id = reservationinfo.c_Id,
                    noofperson = reservationinfo.noofPax,
                    occasion = reservationinfo.occasion,
                    venue = reservationinfo.eventVenue,
                    startdate = reservationinfo.resDate,
                    enddate = reservationinfo.resDate,
                    serve_status = false,
                    fullname = Utilities.getfullname(customer.lastname,customer.firstname,customer.middle),
                    reservationId = reservationId,
                   Servicetype_ListItems = booking.GetServiceType_SelectListItems()
               };
               
            }


            return View(bookingviewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookReservation(BookingsViewModel bookingViewModel)
        {

            if (!ModelState.IsValid) return View(bookingViewModel);

            bool success = false;

            //  _dbcontext.Configuration.ProxyCreationEnabled = false;

            int transactionId = 0;

            try
            {

                var newBooking = new Booking
                {
                    c_Id = bookingViewModel.c_Id,
                    noofperson = bookingViewModel.noofperson,
                    occasion = bookingViewModel.occasion,
                    venue = bookingViewModel.venue,
                    typeofservice = bookingViewModel.serviceId ?? 2,
                    startdate = bookingViewModel.startdate,
                    enddate = bookingViewModel.startdate,
                    transdate = bookingViewModel.transdate,
                    p_id = bookingViewModel.pId,
                    apply_extendedAmount = bookingViewModel.apply_extendedAmount,
                    serve_stat = false


                };
                _dbcontext.Bookings.Add(newBooking);
                _dbcontext.SaveChanges();

                transactionId = newBooking.trn_Id;
                var _reservationId = bookingViewModel.reservationId;

                var reservation=new Reservation();
                reservation = _dbcontext.Reservations.Find(_reservationId);
               

                if (reservation != null)
                {
                    reservation.reserveStat = true;

                    _dbcontext.Reservations.Attach(reservation);
                    _dbcontext.Entry(reservation).Property(x => x.reserveStat).IsModified = true;
                    _dbcontext.SaveChanges();
                }
              


                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Json(new { success = success, trnsId = transactionId }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }

    }
}