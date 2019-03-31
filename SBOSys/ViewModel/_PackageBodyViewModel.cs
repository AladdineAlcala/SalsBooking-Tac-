using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class _PackageBodyViewModel
    {

       
        public int packageId { get; set; }
        public string packagename { get; set; }
        public int NoofMainCourse { get; set; }
        public int qty_serve { get; set; }
        public int[] selectedMenuCategoryId { get; set; }
        public List<Select2Model> Select2Models { get; set; }
        public IEnumerable<PackageBody> PackageBodies { get; set; }



        public List<Select2Model> GetCategoryMenu()
        {
            List<Select2Model> listSelect = new List<Select2Model>();

            using (var _dbcontext=new PegasusEntities())
            {
                var listmenuCategory = _dbcontext.CourseCategories.Where(x=>x.Main_Bol!=true);

                foreach (var item in listmenuCategory)
                {
                    listSelect.Add(new Select2Model()
                    {
                        id = item.Course,
                        text = item.Course
                    });
                }
            }

            return listSelect;


        }


    }
}