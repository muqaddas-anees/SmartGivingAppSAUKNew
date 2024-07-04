using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class DonationResultNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    var orgData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.OrgarnizationGUID == QueryStringValues.UNID).FirstOrDefault();
                    hurl.Value = orgData.OrgUniqID;
                    sessionKeys.PortfolioID = orgData.ID;
                    lblOrgname.Text = orgData.PortFolio;
                    pnlVisibilt(true, false, false, false);

                   

                   if( Request.QueryString["tunid"] != null)
                    {
                        var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();

                        var pmEntity = pmRep.GetAll().Where(o => o.unid == Request.QueryString["tunid"].ToString()).FirstOrDefault();
                        if(pmEntity != null)
                        {
                            txtFirstName.Text = pmEntity.DonerName == null?"": pmEntity.DonerName;
                            txtEmailAddress.Text = pmEntity.DonerEmail== null? "": pmEntity.DonerEmail;
                            txtContactNumber.Text = pmEntity.DonerContact== null?"":pmEntity.DonerContact;
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        private void pnlVisibilt(bool first, bool userbasic, bool useraddress, bool final)
        {
            pnlFist.Visible = first;
            pnlUserBasic.Visible = userbasic;
            pnlUserAddress.Visible = useraddress;
            pnlResult.Visible = final;
        }
        protected void btnNO_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl());
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            pnlVisibilt(false, false, true, false);
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
            Response.Redirect( Deffinity.systemdefaults.GetWebUrl() +"/web/"+ hurl.Value, false);
            //hurl.Value
        }

        private void SaveData()
        {
            try
            {
                int userid = 0;

                if (txtEmailAddress.Text.Length > 0)
                    AddOrUpdateMembers(txtEmailAddress.Text.Trim(), txtFirstName.Text.Trim(), "", txtContactNumber.Text.Trim(),txtAddress.Text.Trim(),txtTown.Text.Trim(),txtState.Text.Trim(), txtZipcode.Text.Trim(), "", "");

                //if (txtEmailAddress.Text != null)
                //{
                //    var cRep = new UserRepository<Contractor>();
                //    var uRep = new UserRepository<UserDetail>();
                //    var cvRep = new UserRepository<v_contractor>();
                //    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                //    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.EmailAddress == txtEmailAddress.Text && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                //    if (cDetails != null)
                //    {
                //        var value = cRep.GetAll().Where(o => o.ID == cDetails.ID).FirstOrDefault();
                //        value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                //        value.EmailAddress = txtEmailAddress.Text;
                //        value.LoginName = txtEmailAddress.Text;
                //        if (txtPassword.Text.Trim().Length > 0)
                //        {
                //            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                //        }
                //        //1 - Admin
                //        //value.SID = Convert.ToInt32(ddlPermission.SelectedValue);
                //        value.CreatedDate = DateTime.Now;
                //        value.ModifiedDate = DateTime.Now;
                //        value.Status = UserStatus.Active;
                //        value.isFirstlogin = 0;
                //        value.ResetPassword = false;
                //        value.Company = "";
                //        value.ContactNumber = txtContactNumber.Text;

                //        cRep.Edit(value);

                //        //    if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                //        //{
                //        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                //        var cdEntity = cdRep.GetAll().Where(o => o.UserId == cDetails.ID).FirstOrDefault();
                //        //}
                //        cdEntity.Address1 = txtAddress.Text;
                //        cdEntity.Country = 190;
                //        cdEntity.PostCode = txtZipcode.Text;
                //        cdEntity.State = txtState.Text;
                //        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                //        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                //        cdEntity.Town = txtTown.Text;
                //        cdEntity.UserId = value.ID;
                //        cdEntity.DenominationDetailsID = 0;
                //        cdEntity.SubDenominationDetailsID = 0;
                //        cdRep.Edit(cdEntity);

                //        //uploadLogo(value.ID);
                //        //img.ImageUrl = GetImageUrl(value.ID.ToString());
                //        userid = value.ID;
                //        //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                //        //Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                //    }
                //    else
                //    {

                //        cRep = new UserRepository<Contractor>();
                //        cvRep = new UserRepository<v_contractor>();
                //        string pw = "Faith@2021";
                //        var value = new UserMgt.Entity.Contractor();
                //        value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                //        value.EmailAddress = txtEmailAddress.Text;
                //        value.LoginName = txtEmailAddress.Text;
                //        if (txtPassword.Text.Trim().Length > 0)
                //        {
                //            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                //        }
                //        else
                //        {
                //            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                //        }
                //        //1 - Admin
                //        //value.SID = UserType.SmartPros;
                //        value.SID = 2;
                //        value.CreatedDate = DateTime.Now;
                //        value.ModifiedDate = DateTime.Now;
                //        value.Status = UserStatus.Active;
                //        value.isFirstlogin = 0;
                //        value.ResetPassword = false;
                //        value.Company = "";
                //        value.ContactNumber = txtContactNumber.Text;


                //        if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).Count() == 0)
                //        {
                //            cRep.Add(value);
                //        }

                //        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                //        var cdEntity = new UserMgt.Entity.UserDetail();
                //        cdEntity.Address1 = txtAddress.Text;
                //        cdEntity.Country = 190;
                //        cdEntity.PostCode = txtZipcode.Text;
                //        cdEntity.State = txtState.Text;
                //        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                //        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                //        cdEntity.Town = txtTown.Text;
                //        cdEntity.UserId = value.ID;
                //        cdEntity.DenominationDetailsID = 0;
                //        cdEntity.SubDenominationDetailsID = 0;



                //        cdRep.Add(cdEntity);

                //        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                //        var urEntity = new UserMgt.Entity.UserToCompany();
                //        urEntity.CompanyID = sessionKeys.PortfolioID;
                //        urEntity.UserID = value.ID;
                //        urRep.Add(urEntity);

                //        //uploadLogo(value.ID);
                //        //img.ImageUrl = GetImageUrl(value.ID.ToString());

                //        userid = value.ID;
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //protected void btnLookup_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        var Zip = txtZipcode.Text.Trim();
               
        //            string latlong = "", address = "";

        //            //IDCRespository<DC.Entity.GeoCode> gRep = new DCRepository<DC.Entity.GeoCode>();
        //            //var gEntity = gRep.GetAll().Where(o=>o.Zip == Zip.Trim()).FirstOrDefault();
        //            //if(gEntity!= null)
        //            //{
        //            //    latlong = Zip + "," + Convert.ToString(gEntity.Latitude) + "," + Convert.ToString(gEntity.Longitude) + "," + "" + Zip;
        //            //}

        //            address = "https://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false&key=AIzaSyC5fVno1A77Hcx9XMr5k070Nm9TwFPEYuM";
        //            var result = new System.Net.WebClient().DownloadString(address);
        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(result);
        //            XmlNodeList parentNode = doc.GetElementsByTagName("location");
        //            var lat = "";
        //            var lng = "";
        //            foreach (XmlNode childrenNode in parentNode)
        //            {
        //                lat = childrenNode.SelectSingleNode("lat").InnerText;
        //                lng = childrenNode.SelectSingleNode("lng").InnerText;
        //            }
        //            latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
        //           // return latlong;
                
        //    }
        //    catch(Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        protected void btnBackContinue_Click(object sender, EventArgs e)
        {

        }

        protected void btnBackSave_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl() + "/web/" + hurl.Value, false);
        }

        public void AddOrUpdateMembers(string email, string firstname, string lastname, string contactno, string address, string town, string state, string zipcode, string eventname, string eventstatus)
        {

            try
            {
                int userid = 0;

                if (email.Length > 0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.Status == "Active").FirstOrDefault();
                    if (cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();

                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = firstname.Trim() + " " + lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
var pw = "SMG@2022";
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber = contactno;

                        cRep.Add(value);


                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();


                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = 190;
                        cdEntity.PostCode = zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);

                        userid = value.ID;
                    }



                    //update company
                    var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }

                    var tags = "";
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "All";
                        //if (eventstatus == "Pending")
                        //{
                        //    toadd = eventname + " - Not Attended";
                        //}
                        //else if (eventstatus == "Attended")
                        //{
                        //    toadd = eventname + " - Attended";
                        //}
                        var notes = "[{\"value\":\"" + toadd + "\"}]";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if (!exitingNotes.Contains("All"))
                        {
                            string toadd = "All";
                            //if (eventstatus == "Pending")
                            //{
                            //    toadd = eventname + " - Not Attended";
                            //}
                            //else if (eventstatus == "Attended")
                            //{
                            //    toadd = eventname + " - Attended";
                            //}

                            exitingNotes = exitingNotes.Replace("]", ",{\"value\":\"" + toadd + "\"}]");
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    }
}