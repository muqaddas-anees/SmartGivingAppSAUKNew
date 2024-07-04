using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DeffinityAppDev.WF.Assets
{
    public partial class WarrantyDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["assetid"] != null)
                {
                    int assetid = Convert.ToInt32(Request.QueryString["assetid"]);
                    var folder = Server.MapPath("~/WF/UploadData/Assets/asset" + assetid);
                    Gridbind(assetid);
                }
            }
        }

        public void Gridbind(int SID)
        {
            try
            {
                var folderpath = Server.MapPath("~/WF/UploadData/Assets/asset" + SID);
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Assets/asset" + SID));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                    }

                    gridfiles.DataSource = files;
                    gridfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void FileuploadMethod(string folder)
        {
            try
            {
                if (fileupload.HasFile)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];
                        string fileName = Path.GetFileName(userPostedFile.FileName);

                        if (!System.IO.Directory.Exists(folder))
                        {
                            System.IO.Directory.CreateDirectory(folder);
                            userPostedFile.SaveAs(folder + "\\" + fileName);
                        }
                        else
                        {
                            userPostedFile.SaveAs(folder + "\\" + fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int assetid = Convert.ToInt32(Request.QueryString["assetid"]);
             var folder = Server.MapPath("~/WF/UploadData/Assets/asset" + assetid);
             FileuploadMethod(folder);
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                File.Delete(filePath);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}