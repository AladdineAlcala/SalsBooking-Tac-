using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SBOSys.Models;

namespace SBOSys.ViewModel
{
    public class PackageDetailsLocationViewModel
    {
        public int? PackageId { get; set; }
        public Package Packages { get; set; }
        public PackageBody PBody { get; set; }
        public IPagedList<PackageAreaCoverage> PackageAreaCoverages { get; set; }

        

        public IEnumerable<PackageDetailsLocationViewModel> GetPackageDetailsView()
        {
            var package = new List<PackageDetailsLocationViewModel>();

            using (var _dbcontext = new PegasusEntities())
            {
               
                package = (from p in _dbcontext.Packages
                    join pb in _dbcontext.PackageBodies on p.p_id equals pb.p_id
                    select new PackageDetailsLocationViewModel()
                    {   
                        PackageId = p.p_id,
                        Packages = p,
                        PBody = pb
                    }).ToList();

            }

            return package;
        }
    }

    
}