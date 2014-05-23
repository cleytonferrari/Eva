using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eva.UI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "TodasNoticias",
                url: "noticia/{categoria}/{page}",
                defaults: new { controller = "Noticia", action = "Index", page = UrlParameter.Optional },
                namespaces: new[] { "Eva.UI.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",  id = UrlParameter.Optional },
                namespaces: new[] { "Eva.UI.Web.Controllers" }
            );
        }
    }
}
