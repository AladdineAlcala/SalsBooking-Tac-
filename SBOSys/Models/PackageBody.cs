//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SBOSys.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageBody
    {
        public int No { get; set; }
        public Nullable<int> p_id { get; set; }
        public Nullable<int> mainCourse { get; set; }
        public Nullable<int> sea_vegi { get; set; }
        public Nullable<int> noodlepasta { get; set; }
        public Nullable<int> salad { get; set; }
        public Nullable<int> dessert { get; set; }
        public Nullable<int> pineapple { get; set; }
        public Nullable<int> drinks { get; set; }
        public Nullable<int> rice { get; set; }
    
        public virtual Package Package { get; set; }
    }
}