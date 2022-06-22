using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trekking
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "ProductsIndex",
                url: "products",
                defaults: new {controller = "Product", action = "Index"}
            );
            
            routes.MapRoute(
                name: "Product",
                url: "products/{productId}",
                defaults: new {controller = "Product", action = "Show"}
            );
            
            // routes.MapRoute(
            //     name: "Products",
            //     url: "products/{productId}",
            //     defaults: new {controller = "Product", action = "Show"}
            // );
            //
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

        }
    }
}