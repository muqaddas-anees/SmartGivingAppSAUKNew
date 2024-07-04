using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)

        {
            int uid = 0;
            string uid_str = string.Empty;

            if (context.Request.QueryString["mid"] != null)
            {
                uid_str = context.Request.QueryString["mid"].ToString();
            }

            if (context.Request.Files.Count > 0)

            {

                var foldername = uid_str.ToString();


                if (uid_str.Length > 0)
                {
                    foldername = uid_str;
                }

                var folder = context.Server.MapPath("~/WF/UploadData/Users/" + foldername);
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);

                }

                HttpFileCollection SelectedFiles = context.Request.Files;

                for (int i = 0; i < SelectedFiles.Count; i++)

                {
                    HttpPostedFile PostedFile = SelectedFiles[i];

                    string FileName = context.Server.MapPath("~/WF/UploadData/Users/" + foldername + "/" + PostedFile.FileName);
                    PostedFile.SaveAs(FileName);

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