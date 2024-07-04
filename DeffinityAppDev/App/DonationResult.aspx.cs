using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class DonationResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    var orgData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == QueryStringValues.UNID).FirstOrDefault();
                    sessionKeys.PortfolioID = orgData.ID;
                    lblOrgname.Text = orgData.PortFolio;
                    pnlVisibilt(true, false, false, false);
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        private void pnlVisibilt(bool first,bool userbasic,bool useraddress,bool final)
        {
            pnlFist.Visible = first;
            pnlUserBasic.Visible = userbasic;
            pnlUserAddress.Visible = useraddress;
            pnlResult.Visible = false;
        }
        protected void btnNO_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl());
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            pnlVisibilt(false, true, false, false);
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            pnlVisibilt(false, false, true, false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            pnlVisibilt(false, false, false, true);
        }

        protected void btnBacktologin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl());
        }

        private void SaveData()
        {
            try
            {
                int userid = 0;

                if (txtEmailAddress.Text != null)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.EmailAddress == txtEmailAddress.Text && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (cDetails != null)
                    {
                        var value = cRep.GetAll().Where(o => o.ID == cDetails.ID).FirstOrDefault();
                        value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                        value.EmailAddress = txtEmailAddress.Text;
                        value.LoginName = txtEmailAddress.Text;
                        if (txtPassword.Text.Trim().Length > 0)
{
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim());//  FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                        }
                        //1 - Admin
                        //value.SID = Convert.ToInt32(ddlPermission.SelectedValue);
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber = txtContactNumber.Text;

                        cRep.Edit(value);

                        //    if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                        //{
                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = cdRep.GetAll().Where(o => o.UserId == cDetails.ID).FirstOrDefault();
                        //}
                        cdEntity.Address1 = txtAddress.Text;
                        cdEntity.Country = 190;
                        cdEntity.PostCode = txtZipcode.Text;
                        cdEntity.State = txtState.Text;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = txtTown.Text;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;
                        cdRep.Edit(cdEntity);

                        //uploadLogo(value.ID);
                        //img.ImageUrl = GetImageUrl(value.ID.ToString());
                        userid = value.ID;
                        //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                        //Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                    }
                    else
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();
                        string pw = "Faith@2021";
                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                        value.EmailAddress = txtEmailAddress.Text;
                        value.LoginName = txtEmailAddress.Text;
                        if (txtPassword.Text.Trim().Length > 0)
                        {
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim());// FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                        }
                        else
                        {
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(pw); //FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        }
                        //1 - Admin
                        //value.SID = UserType.SmartPros;
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber = txtContactNumber.Text;


                        if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).Count() == 0)
                        {
                            cRep.Add(value);
                        }

                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = txtAddress.Text;
                        cdEntity.Country = 190;
                        cdEntity.PostCode = txtZipcode.Text;
                        cdEntity.State = txtState.Text;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = txtTown.Text;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;



                        cdRep.Add(cdEntity);

                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = value.ID;
                        urRep.Add(urEntity);

                        //uploadLogo(value.ID);
                        //img.ImageUrl = GetImageUrl(value.ID.ToString());

                        userid = value.ID;
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}