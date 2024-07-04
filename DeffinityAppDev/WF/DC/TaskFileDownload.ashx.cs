using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.WF.DC
{
    /// <summary>
    /// Summary description for TaskFileDownload
    /// </summary>
    public class TaskFileDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //string file = context.Request.QueryString["p"];
            //string filename = context.Server.MapPath("~/images/" + file);
            
            var docid = HttpContext.Current.Request.QueryString["docid"].ToString();

            var u = JobTargetsDocBAL.JobTargetsDocBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(docid)).FirstOrDefault();
            if (u != null)
            {
                var file = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/Tasks/{0}/{1}", u.JobTargetID, u.DocumentID));
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + u.FileName + "\"");
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