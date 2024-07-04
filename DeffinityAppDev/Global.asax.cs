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
using System.Web.Configuration;
using Stripe;
using System.IO;
using System.Security.AccessControl;
using Hangfire;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(DeffinityAppDev.Startup))]
    
namespace DeffinityAppDev
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Headers.Add("Content-Security-Policy", "script-src 'self' https://js.stripe.com; frame-src 'self' https://js.stripe.com;");
        //    HttpContext.Current.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        //    HttpContext.Current.Response.Headers.Add("X-Frame-Options", "DENY");
        //    HttpContext.Current.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        //}

        protected void Application_Start()
        {
           // DeffinityManager.DBCentralAccess.setInstance("HWA");
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;


            //var secretKey = WebConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey();// "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";

            //SetPermissionsToLog("WF\\Log");
            //SetPermissionsToLog("WF\\UploadData");
          ////  Hangfire.GlobalConfiguration.Configuration
          ////.UseSqlServerStorage("DBstring");

          ////  var serverOptions = new BackgroundJobServerOptions
          ////  {
          ////      WorkerCount = 1
          ////  };
          ////  var backgroundJobServer = new BackgroundJobServer(serverOptions);

          ////  RecurringJob.AddOrUpdate("email-job", () => SendEmail(), "*/5 * * * *");

        }

        public void SendEmail()
        {

            DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignSchedule.CampainMainSending();
            // Email em = new Email();
            // em.SendingMail("indra@deffinity.com", "Hangfire mail", "Hangfire mail");
        }
        //public void SetPermissionsToLog(string folder)
        //{
        //   string folderPath = Server.MapPath(folder);
        //    DirectorySecurity folderSecurity = Directory.GetAccessControl(folderPath);

        //    // Create a new access rule for the IIS_IUSRS group
        //    FileSystemAccessRule iisUsersAccessRule = new FileSystemAccessRule("IIS_IUSRS", FileSystemRights.ReadAndExecute | FileSystemRights.Write | FileSystemRights.Modify, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);

        //    // Add the access rule to the folder's access control list
        //    folderSecurity.AddAccessRule(iisUsersAccessRule);

        //    // Apply the updated access control list to the folder
        //    Directory.SetAccessControl(folderPath, folderSecurity);
        //}
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

            //redirect to https page
            //avoid localhost for mail sending purpose
            //if (!Request.Url.AbsoluteUri.ToLower().Contains("localhost"))
            //{
            //    if (!Request.IsSecureConnection)
            //    {
            //        Response.Redirect(string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4)));
            //    }
            //}

            if ((!Request.Path.ToLower().Contains("default.aspx")) && (Request.Path.ToLower().Contains(".aspx")))
            {

                if (sessionKeys.UID > 0)
                {
                    // check wather the page journal is enable 
                    //if (PageJornal.Get_JorunalStatus())
                    //{
                    //    using (Userjournal uj = new Userjournal())
                    //    {
                    //        uj.userJournal_insert(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""), sessionKeys.UID);
                    //    }
                    //    //PageJornal.PageJournal_Insert(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", ""), sessionKeys.UID);
                    //}
                    // i need to check access for only 

                    // try
                    //{
                    //    string path = Request.Path;
                    //    string PageName = path.Substring(path.LastIndexOf('/'), path.Length - (path.Substring(0, path.LastIndexOf('/')).Length));
                    //    PageName = PageName.Replace("/", "");

                    //    int retVal = -1;
                    //    retVal = Deffinity.AddUserPermissionData.AddUserPermissionData.SectionExists(PageName);
                    //    if (retVal > 0)
                    //    {
                    //        bool result = Deffinity.AddUserPermissionData.AddUserPermissionData.CheckPageAccess(sessionKeys.UID, PageName);
                    //        if (result == false)
                    //        {
                    //            //some error page redirection
                    //            Response.Redirect("~/NOAuthorization.aspx");
                    //        }

                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogExceptions.WriteExceptionLog(ex);
                    //}


                }

            }
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
