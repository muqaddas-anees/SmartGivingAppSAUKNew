using System;
using System.Web;

namespace DeffinityAppDev
{
    //public class ContentSecurityPolicyModule : IHttpModule
    //{
    //    /// <summary>
    //    /// You will need to configure this module in the Web.config file of your
    //    /// web and register it with IIS before being able to use it. For more information
    //    /// see the following link: https://go.microsoft.com/?linkid=8101007
    //    /// </summary>
    //    #region IHttpModule Members

    //    public void Dispose()
    //    {
    //        //clean-up code here.
    //    }

    //    public void Init(HttpApplication context)
    //    {
    //        context.BeginRequest += (sender, e) => AddContentSecurityPolicyHeader();
    //    }

    //    private void AddContentSecurityPolicyHeader()
    //    {
    //        HttpContext.Current.Response.Headers.Add("Content-Security-Policy", "script-src 'self' https://js.stripe.com; frame-src 'self' https://js.stripe.com;");
    //    }

    //    #endregion

    //    public void OnLogRequest(Object source, EventArgs e)
    //    {
    //        //custom logging logic can go here
    //    }
    //}
}
