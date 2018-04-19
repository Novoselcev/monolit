using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Monolit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "MainPage",
              url: "",
              defaults: new { controller = "Home", action = "TopMenuContext", id = "Home" }
          );

            routes.MapRoute(name: "product",
               url: "{id}",
               defaults: new { controller = "Home", action = "ProdContext", id = UrlParameter.Optional });

            routes.MapRoute(name: "topMenu",
                              url: "Menu/{id}",
                              defaults: new { controller = "Home", action = "TopMenuContext", id = UrlParameter.Optional });

            routes.MapRoute(name: "InfoRazdel",
                              url: "InfoRazdel/{id}",
                              defaults: new { controller = "Home", action = "InfoContext", id = UrlParameter.Optional });

            routes.MapRoute(name: "InfoArticle",
                              url: "Information/{id}",
                              defaults: new { controller = "Home", action = "InfoPage", id = UrlParameter.Optional });


            routes.MapRoute(
               name: "Default2",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "TopMenuContext", id = "Home" }
           );
        }
    }
}
