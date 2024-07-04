<%@ WebHandler Language="C#" Class="Print" %>

using System;
using System.Web;

public class Print : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        try
        {
            string filename = string.Empty;
            if (context.Request.QueryString["HealthCheckID"] != null)
            {
                int healthCheckListID = Convert.ToInt32(context.Request.QueryString["HealthCheckID"]);
                filename = context.Server.MapPath("~\\WF\\UploadData\\HC\\HC" + healthCheckListID.ToString() + ".pdf");
            }
            if (context.Request.QueryString["HealthCheckIDInSD"] != null)
            {
                int HealthCheckIdInSd = Convert.ToInt32(context.Request.QueryString["HealthCheckIDInSD"]);
                filename = context.Server.MapPath("~\\WF\\UploadData\\HC\\SD" + HealthCheckIdInSd.ToString() + ".pdf");
            }
            if (context.Request.QueryString["HealthCheckIDInPD"] != null)
            {
                int HealthCheckIdInPd = Convert.ToInt32(context.Request.QueryString["HealthCheckIDInPD"]);
                filename = context.Server.MapPath("~\\WF\\UploadData\\HC\\PD" + HealthCheckIdInPd.ToString() + ".pdf");
            }
            System.IO.FileInfo file = new System.IO.FileInfo(filename);
            if ((file.Exists))
            {
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                context.Response.AddHeader("Content-Length", file.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
                context.Response.Close();
                file = null;
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