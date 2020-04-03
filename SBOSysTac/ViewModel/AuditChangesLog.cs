using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBOSysTac.ViewModel
{
    public class AuditChangesLog
    {
        public string columnName { get; set; }
        public string OrigValue { get; set; }
        public string NewValue { get; set; }

    }
}