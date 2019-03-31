using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SBOSys.Models;
using SBOSys.HtmlHelperClass;
using SBOSys.ViewModel;
using PagedList;

namespace SBOSys.Controllers
{
    public class PackagesController : Controller
    {
      
        private PegasusEntities _dbcontext;
        private PackageDetailsLocationViewModel packagesIndex=new PackageDetailsLocationViewModel();
        private PackageAreaLocationViewModel packageArea=new PackageAreaLocationViewModel();
        private AreaPackageViewModel areaPackage= new AreaPackageViewModel();
        private PackageBookingViewModel pb=new PackageBookingViewModel();
        public PackagesController()
        {
            ViewBag.ActiveForm= Utilities.ActiveForm;
            _dbcontext =new PegasusEntities();
        }
        // GET: Packages
        public ActionResult Index(int?page,int pageSize=4)
        {
            ViewBag.FormTitle = "Package Buffet Details";


            var packageIndexDetails = packagesIndex.GetPackageDetailsView();

            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_packageListPartialView", packageIndexDetails.ToList().ToPagedList(pageNumber,pageSize) as PagedList<PackageDetailsLocationViewModel>);
            }

            return View(packageIndexDetails.ToPagedList(pageNumber,pageSize)as PagedList<PackageDetailsLocationViewModel>);
        }


        public ActionResult CreatePackage()
        {
           

            ViewBag.FormTitle = "Create New Buffet Package";
            
            return View();
        }

        [HttpGet]
        public ActionResult PackageForm()
        {
            Utilities.ActiveForm = "";
            ViewBag.ActiveForm = Utilities.ActiveForm;
            return PartialView("_packages");
        }

