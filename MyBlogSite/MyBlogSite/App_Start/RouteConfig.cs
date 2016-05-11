using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlogSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            // "Page",                                           // Route name
            // "Page/{id}",                            // URL with parameters
            // new { controller = "Page", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            //);
            routes.MapRoute(
             "PageControllers",                                           // Route name
             "Page/{action}/{id}",                            // URL with parameters
             new { controller = "Page", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
