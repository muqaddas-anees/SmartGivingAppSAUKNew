using Deffinity.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)

        {
            int Project = 0;
            int folderid = 0;
            //if (context.Request.QueryString["Project"] != null)
            //{
            //    Project = Convert.ToInt32(context.Request.QueryString["Project"].ToString());
            //}
            if (context.Request.QueryString["FolderID"] != null)
            {
                folderid = Convert.ToInt32(context.Request.QueryString["folderid"].ToString());
            }
            if (folderid > 0)
            {
                if (context.Request.Files.Count > 0)

                {
                    HttpFileCollection SelectedFiles = context.Request.Files;

                    for (int i = 0; i < SelectedFiles.Count; i++)

                    {

                        HttpPostedFile PostedFile = SelectedFiles[i];

                        string folderID = folderid.ToString();// getFolderID();
                                                              //string project = Request.QueryString["project"].ToString();
                        var fileContentType = PostedFile.ContentType;
                        var fileName = PostedFile.FileName;
                        var fileContentLength = PostedFile.ContentLength;
                        //byte[] imageBytes = PostedFile.GetContents();
                        //var filecontent = imageBytes;
                        using (Stream fs = PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                var filecontent = bytes;

                                AC2P_DocumentsController AC2PDocumentsController = new AC2P_DocumentsController();
                                AC2PDocumentsController.DN_ProjectUploadInsertNew(Project.ToString(), fileName, filecontent, fileName, fileContentType, "P", fileContentLength, int.Parse(folderID), sessionKeys.UID, sessionKeys.IncidentID);
                            
                             
                            }
                        }

                    }

                }



                else

                {

                    context.Response.ContentType = "text/plain";

                    context.Response.Write("Please Select Files");

                }
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