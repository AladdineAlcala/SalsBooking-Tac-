﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBOSysTac.Models;

namespace SBOSysTac.ViewModel
{
    public class PackageViewModel
    {

        public int? p_id { get; set; }
        [Required(ErrorMessage = "Pls. select package type ")]
        public string packagetype { get; set; }
        [Display(Name = "PACKAGE:")]
        [Required(ErrorMessage = "Package name Required")]
        public string p_descripton { get; set; }
        [Required(ErrorMessage = "Amount per head Required")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal p_amountPax { get; set; }
       
        public int? p_min { get; set; }
        [Required(ErrorMessage = "Minimum total pax Required")]
        public int packagenopax_id { get; set; }
        public IEnumerable<SelectListItem> packageNoPax_listitem { get; set; }


        public IEnumerable<SelectListItem> GetPackageNoofPaxListItems()
        {
            var dbcontext=new PegasusEntities();

            var packagenoofpax=dbcontext.Packages_No_Pax_applicable.AsEnumerable().Select( x => new SelectListItem
                {
                    Value = x.nopax_id.ToString(),
                    Text = x.package_n_pax
                  }
            );

            return new SelectList(packagenoofpax, "Value", "Text");
        }

    }
}