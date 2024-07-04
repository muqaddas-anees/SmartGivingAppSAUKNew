using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubdomainModule
/// </summary>
 public class SubdomainModule : IHttpModule

    {

        public void Init(HttpApplication app)

        {

            // register for pipeline events

            app.BeginRequest += new EventHandler(this.OnBeginRequest);

        }

 

        public void Dispose() {}

 

        public void OnBeginRequest(object o, EventArgs args)

        {

 

            // get access to app and context

            HttpApplication app = (HttpApplication)o;

            HttpContext ctx = app.Context;

 

            if (!(app.Context.Request.Url.Host.StartsWith("localhost") || app.Context.Request.Url.Host.StartsWith("www")))

            {

                if (ctx.Request.Path.ToUpper() == "/DEFAULT.ASPX")

                {

                    string[] domainParts = app.Context.Request.Url.Host.Split(".".ToCharArray());

                    if (domainParts.Length > 2)

                        ctx.Server.Transfer("/explore/explore.aspx?q=" + domainParts[0]);

                }

            }

        }

    }