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
    public partial class TestTouchSpin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                var tList = PortfolioMgt.BAL.TithingPaymentTrackerBAL.TithingPaymentTrackerBAL_SelectAll().Where(o=>o.OrganizationID == 3071).Where(o=>o.DonerEmail != null).ToList();

                foreach(var t in tList)
                {
                    if (t.DonerEmail.Length > 0)
                    {
                        SaveMember(t.DonerName, "", t.DonerEmail, t.DonerContact, "member", "All", t.OrganizationID.Value);

                        Response.Write(t.DonerEmail + "added");
                    }
;                }
            }
        }


        private void SaveMember(string firstname, string lastname, string emailaddress, string cellnumber, string permission, string tags,int portfolioid)
        {
            try
            {
                int userid = 0;
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
                    userid = value.ID;
                }
                else
                {
                    userDetails.ContactNumber = value.ContactNumber;
                    userDetails.SID = value.SID;
                    userDetails.ContractorName = value.ContractorName;
                    userDetails.EmailAddress = value.EmailAddress;
                    userDetails.ModifiedDate = DateTime.Now;
                    cRep.Edit(userDetails);
                    userid = userDetails.ID;
                }

                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var cdEntity = new UserMgt.Entity.UserDetail();

                if (cdRep.GetAll().Where(o => o.UserId == userid).Count() == 0)
                {
                    cdEntity.Address1 = "";
                    cdEntity.Country = 190;
                    cdEntity.PostCode = "";
                    cdEntity.State = "";
                    //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                    //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                    cdEntity.Town = "";
                    cdEntity.UserId = userid;

                    cdRep.Add(cdEntity);
                }

                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var urEntity = new UserMgt.Entity.UserToCompany();

                if (urRep.GetAll().Where(o => o.CompanyID == portfolioid && o.UserID == userid).Count() == 0)
                {
                    urEntity.CompanyID = portfolioid;
                    urEntity.UserID = userid;
                    urRep.Add(urEntity);
                }


                var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                if (ud == null)
                {
                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = "All", UserId = userid });
                }
              

                //uploadLogo(value.ID);
                //img.ImageUrl = GetImageUrl(value.ID.ToString());
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}