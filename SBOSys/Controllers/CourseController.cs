﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;
using SBOSys.ViewModel;

namespace SBOSys.Controllers
{
    public class CourseController : Controller
    {
        private PegasusEntities _dbcontext;
        private CourseCategoryViewModel cv=new CourseCategoryViewModel();

        // GET: Course
        public CourseController()
        {
            _dbcontext=new PegasusEntities();
        }

       
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.FormTitle = "Course Category";

            var course = new List<CourseCategory>();
            
            try
            {
                course = (from c in _dbcontext.CourseCategories as IEnumerable<CourseCategory>
                    select new CourseCategory()
                    {
                        CourserId = c.CourserId,
                        Course = c.Course,
                        Note = c.Note,
                        Main_Bol = c.Main_Bol

                    }).OrderBy(x=>x.CourserId).ToList();
            }
            catch (Exception)
            {
                throw new HttpException(404, "Page Not Found");
            }


            return View(course);
        }



        [HttpGet]
        public ActionResult CreateCourse()
        {
            ViewBag.FormTitle = "New Course Category";

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CourseCategoryViewModel newcourseCatViewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                   
                        if (_dbcontext.CourseCategories.Any(x => x.Course.Contains(newcourseCatViewModel.Coursename)))
                        {
                            ModelState.AddModelError("Course", newcourseCatViewModel.Coursename + " already exist!.");
                            
                        }

                        else
                        {

                            var coursecat = new CourseCategory()
                            {
                                CourserId = Convert.ToInt32(newcourseCatViewModel.CourserId),
                                Course = newcourseCatViewModel.Coursename,
                                Note = newcourseCatViewModel.Note,
                                Main_Bol = newcourseCatViewModel.Main_Bol

                            };

                            _dbcontext.CourseCategories.Add(coursecat);
                            _dbcontext.SaveChanges();

                            //return RedirectToAction("Index", "Course");

                            return Json(new {success=true}, JsonRequestBehavior.AllowGet);
                        }
                    

                }
                catch (Exception)
                {

                    throw new HttpException(404,"Page Not Found");
                }

            }

            ViewBag.FormTitle = "New Course Category";
            return View(newcourseCatViewModel);


        }

        [HttpPost]
        public ActionResult RemoveCourse(int courseId)
        {
            string deletedcourse = null;
            bool success = false;
            try
            {

                var course = _dbcontext.CourseCategories.Find(courseId);


                if (course != null)
                {
                    //check if course has no booking menus
                    bool has_existing_book = cv.isCoursehasBooking(courseId);

                   
                    if (has_existing_book==false)
                    {

                        //if course has menus but no bookings

                        var has_menus = cv.isCourseHasMenus(courseId).ToList();

                        if (has_menus.Any())
                        {
                            foreach (var item in has_menus)
                            {
                                var menu = _dbcontext.Menus.Find(item.menu_Id);
                                _dbcontext.Menus.Remove(menu);
                                _dbcontext.Entry(menu).State=EntityState.Deleted;

                            }

                        }


                        _dbcontext.CourseCategories.Remove(course);
                        _dbcontext.SaveChanges();

                        deletedcourse = course.Course;

                        success = true;
                    }

                  
                }
            }
            catch (Exception e)
            {
                // throw new HttpException(404, "Page Not Found");
                Console.WriteLine(e);
                throw;
            }



            return Json(new { deletedcourse = deletedcourse, success = success }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ModifyCourse(int courseId)
        {
            var courseviewModel = (from c in _dbcontext.CourseCategories
                where c.CourserId == courseId
                select new CourseCategoryViewModel()
                {
                    CourserId = c.CourserId,
                    Coursename = c.Course,
                    Main_Bol = (bool) c.Main_Bol,
                    Note = c.Note
                }).FirstOrDefault();

            return View(courseviewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyCourse(CourseCategoryViewModel courseviewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {


                        var coursecat = new CourseCategory()
                        {
                            CourserId =Convert.ToInt32(courseviewModel.CourserId),
                            Course = courseviewModel.Coursename,
                            Note = courseviewModel.Note,
                            Main_Bol = courseviewModel.Main_Bol

                        };



                    _dbcontext.CourseCategories.Attach(coursecat);
                    _dbcontext.Entry(coursecat).State = System.Data.Entity.EntityState.Modified;
                    _dbcontext.SaveChanges();


                    return RedirectToAction("Index", "Course");
                    


                }
                catch (Exception)
                {

                    throw new HttpException(404, "Page Not Found");
                }

            }

            ViewBag.FormTitle = "Modify Course Category";
            return View(courseviewModel);

        }
        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }

    }
}