using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Portal
{
    public partial class CustomerWarrantyDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var c = GetContact();
                    Gridbind(c);

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private static int GetContact()
        {
            int contactid = 0;
            using (PortfolioMgt.DAL.PortfolioDataContext pdc = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                contactid = pdc.PortfolioContactAssociates.Where(o => o.CustomerUserID == sessionKeys.UID).FirstOrDefault().ContactID.Value;
            }

            return contactid;
        }


        public void Gridbind(int SID)
        {
            try
            {

                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID);
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID));
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

        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                var id = e.CommandArgument;
                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + GetContact());

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(folderpath +"\\"+ e.CommandArgument);
                Response.End();  
            }
        }
    }
}