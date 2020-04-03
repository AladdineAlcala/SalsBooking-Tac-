using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSysTac.Models;

namespace SBOSysTac.ViewModel
{
    public class AddonsUpgrade_BookRegisterViewModel
    {
        public int BookTransId { get; set; }
        public int addonsCatId { get; set; }
        public IEnumerable<SelectListItem> selectlistAddonCat { get; set; }



        public IEnumerable<SelectListItem> Get_SelectListAddonCat()
        {
            var dbentities=new PegasusEntities();

            var addoncatselectlist = dbentities.AddonCategories.AsEnumerable().Select(x => new SelectListItem()
            {
                Value = x.addoncatId.ToString(),
                Text = x.addoncatdesc
            });

            return new SelectList(addoncatselectlist, "Value", "Text");
        }
    }
}