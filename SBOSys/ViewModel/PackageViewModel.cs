using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBOSys.ViewModel
{
    public class PackageViewModel
    {

        public int? p_id { get; set; }
        [Display(Name = "PACKAGE:")]
        [Required(ErrorMessage = "Package name Required")]
        public string p_descripton { get; set; }
        [Required(ErrorMessage = "Amount per head Required")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal p_amountPax { get; set; }
        [Required(ErrorMessage = "Minimum total pax Required")]
        public int? p_min { get; set; }

    }
}