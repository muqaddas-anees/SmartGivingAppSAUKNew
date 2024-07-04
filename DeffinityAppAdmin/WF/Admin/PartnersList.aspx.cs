using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class PartnersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       
        private void BindGrid()
        {
            try
            {
                var mlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll();
                GridPartner.DataSource = mlist;
                GridPartner.DataBind();
                if (mlist.Count == 0)
                {
                    AddNewPopup();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridPartner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if(e.CommandName== "Theme")
                {
                    var partnerID = e.CommandArgument.ToString();
                    Response.Redirect(string.Format( "~/WF/Admin/SetDefaults.aspx?tid={0}",partnerID.ToString()),false);
                }
                else
                if (e.CommandName == "editmodule")
                {
                    huid.Value = e.CommandArgument.ToString();
                    var m = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32(huid.Value));
                    if (m != null)
                    {
                        txtPartnerName.Text = m.PartnerName;
                        txtWebSite.Text = m.PartnerWebSite;
                        txtPortalUrl.Text = m.ParnerPortal;
                        chkIsActive.Checked = m.IsActive.HasValue?m.IsActive.Value:false;
                        txtPortalName.Text = m.Portalname;
                        txtSupportEmail.Text = m.SupportEmail;
                        txtFromEmail.Text = m.FromEmail;
                        txtTrackerUrl.Text = m.TrackerUrl;
                        mdlManageOptions.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var moduleid = Convert.ToInt32(huid.Value);

                if (huid.Value == "0")
                {
                    var p = new PortfolioMgt.Entity.PartnerDetail();
                    p.PartnerName = txtPartnerName.Text.Trim();
                    p.PartnerWebSite = txtWebSite.Text.Trim();
                    p.ParnerPortal = txtPortalUrl.Text.Trim();
                    p.IsActive = true;
                    p.LoggedDate = DateTime.Now;
                    p.Portalname = txtPortalName.Text.Trim();
                    p.SupportEmail = txtSupportEmail.Text.Trim();
                    p.FromEmail = txtFromEmail.Text.Trim();
                    p.TrackerUrl = txtTrackerUrl.Text.Trim();
                    var pret = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Add(p);
                    uploadLogo(pret.ID);
                    uploadBackgroundImage(pret.ID);
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    ClearFields();
                    mdlManageOptions.Hide();
                    BindGrid();
                }
                else
                {
                    var p = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32(huid.Value));
                    if (p != null)
                    {
                        p.PartnerName = txtPartnerName.Text.Trim();
                        p.PartnerWebSite = txtWebSite.Text.Trim();
                        p.ParnerPortal = txtPortalUrl.Text.Trim();
                        p.IsActive = chkIsActive.Checked;
                        p.Portalname = txtPortalName.Text.Trim();
                        p.SupportEmail = txtSupportEmail.Text.Trim();
                        p.FromEmail = txtFromEmail.Text.Trim();
                        p.TrackerUrl = txtTrackerUrl.Text.Trim();
                        PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Update(p);
                        uploadLogo(p.ID);
                        uploadBackgroundImage(p.ID);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        ClearFields();
                        BindGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string DisplayLogo(string partnerid)
        {
            string retval = string.Empty;
            try
            {
                string filePath = Deffinity.systemdefaults.GetLogoFolderPath() + "\\Partner_" + partnerid + ".png";
                if (File.Exists(filePath))
                {
                    retval = Deffinity.systemdefaults.GetWebUrl() + "\\WF\\UploadData\\Customers\\Partner_" + partnerid + ".png" + "?rid=" + RandomNumber();
                    //imgLogo.Visible = true;
                }
                else
                    retval = string.Empty;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        protected string DisplayBackLogo(string partnerid)
        {
            string retval = string.Empty;
            try
            {
                string filePath = Deffinity.systemdefaults.GetLogoFolderPath() + "\\PartnerBack_" + partnerid + ".png";
                if (File.Exists(filePath))
                {
                    retval = Deffinity.systemdefaults.GetWebUrl() + "/WF/UploadData/Customers/PartnerBack_" + partnerid + ".png" + "?rid=" + RandomNumber();
                    //imgLogo.Visible = true;
                }
                else
                    retval = string.Empty;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        protected bool DisplayLogoVisble(string partnerid)
        {
            bool retval = false;
            try
            {
                string filePath = Deffinity.systemdefaults.GetLogoFolderPath() + "\\Partner_" + partnerid + ".png";
                if (File.Exists(filePath))
                {
                    retval = true;
                    //imgLogo.Visible = true;
                }
              
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        protected bool DisplayBackLogoVisble(string partnerid)
        {
            bool retval = false;
            try
            {
                string filePath = Deffinity.systemdefaults.GetLogoFolderPath() + "\\PartnerBack_" + partnerid + ".png";
                if (File.Exists(filePath))
                {
                    retval = true;
                    //imgLogo.Visible = true;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        private int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 1000);
        }
        private void uploadLogo(int partnerID)
        {
            try
            {
                if (FileUpload1.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload1.PostedFile.InputStream);
                    ImageManager.SavePartnerImage_setpath(FileUpload1.FileBytes, partnerID.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                   // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void uploadBackgroundImage(int partnerID)
        {
            try
            {
                if (FileUpload2.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload2.PostedFile.InputStream);
                    ImageManager.SavePartnerBackImage_setpath(FileUpload2.FileBytes, partnerID.ToString(), Deffinity.systemdefaults.GetLogoFolderPath());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewPopup();
        }

        private void AddNewPopup()
        {
            huid.Value = "0";
            ClearFields();
            mdlManageOptions.Show();
        }

        private void ClearFields()
        {
            txtPartnerName.Text = string.Empty;
            txtWebSite.Text = string.Empty;
            txtPortalUrl.Text = string.Empty;
            chkIsActive.Checked = false;
            txtPortalName.Text = string.Empty;
            txtFromEmail.Text = string.Empty;
            txtSupportEmail.Text = string.Empty;
            txtTrackerUrl.Text = string.Empty;
        }
    }
}