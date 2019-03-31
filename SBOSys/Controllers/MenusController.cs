using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;
using SBOSys.HtmlHelperClass;
using System.Data.Entity;

namespace SBOSys.Controllers
{
    public class MenusController : Controller
    {

        private PegasusEntities _dbEntities;
        private CourseMenuViewModel _coursemenuViewModel=new CourseMenuViewModel();

        public MenusController()
        {

            _dbEntities=new PegasusEntities();
        }
        // GET: Menus
        public ActionResult Index()
        {
            var courseMenu = new CourseMenuViewModel
            {

                coursecategory_list =this._coursemenuViewModel.Get_CourseListItems()
            };

            ViewBag.FormTitle = "Menus Details";

            return View(courseMenu);
        }

        [HttpPost]
        public ActionResult loadDatatoTable()
        {

            var draw = Request.Unvalidated.Form.GetValues("draw").FirstOrDefault();

            var start = Request.Unvalidated.Form.GetValues("start").FirstOrDefault();

            var length = Request.Unvalidated.Form.GetValues("length").FirstOrDefault();

            var sortColumn = Request.Unvalidated.Form
                .GetValues("columns[" + Request.Unvalidated.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]")
                .FirstOrDefault();
            var sortColumnDir = Request.Unvalidated.Form.GetValues("order[0][dir]").FirstOrDefault();

            var menu = Request.Unvalidated.Form.GetValues("columns[2][search][value]").FirstOrDefault();

            var courseCategoryid = Request.Unvalidated.Form.GetValues("columns[3][search][value]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;


            var menus = _coursemenuViewModel.GetListofCourseMenu();

            //Searching menu name
            if (!(string.IsNullOrEmpty(menu.Trim())))
            {
                menus = menus.Where(x => x.menudesc.ToLower().Contains(menu.Trim().ToLower()));
            }

            //Filter Course
            if (!(string.IsNullOrEmpty(courseCategoryid.Trim())))
            {
                menus = menus.Where(c => c.CourserId == Convert.ToInt32(courseCategoryid));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {

                menus = menus.OrderBy(sortColumn + " " + sortColumnDir);

            }

            else
            {
                menus = menus.OrderBy(x => x.menu_Id);
            }

            recordsTotal = menus.Count();

            var data = menus.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult CreateMenus()
        {
            ViewBag.FormTitle = "Create New Menus";

            var newcCourseMenuViewModel = new CourseMenuViewModel
            {
                menu_Id = Utilities.MenusCode_Generator(),
                coursecategory_list = _coursemenuViewModel.Get_CourseListItems(),
                deptincharge_list = _coursemenuViewModel.Get_MenuDepartmentInchargeListItems(),
                dateAdded = Convert.ToDateTime(DateTime.Today)


            };


            return View(newcCourseMenuViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenus(CourseMenuViewModel courseMenu)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    var newMenu = new Menu
                    {
                        menuid = courseMenu.menu_Id,
                        CourserId = Convert.ToInt32(courseMenu.CourserId),
                        menu_name = courseMenu.menudesc,
                        deptId = Convert.ToInt32(courseMenu.deptId),
                        note = courseMenu.Note,
                        date_added = courseMenu.dateAdded


                    };
                    _dbEntities.Menus.Add(newMenu);
                    _dbEntities.SaveChanges();

                }
                catch (Exception)
                {

                    throw;
                }

                //return RedirectToAction("Index");

                return Json(new {success = true}, JsonRequestBehavior.AllowGet);

            }
            else
            {
                courseMenu.menu_Id = Utilities.MenusCode_Generator();
                courseMenu.coursecategory_list = courseMenu.Get_CourseListItems();
                courseMenu.deptincharge_list = courseMenu.Get_MenuDepartmentInchargeListItems();


                return View(courseMenu);
            }
           

            
        }
        [HttpPost]
        public ActionResult RemoveMenu(string menuId)
        {
            bool success = false;

            string removemenudesc = String.Empty;
            try
            {
                if (!string.IsNullOrEmpty(menuId))
                {
                    var removemenu = _dbEntities.Menus.Find(menuId);

                    if (removemenu != null)
                    {
                        bool hasExistingBooking = _coursemenuViewModel.is_Menu_hasBooking(menuId);

                        if (!hasExistingBooking)
                        {
                            _dbEntities.Menus.Remove(removemenu);
                            _dbEntities.SaveChanges();

                            removemenudesc = removemenu.menu_name;

                            success = true;
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                throw new HttpException(404, "Page Not Found");
            }


            return Json(new { success = success, deletedMenu = removemenudesc }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ModifyMenu(string menuId = null)
        {
            try
            {
                if (menuId != null)
                {
                    var list = _coursemenuViewModel.GetListofCourseMenu().FirstOrDefault(x => x.menu_Id.Equals(menuId));



                    list.coursecategory_list = _coursemenuViewModel.Get_CourseListItems();
                    list.deptincharge_list = _coursemenuViewModel.Get_MenuDepartmentInchargeListItems();


                    return View("ModifyMenu", list);
                }

            }
            catch (Exception)
            {
                throw new HttpException(404, "Page Not Found");
            }

            return RedirectToAction("Index");




        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyMenu(CourseMenuViewModel editedCourseMenuModel)
        {
  
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedMenu = new Menu
                    {
                        menuid = editedCourseMenuModel.menu_Id,
                        CourserId = Convert.ToInt32(editedCourseMenuModel.CourserId),
                        menu_name = editedCourseMenuModel.menudesc,
                        deptId = Convert.ToInt32(editedCourseMenuModel.deptId),
                        note = editedCourseMenuModel.Note,
                        date_added = editedCourseMenuModel.dateAdded


                    };
                    _dbEntities.Menus.Attach(updatedMenu);
                    _dbEntities.Entry(updatedMenu).State = EntityState.Modified;
                    _dbEntities.SaveChanges();

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    throw new HttpException(404, "Page Not Found");
                }

             
               
            }
            else
            {
                editedCourseMenuModel.menu_Id = Utilities.MenusCode_Generator();
                editedCourseMenuModel.coursecategory_list = _coursemenuViewModel.Get_CourseListItems();
                editedCourseMenuModel.deptincharge_list = _coursemenuViewModel.Get_MenuDepartmentInchargeListItems();

                return View(editedCourseMenuModel);

            }

        }

        protected override void Dispose(bool disposing)
        {
           _dbEntities.Dispose();
        }
    }
}