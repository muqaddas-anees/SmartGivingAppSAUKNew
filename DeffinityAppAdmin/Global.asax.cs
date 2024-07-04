using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
[assembly: OwinStartup(typeof(DeffinityAppAdmin.Startup))]
namespace DeffinityAppDev
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //DeffinityManager.DBCentralAccess.setInstance("HWA");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
        }

        protected void SetCulture(string name, string locale)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(name);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(locale);

            Application["MyUICulture"] = System.Threading.Thread.CurrentThread.CurrentUICulture;
            Application["MyCulture"] = System.Threading.Thread.CurrentThread.CurrentCulture;

        }
         protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
         {
             //set instance name
            // DeffinityManager.DBCentralAccess.setInstance("XACTNew");

             //set culture 
             if (Application["MyUICulture"] != null && Application["MyCulture"] != null)
             {
                 SetCulture(Application["MyUICulture"].ToString(), Application["MyCulture"].ToString());

             }
            //avoid localhost for mail sending purpose
            if (!Request.Url.AbsoluteUri.ToLower().Contains("localhost"))
            {
                if (!Request.IsSecureConnection)
                {
                    Response.Redirect(string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4)));
                }
            }
            //redirect to https page
            //avoid localhost for mail sending purpose
            //if (!Request.Url.AbsoluteUri.ToLower().Contains("localhost"))
            //{
            //    if (!Request.Url.AbsoluteUri.ToLower().Contains("31.28.75.42"))
            //    {
            //        if (!Request.IsSecureConnection && Request.Url.AbsoluteUri.ToLower().Contains("libertyhomeguard.deffinity.com"))
            //        {
            //            string path = string.Empty;
            //            if (Request.Url.AbsoluteUri.ToLower().Contains("excel.deffinity.com"))
            //            {
            //                path = string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4).Replace("excel.deffinity.com", "xact.excelit.com"));
            //            }
            //            else
            //            {
            //                path = string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4));
            //            }
            //            Response.Redirect(path);
            //        }
            //    }
            //}

            //if ((!Request.Path.ToLower().Contains("default.aspx")) && (Request.Path.ToLower().Contains(".aspx")))
            //{

            //    if (sessionKeys.UID > 0)
            //    {
            //        // check wather the page journal is enable 
            //        if (PageJornal.Get_JorunalStatus())
            //        {
            //            using (Userjournal uj = new Userjournal())
            //            {
            //                uj.userJournal_insert(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""), sessionKeys.UID);
            //            }
            //            //PageJornal.PageJournal_Insert(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""), sessionKeys.UID);
            //        }
            //        // i need to check access for only 

            //        // try
            //        //{
            //        //    string path = Request.Path;
            //        //    string PageName = path.Substring(path.LastIndexOf('/'), path.Length - (path.Substring(0, path.LastIndexOf('/')).Length));
            //        //    PageName = PageName.Replace("/", "");

            //        //    int retVal = -1;
            //        //    retVal = Deffinity.AddUserPermissionData.AddUserPermissionData.SectionExists(PageName);
            //        //    if (retVal > 0)
            //        //    {
            //        //        bool result = Deffinity.AddUserPermissionData.AddUserPermissionData.CheckPageAccess(sessionKeys.UID, PageName);
            //        //        if (result == false)
            //        //        {
            //        //            //some error page redirection
            //        //            Response.Redirect("~/NOAuthorization.aspx");
            //        //        }

            //        //    }
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //    LogExceptions.WriteExceptionLog(ex);
            //        //}


            //    }

            //}
        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private static bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/" + WebApiConfig._WebApiExecutionPath);
        }

        protected void Application_Error(object src, EventArgs e)
        {
            bool cont = true;
            if (Request.Url.ToString().ToLower().Contains(".axd"))
            { cont = false; }
            else if (Request.Url.ToString().ToLower().Contains(".asmx"))
            { cont = false; }


            if (cont)
            {
                if (Server.GetLastError() != null)
                {
                    Exception ex = Server.GetLastError();
                    LogExceptions.WriteExceptionLog(ex);
                    // do whatever you want to the error itself (ex.Message, etc.)
                    // clear out the ASP.Net error
                    Context.ClearError();
                    // redirect to an error page
                    // Response.Redirect("~/WF/Message.aspx");
                }
            }
        }
    }
}
