<%@ WebHandler Language="C#" Class="CustomerPortalNav" %>

using System;
using System.Web;

public class CustomerPortalNav : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        sessionKeys.PortalUser = true;
        if (sessionKeys.PortfolioID >0)
        context.Response.Redirect("~/WF/Portal/Home.aspx", false);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}