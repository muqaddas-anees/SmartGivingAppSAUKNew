//using Bryntum.Gantt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.WF.DC
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler

    {

        public void ProcessRequest(HttpContext context)

        {
            int callid = 0;
            int ivref = 0;
            int option = 0;
            string qid = string.Empty;
            if(context.Request.QueryString["callid"] != null)
            {
                callid = Convert.ToInt32( context.Request.QueryString["callid"].ToString());
            }

            if (context.Request.QueryString["ivref"] != null)
            {
                ivref = Convert.ToInt32(context.Request.QueryString["ivref"].ToString());
            }

            if (context.Request.QueryString["Option"] != null)
            {
                option = Convert.ToInt32(context.Request.QueryString["Option"].ToString());
            }

            if (context.Request.QueryString["qid"] != null)
            {
                qid = context.Request.QueryString["qid"].ToString();
            }

            if (context.Request.Files.Count > 0)

            {

                var foldername = callid.ToString();

                if(ivref > 0)
                {
                    foldername = callid.ToString() + "-" + ivref.ToString();
                }

                if (option > 0)
{
                    foldername = callid.ToString() + "-OPTION" + option.ToString();
                }

                if(qid.Length >0)
                {
                    foldername = qid;
                }

                var folder = context.Server.MapPath("~/WF/UploadData/DC/"+ foldername);
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                    
                }
               

                HttpFileCollection SelectedFiles = context.Request.Files;

                for (int i = 0; i < SelectedFiles.Count; i++)

                {

                    HttpPostedFile PostedFile = SelectedFiles[i];

                    string FileName = context.Server.MapPath("~/WF/UploadData/DC/"+ foldername + "/" + PostedFile.FileName);

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