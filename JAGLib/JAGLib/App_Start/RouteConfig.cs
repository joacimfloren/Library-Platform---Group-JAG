﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JAGLib
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "JAGLib.Controllers" }
            );

            routes.MapRoute(
                name: "Api",
                url: "Api/{controller}/{action}/{id}",
                namespaces: new[] { "JAGLib.Controllers.Api" }
            );
        }
    }
}
