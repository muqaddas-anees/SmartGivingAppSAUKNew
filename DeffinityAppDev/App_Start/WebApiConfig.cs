using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DeffinityAppDev
{
    public static class WebApiConfig
    {
        public static string _WebApiExecutionPath = "api";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: string.Format("{0}/{1}/{2}",_WebApiExecutionPath,"{controller}","{id}"),
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
