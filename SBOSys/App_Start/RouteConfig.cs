using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SBOSys
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("Reports/ReportViewers/{resource}.aspx/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute
            (
                name: "BookMenustoPackage",
                url: "{controller}/{action}/{transacId}/{menuId}",
                defaults: new {controller= "Bookings",action= "AddMenusToPackage", transacId=UrlParameter.Optional,mennuId=UrlParameter.Optional }
            );

            routes.MapRoute
            (
                name: "PrintContract",
                url: "{controller}/{action}/{transId}/{selprintopt}",
                defaults: new { controller = "Bookings", action = "PrintContract", transId = UrlParameter.Optional, selprintopt = UrlParameter.Optional }
            );
        }
    }
}
