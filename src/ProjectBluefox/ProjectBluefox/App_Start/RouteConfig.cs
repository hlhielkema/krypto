using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectBluefox
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Sign-Up",
                url: "signup/{token}",
                defaults: new { controller = "Account", action = "SignUp" }
            );

            routes.MapRoute(
                name: "Sign-in",
                url: "signin",
                defaults: new { controller = "Account", action = "SignIn" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );                        
        }
    }
}
