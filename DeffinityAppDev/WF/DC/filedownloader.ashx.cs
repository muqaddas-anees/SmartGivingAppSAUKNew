using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.WF.DC
{
    /// <summary>
    /// Summary description for filedownloader
    /// </summary>
    public class filedownloader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var unid = "";
            //var cid = "";

            if (HttpContext.Current.Request.QueryString["unid"] != null)
                unid = HttpContext.Current.Request.QueryString["unid"].ToString();

            //if (HttpContext.Current.Request.QueryString["cid"] != null)
            //    cid = HttpContext.Current.Request.QueryString["cid"].ToString();



            if (unid.Length > 0)
            {
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> frep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();
                var f = frep.GetAll().Where(o => o.FileUNID == unid).FirstOrDefault();
                //  var f = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == unid).FirstOrDefault();
                var fileExtension = f.FileName.Split('.').Last();


                var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Progress/{0}", f.FileUNID + "." + fileExtension));
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + f.FileName + "\"");
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