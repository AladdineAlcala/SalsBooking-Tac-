using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBOSys.ViewModel
{
    public class PackageBodyViewModel
    {
        public int pbodyKey { get; set; }
        public int package_Id { get; set; }
        public string package_name { get; set; }
        [Display(Name = "Main Course:")]
        [Required(ErrorMessage = "Quantity Required")]
        [Range(0,Int32.MaxValue,ErrorMessage = "Please enter valid integer")]
        public int mainCourse { get; set; }
        [Display(Name = "Seafood/Vegitables:")]
        [Required(ErrorMessage = "Quantity Required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int sea_vegi { get; set; }
        [Display(Name = "Noodle/Pasta:")]
        [Required(ErrorMessage = "Quantity Required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int noodlepasta { get; set; }
        [Display(Name = "Salad:")]
        [Required(ErrorMessage = "Quantity Required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int salad { get; set; }
        [Display(Name = "Dessert:")]
        [Required(ErrorMessage = "Quantity Required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int dessert { get; set; }
        [Display(Name = "SAL Queen Pineapple:")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int pineapple { get; set; }
        [Display(Name = "SoftDrinks or Ice Tea:")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int drinks { get; set; }
        [Display(Name = "Rice:")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid integer")]
        public int rice { get; set; }

    }
}