using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App
{
    public partial class MemberForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    linkBack.NavigateUrl = "~/App/Members.aspx?type="+QueryStringValues.Type;
                    BindCountry();
                    ddlCountry.SelectedValue = "190";
                    
                    pnlDeactive.Visible = false;
                    if (Request.QueryString["mid"] != null)
                    {
                        pnlDeactive.Visible = true;
                        pnlPassword.Visible = true;
                        pnlskills.Visible = true;
                        pnlDocuments.Visible = true;
                        var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                        

                        NewMethod(uid);
                        Gridfilesbind();
                        BindSkills();
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction()", true);

                        pnlDeactive.Visible = false;
                        pnlPassword.Visible = false;
                        pnlskills.Visible = false;
                        pnlDocuments.Visible = false;

                    }


                    if (sessionKeys.Message.Length > 0)
                    {
                      //  DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void NewMethod(int uid)
        {
            try
            {



                var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == uid).FirstOrDefault();
                if (cDetails != null)
                {
                    txtFirstName.Text = cDetails.ContractorName.Trim().Split(' ').Length > 1 ? cDetails.ContractorName.Trim().Split(' ')[0] : cDetails.ContractorName;
                    txtSurname.Text = cDetails.ContractorName.Trim().Split(' ').Length > 1 ? cDetails.ContractorName.Trim().Split(' ')[1] : string.Empty;
                    txtAddress.Text = cDetails.Address1;
                    txtState.Text = cDetails.State;
                    txtTown.Text = cDetails.Town;
                    txtEmailAddress.Text = cDetails.EmailAddress;
                    lblEmail.Text = cDetails.EmailAddress;
                    txtemailaddress_update.Value = cDetails.EmailAddress;
                    txtContactNumber.Text = cDetails.ContactNumber;
                    txtZipcode.Text = cDetails.PostCode;
                    ddlCountry.SelectedValue = (cDetails.Country.HasValue ? cDetails.Country.Value : 0).ToString();
                    
                    //ddlReligion.SelectedValue = cDetails.DenominationDetailsID.ToString();
                    //BindDenomination(cDetails.DenominationDetailsID);
                    //ddlDenimination.SelectedValue = cDetails.SubDenominationDetailsID.ToString();
                    img.ImageUrl = GetImageUrl(cDetails.ID.ToString());



                    var religion = cDetails.DenominationDetailsID;

                    var Group = cDetails.SubDenominationDetailsID;

                    var Denomination = 2;




                    // BELOW LINE IS TO CALL JAVASCRIPT FUNCTION  MyFunction
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction(" + religion + "," + Group + "," + Denomination + ")", true);



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

            //string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            //if (System.IO.File.Exists(filepath))
            //{
            //    if (isOriginal)
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
            //    else
            //        img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
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
            return "~/ImageHandler.ashx?id=" + contactsId + "&s=" + ImageManager.file_section_user; 
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
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["mid"] != null)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == uid).FirstOrDefault();
                    if (cDetails != null)
                    {
                        var value = cRep.GetAll().Where(o => o.ID == uid).FirstOrDefault();
                        value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                        value.EmailAddress = txtEmailAddress.Text;
                        value.LoginName = txtEmailAddress.Text;
                        if (txtPassword.Text.Trim().Length > 0)
                        {
                            value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim()); //FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");

                            // db.AddInParameter(cmd, "@Password", DbType.String, Deffinity.Users.Login.GeneratePasswordString(password));
                        }
                        //1 - Admin
                        value.SID = UserType.SmartPros;
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
                        var cdEntity = cdRep.GetAll().Where(o => o.UserId == uid).FirstOrDefault();
//}
                        cdEntity.Address1 = txtAddress.Text;
                        cdEntity.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                        cdEntity.PostCode = txtZipcode.Text;
                        cdEntity.State = txtState.Text;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = txtTown.Text;
                        cdEntity.UserId = value.ID;

                        cdEntity.DenominationDetailsID = Convert.ToInt32(HiddenFieldReligion.Value);

                        cdEntity.GroupDetailsID = Convert.ToInt32(HiddenFieldGroup.Value);

                        cdEntity.SubDenominationDetailsID = Convert.ToInt32(HiddenFieldDenomination.Value);

                        //cdEntity.DenominationDetailsID = Convert.ToInt32(ddlReligion.SelectedValue);
                        //cdEntity.SubDenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue);
                        cdRep.Edit(cdEntity);

                        uploadLogo(value.ID);
                        img.ImageUrl = GetImageUrl(value.ID.ToString());

                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                        Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                    }
                }
                else
                {
                    var cRep = new UserRepository<Contractor>();
                    var cvRep = new UserRepository<v_contractor>();
                    string pw = "Faith@2021";
                    var value = new UserMgt.Entity.Contractor();
                    value.ContractorName = txtFirstName.Text + " " + txtSurname.Text.Trim();
                    value.EmailAddress = txtEmailAddress.Text;
                    value.LoginName = txtEmailAddress.Text;
                    if (txtPassword.Text.Trim().Length > 0)
                    {
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(txtPassword.Text.Trim()); // FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                    }
                    else
                    {
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(pw); //FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                    }
                    //1 - Admin
                    value.SID = UserType.SmartPros;
                    value.CreatedDate = DateTime.Now;
                    value.ModifiedDate = DateTime.Now;
                    value.Status = UserStatus.Active;
                    value.isFirstlogin = 0;
                    value.ResetPassword = false;
                    value.Company = "";
                    value.ContactNumber = txtContactNumber.Text;


                    if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                    {
                        cRep.Add(value);
                    }

                    var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                    var cdEntity = new UserMgt.Entity.UserDetail();
                    cdEntity.Address1 = txtAddress.Text;
                    cdEntity.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                    cdEntity.PostCode = txtZipcode.Text;
                    cdEntity.State = txtState.Text;
                    //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                    //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                    cdEntity.Town = txtTown.Text;
                    cdEntity.UserId = value.ID;
                    cdEntity.DenominationDetailsID = Convert.ToInt32(HiddenFieldReligion.Value);

                    cdEntity.GroupDetailsID = Convert.ToInt32(HiddenFieldGroup.Value);

                    cdEntity.SubDenominationDetailsID = Convert.ToInt32(HiddenFieldDenomination.Value);



                    cdRep.Add(cdEntity);
                    if (sessionKeys.PortfolioID > 0)
                    {
                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = value.ID;
                        urRep.Add(urEntity);
                    }

                    uploadLogo(value.ID);
                    img.ImageUrl = GetImageUrl(value.ID.ToString());

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;

                    Response.Redirect("~/App/Member.aspx?mid=" + value.ID);
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
                    ImageManager.SaveUserImage_setpath(imgLogo.FileBytes, partnerID.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
                    // DisplayLogo();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().FirstOrDefault();
                    if (cDetails != null)
                    {

                        UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(uid, "InActive");
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {
            try
            {

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {

                sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnUpdateSkill_Click

        private void BindSkills()
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    var uskill = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == uid).FirstOrDefault();// (new UserMgt.Entity.UserSkill() { Skills = txtSkills.Value, UserId = uid });
                    if (uskill != null)
                    {
                        txtSkills.Value = uskill.Skills;
                    }
                    //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    // Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpdateSkill_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Skills = txtSkills.Value, UserId = uid });

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Download")
            {
                try
                {
                    GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    // string contenttype = gridfiles.DataKeys[gvrow.RowIndex].Values[1].ToString();
                    //string filename = gridfiles.DataKeys[gvrow.RowIndex].Values[2].ToString();
                    //string[] ex = filename.Split('.');
                    //string ext = ex[ex.Length - 1];
                    //"~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString(
                    string filepath = string.Format("~/WF/UploadData/Users/{0}/", Request.QueryString["mid"].ToString(), e.CommandArgument.ToString());
                    //Response.ContentType = contenttype;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + e.CommandArgument.ToString() + "\"");
                    Context.Response.ContentType = "octet/stream";
                    Response.TransmitFile(filepath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
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
        public void Gridfilesbind()
        {
            try
            {
                if (Request.QueryString["mid"] != null)
                {
                    var folderpath = Server.MapPath("~/WF/UploadData/Users/" + Request.QueryString["mid"].ToString());
                    if (System.IO.Directory.Exists(folderpath))
                    {
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Users/" + Request.QueryString["mid"].ToString()));
                        List<ListItem> files = new List<ListItem>();
                        foreach (string filePath in filePaths)
                        {
                            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                        }

                        gridfiles.DataSource = files;
                        gridfiles.DataBind();
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