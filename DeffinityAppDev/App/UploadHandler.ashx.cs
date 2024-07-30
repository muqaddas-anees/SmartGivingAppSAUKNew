using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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

            string unid = string.Empty;
            string donate_unid = string.Empty;
            

            if (context.Request.QueryString["mid"] != null)
            {
                uid_str = context.Request.QueryString["mid"].ToString();
            }

            if (context.Request.QueryString["unid"] != null)
            {
                unid = context.Request.QueryString["unid"].ToString();
            }

            if (context.Request.QueryString["donate_unid"] != null)
            {
                donate_unid = context.Request.QueryString["donate_unid"].ToString();
            }


            if (uid_str.Length > 0)
            {
                if (context.Request.Files.Count > 0)

                {

                    var foldername = uid_str.ToString();


                    if (uid_str.Length > 0)
                    {
                        foldername = uid_str;
                    }

                    //var folder = context.Server.MapPath("~/WF/UploadData/Users/" + foldername);
                    //if (!System.IO.Directory.Exists(folder))
                    //{
                    //    System.IO.Directory.CreateDirectory(folder);

                    //}

                    HttpFileCollection SelectedFiles = context.Request.Files;

                    for (int i = 0; i < SelectedFiles.Count; i++)

                    {
                        HttpPostedFile PostedFile = SelectedFiles[i];

                        //string FileName = context.Server.MapPath("~/WF/UploadData/Users/" + foldername + "/" + PostedFile.FileName);
                        //PostedFile.SaveAs(FileName);

                        // ImageManager.SaveUserImage_setpath()
                        //byte[] imageData = null;
                        //using (BinaryReader reader = new BinaryReader(PostedFile.InputStream))
                        //{
                        //    imageData = reader.ReadBytes(PostedFile.ContentLength);
                        //}
                        //ImageManager.SaveUserImage_setpath(imageData, foldername, "");


                        using (Stream fs = PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                ImageManager.FileDBSave(bytes, null, foldername, ImageManager.file_section_user_doc, System.IO.Path.GetExtension(PostedFile.FileName).ToLower(), PostedFile.FileName, PostedFile.ContentType,"",true);

                            }
                        }

                    }

                }
            }
           else if (donate_unid.Length > 0)
            {
                if (context.Request.Files.Count > 0)

                {

                    var foldername = donate_unid.ToString();


                    //if (donate_unid.Length > 0)
                    //{
                    //    foldername = donate_unid;
                    //}

                    //var folder = context.Server.MapPath("~/WF/UploadData/Donations/" + foldername);
                    //if (!System.IO.Directory.Exists(folder))
                    //{
                    //    System.IO.Directory.CreateDirectory(folder);

                    //}

                    //HttpFileCollection SelectedFiles = context.Request.Files;

                    //for (int i = 0; i < SelectedFiles.Count; i++)

                    //{
                    //    HttpPostedFile PostedFile = SelectedFiles[i];

                    //    string FileName = context.Server.MapPath("~/WF/UploadData/Donations/" + foldername + "/" + PostedFile.FileName);
                    //    PostedFile.SaveAs(FileName);

                    //}
                    HttpFileCollection SelectedFiles = context.Request.Files;

                    for (int i = 0; i < SelectedFiles.Count; i++)

                    {
                        HttpPostedFile PostedFile = SelectedFiles[i];
                        using (Stream fs = PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                ImageManager.FileDBSave(bytes, null, donate_unid, ImageManager.file_section_donor_doc, System.IO.Path.GetExtension(PostedFile.FileName).ToLower(), PostedFile.FileName, PostedFile.ContentType, "", true);

                            }
                        }
                    }
                }
            }
            else if(unid.Length >0)
            {

                if (context.Request.Files.Count > 0)

                {

                    var foldername = unid.ToString();

                    //var folder = context.Server.MapPath("~/WF/UploadData/Events/" + foldername);
                    //if (!System.IO.Directory.Exists(folder))
                    //{
                    //    System.IO.Directory.CreateDirectory(folder);

                    //}

                    HttpFileCollection SelectedFiles = context.Request.Files;

                    for (int i = 0; i < SelectedFiles.Count; i++)

                    {
                        HttpPostedFile PostedFile = SelectedFiles[i];

                        //var cnt = System.IO.Directory.GetFiles(folder).Count();

                        // string FileType = Path.GetExtension(PostedFile.FileName).ToLower().Trim();
                        // string fName = cnt + ".png";

                        // string FileName = context.Server.MapPath("~/WF/UploadData/Events/" + foldername + "/" + fName);
                        // PostedFile.SaveAs(FileName);

                        string FileType = Path.GetExtension(PostedFile.FileName).ToLower().Trim();
                        string fName = "0" + ".png";

                        //string FileName = context.Server.MapPath("~/WF/UploadData/Events/" + foldername + "/" + fName);
                        //PostedFile.SaveAs(FileName);

                        byte[] imageData = null;
                        using (BinaryReader reader = new BinaryReader(PostedFile.InputStream))
                        {
                            imageData = reader.ReadBytes(PostedFile.ContentLength);
                        }
                        ImageManager.SaveEventImage_setpath(imageData, foldername, "", foldername);


                    }

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