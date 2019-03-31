using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class BookMenusViewModel
    {
        public int menuNo { get; set; }
        public int transId { get; set; }
        public string menuId { get; set; }
        public string menu_name { get; set; }
        public string dept { get; set; }

        public IEnumerable<BookMenusViewModel> LisofMenusBook()
        {
            List<BookMenusViewModel> bookMenusList=new List<BookMenusViewModel>();

            var _dbentities =new PegasusEntities();

            try
            {
                bookMenusList = (from bm in _dbentities.Book_Menus
                    select new BookMenusViewModel()
                    {
                        menuNo=bm.No,
                        transId = (int) bm.trn_Id,
                        menuId = bm.menuid,
                        menu_name = bm.Menu.menu_name,
                        dept = bm.Menu.Department.deptName


                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return bookMenusList;
        }


        public int get_totalselectedMainMenus(int transactionId)
        {
            int i = 0;

            var dbEntities=new PegasusEntities();
            try
            {
                var bookmenus = (from bm in dbEntities.Book_Menus
                    join m in dbEntities.Menus on bm.menuid equals m.menuid
                    join cc in dbEntities.CourseCategories on m.CourserId equals cc.CourserId 
                    where bm.trn_Id == transactionId && cc.Main_Bol==true
                    select new { sel_mainmenus=cc.Course }).ToList();

                i = bookmenus.Select(x => x.sel_mainmenus).Count();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return i;
        }
    }
}