using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App.controls
{
    public partial class SignupCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {
            try {
                string firstname =  txtFirstName.Text.Trim();
                string lastname =  txtLastname.Text.Trim();
                string emailaddress =  txtEmailaddress.Text.Trim();
                string cellnumber =  txtContactNumber.Text.Trim();
                //concider as member
                string permission = "member";

                string state =  ddlStates.SelectedValue;

               var retval=  SaveMember(firstname, lastname, emailaddress, cellnumber, permission, state);

                if(retval )
                {
                    Response.Redirect("~/SignupResult.aspx",false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private bool SaveMember(string firstname, string lastname, string emailaddress, string cellnumber, string permission, string state)
        {
            bool retval = false;
            try
            {
                var cRep = new UserRepository<UserMgt.Entity.Contractor>();
                var cvRep = new UserRepository<v_contractor>();
                string pw = "SmartGiving@2022";
                var value = new UserMgt.Entity.Contractor();
                value.ContractorName = firstname + " " + lastname;
                value.EmailAddress = emailaddress;
                value.LoginName = emailaddress;
                //if (txtPassword.Text.Trim().Length > 0)
                //{
                //    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                //}
                //else
                //{

                //}
                //1 - Admin
                //value.SID = UserType.SmartPros;
                value.SID = permission.ToLower().Contains("admin") ? 1 : 2;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.Status = UserStatus.Active;
                value.isFirstlogin = 0;
                value.ResetPassword = false;
                // value.Company = "";
                value.ContactNumber = cellnumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                var userDetails = cRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).FirstOrDefault();
                if (userDetails == null)
                {
                    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                    value.Company = "";
                    cRep.Add(value);
                    retval = true;

                }
                else
                {
                    userDetails.ContactNumber = value.ContactNumber;
                    userDetails.SID = value.SID;
                    userDetails.ContractorName = value.ContractorName;
                    userDetails.EmailAddress = value.EmailAddress;
                    userDetails.ModifiedDate = DateTime.Now;
                    cRep.Edit(userDetails);
                    retval = true;
                }

                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var cdEntity = new UserMgt.Entity.UserDetail();
                cdEntity = cdRep.GetAll().Where(o => o.UserId == value.ID).FirstOrDefault();
                if (cdEntity == null)
                {
                    cdEntity = new UserMgt.Entity.UserDetail();
                    cdEntity.Address1 = "";
                    cdEntity.Country = 190;
                    cdEntity.PostCode = "";
                    cdEntity.State = state;
                    cdEntity.Town = "";
                    cdEntity.UserId = value.ID;
                    cdRep.Add(cdEntity);
                }
                else
                {
                    cdEntity.State = state;
                    cdEntity.Country = 190;
                    cdRep.Edit(cdEntity);

                }

                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var urEntity = new UserMgt.Entity.UserToCompany();

                if (urRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID && o.UserID == value.ID).Count() == 0)
                {
                    urEntity.CompanyID = sessionKeys.PortfolioID;
                    urEntity.UserID = value.ID;
                    urRep.Add(urEntity);
                }

                var tags = "All";
                var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == value.ID).FirstOrDefault();
                if (ud == null)
                {
                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = tags, UserId = value.ID });
                }
                //else
                //{
                //    ud.Notes = tags;
                //    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                //}

                //uploadLogo(value.ID);
                //img.ImageUrl = GetImageUrl(value.ID.ToString());
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }
    }
}