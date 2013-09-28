using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExchangeRates
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "",
                url: "{isocode}/",
                defaults: new { controller = "Home", action = "Rates" },
                constraints: new { isocode = @"(?i)^[a-z]{3}$" }
            );

            routes.MapRoute(
                name: "",
                url: "",
                defaults: new { controller = "Home", action = "Rates", isocode = "RUB" }
            );
        }
    }
}