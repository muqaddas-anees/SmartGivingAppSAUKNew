<%@ WebHandler Language="C#" Class="DCNavigation" %>

using System;
using System.Web;

public class DCNavigation : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string callid = string.Empty;
             string ccid = string.Empty;
        string type = string.Empty;
        
        try
        {
            if (context.Request.QueryString["callid"] != null)
            {
                callid = context.Request.QueryString["callid"];
            }
             if (context.Request.QueryString["CCID"] != null)
            {
                ccid = context.Request.QueryString["CCID"];
            }
            if (context.Request.QueryString["type"] != null)
            {
                type = context.Request.QueryString["type"];
                type = type.Replace(" ", String.Empty).ToLower();
            }
            if (type == "FLS".ToLower())
            {
                if (callid != string.Empty)
                    context.Response.Redirect("~/WF/DC/FLSForm.aspx?CCID="+ccid+"&callid=" + callid+"&SDID="+callid,false);
                else
                    context.Response.Redirect("~/WF/DC/FLSForm.aspx",false);
            }
            else if (type == "Delivery".ToLower())
            {
                if (callid != string.Empty)
                    context.Response.Redirect("~/WF/DC/Delivery.aspx?callid=" + callid,false);
                else
                    context.Response.Redirect("~/WF/DC/Delivery.aspx",false);
            }
            else if (type == "AccessControl".ToLower())
            {
                if (callid != string.Empty)
                    context.Response.Redirect("~/WF/DC/AccessControl.aspx?callid=" + callid,false);
                else
                    context.Response.Redirect("~/WF/DC/AccessControl.aspx",false);
            }
            else if (type == "PermittoWork".ToLower())
            {
                if (callid != string.Empty)
                    context.Response.Redirect("~/WF/DC/PermitToWork.aspx?callid=" + callid,false);
                else
                    context.Response.Redirect("~/WF/DC/PermitToWork.aspx",false);
            }
            if (context.Request.QueryString["ticket"] != null)
            {
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}