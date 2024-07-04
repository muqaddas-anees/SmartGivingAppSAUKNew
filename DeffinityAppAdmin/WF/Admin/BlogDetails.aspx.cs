using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class BlogDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["BlogRef"] != null)
                    {
                        var bEntity = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().Where(o => o.BlogRef == Request.QueryString["BlogRef"].ToString()).FirstOrDefault();
                        if (bEntity != null)
                        {
                            lblUserfullstuffName.Text = "Update - " + bEntity.BlogTitle;
                            txtTitle.Text = bEntity.BlogTitle;
                            chkActive.Checked = bEntity.IsActive.HasValue ? bEntity.IsActive.Value : false;
                            CKEditor1.Text = Server.HtmlDecode(bEntity.BlogContent);
                            txtButtonTitle.Text = bEntity.Button1Title;
                            txtButtonLink.Text = bEntity.Button1Link;
                            if (bEntity.StartDate.HasValue)
                                txtStartDate.Text = bEntity.StartDate.Value.ToShortDateString();
                            if (bEntity.EndDate.HasValue)
                                txtEndDate.Text = bEntity.EndDate.Value.ToShortDateString();

                            txtButton2Title.Text = bEntity.Button2Title;
                            txtButton2Link.Text = bEntity.Button2Link;
                        }
                    }
                    else
                    {
                        lblUserfullstuffName.Text = "Add New";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
        private void uploadLogo(string blogref)
        {
            try
            {
                if (fileupload.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(fileupload.PostedFile.InputStream);
                    ImageManager.SaveBlogImage_setpath(fileupload.FileBytes, blogref, Deffinity.systemdefaults.GetLogoFolderPath());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void AddUpdateData()
        {
            if (Request.QueryString["BlogRef"] != null)
            {
                var bEntity = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().Where(o => o.BlogRef == Request.QueryString["BlogRef"].ToString()).FirstOrDefault();
                if (bEntity != null)
                {
                    bEntity.BlogTitle = txtTitle.Text;
                    bEntity.IsActive = chkActive.Checked;
                    bEntity.BlogContent = Server.HtmlEncode(CKEditor1.Text);
                    bEntity.Button1Link = txtButtonLink.Text.Trim();
                    bEntity.Button1Title = txtButtonTitle.Text.Trim();
                    bEntity.Button2Link = txtButton2Link.Text.Trim();
                    bEntity.Button2Title = txtButton2Title.Text.Trim();
                    if (txtStartDate.Text.Length > 0)
                        bEntity.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    if (txtEndDate.Text.Length > 0)
                        bEntity.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());

                   
                    PortfolioMgt.BAL.BlogBAL.BlogBAL_Update(bEntity);
                    uploadLogo(bEntity.BlogRef);
                }
            }
            else
            {
                var bEntity = new PortfolioMgt.Entity.tblBlog();
                bEntity.BlogTitle = txtTitle.Text;
                bEntity.IsActive = chkActive.Checked;
                bEntity.BlogContent = Server.HtmlEncode(CKEditor1.Text);
                bEntity.Button1Link = txtButtonLink.Text.Trim();
                bEntity.Button1Title = txtButtonTitle.Text.Trim();
                bEntity.Button2Link = txtButton2Link.Text.Trim();
                bEntity.Button2Title = txtButton2Title.Text.Trim();
                if (txtStartDate.Text.Length > 0)
                    bEntity.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                if (txtEndDate.Text.Length > 0)
                    bEntity.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                bEntity.Position = PortfolioMgt.BAL.BlogBAL.PBlogBAL_SelectAll().Count()+1;
                PortfolioMgt.BAL.BlogBAL.BlogBAL_Add(bEntity);
                uploadLogo(bEntity.BlogRef);

            }
            Response.Redirect("~/WF/Admin/BlogList.aspx", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AddUpdateData();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}