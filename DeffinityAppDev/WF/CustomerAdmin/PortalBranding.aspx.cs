using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class SetLogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    if (Request.QueryString["nav"] != null)
                        linkBack.Visible = true;
                    else
                        linkBack.Visible = false;

                    imgLogo.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio";
                    imgBanner.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=" + ImageManager.file_section_banner;
                    BindCompanyInfo();
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }


            }

            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }
        }

        private void BindCompanyInfo()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var p = prep.GetAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
            if (p != null)
            {
                txtCompany.Text = p.PortFolio;
                txtAccountNumber.Text = p.AccountNumber;
                txtIBAN.Text = p.IBAN;
                txtSortCode.Text = p.SortCode;
                txtSwiftCode.Text = p.SwiftCode;
                txtTaxReg.Text = p.TaxReg;
                txtBank.Text = p.Chat_ChannelName;// p.BankName;
                txtRegistrationNumber.Text = p.RegistrationNumber;
                txtAddress.Text = p.Address;
            }
        }

        ////private void DisplayLogo()
        ////{
        ////    try
        ////    {
        ////        string filePath = Server.MapPath("~/WF/UploadData/Customers/Portfolio_" + sessionKeys.PortfolioID + ".png");
        ////        if (File.Exists(filePath))
        ////        {
        ////            imgLogo.ImageUrl = "/WF/UploadData/Customers/Portfolio_" + sessionKeys.PortfolioID + ".png" + "?rid=" + RandomNumber();
        ////            imgLogo.Visible = true;

        ////        }
        ////        else
        ////            imgLogo.Visible = false;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        LogExceptions.WriteExceptionLog(ex);
        ////    }
        ////}
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
                    ImageManager.SaveProtfolioImage_setpath(FileUpload1.FileBytes, sessionKeys.PortfolioID.ToString());


                    Deffinity.PortfolioManager.Portfilio.UpdateLogo(sessionKeys.PortfolioID);
                    imgLogo.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s=portfolio";
                    //DisplayLogo();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
                var p = prep.GetAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                if (p != null)
                {
                    p.PortFolio = txtCompany.Text.Trim();
                    p.AccountNumber = txtAccountNumber.Text.Trim();
                    p.IBAN = txtIBAN.Text.Trim();
                    p.SortCode = txtSortCode.Text.Trim();
                    p.SwiftCode = txtSwiftCode.Text.Trim();
                    p.TaxReg = txtTaxReg.Text.Trim();
                    p.Chat_ChannelName = txtBank.Text.Trim();
                    p.RegistrationNumber = txtRegistrationNumber.Text.Trim();
                    p.Address = txtAddress.Text.Trim();
                    prep.Edit(p);
                    sessionKeys.PortfolioName = txtCompany.Text;
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUploadBanner_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload2.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(FileUpload2.PostedFile.InputStream);
                    ImageManager.SaveProtfolioBannermage_setpath(FileUpload2.FileBytes, sessionKeys.PortfolioID.ToString());


                    //  Deffinity.PortfolioManager.Portfilio.UpdateLogo(sessionKeys.PortfolioID);
                    imgBanner.ImageUrl = "~/ImageHandler.ashx?id=" + sessionKeys.PortfolioID + "&s="+ImageManager.file_section_banner;
                    //DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}