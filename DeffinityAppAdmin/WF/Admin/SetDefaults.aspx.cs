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
    public partial class SetDefaults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DisplayLogo();
                setSkinCheckbox();
            }
        }

        private void DisplayLogo()
        {
            try
            {
                string filePath = Deffinity.systemdefaults.GetLogoFolderPath()+ "\\Partner_" + sessionKeys.PartnerID + ".png";
                if (File.Exists(filePath))
                {
                    imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + "\\WF\\UploadData\\Customers\\Partner_" + sessionKeys.PartnerID + ".png" + "?rid=" + RandomNumber();
                    imgLogo.Visible = true;

                }
                else
                    imgLogo.Visible = false;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 1000);
        }
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload1.PostedFile.InputStream);
                    ImageManager.SavePartnerImage_setpath(FileUpload1.FileBytes, sessionKeys.PartnerID.ToString(),Deffinity.systemdefaults.GetLogoFolderPath());
                    DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void setSkinCheckbox()
        {
            try
            {
                var pentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32( Request.QueryString["tid"].ToString()));
                if (pentity != null)
                {
                    var theme = pentity.Theme;

                    if (theme == "Aero")
                        chkAero.Checked = true;
                    else if (theme == "Concrete")
                        chkConcrete.Checked = true;
                    else if (theme == " ")
                        chkDefault.Checked = true;
                    else if (theme == "Facebook")
                        chkFacebook.Checked = true;
                    else if (theme == "Green")
                        chkGreen.Checked = true;

                    else if (theme == "Lemonade")
                        chkLemonade.Checked = true;
                    else if (theme == "Lime")
                        chkLime.Checked = true;
                    else if (theme == "Navy")
                        chkNavy.Checked = true;
                    else if (theme == "Purple")
                        chkPurple.Checked = true;
                    else if (theme == "turquoise")
                        chkTurquise.Checked = true;
                    else if (theme == "Watermelon")
                        chkWatermelon.Checked = true;
                    else if (theme == "White")
                        chkWhite.Checked = true;
                    else
                        chkDefault.Checked = true;

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnApplySKin_Click(object sender, EventArgs e)
        {
            string getSkin = string.Empty;
            if (chkAero.Checked)
                getSkin = "Aero";
            else if (chkConcrete.Checked)
                getSkin = "Concrete";
            else if (chkDefault.Checked)
                getSkin = " ";
            else if (chkFacebook.Checked)
                getSkin = "Facebook";
            else if (chkGreen.Checked)
                getSkin = "Green";
            else if (chkLemonade.Checked)
                getSkin = "Lemonade";
            else if (chkLime.Checked)
                getSkin = "Lime";
            else if (chkNavy.Checked)
                getSkin = "Navy";
            else if (chkPurple.Checked)
                getSkin = "Purple";
            else if (chkTurquise.Checked)
                getSkin = "turquoise";
            else if (chkWatermelon.Checked)
                getSkin = "Watermelon";
            else if (chkWhite.Checked)
                getSkin = "White";


            var pentity = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(Convert.ToInt32(Request.QueryString["tid"].ToString()));
            if(pentity != null)
            {
                pentity.Theme = getSkin;
                PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Update(pentity);
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;

            }

        }
    }
}