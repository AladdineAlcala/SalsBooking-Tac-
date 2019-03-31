using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class AreaPackageViewModel
    {

        public int? areaId { get; set; }
        public int? packageId { get; set; }
        public string packagedetails { get; set; }
        public decimal? amountperPax { get; set; }
        public bool? is_extended { get; set; }

        private PegasusEntities _dbcontext=new PegasusEntities();
       

        public IEnumerable<AreaPackageViewModel> GetAreasByPackages()
        {
            var areapackage = new List<AreaPackageViewModel>();

            areapackage = (from a in _dbcontext.PackageAreaCoverages
                join p in _dbcontext.Packages on a.p_id equals p.p_id
                select new AreaPackageViewModel
                {
                    areaId = a.aID,
                    packageId = p.p_id,
                    packagedetails = p.p_descripton,
                    amountperPax = p.p_amountPax,
                    is_extended=a.is_extended

                }).ToList();


            return areapackage;

        }

        

    }
}