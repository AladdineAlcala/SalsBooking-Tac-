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
    
    public partial class AuditLogDetail
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public Nullable<int> AuditLogId { get; set; }
    
        public virtual AuditLog AuditLog { get; set; }
    }
}
