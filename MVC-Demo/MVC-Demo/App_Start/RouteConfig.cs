﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Dashboard",
                url: "mens-clothing-sale",
                defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Products",
                url: "mens-clothing-sale/Products",
                defaults: new { controller = "Home", action = "Products", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
            );

        }
    }
}
