using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSysTac.ViewModel;

namespace SBOSysTac.ServiceLayer
{
    public static class ContainerClass
    {
        public static List<CateringReportViewModel> CateringReport=new List<CateringReportViewModel>();

        public static List<TransRecievablesViewModel> TransRecievablesAccn = new List<TransRecievablesViewModel>();
    }
}