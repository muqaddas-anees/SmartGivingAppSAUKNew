<%@ WebHandler Language="C#" Class="DCCustomerNavigation" %>

using System;
using System.Web;

public class DCCustomerNavigation : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string callid = string.Empty;
            string ccid = string.Empty;
        string type = string.Empty;

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
        }
        if (type == "FLS")
        {
            if (callid != string.Empty)
            {
                context.Response.Redirect("FLSCustomer.aspx?CCID="+ccid+"&callid=" + callid);
            }
            else
                context.Response.Redirect("FLSCustomer.aspx");
        }
        else if (type == "Delivery")
        {
            if (callid != string.Empty)
                context.Response.Redirect("DeliveryCustomer.aspx?callid=" + callid);
            else
                context.Response.Redirect("DeliveryCustomer.aspx");
        }
        else if (type == "Access Control")
        {
            if (callid != string.Empty)
                context.Response.Redirect("CustomerAccessControl.aspx?callid=" + callid);
            else
                context.Response.Redirect("CustomerAccessControl.aspx");
        }
        else if (type == "Permit to Work")
        {
            if (callid != string.Empty)
                context.Response.Redirect("PermitToWorkCustomer.aspx?callid=" + callid);
            else
                context.Response.Redirect("PermitToWorkCustomer.aspx");
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}