using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.BAL;
using DC.BLL;
using DC.Entity;

namespace DeffinityAppDev.WF.DC
{
    /// <summary>
    /// Summary description for TaskUploadHandler
    /// </summary>
    public class TaskUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)

        {
            int taskid = 0;
           
           
            if (context.Request.QueryString["taskid"] != null)
            {
                taskid = Convert.ToInt32(context.Request.QueryString["taskid"].ToString());
            }

          

            if (context.Request.Files.Count > 0)

            {
                var foldername = taskid.ToString();

                var folder = context.Server.MapPath("~/WF/UploadData/Tasks/" + foldername);
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);

                }


                HttpFileCollection SelectedFiles = context.Request.Files;

                for (int i = 0; i < SelectedFiles.Count; i++)

                {

                    HttpPostedFile PostedFile = SelectedFiles[i];

                    System.IO.FileInfo fi = new System.IO.FileInfo(PostedFile.FileName);

                    var docid = Guid.NewGuid().ToString();
                    var docwithextenstion = docid + "." + fi.Extension;
                    string FileName = context.Server.MapPath("~/WF/UploadData/Tasks/" + foldername + "/" +docwithextenstion);

                    PostedFile.SaveAs(FileName);

                    JobTargetsDocBAL.JobTargetsDocBAL_Add(new JobTargetsDoc() { CallID = 0, JobTargetID = taskid, ContentType = PostedFile.ContentType, FileName = PostedFile.FileName, DocumentID = docwithextenstion });
                    
                }

            }



            else

            {

                context.Response.ContentType = "text/plain";

                context.Response.Write("Please Select Files");

            }



            context.Response.ContentType = "text/plain";

            context.Response.Write("Files Uploaded Successfully!!");

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