using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DeffinityAppDev.WF.DC.Services
{
    /// <summary>
    /// Summary description for filedownload
    /// </summary>
    public class filedownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //string file = context.Request.QueryString["p"];
            //string filename = context.Server.MapPath("~/images/" + file);


           

            var controlid = HttpContext.Current.Request.QueryString["id"].ToString();
                var jobid = HttpContext.Current.Request.QueryString["jobid"].ToString();


                var u = FLSAdditionalInfoBAL.GetFLSAdditonalInfoByCallID(Convert.ToInt32(jobid)).Where(o => o.CustomFieldID == Convert.ToInt32(controlid)).FirstOrDefault();
                if (u != null)
                {
                    var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/DG/{0}/{1}/{2}", jobid.ToString(), controlid, u.FileName));
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    if (fileInfo.Exists)
                    {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
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