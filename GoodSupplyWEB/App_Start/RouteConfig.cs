using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoodSupplyWEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ShowProducts", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "SupplierProducts",
            //    url: "{SupplierProducts}/{Index}/{prodPrice}/{quantity}/{manuProdId}",
            //    defaults: new { controller = "SupplierProduct", action = "Index", prodPrice = UrlParameter.Optional, quantity = UrlParameter.Optional, manuProdId = UrlParameter.Optional }
            //);

        }
    }
}
