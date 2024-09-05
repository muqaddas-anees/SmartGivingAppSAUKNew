using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Web.Security;
using UserMgt.BAL;
using UserMgt.Entity;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DeffinityAppDev.App
{
    public partial class OtherDonations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                imageDiv.Style["background-image"] = "url('" + "/ImageHandler.ashx?id=0&s=" + ImageManager.file_section_user + "')";
                BindCOmpany();
                BindFund();
                if (QueryStringValues.UNID.Length > 0)
                {
                    hunid.Value = QueryStringValues.UNID;
                    BindData(hunid.Value);
                }
                else
                {
                    hunid.Value = Guid.NewGuid().ToString();
                }

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }
                if (QueryStringValues.Type.ToLower() == "cash")
                {
                    lblTitle.Text = "Cash Donations";
                    pnlChecknumber.Visible = true;
                    lblValueofgoods.Text = "Value of donation";
                }
                else
                {
                    lblTitle.Text = "In-Kind Donations";
                    pnlChecknumber.Visible = false;
                    lblValueofgoods.Text = "Value of goods";
                }

                if(QueryStringValues.MUNID.Length >0)
                {
                    btnBack.Visible = true;
                }
                else
                {
                    btnBack.Visible = false;
                }
            }
        }

        private void BindFund()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                //var tithingDetail = pRep.GetAll().Where(o => o.OrganizationID == 0).FirstOrDefault();

                var tithinglist = pRep.GetAll().Where(o => o.Title != "").Where(o => o.OrganizationID == sessionKeys.PortfolioID).Where(o => o.MasterUNID == null).ToList();

                ddlFund.DataSource = from t in tithinglist
                                     select new { Name = Deffinity.Utility.TruncateString(t.Title), Value = t.unid };
                ddlFund.DataTextField = "Name";
                ddlFund.DataValueField = "Value";
                ddlFund.DataBind();

                ddlFund.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", ""));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
      


        protected void btnAddCompnay_Click(object sender, EventArgs e)
        {
            txtAddCompany.Text = string.Empty;

            txtCompanyAddress.Text = string.Empty;
            txtCompanyPhone.Text = string.Empty;
            txtCompanyEmail.Text = string.Empty;
            txtCompanyNotes.Text = string.Empty;
            mdlPopup.Show();
        }

        protected void btnSubmitCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddCompany.Text.Trim().Length > 0)
                {
                    IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                    var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == txtAddCompany.Text.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                    if (cnt == 0)
                    {
                        var cDetails = new PortfolioMgt.Entity.UserCompany()
                        {
                            Name = txtAddCompany.Text.Trim(),
                            OrganisationID = sessionKeys.PortfolioID,
                            Address = txtCompanyAddress.Text.Trim(),
                            Contactno = txtCompanyPhone.Text.Trim(),
                            Email = txtCompanyEmail.Text.Trim(),
                            Notes = txtCompanyNotes.Text.Trim(),
                            ContactName = txtContactName.Text.Trim()
                        };
                        uRep.Add(cDetails);
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                    }
                    BindCOmpany();

                    txtAddCompany.Text = string.Empty;
                    txtCompanyAddress.Text = string.Empty;
                    txtCompanyPhone.Text = string.Empty;
                    txtCompanyEmail.Text = string.Empty;
                    txtCompanyNotes.Text = string.Empty;
                    txtContactName.Text = string.Empty;
                    mdlPopup.Hide();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindCOmpany()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.UserCompany> uRep = new PortfolioRepository<PortfolioMgt.Entity.UserCompany>();
                var pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();

                if (pList.Count == 0)
                {
                    IUserRepository<UserMgt.Entity.v_contractor> pRep = new UserRepository<UserMgt.Entity.v_contractor>();
                    var rList = pRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                    foreach (var r in rList)
                    {
                        if (r.Company.Trim().Length > 0)
                        {
                            var cnt = uRep.GetAll().Where(o => o.Name.ToLower() == r.Company.Trim().ToLower()).Where(o => o.OrganisationID == sessionKeys.PortfolioID).Count();
                            if (cnt == 0)
                            {
                                uRep.Add(new PortfolioMgt.Entity.UserCompany() { Name = r.Company, OrganisationID = sessionKeys.PortfolioID });
                            }
                        }
                    }

                    pList = uRep.GetAll().Where(o => o.OrganisationID == sessionKeys.PortfolioID).ToList();
                }

                ddlCompany.DataSource = pList.OrderBy(o => o.Name).ToList();
                ddlCompany.DataTextField = "Name";
                ddlCompany.DataValueField = "Name";
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", ""));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetFirstName(string name)
        {
            string firstName = string.Empty;

            firstName = name.Split(' ').First();


            return firstName.Trim();
        }

        private string GetLastName(string name)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            firstName = name.Split(' ').First();

            if (firstName.Length > 0)
                lastName = name.Replace(firstName, "");

            return lastName.Trim();
        }
        private void BindData(string unid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();

                var pmEntity = pmRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                if(pmEntity != null)
                {

                    txtDetailsofDonation.Text = pmEntity.DetailsOfDonation;
                    txtValueOfGoods.Text = String.Format("{0:F2}", (pmEntity.ValueOfGoods.HasValue ? pmEntity.ValueOfGoods.Value : 0));
                    if (pmEntity.FundriserUNID != null)
                        if (pmEntity.FundriserUNID.Trim().Length > 0)
                            ddlFund.SelectedValue = pmEntity.FundriserUNID;
                    // txtCompany.Text = pmEntity.Company;
                    //try
                    //{
                    //    if (cDetails.Company.Trim().Length > 0)
                    //        ddlCompany.SelectedValue = cDetails.Company.Trim();
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogExceptions.WriteExceptionLog(ex);
                    //}
                    txtNotes.Text = pmEntity.Notes;
                    txtChecknumber.Text = pmEntity.CheckNumber;



                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.EmailAddress.ToLower().Trim() == pmEntity.DonerEmail.ToLower().Trim() && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (cDetails != null)
                    {
                        txtFirstName.Text = GetFirstName(cDetails.ContractorName.Trim());// cDetails.ContractorName.Trim().Split(' ').Length > 1 ? cDetails.ContractorName.Trim().Split(' ')[0] : cDetails.ContractorName;
                        txtSurname.Text = GetLastName(cDetails.ContractorName.Trim()); // cDetails.ContractorName.Trim().Split(' ').Length > 1 ? cDetails.ContractorName.Trim().Split(' ')[1] : string.Empty;
                        txtAddress.Text = cDetails.Address1;
                        txtState.Text = cDetails.State;
                        txtTown.Text = cDetails.Town;
                        txtEmailAddress.Text = cDetails.EmailAddress;
                        //lblEmail.Text = cDetails.EmailAddress;
                       // txtemailaddress_update.Value = cDetails.EmailAddress;
                        txtContactNumber.Text = cDetails.ContactNumber;
                        txtZipcode.Text = cDetails.PostCode;
                        ddlCountry.SelectedValue = (cDetails.Country.HasValue ? cDetails.Country.Value : 0).ToString();
                       // ddlReligion.SelectedValue = cDetails.DenominationDetailsID.ToString();
                       //BindDenomination(cDetails.DenominationDetailsID);
                       // ddlDenimination.SelectedValue = cDetails.SubDenominationDetailsID.ToString();
                       // img.ImageUrl = GetImageUrl(cDetails.ID.ToString());

                        imageDiv.Style["background-image"] = "url('" + "/ImageHandler.ashx?id=" + cDetails.ID.ToString() + "&s=" + ImageManager.file_section_user + "')";
                        // ddlPermission.SelectedValue = cDetails.SID.Value.ToString();

                        try
                        {
                            if (cDetails.Company.Trim().Length > 0)
                                ddlCompany.SelectedValue = cDetails.Company.Trim();
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }

                    }
                    Gridfilesbind(QueryStringValues.UNID.ToString());

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
        //private void uploadLogo(int partnerID)
        //{
        //    try
        //    {
        //        if (imgLogo.PostedFile.FileName.Length > 0)
        //        {
        //            Bitmap upBmp = (Bitmap)Bitmap.FromStream(imgLogo.PostedFile.InputStream);
        //            ImageManager.SaveUserImage_setpath(imgLogo.FileBytes, partnerID.ToString(), Deffinity.systemdefaults.GetUsersFolderPath());
        //            // DisplayLogo();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SaveData()
        {
            try
            {

                int userid = 0;
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();

                var p = pmRep.GetAll().Where(o => o.unid == hunid.Value).FirstOrDefault();

                if (txtEmailAddress.Text.Length >0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o =>  o.EmailAddress.ToLower().Trim() == txtEmailAddress.Text.ToLower().Trim() && o.Status == UserStatus.Active).FirstOrDefault();
                    if (cDetails != null)
                    {
                        var value = cRep.GetAll().Where(o => o.ID == cDetails.ID).FirstOrDefault();
                        value.ContractorName = txtFirstName.Text;
                        value.LastName= txtSurname.Text.Trim();
                        //value.EmailAddress = txtEmailAddress.Text;
                        //value.LoginName = txtEmailAddress.Text;
                        if (txtPassword.Text.Trim().Length > 0)
                        {
                            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                        }
                        //1 - Admin
                        //value.SID = Convert.ToInt32(ddlPermission.SelectedValue);
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                       // value.Company = "";
                        if(txtContactNumber.Text.Length >0)
                        value.ContactNumber = txtContactNumber.Text;

                        cRep.Edit(value);

                        //    if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                        //{
                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = cdRep.GetAll().Where(o => o.UserId == cDetails.ID).FirstOrDefault();
                        //}
                        if (cdEntity != null)
                        {
                            if(txtAddress.Text.Length >0)
                            cdEntity.Address1 = txtAddress.Text;
                            cdEntity.Country = 190;
                            if(txtZipcode.Text.Length >0)
                            cdEntity.PostCode = txtZipcode.Text;
                            if(txtState.Text.Length >0)
                            cdEntity.State = txtState.Text;
                            //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                            //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                            if(txtTown.Text.Length >0)
                            cdEntity.Town = txtTown.Text;
                            cdEntity.UserId = value.ID;
                            cdEntity.DenominationDetailsID = 0;
                            cdEntity.SubDenominationDetailsID = 0;
                            cdRep.Edit(cdEntity);
                        }

                        uploadLogo(value.ID);
                        //img.ImageUrl = GetImageUrl(value.ID.ToString());
                        imageDiv.Style["background-image"] = "url('" + "/ImageHandler.ashx?id=" + value.ID.ToString() + "&s=" + ImageManager.file_section_user + "')";
                        userid = value.ID;

                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        if (urRep.GetAll().Where(o => o.UserID == value.ID && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                        {
                            var urEntity = new UserMgt.Entity.UserToCompany();
                            urEntity.CompanyID = sessionKeys.PortfolioID;
                            urEntity.UserID = value.ID;
                            urRep.Add(urEntity);
                        }
                        //sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                        //Response.Redirect("~/App/Member.aspx?mid=" + Request.QueryString["mid"].ToString());
                    }
                    else
                    {

                         cRep = new UserRepository<Contractor>();
                         cvRep = new UserRepository<v_contractor>();
                        string pw = "SmartGiving@2022";
                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = txtFirstName.Text;
                        value.LastName= txtSurname.Text.Trim();
                        value.EmailAddress = txtEmailAddress.Text;
                        value.LoginName = txtEmailAddress.Text.Trim();
                        if (txtPassword.Text.Trim().Length > 0)
                        {
                            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                        }
                        else
                        {
                            value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        }
                        //1 - Admin
                        //value.SID = UserType.SmartPros;
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = ddlCompany.SelectedValue;
                        value.ContactNumber = txtContactNumber.Text;


                        if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active ).Count() == 0)
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

                        uploadLogo(value.ID);
                        // img.ImageUrl = GetImageUrl(value.ID.ToString());
                        imageDiv.Style["background-image"] = "url('" + "/ImageHandler.ashx?id=" + value.ID.ToString() + "&s=" + ImageManager.file_section_user + "')";
                        userid = value.ID;
                    }
                }


            
                if (p== null)
                {
                    

                    //LogExceptions.LogException(" address id" + flsDetails.ContactAddressID);
                    //LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                     p = new PortfolioMgt.Entity.TithingPaymentTracker();
                    p.TithingID = 0;
                    p.OrganizationID = sessionKeys.PortfolioID;// flsDetails.ContactAddressID.HasValue ? flsDetails.ContactAddressID.Value : 0;
                    p.IsPaid = true;
                    p.PaidAmount = Convert.ToDouble(string.IsNullOrEmpty(txtValueOfGoods.Text.Trim()) ? "0.00" : txtValueOfGoods.Text.Trim());
                    p.ValueOfGoods = Convert.ToDouble(string.IsNullOrEmpty(txtValueOfGoods.Text.Trim()) ? "0.00" : txtValueOfGoods.Text.Trim());
                    p.PaidDate = DateTime.Now;
                    p.LoggedByID =  userid;
                    p.CreatedDateTime = DateTime.Now;
                    p.ModifiedDateTime = DateTime.Now;
                    
                    p.unid = hunid.Value;
                    p.DetailsOfDonation = txtDetailsofDonation.Text.Trim();

                    p.DonationType = QueryStringValues.Type.ToString();
                    //p.Company = txtCompany.Text.Trim();
                    p.Notes = txtNotes.Text.Trim();
                    p.CheckNumber = txtChecknumber.Text.Trim();
                    p.DonerEmail = txtEmailAddress.Text.Trim();
                    p.DonerName = txtFirstName.Text.Trim() + " " + txtSurname.Text.Trim();
                    p.DonerContact = txtContactNumber.Text.Trim();
                   // p.FundriserUNID = QueryStringValues.MUNID;
                    p.FundriserUNID = ddlFund.SelectedValue;
                    // p.StartDate = startdate;
                    // p.EndDate = enddate;
                    // p.DayStart = dayStart;
                    //p.Notes = "";
                    // p.IsAnonymously = IsAnonymously;
                    //p.RecurringPayID = RecurringPayID;
                    // p.MoreDetails = "";
                    // p.PayOnWebsite = false;
                    //p.Payref = pref;
                    //p.note = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;

                    pmRep.Add(p);

                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    if(QueryStringValues.MUNID.Length >0)
                    {
                        Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + hunid.Value+"&munid="+ QueryStringValues.MUNID, false);
                    }
                    else

                    Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + hunid.Value,false);
                }
                else 
                {
                   // var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();

                    //LogExceptions.LogException(" address id" + flsDetails.ContactAddressID);
                    //LogExceptions.LogException(" amount" + (InvoiceDetails.OriginalPrice.HasValue ? InvoiceDetails.OriginalPrice.Value : 0.00).ToString());
                   // var p = new PortfolioMgt.Entity.TithingPaymentTracker();
                    p.TithingID = 0;
                    p.OrganizationID = sessionKeys.PortfolioID;// flsDetails.ContactAddressID.HasValue ? flsDetails.ContactAddressID.Value : 0;
                    p.IsPaid = true;
                    p.PaidAmount = Convert.ToDouble(string.IsNullOrEmpty(txtValueOfGoods.Text.Trim()) ? "0.00" : txtValueOfGoods.Text.Trim());
                    p.ValueOfGoods = Convert.ToDouble(string.IsNullOrEmpty(txtValueOfGoods.Text.Trim()) ? "0.00" : txtValueOfGoods.Text.Trim());
                    p.PaidDate = DateTime.Now;
                    p.LoggedByID = userid;
                    p.CreatedDateTime = DateTime.Now;
                    p.ModifiedDateTime = DateTime.Now;
                    p.unid = hunid.Value;
                    p.DetailsOfDonation = txtDetailsofDonation.Text.Trim();

                    p.DonationType = QueryStringValues.Type.ToString();
                   // p.Company = txtCompany.Text.Trim();
                    p.Notes = txtNotes.Text.Trim();
                    p.CheckNumber = txtChecknumber.Text.Trim();
                    p.DonerEmail = txtEmailAddress.Text.Trim();
                    p.DonerName = txtFirstName.Text.Trim() + " " + txtSurname.Text.Trim();
                    p.DonerContact = txtContactNumber.Text.Trim();
                    p.FundriserUNID = ddlFund.SelectedValue;     
                    // p.StartDate = startdate;
                    // p.EndDate = enddate;
                    // p.DayStart = dayStart;
                    // p.Notes = "";
                    // p.IsAnonymously = IsAnonymously;
                    //p.RecurringPayID = RecurringPayID;
                    // p.MoreDetails = "";
                    // p.PayOnWebsite = false;
                    //p.Payref = pref;
                    //p.note = "InvoiceID:" + InvoiceDetails.ID.ToString() + ",InvoiceRef:" + InvoiceDetails.InvoiceRef;

                    pmRep.Edit(p);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                    if (QueryStringValues.MUNID.Length > 0)
                    {
                        Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + hunid.Value + "&munid=" + QueryStringValues.MUNID, false);
                    }
                    else
                        Response.Redirect("~/App/OtherDonations.aspx?type=" + QueryStringValues.Type + "&unid=" + hunid.Value,false);
                }
                //else
                //{
                //    DeffinityManager.ShowMessages.ShowSuccessError(this.Page, "Member already exists.Please try again", "OK");
                //}
               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }
        protected void btnSaveDonation_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                //Response.Redirect(Request.RawUrl, false);
                //sessionKeys.Message = "Thank you";
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region File
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
                    string filepath = string.Format("~/WF/UploadData/Donations/{0}/", hunid.Value, e.CommandArgument.ToString());
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
                //  File.Delete(filePath);

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.ID == Convert.ToInt32(filePath)).FirstOrDefault();

                if (fList != null)
                    fRep.Delete(fList);

                Gridfilesbind(QueryStringValues.UNID.ToString());

                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void Gridfilesbind(string SID)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.FileData> fRep = new PortfolioRepository<PortfolioMgt.Entity.FileData>();

                var fList = fRep.GetAll().Where(o => o.Section == ImageManager.file_section_user_doc).Where(o => o.FileID == SID.ToString()).ToList();

                var rList = (from r in fList
                             select new
                             {
                                 ID = r.ID,
                                 Value = r.FileID,
                                 Text = r.FileName
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

                var f = fRep.GetAll().Where(o => o.FileID == filePath && o.Section == ImageManager.file_section_donor_doc).FirstOrDefault();
                if (f != null)
                {
                    Response.Redirect("~/ImageHandler.ashx?id=" + filePath + "&s=" + ImageManager.file_section_donor_doc);
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/App/Fundraiser/peertopeer/Dashboard.aspx?unid={0}", QueryStringValues.MUNID));
        }
    }
}