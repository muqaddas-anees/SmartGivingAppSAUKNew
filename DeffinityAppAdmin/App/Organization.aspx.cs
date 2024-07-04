using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Deffinity.PortfolioManager;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class Organization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindReligion();
                    BindGroup(0);
                    BindDenomination(0);
                    BindCountry();
                    ddlCountry.SelectedValue = "190";
                    //var rlist = 
                    if (Request.QueryString["orgid"] != null)
                    {
                        BindOrganization();

                        if (sessionKeys.Message.Length > 0)
                        {
                            DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, "");
                            sessionKeys.Message = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
          public void Gridbind(int SID)
        {
            try
            {
               
                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_portfolio_doc).Where(o => o.FileID == SID.ToString()).ToList();

                var rList = (from r in fList
                             select new
                             {
                                 Value = r.FileID,
                                 Text = r.FileName,

                             }).ToList();
                gridfiles.DataSource = rList;
                gridfiles.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                // File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var f = fRep.GetAll().Where(o => o.FileID == filePath && o.Section == ImageManager.file_section_portfolio_doc).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id=" + filePath + "&s=" + ImageManager.file_section_portfolio_doc);
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindOrganization()
        {
            var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == QueryStringValues.OrgID).FirstOrDefault();
            if (pEntity != null)
            {
                Gridbind(pEntity.ID);
                txtOrganizationName.Text = pEntity.PortFolio;
                txtCostCenter.Text = pEntity.CostCentre??"";
                txtAddress.Text = pEntity.Address;
                txtState.Text = pEntity.State;
                txtTown.Text = pEntity.Town;
                txtZip.Text = pEntity.Postcode;
                ddlCountry.SelectedValue = (pEntity.CountryID.HasValue ? pEntity.CountryID.Value : 0).ToString();
                //img.ImageUrl = GetImageUrl(pEntity.ID.ToString());
                img.ImageUrl = "~/ImageHandler.ashx?s="+ImageManager.file_section_portfolio+"&id=" + pEntity.ID;
                txtContactname.Text = pEntity.BankName;
                txtPhone.Text = pEntity.TelephoneNumber;
                txtEmailAddress.Text= pEntity.EmailAddress;
                ddlReligion.SelectedValue = pEntity.DenominationDetailsID.HasValue ? pEntity.DenominationDetailsID.Value.ToString() : "0";
                BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));
                ddlGroup.SelectedValue = pEntity.GroupDetailsID.HasValue ? pEntity.GroupDetailsID.Value.ToString() : "0";
                BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));
                ddlDenimination.SelectedValue = pEntity.SubDenominationDetailsID.HasValue ? pEntity.SubDenominationDetailsID.Value.ToString() : "0";

                txtOrgRegistrationNumber.Text = pEntity.RegistrationNumber;
                txtWebURL.Text = pEntity.BaseUrl;
                txtDescription.Text = pEntity.Description;


                var udetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == (pEntity.Owner ?? 0)).FirstOrDefault();
                if(udetails != null)
                {
                    txtContactname.Text = udetails.ContractorName;
                    txtLastName.Text = udetails.LastName;
                    txtPhone.Text = udetails.ContactNumber;
                    txtEmailAddress.Text = udetails.EmailAddress;
                    //txtPhone.Text = udetails.con

                }

                var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(pEntity.ID);
                if (payDetails != null)

                {
                    txtAccount.Text= payDetails.Vendor;
                    if(payDetails.TransactionFee.HasValue)
                    {
                        
                        txtTransactionFee.Text = string.Format("{0:F2}", payDetails.TransactionFee.HasValue ? payDetails.TransactionFee.Value : 0);

                        txtCardTransactionFee.Text = string.Format("{0:F2}", payDetails.CardFee.HasValue ? payDetails.CardFee.Value : 0);

                        txtFixedPrice.Text = string.Format("{0:F2}", payDetails.FixedPrice.HasValue ? payDetails.FixedPrice.Value : 0);
                    }
                }
            }
        }

        private void uploadImage(int portfolioid)
        {
            try

            {

                using (Stream fs = imgFile.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        ImageManager.FileDBSave(bytes, null, portfolioid.ToString(), ImageManager.file_section_portfolio_doc, System.IO.Path.GetExtension(imgFile.PostedFile.FileName).ToLower(), imgFile.PostedFile.FileName, imgFile.PostedFile.ContentType);

                    }
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void uploadLogo(int partnerID)
        {
            try
            {
                if (imgLogo.PostedFile.FileName.Length > 0)
                {
                    Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
                    ImageManager.SaveProtfolioImage_setpath(imgLogo.FileBytes, partnerID.ToString());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        private void updateTrasactionFee(int portfolioid)
        {
            if (txtTransactionFee.Text.Trim().Length > 0)
            {
                try
                {
                    var payDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(portfolioid);
                    if (payDetails == null)
                    {
                        payDetails = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                        payDetails.PortfolioID = portfolioid;
                        payDetails.PayType = "cardconnect";

                       

                    }
                    payDetails.Vendor = txtAccount.Text.Trim();
                    payDetails.TransactionFee = Convert.ToDouble(txtTransactionFee.Text.Trim());
                    payDetails.CardFee = Convert.ToDouble(txtCardTransactionFee.Text.Trim());
                    payDetails.FixedPrice = Convert.ToDouble(txtFixedPrice.Text.Trim());
                    PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(payDetails);
                    //PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_UpdatePlatformFee(payDetails.PortfolioID,Convert.ToDouble(txtTransactionFee.Text.Trim()));
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
       
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // ProjectPortFolioBAL
                if (Request.QueryString["orgid"] == null)


                {
                    var cRep = new UserRepository<Contractor>();
                    var cvRep = new UserRepository<v_contractor>();
                    var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                    var userDetsils = cvRep.GetAll().Where(o => o.LoginName.ToLower() == txtEmailAddress.Text.Trim().ToLower() && o.TypeofLogin.ToString() == "ap" && o.Status == UserStatus.Active).FirstOrDefault();

                    if (userDetsils == null)
                    {
                        var pEntity = new PortfolioMgt.Entity.ProjectPortfolio();
                        pEntity.PortFolio = txtOrganizationName.Text;
                        pEntity.Town = txtTown.Text;
                        pEntity.State = txtState.Text;
                        pEntity.StartDate = DateTime.Now;
                        pEntity.Address = txtAddress.Text.Trim();
                        pEntity.Postcode = txtZip.Text.Trim();
                        pEntity.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                        // pEntity.EmailAddress = txtem
                        //pEntity.EmailAddress = txtem
                        pEntity.BankName = txtContactname.Text.Trim();
                        pEntity.TelephoneNumber = txtPhone.Text.Trim();
                        pEntity.DenominationDetailsID = Convert.ToInt32(ddlReligion.SelectedValue);
                        pEntity.GroupDetailsID = Convert.ToInt32(ddlGroup.SelectedValue);
                        pEntity.SubDenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue);
                        pEntity.EmailAddress = txtEmailAddress.Text.Trim();
                        pEntity.OrgarnizationGUID = Guid.NewGuid().ToString();
                        pEntity.CostCentre = txtCostCenter.Text;
                        pEntity.RegistrationNumber = txtOrgRegistrationNumber.Text.Trim();

                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Add(pEntity);
                        //sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                        //update transaction fee
                        updateTrasactionFee(pEntity.ID);



                        try
                        {

                            string pw = "Faith@2021";
                            var value = new UserMgt.Entity.Contractor();
                            value.ContractorName = txtContactname.Text.Trim();
                            value.EmailAddress = txtEmailAddress.Text.Trim();
value.LoginName = txtEmailAddress.Text.Trim();
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                            //if (txtPassword.Text.Trim().Length > 0)
                            //{
                            //    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                            //}
                            //else
                            //{

                            //}
                            //1 - Admin
                            value.SID = UserType.SmartPros;
                            value.CreatedDate = DateTime.Now;
                            value.ModifiedDate = DateTime.Now;
                            value.Status = UserStatus.Active;
                            value.isFirstlogin = 0;
                            value.ResetPassword = false;
                            value.Company = "";
                            value.ContactNumber = txtPhone.Text;
                            value.TypeofLogin = "ap";

                            if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.TypeofLogin.ToLower() == "ap" && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                            {
                                cRep.Add(value);
                            }


                            var cdEntity = new UserMgt.Entity.UserDetail();
                            cdEntity.Address1 = txtAddress.Text;
                            cdEntity.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                            //cdEntity.PostCode = txtzip;
                            cdEntity.State = txtState.Text;
                            //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                            //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                            cdEntity.Town = txtTown.Text;
                            cdEntity.UserId = value.ID;
                            cdEntity.DenominationDetailsID = Convert.ToInt32(ddlReligion.SelectedValue);

                            cdEntity.GroupDetailsID = Convert.ToInt32(ddlGroup.SelectedValue);

                            cdEntity.SubDenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue);



                            cdRep.Add(cdEntity);
                            if (pEntity.ID > 0)
                            {
                                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                                var urEntity = new UserMgt.Entity.UserToCompany();
                                urEntity.CompanyID = pEntity.ID;
                                urEntity.UserID = value.ID;
                                urRep.Add(urEntity);
                            }

                            //uploadLogo(value.ID);
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        //img.ImageUrl = GetImageUrl(value.ID.ToString());


                        uploadLogo(pEntity.ID);
                        uploadImage(pEntity.ID);
                        img.ImageUrl = "~/ImageHandler.ashx?s=" + ImageManager.file_section_portfolio + "&id=" + pEntity.ID;
                        //DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.Addedsuccessfully);
                        Response.Redirect(Request.RawUrl + "?orgid=" + pEntity.ID, false);
                        
                        
                    }
                    else
                    {
                        DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "Email already exists", "");
                    }
                }
                else
                {
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                    if (pEntity != null)
                    {
                        pEntity.CostCentre = txtCostCenter.Text;
                        pEntity.PortFolio = txtOrganizationName.Text.Trim();
                        pEntity.Town = txtTown.Text.Trim();
                        pEntity.State = txtState.Text.Trim();
                        pEntity.Address = txtAddress.Text.Trim();
                        pEntity.Postcode = txtZip.Text.Trim();
                        //pEntity.StartDate = DateTime.Now;
                        pEntity.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                        pEntity.BankName = txtContactname.Text.Trim();
                        pEntity.TelephoneNumber = txtPhone.Text.Trim();
                        pEntity.DenominationDetailsID = Convert.ToInt32(ddlReligion.SelectedValue);
                        pEntity.GroupDetailsID = Convert.ToInt32(ddlGroup.SelectedValue);
                        pEntity.EmailAddress = txtEmailAddress.Text.Trim();
                        pEntity.SubDenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue);
                        pEntity.RegistrationNumber = txtOrgRegistrationNumber.Text.Trim();
                        if (pEntity.OrgarnizationGUID == null)
                        {
                            pEntity.OrgarnizationGUID = Guid.NewGuid().ToString();
                        }
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pEntity);

                        //update owner details
                        IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
                        var uEntity = uRep.GetAll().Where(o=>o.ID == Convert.ToInt32((pEntity.Owner??0))).FirstOrDefault();
                        if(uEntity != null)
                        {
                            uEntity.ContractorName = txtContactname.Text;
                            uEntity.LastName = txtLastName.Text;
                            uEntity.EmailAddress = txtEmailAddress.Text.Trim();
                            uEntity.LoginName = txtEmailAddress.Text.Trim();
                            uEntity.ContactNumber = txtPhone.Text.Trim();
                            uRep.Edit(uEntity);
                        }

                        //update transaction fee
                        updateTrasactionFee(pEntity.ID);

                        uploadLogo(pEntity.ID);
                        uploadImage(pEntity.ID);
                        //img.ImageUrl = GetImageUrl(pEntity.ID.ToString());
                        img.ImageUrl = "~/ImageHandler.ashx?s="+ImageManager.file_section_portfolio+"&id=" + pEntity.ID;







                        // DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.UpdatedSuccessfully);
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                        Response.Redirect(Request.RawUrl, false);
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            //bool isOriginal = false;

            //string img = string.Empty;

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers/") + "portfolio_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    //if (isOriginal)
            //    //    img = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", contactsId.ToString());
            //    //else
            //        img = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", contactsId.ToString());
            //    // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //    //img = string.Format("<img src='{0}' />", imgurl);
            //}
            //else
            //{
            //    img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
            //    //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            //}
            //return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
            return "~/ImageHandler.ashx?id=" + contactsId.ToString() + "&s=" + ImageManager.file_section_portfolio;

        }
        private void BindReligion()
        {
            try
            {
                ddlReligion.Items.Clear();
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

                ddlReligion.DataSource = rlist;
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataValueField = "ID";
                ddlReligion.DataBind();

                ddlReligion.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }




        public static IQueryable<PortfolioMgt.Entity.DenominationGroupDetail> DenominationGroupDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            return pRep.GetAll();
        }

        private void BindGroup(int religionID)
        {
            try
            {
                ddlGroup.Items.Clear();
                if (religionID > 0)
                {
                    //  var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    var rlist = DenominationGroupDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();



                    ddlGroup.DataSource = rlist;
                    ddlGroup.DataTextField = "Name";
                    ddlGroup.DataValueField = "ID";
                    ddlGroup.DataBind();

                    ddlGroup.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    ddlGroup.Items.Insert(0, new ListItem("Please select...", "0"));
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int GroupId)
        {
            try
            {
                ddlDenimination.Items.Clear();
                if (GroupId > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationGroupDetailsID == GroupId).OrderBy(o => o.Name).ToList();

                    ddlDenimination.DataSource = rlist;
                    ddlDenimination.DataTextField = "Name";
                    ddlDenimination.DataValueField = "ID";
                    ddlDenimination.DataBind();

                    ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));

                BindDenomination(0);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnShowAlert_Click(object sender, EventArgs e)
        {


          

           // string script = "window.onload = function() { showswal('"+"Added"+"', '"+"OK"+"'); };";
           // ClientScript.RegisterStartupScript(this.GetType(), "toastrerror", script, true);
           //// return ;
        }
    }
}