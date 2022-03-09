//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SBOSysTac.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Book_Discount = new HashSet<Book_Discount>();
            this.Book_Menus = new HashSet<Book_Menus>();
            this.BookingAddons = new HashSet<BookingAddon>();
            this.Refunds = new HashSet<Refund>();
            this.CancelledBookings = new HashSet<CancelledBooking>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int trn_Id { get; set; }
        public Nullable<System.DateTime> transdate { get; set; }
        public string booktype { get; set; }
        public Nullable<int> c_Id { get; set; }
        public Nullable<int> noofperson { get; set; }
        public string occasion { get; set; }
        public string venue { get; set; }
        public Nullable<int> typeofservice { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public string eventcolor { get; set; }
        public Nullable<int> p_id { get; set; }
        public string reference { get; set; }
        public Nullable<int> extendedAreaId { get; set; }
        public Nullable<bool> apply_extendedAmount { get; set; }
        public Nullable<decimal> p_amount { get; set; }
        public Nullable<bool> serve_stat { get; set; }
        public Nullable<bool> is_cancelled { get; set; }
        public string b_createdbyUser { get; set; }
        public System.DateTime b_updatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book_Discount> Book_Discount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book_Menus> Book_Menus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingAddon> BookingAddons { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Package Package { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CancelledBooking> CancelledBookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}