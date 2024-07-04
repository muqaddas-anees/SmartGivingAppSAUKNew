using DeffinityAppDev.api;
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
        public static string _WebApiExecutionPath = "api";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("WF/");
            routes.IgnoreRoute("App/");
            routes.IgnoreRoute("Content/");
            routes.IgnoreRoute("assets/");
            routes.IgnoreRoute("api/");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.css/{*pathInfo}");
            routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "content" });


            // Handle .ashx requests
            //routes.Add(new Route("{handler}.ashx/{*pathInfo}", new StopRoutingHandler()));


            routes.MapRoute(
              name: "api",
              url: "api/{controller}/{action}/{id}",
              defaults: new { action = "get", id = UrlParameter.Optional }


          );
            routes.MapRoute("Contact",
   "Contact/{action}/{id}",
   new { controller = "Contact", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("savecontrent",
   "savecontrent/{action}/{id}",
   new { controller = "savecontrent", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Join",
  "Join/{action}/{id}",
  new { controller = "Join", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("GanttCrud",
     "GanttCrud/{action}/{id}",
     new { controller = "GanttCrud", action = "Index", id = UrlParameter.Optional });
            //          routes.MapRoute("Org",
            //"Org/{action}/{id}",
            //new { controller = "Org", action = "Index", id = UrlParameter.Optional });

  //          routes.MapRoute("Org",
  //"Org/{id}",
  //new { controller = "Org", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Web",
"Web/{id}",
new { controller = "Web", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Event",
"Event/{id}",
new { controller = "Event", action = "Index", id = UrlParameter.Optional });
            //Fundraiser
            routes.MapRoute("Fundraiser",
"Fundraiser/{id}",
new { controller = "Fundraiser", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Activate",
"Activate/{id}",
new { controller = "Activate", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Product",
"Product/{id}",
new { controller = "Product", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Register",
"Register/{id}",
new { controller = "Register", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Registration",
"Registration/{id}",
new { controller = "Registration", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("WebNew",
"WebNew/{id}",
new { controller = "WebNew", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Webhook",
"Webhook/{action}",
new { controller = "Webhook", action = "Index", id = UrlParameter.Optional });
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

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