        [HttpGet]
        public ActionResult PackageBody()
        {
            ViewBag.ActiveForm = Utilities.ActiveForm;

            return PartialView("_packagebody", Utilities.PackageBodyModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ProcessNewPackage")]
        public ActionResult CreatePackage(PackageViewModel newpackage)
        {
            if (!ModelState.IsValid) return PartialView("_packages",newpackage);

            var packagedata = new Package
            {
                p_id = Convert.ToInt32(newpackage.p_id),
                p_descripton = newpackage.p_descripton.Trim(),
                p_amountPax = Convert.ToDecimal(newpackage.p_amountPax),
                p_min = newpackage.p_min

            };

            _dbcontext.Packages.Add(packagedata);
            _dbcontext.SaveChanges();
           
            Utilities.ActiveForm= "newPackagebody";
          
            Utilities.PackageBodyModel.package_Id = Convert.ToInt32(packagedata.p_id);
            Utilities.PackageBodyModel.package_name = packagedata.p_descripton;

            var url = Url.Action("Package_Body_Details", "Packages");

            return Json(new {success=true,url=url }, JsonRequestBehavior.AllowGet);
            //return PartialView("_packagebody", Utilities.PackageBodyModel);

        }

        [HttpGet]
        public ActionResult Package_Body_Details()
        {
            PackageBodyViewModel packagebody = new PackageBodyViewModel()
            {
                package_Id = Utilities.PackageBodyModel.package_Id,
                package_name = Utilities.PackageBodyModel.package_name
            };

        
            return PartialView("_packagebody", packagebody);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ProcessPackageDetails")]
        public ActionResult CreatePackage(PackageBodyViewModel p_bodyViewModel)
        {
            if (!ModelState.IsValid) return PartialView("_packagebody", p_bodyViewModel);

            var packagebodyDetails = new PackageBody
            {
                p_id = p_bodyViewModel.package_Id,
                mainCourse = p_bodyViewModel.mainCourse,
                sea_vegi = p_bodyViewModel.sea_vegi,
                noodlepasta = p_bodyViewModel.noodlepasta,
                salad = p_bodyViewModel.salad,
                dessert = p_bodyViewModel.dessert,
                pineapple = p_bodyViewModel.pineapple,
                drinks = p_bodyViewModel.drinks,
                rice = p_bodyViewModel.rice
            };
            _dbcontext.PackageBodies.Add(packagebodyDetails);
            _dbcontext.SaveChanges();

            Utilities.ActiveForm = "";

            ViewBag.ActiveForm = Utilities.ActiveForm;

            //var package_id = packagebodyDetails.p_id;

            return Json(new {success=true, packageId = packagebodyDetails.p_id },JsonRequestBehavior.AllowGet);


        }


        public ActionResult PackageDetails(int packageId)
        {
            ViewBag.FormTitle = "Package Buffet Details";

         
            //IQueryable<PackageAreaCoverage> areaCoverage =
            //(from areacover in _dbcontext.PackageAreaCoverages.Include(x=>x.Area) where areacover.p_id == (Int32) packageId  orderby areacover.p_id select areacover);

            PackageDetailsLocationViewModel P_details = new PackageDetailsLocationViewModel()
            {
                PackageId = packageId,
                Packages = _dbcontext.Packages.Find(packageId),
                PBody = _dbcontext.PackageBodies.First(p => p.p_id == (Int32?)packageId),
              //  PackageAreaCoverages = areaCoverage.ToPagedList(pageIndex,dataCount)
            };

          

            return View(P_details);
        }


        [HttpGet]
        public ActionResult AddPackageCoverage(int packageId)
        {
          
           
            PackageAreaLocationViewModel p_area=new PackageAreaLocationViewModel()
            {
                p_id = packageId,
                //AreasSelectList = packageArea.GetArea_SelectListItems()
            };

            return PartialView("_PackageCoverage", p_area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPackageCoverage(PackageAreaLocationViewModel packageAreaViewModel)
        {
            if (!ModelState.IsValid) return PartialView("_PackageCoverage", packageAreaViewModel);

            var packageCoverage = new PackageAreaCoverage()
            {
                p_id=packageAreaViewModel.p_id,
                aID=packageAreaViewModel.aID,
                is_extended=packageAreaViewModel.is_extended,
                ext_amount=packageAreaViewModel.ext_amount,

            };



            _dbcontext.PackageAreaCoverages.Add(packageCoverage);
            _dbcontext.SaveChanges();

            return Json(new {success=true, packageId = packageAreaViewModel.p_id},JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemovePackage(int packageId)
        {
            Package package=new Package();

            bool success = false;

            //==check package if has existing/active bookings
            bool hasExistingBooking = pb.verifyPackagehasBookings(packageId);

            if (!hasExistingBooking)
            {

                var packagebody = _dbcontext.PackageBodies.First(p => p.p_id == packageId);

                _dbcontext.PackageBodies.Remove(packagebody);
                package = _dbcontext.Packages.Find(packageId);
                _dbcontext.Packages.Remove(package);

                _dbcontext.SaveChanges();

                success = true;
            }
            


            return Json(new {success=success,package_name=package.p_descripton}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchPackage_Transaction()
        {
            var packagebyAreaDistinct=new PackageAreaLocationViewModel();

            packagebyAreaDistinct.AreaByPackageDistinct = packageArea.GetPackageByAreaDistinct();

            return PartialView("_searchPackage", packagebyAreaDistinct);
        }

        
        public JsonResult GetAreas(string query)
        {
            var areaList = packageArea.GetSelect2AreaViewModels().Where(x => x.text.ToLower().Contains(query.ToLower())).ToList();

            return Json(new { areaList }, JsonRequestBehavior.AllowGet);

        }

        //search and load to table => packages
        public JsonResult GetPackagesByLocation(int? areaId)
        {
           
            var packagesbyLocation=new List<AreaPackageViewModel>();

            try
            {
                packagesbyLocation = areaPackage.GetAreasByPackages()
                                    .Where(x => x.areaId.Equals(areaId))
                                    .OrderBy(x=>x.packageId).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {data= packagesbyLocation}, JsonRequestBehavior.AllowGet);
        }

        //load package on booking

        
       [HttpGet]
        public ActionResult ModifyPackage(int packageId)
       {
           ViewBag.packageId = packageId;

            return View();
        }

        [HttpGet]
        public ActionResult PackageMain_Modify(int packageId)
        {
            //ViewBag.ActiveForm = Utilities.ActiveForm;

            var selectedPackage = (from p in _dbcontext.Packages where p.p_id==packageId
                select new PackageViewModel()
                {
                    p_id = p.p_id,
                    p_descripton = p.p_descripton,
                    p_amountPax = (decimal) p.p_amountPax,
                    p_min = p.p_min

                }).FirstOrDefault();
          

            return PartialView("_packageMain_modify_partialview", selectedPackage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageMain_Modify(PackageViewModel updatedPackage)
        {
            //update package main

            if (!ModelState.IsValid) return PartialView("_packages", updatedPackage);

            var packagedata = new Package
            {
                p_id = Convert.ToInt32(updatedPackage.p_id),
                p_descripton = updatedPackage.p_descripton.Trim(),
                p_amountPax = Convert.ToDecimal(updatedPackage.p_amountPax),
                p_min = updatedPackage.p_min

            };

            _dbcontext.Packages.Attach(packagedata);
            _dbcontext.Entry(packagedata).State = EntityState.Modified;
            _dbcontext.SaveChanges();

         

            Utilities.ActiveForm = "modifyPackagebody";

            Utilities.PackageBodyModel.package_Id = Convert.ToInt32(packagedata.p_id);
            Utilities.PackageBodyModel.package_name = packagedata.p_descripton;

            var url = Url.Action("PackageBody_Modify", "Packages",new { packageId = packagedata.p_id});

            return Json(new { success = true, url = url }, JsonRequestBehavior.AllowGet);


          

        }

       




        [HttpGet]
        public ActionResult PackageBody_Modify(int packageId)
        {
           // Utilities.ActiveForm = "modifyPackagebody";

            Utilities.PackageBodyModel.package_Id = Convert.ToInt32(packageId);
            //Utilities.PackageBodyModel.package_name = packagedata.p_descripton;


            var selected_packageBody = (from p in _dbcontext.Packages join pb in _dbcontext.PackageBodies on p.p_id equals pb.p_id 
                                        where p.p_id == packageId
                                        select new PackageBodyViewModel()
                                        {
                                            pbodyKey = pb.No,
                                            package_Id = (int)pb.p_id,
                                            package_name = p.p_descripton,
                                            mainCourse = (int)pb.mainCourse,
                                            sea_vegi = (int)pb.sea_vegi,
                                            noodlepasta = (int)pb.noodlepasta,
                                            salad = (int)pb.salad,
                                            dessert = (int)pb.dessert,
                                            pineapple = (int)pb.pineapple,
                                            drinks = (int)pb.drinks,
                                            rice = (int)pb.rice

                                        }).FirstOrDefault();

            return PartialView("_packageBody_modify_partialview", selected_packageBody);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageBody_Modify(PackageBodyViewModel packagebody)
        {
            if (!ModelState.IsValid) return PartialView("_packagebody", packagebody);

            var packagebodyDetails = new PackageBody
            {
                No = packagebody.pbodyKey,
                p_id = packagebody.package_Id,
                mainCourse = packagebody.mainCourse,
                sea_vegi = packagebody.sea_vegi,
                noodlepasta = packagebody.noodlepasta,
                salad = packagebody.salad,
                dessert = packagebody.dessert,
                pineapple = packagebody.pineapple,
                drinks = packagebody.drinks,
                rice = packagebody.rice
            };
            _dbcontext.PackageBodies.Attach(packagebodyDetails);
            _dbcontext.Entry(packagebodyDetails).State = EntityState.Modified;
            _dbcontext.SaveChanges();


       

            //Utilities.ActiveForm = "";

            //ViewBag.ActiveForm = Utilities.ActiveForm;

            //var package_id = packagebodyDetails.p_id;

            return Json(new { success = true, packageId = packagebody.package_Id }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult get_ListofPackagesbyLocation(int packageId, int? page)
        {

            int pageIndex = page ?? 1;
            int dataCount = 7;


            IQueryable<PackageAreaCoverage> areaCoverage =
                (from areacover in _dbcontext.PackageAreaCoverages.Include(x => x.Area) where areacover.p_id == (Int32)packageId orderby areacover.p_id select areacover);

            PackageDetailsLocationViewModel P_details = new PackageDetailsLocationViewModel()
            {
                PackageId = packageId,
                PackageAreaCoverages = areaCoverage.ToPagedList(pageIndex, dataCount)
            };

            return PartialView("_packagelocationCoverage_ViewModel", P_details);
        }


        [HttpPost]
        public ActionResult Remove_PackageLocaton(int pAreaNo)
        {
            bool success = false;

            int packageId=0;
            var packageAreaCoverage = _dbcontext.PackageAreaCoverages.Find(pAreaNo);

            if (packageAreaCoverage != null)
            {
                packageId =Convert.ToInt32(packageAreaCoverage.p_id);

                _dbcontext.PackageAreaCoverages.Remove(packageAreaCoverage);
                _dbcontext.SaveChanges();

                success = true;
            }
          
            var url=Url.Action("get_ListofPackagesbyLocation", "Packages", new { packageId = packageId });

            return Json(new {success=success,url= url }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }
    }
}