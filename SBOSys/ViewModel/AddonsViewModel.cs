using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class AddonsViewModel
    {
        public int No { get; set; }
        public int TransId { get; set; }
        [Display(Name = "Add-on Description :")]
        [Required(ErrorMessage = "Add on information required")]
        public string AddonsDescription { get; set; }
        [Display(Name = "Note :")]
        public string AddonNote { get; set; }
        [Display(Name = "Add-on Amount :")]
        public decimal AddonAmount { get; set; }

        public IEnumerable<AddonsViewModel> ListofAddons()
        {
            List<AddonsViewModel> list=new List<AddonsViewModel>();
            var _dbentities = new PegasusEntities();
            try
            {
                list = (from a in _dbentities.BookingAddons
                    select new AddonsViewModel
                    {
                        No = a.No,
                        TransId = (int) a.trn_Id,
                        AddonsDescription = a.Addondesc,
                        AddonAmount = (decimal) a.AddonAmount,
                        AddonNote = a.Note
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return list.ToList();
        }

    }
}