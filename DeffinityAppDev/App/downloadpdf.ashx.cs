using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for downloadpdf
    /// </summary>
    public class downloadpdf : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var unid = HttpContext.Current.Request.QueryString["unid"].ToString();

            var type = "";
            if (HttpContext.Current.Request.QueryString["type"] != null)
                type = HttpContext.Current.Request.QueryString["type"].ToString();

            if (type.Length > 0)
            {

                var f= PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == unid).FirstOrDefault();
                var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Tithing/{0}.pdf", unid));
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\""+ f.Title.Trim().Replace(' ','_') + ".pdf" + "\"");
                    //context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    context.Response.ContentType = "octet/stream";
                    //context.Response.Flush();
                    context.Response.TransmitFile(fileInfo.FullName);
                    context.Response.End();
                    // Response.End();

                }
            }
            else
            {

                var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Customers/{0}.pdf", unid));
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + "Donation.pdf" + "\"");
                    //context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    context.Response.ContentType = "octet/stream";
                    //context.Response.Flush();
                    context.Response.TransmitFile(fileInfo.FullName);
                    context.Response.End();
                    // Response.End();

                }
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