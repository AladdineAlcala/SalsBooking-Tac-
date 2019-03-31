using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SBOSys.ViewModel
{
    public class PrintOptionViewModel
    {
        public int Id { get; set; }
        public string selPrintOpt { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

    }

    public enum printoption
    {
        contform,
        confunction
    }
}