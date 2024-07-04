using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App.Events
{
    /// <summary>
    /// Summary description for downloadpdf
    /// </summary>
    public class downloadpdf : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var unid = HttpContext.Current.Request.QueryString["unid"].ToString();

            var eD= PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(unid);
            var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Events/{0}.pdf", unid));
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
            if (fileInfo.Exists)
            {
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + eD.Title.Trim().Replace(' ','_')+ ".pdf" + "\"");
                //context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                context.Response.ContentType = "octet/stream";
                //context.Response.Flush();
                context.Response.TransmitFile(fileInfo.FullName);
                context.Response.End();
                // Response.End();
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
}