<%@ WebHandler Language="C#" Class="DownloadAppFile" %>

using System;
using System.Web;

public class DownloadAppFile : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        if (context.Request.QueryString["file"] != null)
        {
            string newFile = context.Server.MapPath(string.Format("~\\UploadData\\App\\{0}.xlsx", context.Request.QueryString["file"].ToString()));
            if (System.IO.File.Exists(newFile))
            {
                //File.Copy(resumeFile, newFile);
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.Response.AppendHeader("content-disposition", "attachment;filename=\"" + context.Request.QueryString["file"].ToString() + ".xlsx\"");
                context.Response.TransmitFile(newFile);
                context.Response.End();
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}