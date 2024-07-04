using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;
using System.IO;
using DC.DAL;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactDetailsPage : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
        IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
        PortfolioContact pContact = null;
        PortfolioContactsTagsBAL pcTags = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindTags();
                    BindSourceofLead();
                    DisplayAddressMsg();
                    hsid.Value = sessionKeys.SID.ToString();
                    huid.Value = sessionKeys.UID.ToString();
                    SetContactDetails();

                    if (Request.QueryString["ContactID"] != null)
                    {
                        pnlDocuments.Visible = true;
                        pnlDeactive.Visible = true;
                        ShowDisplay(Convert.ToInt32(Request.QueryString["ContactID"].ToString()));

                        btnAddNewAddress.NavigateUrl = "~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx?ContactID=" + Request.QueryString["ContactID"].ToString();
                    }
                    else
                    {
                        ShowDisplay(0);
                    }

                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void UpdateAddressDetails(int contactid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();

                PortfolioMgt.Entity.PortfolioContactAddress sResult = pRepository.GetAll().Where(o => o.ContactID == contactid).OrderBy(o => o.ID).FirstOrDefault();
                //
                if (sResult == null)
                {
                    sResult = new PortfolioMgt.Entity.PortfolioContactAddress();
                    sResult.ContactID = contactid;
                    sResult.Address = txtAddress1.Text.Trim();
                    sResult.Address2 = txtAddress2.Text.Trim();
                    sResult.Amount = 0;
                    sResult.City = txtCity.Text.Trim();
                    sResult.State = txtState.Text.Trim();
                    sResult.PostCode = txtZipcode.Text.Trim();
                    sResult.LoggedBy = sessionKeys.UID;
                    sResult.LoggedDatetime = DateTime.Now;
                    pRepository.Add(sResult);
                    //Session["msg"] = "Address details added successfully";

                }
                else
                {
                    sResult.Address = txtAddress1.Text.Trim();
                    sResult.Address2 = txtAddress2.Text.Trim();
                   // sResult.BillingCity = string.Empty; //txtbCity.Text.Trim();
                    sResult.City = txtCity.Text.Trim();
                    sResult.State = txtState.Text.Trim();
                    sResult.PostCode = txtZipcode.Text.Trim();
                    pRepository.Edit(sResult);
                    //Session["msg"] = "Address details updated successfully";
                }
                //return id
            }
            catch(Exception e)
            {
                LogExceptions.WriteExceptionLog(e);
            }
            
        }
        private void BindAddressDetails(int contactid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> pRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> paRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
                IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var sResult = pRepository.GetAll().Where(o => o.ContactID == contactid).OrderBy(o => o.ID).FirstOrDefault();

                if (sResult != null)
                {
                    txtAddress1.Text = sResult.Address;
                    // txtbAddress1.Text = sResult.BillingAddress1;
                    txtAddress2.Text = sResult.Address2;
                    //txtbAddress2.Text = sResult.BillingAddress2;
                    //txtAmount.Text = string.Format("{0:F2}", sResult.Amount.HasValue ? sResult.Amount.Value : 0);
                    //txtbCity.Text = sResult.BillingCity;
                    txtCity.Text = sResult.City;
                    //txtbName.Text = sResult.BillingName;
                    //txtbState.Text = sResult.BillingState;
                    txtState.Text = sResult.State;
                    //txtbZipcode.Text = sResult.BillingZipcode;
                    txtZipcode.Text = sResult.PostCode;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void ShowDisplay(int ContactID)
        {
            using (DCDataContext DC = new DCDataContext())
            {
                var c = DC.ServiceDesk_RequesterSummaryDisplay(ContactID).FirstOrDefault();
                if (c != null)
                {
                    lblTotalRevenueThisYear.Text = string.Format("{0:F2}", c.TotalRevenueThisYear);
                    lblOutstandingInvoices.Text = string.Format("{0:F2}", c.OutstandingInvoices);
                    lblDebtorDays.Text = c.DebtorDays.ToString();


                }
            }
        }
        private void DisplayAddressMsg()
        {
            if (Session["msg"] != null)
            {
                lblMsg1.Text = Session["msg"].ToString();
                Session["msg"] = null;
            }
        }
        private void BindSourceofLead()
        {
            try
            {
                ddlSourceofLead.DataSource = PortfolioMgt.BAL.PortfolioContactsBAL.SourceofLead_Select().OrderBy(o => o.Item).ToList();
                ddlSourceofLead.DataTextField = "Item";
                ddlSourceofLead.DataValueField = "Item";
                ddlSourceofLead.DataBind();
                ddlSourceofLead.Items.Insert(0, new ListItem("Please select...", ""));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
            private void BindSummary(int ContactID)
        {
            try {
                using(DCDataContext DC = new DCDataContext())
                {
                    //22	New
                    //33  Cancelled
                    //35  Closed
                    //43  Scheduled
                    //44  Awaiting Schedule
                    //45  Arrived
                    //46  Customer Not Responding
                    //47  Feedback Submitted
                    //48  Feedback Received
                    //49  Quote Rejected
                    //50  Quote Accepted
                    //51  Awaiting Information
                    //52  Waiting On Parts
                    //53  Authorised
                    int[] sids = { 33, 35 };
                    int[] cids = {  35 };

                    var opentickets = DC.CallDetails.Where(o => o.RequesterID == ContactID).Where(o => sids.Contains(o.StatusID.Value)).Count();
                    lblOpen.InnerText = opentickets.ToString();
                    var closedtickets = DC.CallDetails.Where(o => o.RequesterID == ContactID).Where(o => cids.Contains(o.StatusID.Value)).Count();
                    lblCompletedJobs.InnerText = closedtickets.ToString();
                    //lblInvoiceOverdue.te


                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SetContactDetails()
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    //pnladdress.Visible = true;
                    btnuser.Visible = true;
                    lbtnUpload.Visible = true;
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    //cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    //pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();
                    PortfolioContactBAL pb = new PortfolioContactBAL();
                    var pContact = pb.V_PortfolioContact_SelectByID(contactid);
                    if (pContact != null)
                    {
                       // txtAddress.Text = pContact.Address1;
                       // txtCity.Text = pContact.City;
                        txtEmail.Text = pContact.Email;
                        txtmobileno.Text = pContact.Mobile;
                        txtname.Text = pContact.Name;
                        //txtPostal.Text = pContact.Postcode;
                        txtTelephone.Text = pContact.Telephone;
                       // txtTown.Text = pContact.Town;
                        lblFullName.Text = pContact.Name;
                        imguser.ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + pContact.ID.ToString();
                        // ListBox_setValues(pContact.Tags);
                        //lblCreatedBy.InnerText = pContact.LoggedByName;
                        //lblPremiums.Text = string.Format("{0:F2}", pContact.ActualCost);
                        //lblClaimsCost.Text = string.Format("{0:F2}", pContact.FixedCost);
                        //lblClaimsRevenue.Text = string.Format("{0:F2}", Convert.ToDouble(pContact.ActualCost) - pContact.FixedCost);
                        //ddlSourceofLead.SelectedValue = ddl pContact.SourceofLead;
                        ddlSourceofLead.SelectedIndex = ddlSourceofLead.Items.IndexOf(ddlSourceofLead.Items.FindByText(pContact.SourceofLead));

                        ListBox_setValues(pContact.Tags);

                        //get first addrss

                        BindAddressDetails(contactid);

                    }
                    Gridbind(Convert.ToInt32(Request.QueryString["ContactID"]));
                }
                else
                {
                    btnuser.Visible = false;
                    lbtnUpload.Visible = false;
                    pnladdress.Visible = false;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
           
            try
            {
                string[] emails = txtEmail.Text.ToLower().Trim().Split(',');
                cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                 uRepository = new UserRepository<UserMgt.Entity.Contractor>();
                if (Request.QueryString["ContactID"] == null)
                {
                    //if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID ).Count() == 0) &&
                    //     (uRepository.GetAll().Where(o => o.SID != 7 && o.Status=="Active" && o.EmailAddress.ToLower() == emails[0] ).Count() == 0))
                    if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && (o.isDisabled.HasValue?o.isDisabled.Value:false) == false && o.PortfolioID == sessionKeys.PortfolioID).Count() == 0))
                    {
                        pContact = new PortfolioContact();
                        pContact.Name = txtname.Text.Trim();
                        pContact.PortfolioID = sessionKeys.PortfolioID;
                        pContact.Email = txtEmail.Text.Trim();
                        pContact.Telephone = txtTelephone.Text.Trim();
                        pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
                        pContact.Mobile = txtmobileno.Text.Trim();
                    pContact.Address1 = string.Empty;
                    pContact.Town = string.Empty;
                    pContact.City = string.Empty;
                    pContact.Postcode = string.Empty;
                        pContact.DateLogged = DateTime.Now;
                    pContact.SourceofLead = ddlSourceofLead.SelectedValue;
                    pContact.Tags = ListBox_getValues();
                    //add to contact 
                    cRepository.Add(pContact);
                        if (pContact != null)
                        {
                            if (Fileupload_contact.HasFile)
                            {
                                string fname = "contact_" + pContact.ID;
                                ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                            }
                            FileuploadMethod(pContact.ID);


                            UpdateAddressDetails(pContact.ID);

                            lblmsg.Text = "<p class='bg-success'>Added successfully</p>";
                            Response.Redirect("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID=" + pContact.ID, false);
                        }
                        else
                        {
                            lblmsg.Text = "Cannot insert contact";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                    }
                }
                else
                {
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    //if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && o.PortfolioID == sessionKeys.PortfolioID && o.ID != contactid).Count() == 0)
                    //    &&
                    //     (uRepository.GetAll().Where(o => o.SID != 7 && o.Status == "Active" && o.EmailAddress.ToLower() == emails[0]).Count() == 0))
                    if ((cRepository.GetAll().Where(o => o.Email.ToLower() == emails[0] && (o.isDisabled.HasValue ? o.isDisabled.Value : false) == false && o.PortfolioID == sessionKeys.PortfolioID && o.ID != contactid).Count() == 0))
                    {
                        pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();
                        pContact.Name = txtname.Text.Trim();
                        pContact.Email = txtEmail.Text.Trim();
                        pContact.Telephone = txtTelephone.Text.Trim();
                        pContact.Mobile = txtmobileno.Text.Trim();
                    pContact.Address1 = string.Empty;
                    pContact.Town = string.Empty;
                    pContact.City = string.Empty;
                    pContact.Postcode = string.Empty;
                    pContact.SourceofLead = ddlSourceofLead.SelectedValue;
                    pContact.Tags = ListBox_getValues();
                    //add to contact 
                    cRepository.Edit(pContact);
                        if (pContact != null)
                        {
                            if (Fileupload_contact.HasFile)
                            {
                                string fname = "contact_" + pContact.ID;
                                ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                            }
                            FileuploadMethod(pContact.ID);
                            UpdateAddressDetails(pContact.ID);
                            lblmsg.Text = "<p class='bg-success'> Updated successfully</p>";
                            //Response.Redirect("~/WF/CustomerAdmin/PortfolioContacts.aspx", false);
                        }
                        else
                        {
                            lblmsg.Text = "Cannot insert contact";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "<p class='bg-danger'> Email address already exists. please try again </p>";
                    }
                }
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

      

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/CustomerAdmin/PortfolioContacts.aspx", false);
        }

        protected void btnuser_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ContactID"] != null)
            {
                int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                if (Fileupload_contact.HasFile)
                {
                    string fname = "contact_" + contactid;
                    ImageManager.SaveImage_setpath(fname, Fileupload_contact.FileBytes, "PortfolioContacts");
                }
                // set contact details
                SetContactDetails();
            }
        }

       

        public void Gridbind(int SID)
        {
            try
            {
                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID);
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/Contacts/warranty" + SID));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                    }

                    gridfiles.DataSource = files;
                    gridfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void FileuploadMethod(int contactid)
        {
            try
            {
                if (fileupload.HasFile)
                {
                    // Server.MapPath("~/WF/UploadData/AutoResponder/Attachments/TemplateID" + TemplateID);
                    var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + contactid);
                    //HttpFileCollection uploadedFiles = fileupload.PostedFiles // Request.Files;
                    foreach(var postedfiles in fileupload.PostedFiles)
                    {
                         string fileName = Path.GetFileName(postedfiles.FileName);
                         if (!System.IO.Directory.Exists(folderpath))
                        {
                            System.IO.Directory.CreateDirectory(folderpath);
                            fileupload.SaveAs(folderpath + "\\" + fileName);
                        }
                        else
                        {
                            fileupload.SaveAs(folderpath + "\\" + fileName);
                        }
                    }


                    //for (int i = 0; i < uploadedFiles.Count; i++)
                    //{
                    //    HttpPostedFile userPostedFile =  uploadedFiles[i];
                    //    string fileName = Path.GetFileName(userPostedFile.FileName);

                    //    if (!System.IO.Directory.Exists(folderpath))
                    //    {
                    //        System.IO.Directory.CreateDirectory(folderpath);
                    //        userPostedFile.SaveAs(folderpath + "\\" + fileName);
                    //    }
                    //    else
                    //    {
                    //        userPostedFile.SaveAs(folderpath + "\\" + fileName);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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

        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Download")
            {
                var id = e.CommandArgument;
                var folderpath = Server.MapPath("~/WF/UploadData/Contacts/warranty" + Convert.ToInt32(Request.QueryString["ContactID"]));


            }
        }

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            pcTags = new PortfolioContactsTagsBAL();
            if (!string.IsNullOrEmpty(txtTag.Text.Trim()))
            {
                pcTags.PortfolioContactsTags_Add(txtTag.Text.Trim());
                BindTags();
            }
        }
        #region Bind Contacts
        public void BindTags()
        {
            pcTags = new PortfolioContactsTagsBAL();
            listTags.DataSource = pcTags.PortfolioContactsTags_SelectAll().ToList();
            listTags.DataTextField = "Tag";
            listTags.DataValueField = "ID";
            listTags.DataBind();
            listTags.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region listview set and get
        private void ListBox_setValues(string values)
        {
            if (values.Length > 0)
            {
                string[] svalues = values.Split(',');

                foreach (ListItem litem in listTags.Items)
                {
                    if (svalues.Contains(litem.Text) && !string.IsNullOrEmpty(litem.Text.Trim()))
                        litem.Selected = true;
                }
            }
        }
        private string ListBox_getValues()
        {
            string retval = string.Empty;
            foreach (ListItem litem in listTags.Items)
            {
                if (litem.Selected)
                    retval = retval + litem.Text + ",";
            }

            return retval;
        }
        #endregion


        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    PortfolioContactBAL pb = new PortfolioContactBAL();
                    var uid = Convert.ToInt32(Request.QueryString["ContactID"].ToString());
                    var cDetails = pb.PortfolioContact_SelectByID(uid).FirstOrDefault();
                    if (cDetails != null)
                    {
                        cDetails.isDisabled = true;
                        pb.PortfolioContact_update(cDetails);
                      //  UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(uid, "InActive");
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
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