//using DeffinityAppDev.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeffinityAppDev
{
    public class RouteConfig
    {
        //public static string _WebApiExecutionPath = "api";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("WF/");
            routes.IgnoreRoute("App/");
            routes.IgnoreRoute("Content/");
           // routes.IgnoreRoute("assets/");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.css/{*pathInfo}");
           // routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "content" });

            routes.MapRoute(
              name: "api",
              url: "api/{controller}/{action}/{id}",
              defaults: new { action = "get", id = UrlParameter.Optional }


          );
     //       routes.MapRoute("GanttCrud",
     //"GanttCrud/{action}/{id}",
     //new { controller = "GanttCrud", action = "Index", id = UrlParameter.Optional });
         //   routes.MapRoute(
         //    name: "api",
         //    url: "api/{controller}/{action}/{id}",
         //    defaults: new { controller = "Inbox", action = "get", id = UrlParameter.Optional }
         //).RouteHandler = new SessionStateRouteHandler();
         //   routes.MapRoute(
         //    name: "api",
         //    url: "api/{controller}/{action}/{id}",
         //    defaults: new { controller = "Project", action = "get", id = UrlParameter.Optional }
         //).RouteHandler = new SessionStateRouteHandler();
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
