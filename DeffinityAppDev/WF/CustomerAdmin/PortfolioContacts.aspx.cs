using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
//using Deffinity.PortfolioContactManager;
//using Deffinity.PortfolioContact;
using System.IO;
using System.Data.OleDb;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.BLL;
using DC.BLL;
using UserMgt.DAL;
using UserMgt.Entity;
using System.Reflection;
using PortfolioMgt.BAL;
using ClosedXML.Excel;
public partial class Contacts : System.Web.UI.Page
{
    IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
    IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
    PortfolioContact pContact = null;
    int PortfolioID;
    //PorfolioContactsCS objPFContactCS = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        PortfolioID = sessionKeys.PortfolioID;
        //lblmsg.Visible = false;
        if (!IsPostBack)
        {
            if (Request.QueryString["nav"] != null)
            {
                //linkBack.NavigateUrl = "~/WF/Onboarding/Default.aspx";
                //linkBack.Text= "<i class='fa fa-arrow-left'></i> Return to Onboarding";
                //linkBack.Visible = true;
            }
            //tags
            BindTags();
            //maintenace dropdown
            //BindMaintenanceType();
            BindGrid();
            //BindDepartment();

        }
    }
    #region Bind Contacts
    public void BindTags()
    {
        var pcTags = new PortfolioContactsTagsBAL();
        listTags.DataSource = pcTags.PortfolioContactsTags_SelectAll().ToList();
        listTags.DataTextField = "Tag";
        listTags.DataValueField = "ID";
        listTags.DataBind();
        listTags.Items.Insert(0, new ListItem("", ""));
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
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
    private List<d_PortfolioContact> GetContactList()
    {
        string searchtext = txtSearch.Text.Trim().ToLower();
        var cList = new List<PortfolioMgt.Entity.PortfolioContact>();
        //var fList = new List<PortfolioMgt.Entity.PortfolioContact>();
        var aList = new List<PortfolioMgt.Entity.PortfolioContactAddress>();

        cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
        var aRep =  new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
        aList = aRep.GetAll().ToList();
        cList = cRepository.GetAll().Where(o => (o.isDisabled.HasValue ? o.isDisabled.Value : false) == false).Where(p => p.PortfolioID == sessionKeys.PortfolioID).OrderBy(p => p.Name).Select(p => p).ToList();

        if (searchtext == "" && ListBox_getValues().Length == 0)
        {
            cList = cList.Select(p => p).ToList();

        }
        else if(ListBox_getValues().Length >0)
        {
            cList = cList.Where(p => (
               (p.Tags != null ? p.Tags.ToLower().Contains(ListBox_getValues().ToLower()) : false)
               )).OrderBy(p => p.Name).Select(p => p).ToList();
        }
        else
        {
            cList = cList
                .Where(p => (
                (p.Name != null ? p.Name.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Email != null ? p.Email.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Mobile != null ? p.Mobile.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Telephone != null ? p.Telephone.ToLower().Contains(searchtext.ToLower()) : false)
               //|| (p.Notes != null ? p.Notes.ToLower().Contains(searchtext.ToLower()) : false)
               || (p.Tags != null ? p.Tags.ToLower().Contains(searchtext.ToLower()) : false)
                )).OrderBy(p => p.Name).ToList();
            //ListBox_getValues()

            if (ListBox_getValues().Length > 0)
            {
                cList = cList.Where(p => (
                   (p.Tags != null ? p.Tags.ToLower().Contains(ListBox_getValues().ToLower()) : false)
                   )).OrderBy(p => p.Name).Select(p => p).ToList();
            }
        }

        var list = (from p in cList
                    select new d_PortfolioContact
                    {
                        Address1 = p.Address1,
                        Address2 = p.Address2,
                        BuildingName = p.BuildingName,
                        City = p.City,
                        Country = p.Country,
                        County = p.County,
                        //DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateformat(), p.DateLogged.HasValue ? p.DateLogged.Value : DateTime.Now),
                        DateLogged = p.DateLogged.HasValue ? p.DateLogged.Value : DateTime.Now,
                        DateOfBirth = p.DateOfBirth,
                        DepartmentID = p.DepartmentID,
                        Email = p.Email,
                        Fax = p.Fax,
                        ID = p.ID,
                        isDisabled = p.isDisabled,
                        Key_Contact = p.Key_Contact,
                        Likes_Dislikes = p.Likes_Dislikes,
                        Location = p.Location,
                        LogintoPortal = p.LogintoPortal,
                        Mobile = p.Mobile,
                        Name = p.Name,
                        Notes = p.Notes,
                        PortfolioID = p.PortfolioID,
                        Postcode = p.Postcode,
                        Telephone = p.Telephone,
                        Title = p.Title,
                        Town = p.Town,
                        Tags = p.Tags,
                        cnt = aList.Where(o=>o.ContactID == p.ID).Count()

                    }).ToList(); ;

        return list;
    }

    private void BindGrid()
    {
        try
        {

            GridContactsInfo.DataSource = GetContactList();
            //GridContactsInfo.DataSource = (from p in list
            //                               select new PortfolioContact
            //                               {
            //                                   Address1= p.Address1,
            //                                   Address2 = p.Address2,
            //                                   BuildingName = p.BuildingName,
            //                                   City = p.City,
            //                                   Country = p.Country,
            //                                   County = p.County,
            //                                   //DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateformat(), p.DateLogged.HasValue ? p.DateLogged.Value : DateTime.Now),
            //                                   DateLogged = p.DateLogged.HasValue ? p.DateLogged.Value : DateTime.Now,
            //                                   DateOfBirth = p.DateOfBirth,
            //                                   DepartmentID = p.DepartmentID,
            //                                   Email = p.Email,
            //                                   Fax = p.Fax,
            //                                   ID = p.ID,
            //                                   isDisabled = p.isDisabled,
            //                                   Key_Contact = p.Key_Contact,
            //                                   Likes_Dislikes = p.Likes_Dislikes,
            //                                   Location = p.Location,
            //                                   LogintoPortal = p.LogintoPortal,
            //                                   Mobile = p.Mobile,
            //                                   Name = p.Name,
            //                                   Notes = p.Notes,
            //                                   PortfolioID = p.PortfolioID,
            //                                   Postcode = p.Postcode,
            //                                   Telephone = p.Telephone,
            //                                   Title = p.Title,
            //                                   Town = p.Town
                                               
            //                               }).ToList();
            
            GridContactsInfo.DataBind();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


    #region Bind Department dropdown
    private void BindDepartment()
    {
        var departmentList = PortfolioContactsDepartmentBAL.GetPortfolioContactsDepartmentList();
        ddlDepartment.DataSource = departmentList.Where(a => a.CustomerID == sessionKeys.PortfolioID).ToList();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    #endregion

    protected void btnUpdateDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int ContactID = Convert.ToInt32(hcontactid.Value);
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contactdetails = pd.PortfolioContacts.Where(o => o.ID == ContactID).FirstOrDefault();
                    var associatedUser = pd.PortfolioContactAssociates.Where(o => o.ContactID == ContactID).FirstOrDefault();
                    if (associatedUser != null)
                    {
                        var userdetails = ud.Contractors.Where(o => o.ID == associatedUser.CustomerUserID).FirstOrDefault();
                        if (userdetails != null)
                        {
                            if (!string.IsNullOrEmpty(txtPortalPassword.Text.Trim()))
                            {
                                userdetails.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPortalPassword.Text.Trim(), "SHA1");
                                userdetails.Status = chkUserStatus.Checked ? "Active" : "InActive";
                                userdetails.ModifiedDate = DateTime.Now;

                                ud.SubmitChanges();
                                //update password table
                                PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(ContactID, userdetails.ID, userdetails.LoginName, txtPortalPassword.Text.Trim());
                                //Change the reset password status is modified
                                var resetpassword_select = (from p in ud.UserPasswordResets
                                                            where p.UserID == associatedUser.CustomerUserID && p.IsActive == true && p.IsModified == false
                                                            select p).ToList();
                                if (resetpassword_select != null)
                                {
                                    if (resetpassword_select.Count > 0)
                                    {
                                        foreach (var rp in resetpassword_select)
                                        {
                                            rp.IsModified = true;
                                            ud.SubmitChanges();
                                        }
                                    }
                                }
                            }


                            if (userdetails.Status == "Active")
                            {
                                //inter user to portal user
                                var portalUser = pd.AssignedCustomerToPortfolios.Where(o => o.CustomerID == userdetails.ID && o.Portfolio == sessionKeys.PortfolioID).FirstOrDefault();
                                if (portalUser == null)
                                {
                                    portalUser = new AssignedCustomerToPortfolio();
                                    portalUser.Portfolio = sessionKeys.PortfolioID;
                                    portalUser.CustomerID = userdetails.ID;
                                    pd.AssignedCustomerToPortfolios.InsertOnSubmit(portalUser);
                                }
                                contactdetails.LogintoPortal = true;
                                pd.SubmitChanges();
                            }
                            else
                            {
                                contactdetails.LogintoPortal = false;
                                pd.SubmitChanges();
                            }
                            lblUserDetails.Text = "Updated successfully.";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            mdlOptions.Hide();
            BindGrid();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        mdlOptions.Hide();
        //Response.Redirect("~/PortfolioContacts.aspx");
        BindGrid();
    }
    protected void btnLogintoportal_Click(object sender, EventArgs e)
    {
        try
        {
            if (ChkLogintoPortal.Checked)
            {
                var ContactID = Convert.ToInt32(hcontactid.Value);
                ContractorsAndAssociateInsert(ContactID);
            }

        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            mdlOptions.Hide();
            //Response.Redirect("~/PortfolioContacts.aspx");
            BindGrid();
        }
    }

    protected void btnloginClose_Click(object sender, EventArgs e)
    {
        mdlOptions.Hide();
        //Response.Redirect("~/PortfolioContacts.aspx");
        BindGrid();
    }
    protected void GridContactsInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SetReminder")
        {
            hcontactid.Value =e.CommandArgument.ToString();
            mdlExnter.Show();
        }
            if (e.CommandName == "Options")
        {
            int ContactID = Convert.ToInt32(e.CommandArgument);
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    hcontactid.Value = ContactID.ToString();
                    var contactdetails = pd.PortfolioContacts.Where(o => o.ID == ContactID).FirstOrDefault();
                    var associatedUser = pd.PortfolioContactAssociates.Where(o => o.ContactID == ContactID).FirstOrDefault();
                    if (associatedUser != null)
                    {
                        var userdetails = ud.Contractors.Where(o => o.ID == associatedUser.CustomerUserID).FirstOrDefault();
                        txtUsername.Text = userdetails.ContractorName;
                        txtLoginName.Text = userdetails.LoginName;
                        chkUserStatus.Checked = userdetails.Status == "Active" ? true : false;
                        pnlUserDeatils.Visible = true;
                        pnlLogintoportal.Visible = false;
                    }
                    else
                    {
                        pnlLogintoportal.Visible = true;
                        pnlUserDeatils.Visible = false;
                    }
                }
            }
            mdlOptions.Show();

        }
        if (e.CommandName == "Delete")
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            try
            {
             //  DeleteAddressData(ID);
              // DeleteRelatedCalles(ID);

                cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                var pcDetails = cRepository.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (pcDetails != null)
                {
                    pcDetails.isDisabled = true;
                    cRepository.Edit(pcDetails);
                }
                //    objPFContactCS = new PorfolioContactsCS();
                //objPFContactCS.DeletePortfolioContacts(ID);
                //PorfolioContacts.DeletePortfolioContacts(ID);
                //lblmsg.Visible = true;
                lblmsg.Text = "Contact deleted successfully";

            }
            catch (Exception ex)
            {
                //lblmsg.Visible = true;
                lblError.Text = "Cannot delete contact";
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                BindGrid();
            }
        }
        if (e.CommandName == "ManageAssets")
        {
            Response.Redirect("~/WF/Assets/AssetsAdmin.aspx?PContactId=" + e.CommandArgument.ToString(), false);
        }
        if (e.CommandName == "docs")
        {
            Response.Redirect(string.Format("~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}", e.CommandArgument.ToString()), false);
        }

        if (e.CommandName == "MoreOptions")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());

            HiddenFiled1.Value = ID.ToString();
            if (ID != null)
            {

                //objPFContactCS = new PorfolioContactsCS();
                cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                var pcDetails = cRepository.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                if (pcDetails != null)
                {
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    Page.SetFocus(btnUpdate);
                    //btnUpdate.Focus();
                    //DataRow dr = dt.Rows[0];
                    txtName.Text = pcDetails.Name;//dr["Name"].ToString();
                    txtTitle.Text = pcDetails.Title; //dr["Title"].ToString();
                    TxtEmail.Text = pcDetails.Email;// dr["Email"].ToString();
                    txtTelephone.Text = pcDetails.Telephone;// dr["Telephone"].ToString();
                    txtLocation.Text = pcDetails.Location;// dr["Location"].ToString();
                    txtNotes.Text = pcDetails.Notes;// dr["Notes"].ToString();
                    CheckKeyContact.Checked = (pcDetails.Key_Contact.HasValue) ? pcDetails.Key_Contact.Value : false;
                    //txtLikesDislikes.Text = dr["Likes_Dislikes"].ToString();

                    

                    txtbuildingName.Text = pcDetails.BuildingName;
                    txtAddrs1.Text = pcDetails.Address1;
                    txtPostcode.Text = pcDetails.Postcode;
                    txtTown.Text = pcDetails.Town;
                    txtCity.Text = pcDetails.City;
                    txtCountry.Text = pcDetails.Country;

                    //if (dateOfBirth != "01/01/1900")
                    //    txtDateofBirth.Text = dateOfBirth;
                    //else
                    //    txtDateofBirth.Text = string.Empty;

                    //txtAddrs1.Text = dr["Address1"].ToString();
                    //txtAddrs2.Text = dr["Address2"].ToString();
                    //txtCountry.Text = dr["Country"].ToString();
                    //txtPostcode.Text = dr["Postcode"].ToString();
                    //txtFax.Text = dr["Fax"].ToString();
                    //txtMobile.Text = dr["Mobile"].ToString();
                    //txtCounty.Text = dr["County"].ToString();
                    //ddlDepartment.SelectedValue = dr["DepartmentID"].ToString();
                    //if ((dr["isDisabled"].ToString()) == "True")
                    //{
                    //    chkHideContact.Checked = true;

                    //}
                    //else
                    //{
                    //    chkHideContact.Checked = false;
                    //}
                }

            }


        }
    }

private static void DeleteRelatedCalles(int ID)
{
                try{
                //delete the related Tickets
                using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                {
                    var callIDList = dc.CallDetails.Where(o => o.RequesterID == ID).ToList();
                    if(callIDList.Count >0)
                    {
                       //Delete in FLS details 
                        var flist = dc.FLSDetails.Where(o => callIDList.Select(p => p.ID).ToArray().Contains(o.CallID.HasValue ? o.CallID.Value : 0)).ToList();
                        if(flist.Count >0)
                        {
                             dc.FLSDetails.DeleteAllOnSubmit(flist);
                             dc.SubmitChanges();
                        }
                        dc.CallDetails.DeleteAllOnSubmit(callIDList);
                        dc.SubmitChanges();
                    }
                }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
}

    private static void DeleteAddressData(int ID)
    {
        try
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var addlist = pd.PortfolioContactAddresses.Where(o => o.ContactID == ID).ToList();
                if (addlist.Count > 0)
                {

                    //delete if any products are there
                    var addonslist = pd.ProductAddonPriceAssociates.Where(o => addlist.Select(a => a.ID).ToArray().Contains(o.AddressID)).ToList();
                    if (addonslist.Count > 0)
                    {
                        pd.ProductAddonPriceAssociates.DeleteAllOnSubmit(addonslist);
                        pd.SubmitChanges();
                    }
                    var paylist = pd.PortfolioContactPaymentDetails.Where(o => addlist.Select(a => a.ID).ToArray().Contains(o.AddressID)).ToList();
                    if (paylist.Count > 0)
                    {
                        pd.PortfolioContactPaymentDetails.DeleteAllOnSubmit(paylist);
                        pd.SubmitChanges();
                    }

                    pd.PortfolioContactAddresses.DeleteAllOnSubmit(addlist);
                    pd.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ContractorsAndAssociateInsert(int contactid)
    {
        try
        {
            using (UserDataContext ud = new UserDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var pcontact = pd.PortfolioContacts.Where(o => o.ID == contactid).FirstOrDefault();
                    string name = pcontact.Name;
                    string email = pcontact.Email;
                    string contactNo = pcontact.Telephone;
                    var contactUsers = pd.PortfolioContactAssociates.Where(o => o.ContactID == contactid).FirstOrDefault();
                    if (contactUsers == null)
                    {
                        //
                        UserMgt.Entity.Contractor cont = new UserMgt.Entity.Contractor();
                        string[] loginname = name.Split(' ');
                        string userName = string.Empty;
                        string password = string.Empty;

                        if (loginname.Length > 1)
                        {
                            if (loginname[0].Length > 1)
                                userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[1].ToLower();
                            else
                                userName = cont.LoginName = loginname[0].ToLower() + loginname[1].ToLower();
                        }
                        else
                        {
                            userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[0].ToLower();
                        }
                        //Check the user name is exists 
                        //if exists get new name
                        cont.LoginName = GetUserName(ud.Contractors.Select(p => p).ToList(), userName);
                        password = DeffinityManager.RandomPassword.GetCustomerPortalPassword(); //Membership.GeneratePassword(8, 0);
                        cont.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                        cont.ContractorName = name;
                        cont.EmailAddress = email;
                        var cno = contactNo;
                        if (cno.Length > 20)
                            cno = contactNo.Substring(0, 20);
                        cont.ContactNumber = cno;
                        cont.Status = "Active";
                        cont.CreatedDate = DateTime.Now;
                        cont.ModifiedDate = DateTime.Now;
                        //customer user type
                        cont.SID = 7;
                        cont.ResetPassword = false;
                        cont.IsImage = false;

                        ud.Contractors.InsertOnSubmit(cont);
                        ud.SubmitChanges();

                        //update details password
                        PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(contactid, cont.ID, cont.LoginName, password);
                        AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == cont.ID).Select(a => a).FirstOrDefault();
                        if (actp == null)
                        {
                            AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                            acp.Portfolio = sessionKeys.PortfolioID;
                            acp.CustomerID = cont.ID;
                            pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                            pd.SubmitChanges();
                        }
                        contactUsers = new PortfolioContactAssociate();
                        contactUsers.ContactID = contactid;
                        contactUsers.CustomerUserID = cont.ID;
                        pd.PortfolioContactAssociates.InsertOnSubmit(contactUsers);
                        pd.SubmitChanges();
                        //Add customer user to Assoicate Contact table
                        // DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(cont.ID, sessionKeys.PortfolioID);


                        //Mail to New Contractors
                        LoginDetailsMail(cont.ContractorName, cont.LoginName, password, cont.EmailAddress);
                        //enable login to portal
                        pcontact.LogintoPortal = true;
                        pd.SubmitChanges();


                    }
                    else
                    {

                        //check portfolio associate is working
                        AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == sessionKeys.PortfolioID && a.CustomerID == contactid).Select(a => a).FirstOrDefault();
                        if (actp == null)
                        {
                            AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                            acp.Portfolio = sessionKeys.PortfolioID;
                            acp.CustomerID = contactid;
                            pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                            pd.SubmitChanges();
                        }
                        // IF InActive customer User is ther make active
                        UserMgt.Entity.Contractor Contractor_update = ud.Contractors.Where(c => c.ID == contactid).FirstOrDefault();
                        if (Contractor_update != null)
                        {
                            if (Contractor_update.Status == "InActive")
                            {
                                Contractor_update.Status = "Active";
                                ud.SubmitChanges();
                            }
                        }
                        //Add customer user to Assoicate Contact table
                        //DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(contactid, sessionKeys.PortfolioID);
                    }
                }
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    public string GetUserName(List<UserMgt.Entity.Contractor> userlist, string UserName)
    {
        string retVal = string.Empty;
        bool checkUserExist = false;
        int i = 1;
        while (!checkUserExist)
        {
            int cnt = userlist.Where(p => p.LoginName == UserName).Count();
            if (cnt > 0)
            {

                UserName = UserName + i.ToString();
                retVal = UserName;
                checkUserExist = false;
                i++;
            }
            else
            {
                retVal = UserName;
                checkUserExist = true;
            }
        }



        return retVal;
    }

    public void LoginDetailsMail(string name, string uname, string password, string toEmail)
    {
        try
        {
            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            string subject = "Welcome to your portal";
            Emailer em = new Emailer();
            string body = em.ReadFile("~/WF/DC/EmailTemplates/ContractorWelcomeMail.htm");
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[user]", name);
            body = body.Replace("[username]", uname);
            body = body.Replace("[password]", password);
            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
            body = body.Replace("[year]", DateTime.Now.Year.ToString());
            em.SendingMail(fromemailid, subject, body, toEmail);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void chkHideContact_CheckedChanged(Object sender, EventArgs args)
    {

        try
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            Label id = row.FindControl("lblid1") as Label;
            Boolean hideContact = checkbox.Checked;

            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                var pcontact = pd.PortfolioContacts.Where(o => o.ID == Convert.ToInt32(id.Text)).FirstOrDefault();
                if (pcontact != null)
                {
                    pcontact.isDisabled = hideContact;
                    pd.SubmitChanges();
                    //lblmsg.Visible = true;
                    //lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Contact updated successfully";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void chkLogin_CheckedChanged(Object sender, EventArgs args)
    {

        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        Label lblName = row.FindControl("lblname") as Label;
        Label lblContactNo = row.FindControl("lblcontactnumber") as Label;
        Label lblEmail = row.FindControl("lblemail") as Label;
        Label id = row.FindControl("lblid1") as Label;
        Boolean logintoPortal = checkbox.Checked;
        string name = lblName.Text;
        string email = lblEmail.Text;
        string contactNo = lblContactNo.Text;


        if (checkbox.Checked)
        {
            try
            {
                //using (UserDataContext ud = new UserDataContext())
                //{
                //    using (PortfolioDataContext pd = new PortfolioDataContext())
                //    {
                int ContactID = int.Parse(id.Text);
                //PortfolioContactAssociate pca = pd.PortfolioContactAssociates.Where(p => p.ContactID == ContactID).FirstOrDefault();
                // Contact and Associate insertion
                ContractorsAndAssociateInsert(ContactID);
                // PortfolioContacts LogintoPortal update
                //PortfolioContact pc = pd.PortfolioContacts.Where(p => p.ID == int.Parse(id.Text)).Select(p => p).FirstOrDefault();
                //pc.LogintoPortal = logintoPortal;
                //pd.SubmitChanges();

                //    }
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        else
        {
            try
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        int ContactID = int.Parse(id.Text);
                        int ContractorID = 0;


                        //Add customer user to Assoicate Contact table
                        PortfolioContactAssociate pca = pd.PortfolioContactAssociates.Where(p => p.ContactID == ContactID).FirstOrDefault();
                        if (pca != null)
                        {
                            ContractorID = pca.CustomerUserID.Value;
                            //pd.PortfolioContactAssociates.DeleteOnSubmit(pca);
                            //pd.SubmitChanges();
                        }

                        //delete associate in associate ID 
                        AssignedCustomerToPortfolio acp = pd.AssignedCustomerToPortfolios.Where(p => p.CustomerID == ContractorID && p.Portfolio == sessionKeys.PortfolioID).FirstOrDefault();
                        if (acp != null)
                        {
                            pd.AssignedCustomerToPortfolios.DeleteOnSubmit(acp);
                            pd.SubmitChanges();
                        }

                        //In Active the Customer User
                        //UserMgt.Entity.Contractor cc = ud.Contractors.Where(p=> p.ID == ContractorID && p.SID == 7).FirstOrDefault();
                        //if(cc != null)
                        //{
                        //    cc.Status = "InActive";
                        //    cc.ModifiedDate = DateTime.Now;
                        //    ud.SubmitChanges();
                        //}

                        // PortfolioContacts LogintoPortal update
                        PortfolioContact pc = pd.PortfolioContacts.Where(p => p.ID == ContactID).Select(p => p).FirstOrDefault();
                        if (!logintoPortal)
                            pc.LogintoPortal = null;
                        else
                            pc.LogintoPortal = logintoPortal;
                        pd.SubmitChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }


    //private void controls_PortfolioContactsActivities_btnADD_Clicked(object sender, EventArgs e)
    //{
    //    BindActivityGrid(Convert.ToInt32(HiddenFiled1.Value));
    //    ActivityTab.ActiveTabIndex=0;
    //}


    protected void GridContactsInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridContactsInfo.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GridContactsInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int i = -1;
        int ID = Convert.ToInt32(((Label)GridContactsInfo.Rows[e.RowIndex].FindControl("lblid")).Text);
        string Name = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtname")).Text;
        string email = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtemail")).Text;
        string number = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtcontactnumber")).Text;
        string title = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txttitle")).Text;
        string location = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtlocation")).Text;
        string notes = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtnotes")).Text;
        bool key_contact = ((CheckBox)GridContactsInfo.Rows[e.RowIndex].FindControl("chkedit")).Checked;
        string likes_dislikes = ((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtlikesdislikes")).Text;
        //DateTime dateofbirth = Convert.ToDateTime(((TextBox)GridContactsInfo.Rows[e.RowIndex].FindControl("txtdateofbirth")).Text);
        //chkEditHideContact
        bool hidecontact = ((CheckBox)GridContactsInfo.Rows[e.RowIndex].FindControl("chkEditHideContact")).Checked;
        // bool  Keycontact = GetChecked(keycontact);
        try
        {
           //objPFContactCS = new PorfolioContactsCS();
            //DataTable _dt = objPFContactCS.SelectPortfolioContacts(ID);
            //if (_dt.Rows.Count > 0)
            //{
            //    DataRow _dr = _dt.Rows[0];
            //    string Address1 = _dr["Address1"].ToString();
            //    string Address2 = _dr["Address2"].ToString();
            //    string Country = _dr["Country"].ToString();
            //    string Postcode = _dr["Postcode"].ToString();
            //    string Fax = _dr["Fax"].ToString();
            //    string Mobile = _dr["Mobile"].ToString();
            //    string County = _dr["County"].ToString();
            //    int departmentId = Convert.ToInt32(_dr["DepartmentID"]);
            //    DateTime dateofbirth = Convert.ToDateTime(!string.IsNullOrEmpty(_dr["DateOfBirth"].ToString()) ? _dr["DateOfBirth"].ToString() : "01/01/1900");
            //    //i = objPFContactCS.UpdatePortfolioContacts(ID, PortfolioID, Name, title,
            //    //    email, number, location, key_contact, notes, likes_dislikes, dateofbirth,
            //    //    Address1, Address2, Country, Postcode, Fax, Mobile,
            //    //    County, departmentId, hidecontact, string.Empty, string.Empty, string.Empty);
            //    //pContact = cRepository.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            //    //if (pContact != null)
            //    //{

            //    //    pContact.Name = txtName.Text.Trim();
            //    //    pContact.Title = txtTitle.Text.Trim();
            //    //    pContact.Email = TxtEmail.Text.Trim();
            //    //    pContact.Telephone = txtTelephone.Text.Trim();
            //    //    pContact.Location = txtLocation.Text.Trim();
            //    //    pContact.Notes = txtNotes.Text.Trim();
            //    //    pContact.Key_Contact = CheckKeyContact.Checked;
            //    //    pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
            //    //    pContact.Mobile = txtMobile.Text.Trim();
            //    //    pContact.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            //    //    pContact.Address1 = txtAddrs1.Text.Trim();
            //    //    pContact.City = txtCity.Text.Trim();
            //    //    pContact.Postcode = txtPostcode.Text;
            //    //    pContact.Town = txtTown.Text.Trim();
            //    //    cRepository.Edit(pContact);
            //    //}
            //}
            //i = PorfolioContacts.UpdatePortfolioContacts(ID, PortfolioID, Name, title, email, number, location, key_contact, notes);
            if (i == 1)
            {
                //lblmsg.Visible = true;
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Contact updated successfully";
            }
            else
                if (i == -1)
                {
                    //lblmsg.Visible = true;
                    lblError.Text = "Contact name already exists please enter other name";
                }
                else
                {
                    //lblmsg.Visible = true;
                    lblError.Text = "Cannot update contact";
                }

            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            GridContactsInfo.EditIndex = -1;
            BindGrid();
        }
    }
    protected void GridContactsInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridContactsInfo.EditIndex = -1;
        //e.RowIndex = -1;
        BindGrid();
    }

    protected void GridContactsInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
            e.ExceptionHandled = true;
    }
    protected void GridContactsInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


    }
    protected void GridContactsInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridContactsInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridContactsInfo.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(HiddenFiled1.Value);
        cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
        uRepository = new UserRepository<UserMgt.Entity.Contractor>();

        pContact = cRepository.GetAll().Where(o => o.ID == ID).FirstOrDefault();
        if (pContact != null)
        {

            pContact.Name = txtName.Text.Trim();
            pContact.Title = txtTitle.Text.Trim();
            pContact.Email = TxtEmail.Text.Trim();
            pContact.Telephone = txtTelephone.Text.Trim();
            pContact.Location = txtLocation.Text.Trim();
            pContact.Notes = txtNotes.Text.Trim();
            pContact.Key_Contact = CheckKeyContact.Checked;
            pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
            pContact.Mobile = txtMobile.Text.Trim();
            pContact.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            pContact.Address1 = txtAddrs1.Text.Trim();
            pContact.City = txtCity.Text.Trim();
            pContact.Postcode = txtPostcode.Text;
            pContact.Town = txtTown.Text.Trim();
            cRepository.Edit(pContact);


            int PortfolioID = sessionKeys.PortfolioID;
            string Name = txtName.Text.Trim();
            string Title = txtTitle.Text.Trim();
            string Email = TxtEmail.Text.Trim();
            string Telephone = txtTelephone.Text.Trim();
            string Location = txtLocation.Text.Trim();
            string Notes = txtNotes.Text.Trim();
            bool Keycontact;
            if (CheckKeyContact.Checked)
            {
                Keycontact = true;
            }
            else
                Keycontact = false;
            DateTime DateofBirth = Convert.ToDateTime("01/01/1900");

            //  string LikesDislikes = txtLikesDislikes.Text.Trim();
            //string Address1 = txtAddrs1.Text.Trim();
            //string Address2 = txtAddrs2.Text.Trim();
            //string Country = txtCountry.Text.Trim();
            //string Postcode = txtPostcode.Text.Trim();
            //string Fax = txtFax.Text.Trim();
            string Mobile = txtMobile.Text.Trim();
            // string County = txtCounty.Text.Trim();
            int departmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
           // objPFContactCS = new PorfolioContactsCS();

            //int i = objPFContactCS.UpdatePortfolioContacts(ID, PortfolioID, Name, Title, Email,
            //    Telephone, Location, Keycontact, Notes, string.Empty, DateofBirth,
            //     string.Empty, string.Empty, string.Empty, string.Empty,
            //     string.Empty, Mobile, string.Empty, departmentId,
            //     chkHideContact.Checked, txtbuildingName.Text.Trim(),
            //     txtTown.Text.Trim(), txtCity.Text.Trim());
            //object obj = objPFContactCS.InsertPortfolioContacts(PortfolioID, Name, Title, Email, Telephone, Location, Keycontact, Notes, LikesDislikes, DateofBirth);
            if (pContact != null)
            {
                if (ImageFileupload_PFContact.HasFile)
                {
                    string fname = "contact_" + pContact.ID;
                    ImageManager.SaveImage_setpath(fname, ImageFileupload_PFContact.FileBytes, "PortfolioContacts");
                }
                //lblmsg.Visible = true;
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Contact updated successfully";
                clearFields();
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
            }
            else
            {
                //lblmsg.Visible = true;
                lblError.Text = "Cannot updated contact";
            }

            BindGrid();
        }


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
        if (cRepository.GetAll().Where(o => o.Email.ToLower() == TxtEmail.Text.Trim().ToLower() && o.PortfolioID == sessionKeys.PortfolioID).Count() == 0)
        {

            pContact = new PortfolioContact();
            pContact.PortfolioID = sessionKeys.PortfolioID;
            pContact.Name = txtName.Text.Trim();
            pContact.Title = txtTitle.Text.Trim();
            pContact.Email = TxtEmail.Text.Trim();
            pContact.Telephone = txtTelephone.Text.Trim();
            pContact.Location = txtLocation.Text.Trim();
            pContact.Notes = txtNotes.Text.Trim();
            pContact.Key_Contact = CheckKeyContact.Checked;
            pContact.DateOfBirth = Convert.ToDateTime("01/01/1900");
            pContact.Mobile = txtMobile.Text.Trim();
            pContact.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            pContact.Address1 = txtAddrs1.Text.Trim();
            pContact.Town = txtTown.Text.Trim();
            pContact.City = txtCity.Text.Trim();
            pContact.Town = txtTown.Text.Trim();
            pContact.BuildingName = txtbuildingName.Text.Trim();
            pContact.Country = txtCountry.Text.Trim();

            //add to contact 
            cRepository.Add(pContact);

            if (pContact != null)
            {
                if (ImageFileupload_PFContact.HasFile)
                {
                    string fname = "contact_" + pContact.ID;
                    ImageManager.SaveImage_setpath(fname, ImageFileupload_PFContact.FileBytes, "PortfolioContacts");
                }
                //lblmsg.Visible = true;
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Contact added successfully";
                clearFields();
            }
            else
            {
                //lblmsg.Visible = true;
                lblError.Text = "Cannot insert contact";
            }

            BindGrid();
        }
    }
    protected void btnCanel_Click(object sender, EventArgs e)
    {
        clearFields();
    }
    private void clearFields()
    {
        txtName.Text = "";
        txtTitle.Text = "";
        TxtEmail.Text = "";
        txtTelephone.Text = "";
        txtLocation.Text = "";
        txtNotes.Text = "";
        CheckKeyContact.Checked = false;
        //txtLikesDislikes.Text = "";
        ImageFileupload_PFContact.Attributes.Add("Value", "");
        //txtDateofBirth.Text = "";
        //txtAddrs1.Text = "";
        //txtAddrs2.Text = "";
        //txtCountry.Text = "";
        //txtPostcode.Text = "";
        //txtFax.Text = "";
        //txtMobile.Text="";
        //txtCounty.Text = "";
        ddlDepartment.SelectedValue = "0";
        chkHideContact.Checked = false;

        txtbuildingName.Text = string.Empty;
        txtPostcode.Text = string.Empty;
        txtAddrs1.Text = string.Empty;
        txtTown.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtCountry.Text = string.Empty;

    }
    public int GetChecked(bool value)
    {
        if (value)
        {
            return 1;
        }
        else return 0;
    }
    public string FormateDate(string value)
    {
        string mydate = "";
        if (value == "01/01/1900" || value == "01/01/1900 00:00:00")
        {
            mydate = "";
        }
        else
        {
            mydate = value;
        }

        return mydate;

    }

    protected void btnDownloadData_Click(object sender, EventArgs e)
    {
        try
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("worksheet1");

            int x = 2;
            // Title
            ws.Cell("A1").Value = "Name";
            ws.Cell("B1").Value = "Email Address";
            ws.Cell("C1").Value = "Contact Number";
            ws.Cell("D1").Value = "Login (Yes/No)";
            ws.Cell("E1").Value = "Address1";
            ws.Cell("F1").Value = "Address2";
            ws.Cell("G1").Value = "City";
            ws.Cell("H1").Value = "State";
            ws.Cell("I1").Value = "Zipcode";
            ws.Cell("J1").Value = "";
            ws.Cell("K1").Value = "";

            

            ws.Columns(1, 11).AdjustToContents();
            var rngTable = ws.Range("A1:K1");
            // var rngHeaders = rngTable.Range("A2:I2");
            rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngTable.Style.Font.Bold = true;
            rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;

            var clist = PortfolioMgt.BAL.PortfolioContactsBAL.PorfolioContact_SelectAll(sessionKeys.PortfolioID).ToList();
            var calist = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_Address_ByPortoflioID(sessionKeys.PortfolioID).ToList();

            if(clist.Count >0 && calist.Count >0)
            {

                var rlist = (from c in clist
                             join a in calist on c.ID equals a.ContactID
                             select new
                             {
                                 Name = c.Name,
                                 EmailAddress = c.Email,
                                 ContactNumber =c.Mobile,
                                 Login = c.LogintoPortal.HasValue?c.LogintoPortal.Value:false,
                                 Address1 = a.Address,
                                 Address2 = a.Address2,
                                 City = a.City,
                                 State = a.State,
                                 Zipcode = a.PostCode

                             }).ToList();

                if(rlist.Count >0 )
                {
                    int index = 1;
                    foreach(var r in rlist)
                    {
                        index++;

                        ws.Cell("A"+index).Value = r.Name;
                        ws.Cell("B" + index).Value = r.EmailAddress;
                        ws.Cell("C" + index).Value = r.ContactNumber;
                        ws.Cell("D" + index).Value = r.Login?"Yes":"No";
                        ws.Cell("E" + index).Value = r.Address1;
                        ws.Cell("F" + index).Value = r.Address2;
                        ws.Cell("G" + index).Value = r.City;
                        ws.Cell("H" + index).Value = r.State;
                        ws.Cell("I" + index).Value = r.Zipcode;
                        ws.Cell("J" + index).Value = "";
                        ws.Cell("K" + index).Value = "";
                    }
                }
            }

            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Contacts_data.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnDownLoadTemplate_Click(object sender, EventArgs e)
    {
        try
        {

            string filename = Server.MapPath("~/WF/UploadData/Templates/CustomerContact_Template.xlsx");
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
            if (fileInfo.Exists)
            {
                Response.Clear();

                HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=CustomerContacts_Template.xlsx");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgUpload_Click(object sender, EventArgs e)
    {
        #region Customer Contacts Excel Upload
        try
        {
            string filePath = fileUpload.PostedFile.FileName;
            string Extension = Path.GetExtension(filePath);
            //Check the Extention of file
            if (string.IsNullOrEmpty(Extension))
            {
                //lblmsg.Visible = true;
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                return;
            }
            if (isValid(fileUpload.PostedFile.FileName))
            {
                //lblmsg.Visible = false;
                string path = Server.MapPath("WF\\UploadData\\Contacts");
                string fileName = "\\" + fileUpload.FileName;

                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + fileName);
                string conStr = string.Empty;
                //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                                 .ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                                  .ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, path + fileName, "Yes");

                InsertExcelData(conStr);

                if (File.Exists(path + fileName))
                {
                    File.Delete(path + fileName);
                }

                //lblmsg.Visible = true;
                //lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Uploaded Successfully.";
            }
            else
            {
                //lblmsg.Visible = true;
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                lblError.Text = Resources.DeffinityRes.Pleaseselectvalidfile; //"Please select valid file";

            }

            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        #endregion
    }
    private bool isValid(string fileName)
    {

        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {

            case ".xls":
                return true;
            case ".xlsx":
                return true;

            default:
                return false;
        }

    }
    private void InsertExcelData(string conStr)
    {
        try
        {
            DataTable dt = Import_To_Grid(conStr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string surname = string.Empty;
                string Address1 = string.Empty;
                string City = string.Empty;
                string State = string.Empty;
                string Zipcode = string.Empty;
                string fax = string.Empty;

                string name = dt.Rows[i][0].ToString();
                if (dt.Rows[i][1] != null)
                    surname = dt.Rows[i][1].ToString();
                string title = dt.Rows[i][2].ToString();
                string email = dt.Rows[i][3].ToString();
                string contact = dt.Rows[i][4].ToString();
                string login = dt.Rows[i][5].ToString();


                string Address2 = string.Empty;


                if (dt.Rows[i][6] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][6].ToString()))
                        Address1 = dt.Rows[i][6].ToString();
                }
                if (dt.Rows[i][7] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][7].ToString()))
                        Address2 = dt.Rows[i][7].ToString();
                }
                if (dt.Rows[i][8] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][8].ToString()))
                        City = dt.Rows[i][8].ToString();
                }
                if (dt.Rows[i][9] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][9].ToString()))
                        State = dt.Rows[i][9].ToString();
                }
                if (dt.Rows[i][10] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][10].ToString()))
                        Zipcode = dt.Rows[i][10].ToString();
                }
                if (dt.Rows[i][11] != null)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][11].ToString()))
                        fax = dt.Rows[i][11].ToString();
                }

                bool logintoPortal = login.ToLower() == "yes" ? true : false;
                int contactID = 0;
                name = name + " " + surname;
                if (!string.IsNullOrEmpty(name))
                {

                    PortfolioContact pc_check = CustomerContactsBAL.CheckContact( email, sessionKeys.PortfolioID);

                    if (pc_check == null)
                    {
                        PortfolioContact pc = new PortfolioContact();
                        pc.PortfolioID = sessionKeys.PortfolioID;
                        pc.Name = name;
                        pc.Title = title;
                        pc.Email = email;
                        pc.Telephone = contact;
                        pc.Mobile = contact;
                        pc.LogintoPortal = logintoPortal;
                        pc.Key_Contact = false;
                        pc.Fax = fax;
                        CustomerContactsBAL.AddCustomerContacts(pc);
                        contactID = pc.ID;

                    }
                    else
                    {
                        //PortfolioContact pc = new PortfolioContact();
                        if(!string.IsNullOrEmpty(name))
                        pc_check.Name = name;
                        if (!string.IsNullOrEmpty(title))
                            pc_check.Title = title;
                        //pc_check.Email = email;
                        if (!string.IsNullOrEmpty(contact))
                            pc_check.Telephone = contact;
                        if (!string.IsNullOrEmpty(contact))
                            pc_check.Mobile = contact;
                        pc_check.LogintoPortal = logintoPortal;
                        if (!string.IsNullOrEmpty(fax))
                            pc_check.Fax = fax;
                        CustomerContactsBAL.UpdateCustomerContacts(pc_check);
                        contactID = pc_check.ID;
                    }

                    if (logintoPortal && contactID > 0)
                    {
                        ContractorsAndAssociateInsert(contactID);
                    }

                    if (contactID > 0)
                    {

                        if (!string.IsNullOrEmpty(Address1))
                        {
                            var addressid = CreateAddressFromVS(contactID,
                      Address1, City, State, Zipcode,
                      name, Address1, City, State, Zipcode, "0");
                            if (addressid > 0)
                            {
                                // retval = "Saved Successfully";
                                LogExceptions.LogException("contact address id:" + addressid.ToString());
                            }
                        }
                    }

                }


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    public int CreateAddressFromVS(int contactid,
            string address, string city, string state, string postcode,
            string bname, string baddress, string bcity, string bstate, string bpostcode,
              string amount)
    {

        int addressid = 0;
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
        IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ppRes = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
        IPortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn> pstartRes = new PortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn>();
        IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> passRes = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
        var ptypelist = ppRes.GetAll().ToList();
        var contact = pRes.GetAll().Where(o => o.ID == contactid).FirstOrDefault();
        //check order number should not exists

        if (contact != null)
        {
            //var adItem = paRes.GetAll().Where(o => o.ContactID == contact.ID).Where(o => o.Address.ToLower() == address.ToLower() && o.State.ToLower() == state.ToLower() && o.City.ToLower() == city.ToLower() && o.PostCode.ToLower() == postcode.ToLower()).FirstOrDefault();
            //if (adItem == null)
            //{
            var adItem = new PortfolioContactAddress();
            adItem.ContactID = contactid;

            adItem.BillingName = bname;

            adItem.Address = address;
            adItem.BillingAddress1 = baddress;

            adItem.City = city;
            adItem.BillingCity = bcity;

            adItem.State = state;
            adItem.BillingState = bstate;

            adItem.PostCode = postcode;
            adItem.BillingZipcode = bpostcode;

            //adItem.PolicyTypeID = ptypelist.Where(o => o.Title.ToLower() == policytype.Trim().ToLower()).FirstOrDefault().ID;
            //adItem.PolicyNumber = WSGetPolicyno(adItem.PolicyTypeID.Value);
            //Policy start from 
            //var psDate = pstartRes.GetAll().Where(o => o.PSIID == 1).FirstOrDefault();
            //adItem.PolicyStartsID = 1;

            //adItem.StartDate = Convert.ToDateTime(DateTime.Now).AddDays(psDate.Value);

            //1. monthly
            //2. Yearly
            //if (subscriptiontype == "1")
            //    adItem.ExpiryDate = adItem.StartDate.Value.AddMonths(1);
            //else
            //    adItem.ExpiryDate = adItem.StartDate.Value.AddYears(1);

            //adItem.ContractTermID = 2;
            adItem.Amount = Convert.ToDouble(amount);
            adItem.LoggedDatetime = DateTime.Now;



            paRes.Add(adItem);
            addressid = adItem.ID;




        }

        return addressid;
    }
    public string[] GetExcelSheetNames(string connectionString)
    {
        OleDbConnection con = null; DataTable dt = null;
        String conStr = connectionString;
        con = new OleDbConnection(conStr);
        con.Close();
        con.Open();
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dt == null)
        {
            return null;
        }
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }
        con.Close();
        return excelSheetNames;
    }
    private DataTable Import_To_Grid(string conStr)
    {
        string[] sheetnames = GetExcelSheetNames(conStr);
        string sheetname = string.Empty;
        if (sheetnames.Length > 0)
            sheetname = sheetnames[0];

        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        //Get the name of First Sheet
        connExcel.Open();
        //To fetch sheet names
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = sheetname;
        cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", SheetName);
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        return dt;

    }

    protected string GetUserImage(string contactsId)
    {
        string img = string.Empty;

        string filepath = Server.MapPath("~/WF/UploadData/Users/ThumbNailsMedium/") + "contact_" + contactsId.ToString() + ".png";

        if (System.IO.File.Exists(filepath))
        {
            string imgurl = string.Format("../../WF/UploadData/Users/ThumbNailsMedium/contact_{0}.png", contactsId.ToString());
            // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            img = string.Format("<img src='{0}' />", imgurl);

        }
        else
        {
            string imgurl = "../../WF/UploadData/Users/ThumbNailsMedium/user_0.png";
            img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
        }
        return img;
    }
    protected static string GetImageUrl(string contactsId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);
        bool isOriginal = false;
        ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
                
            }
        }
        else
        {
            isOriginal = true;
            eImageType = ImageManager.ImageType.OriginalData;
        }

        string img = string.Empty;

        string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/ThumbNailsMedium/") + "contact_" + contactsId.ToString() + ".png";

        if (System.IO.File.Exists(filepath))
        {
            if (isOriginal)
                img = string.Format("~/WF/UploadData/Users/OriginalData/contact_{0}.png", contactsId.ToString());
            else
                img = string.Format("~/WF/UploadData/Users/ThumbNailsMedium/contact_{0}.png", contactsId.ToString());
            // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
            //img = string.Format("<img src='{0}' />", imgurl);
        }
        else
        {
            img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
            //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
        }
        return img+"?r="+DateTime.Now.TimeOfDay.Milliseconds.ToString();
        // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

    }
    protected void GridContactsInfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        string Sortdir = GetSortDirection(e.SortExpression);
        string SortExp = e.SortExpression;
        var list = GetContactList();
        //if (Sortdir == "ASC")
        //{
        //    list = Sort<d_PortfolioContact>(list, SortExp, SortDirection.Ascending);
        //}
        //else
        //{
        //    list = Sort<d_PortfolioContact>(list, SortExp, SortDirection.Descending);
        //}
        this.GridContactsInfo.DataSource = list;
        this.GridContactsInfo.DataBind();
    }
    /// <summary>
    /// GEt Sorting direction
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    private string GetSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["SortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;
        return sortDirection;
    }
    /// <summary>
    /// Sort function
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="sortBy"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public List<PortfolioMgt.Entity.PortfolioContact> Sort<TKey>(List<PortfolioMgt.Entity.PortfolioContact> list, string sortBy, SortDirection direction)
    {
        PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
        if (direction == SortDirection.Ascending)
        {
            return list.OrderBy(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.PortfolioContact>();
        }
        else
        {
            return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PortfolioMgt.Entity.PortfolioContact>();
        }
    }

    protected void GridContactsInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblId = ((Label)e.Row.FindControl("lblid1"));
                LinkButton LinkButtonDelete = ((LinkButton)e.Row.FindControl("LinkButtonDelete"));
                if (LinkButtonDelete != null)
                {
                    if (sessionKeys.SID == 1)
                        LinkButtonDelete.Visible = true;
                    else
                        LinkButtonDelete.Visible = false;
                }
                //LinkButtonDelete
                //GridView GridAddress = ((GridView)e.Row.FindControl("gvAddress"));
                //if (GridAddress != null)
                //{
                //    gvAddress_Databind(Convert.ToInt32(lblId.Text), GridAddress);
                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void gvAddress_Databind(int contactID, GridView GridAddress)
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> aRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
        IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> aTypeRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
        var rlist = aRepository.GetAll().Where(o => o.ContactID == contactID).ToList();
        var tlist = aTypeRepository.GetAll().ToList();
        if (rlist.Count >0)
        {
            var result = (from r in rlist
                          select new AddressDisplayCalss
                          {
                              Address = r.Address,
                              City = r.City,
                              ContactID = r.ContactID,
                              DaysRemaining = r.ExpiryDate.HasValue?(r.ExpiryDate.Value - DateTime.Now).Days > 0 ? (r.ExpiryDate.Value - DateTime.Now).Days.ToString() : "Expired":"",
                              ExpiryDate = r.ExpiryDate.HasValue ? string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.ExpiryDate.HasValue ? r.ExpiryDate.Value : DateTime.Now):string.Empty,
                              ID = r.ID,
                              PolicyNumber = r.PolicyNumber != null? r.PolicyNumber:string.Empty   ,
                              PolicyTypeID = r.PolicyTypeID,
                              PostCode = r.PostCode,
                              StartDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.StartDate.HasValue ? r.StartDate.Value : DateTime.Now),
                              State = r.State,
                              PolicyTypeName = r.PolicyTypeID.HasValue ? tlist.Where(o => o.ID == r.PolicyTypeID).FirstOrDefault().Title : string.Empty,
                              DateCreated = string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.LoggedDatetime.HasValue ? r.LoggedDatetime.Value : (r.StartDate.HasValue ? r.StartDate.Value : DateTime.Now)),
                          }).ToList();
            GridAddress.DataSource = result;
            GridAddress.DataBind();
        }
       
    }

    public class AddressDisplayCalss
    {
        public string Address { set; get; }
        public string City { set; get; }
        public int ContactID { set; get; }
        public string DaysRemaining { set; get; }
        public string ExpiryDate { set; get; }
        public int ID { set; get; }
        public string PolicyNumber { set; get; }
        public int? PolicyTypeID { set; get; }
        public string PostCode { set; get; }
        public string StartDate { set; get; }
        public string State { set; get; }
        public string PolicyTypeName { set; get; }
        public string DateCreated { set; get; }
    }
    public class d_PortfolioContact 
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string BuildingName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public DateTime? DateLogged { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentID { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public int ID { get; set; }
        public bool? isDisabled { get; set; }
        public bool? Key_Contact { get; set; }
        public string Likes_Dislikes { get; set; }
        public string Location { get; set; }
        public bool? LogintoPortal { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int? PortfolioID { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
        public string Town { get; set; }
        public string Tags { get; set; }
        public int cnt { get; set; }
       
    }
    #region Download BoM Template

    private List<AddressDisplayCalss> GetAddressList(List<d_PortfolioContact> plist)
    {
        List<AddressDisplayCalss> addressList = new List<AddressDisplayCalss>();

        if (plist.Count > 0)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> aRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
            IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> aTypeRepository = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
            var rlist = aRepository.GetAll().Where(o => plist.Select(p => p.ID).Contains(o.ContactID)).ToList();
            var tlist = aTypeRepository.GetAll().ToList();
            if (rlist.Count > 0)
            {
                addressList = (from r in rlist
                               select new AddressDisplayCalss
                               {
                                   Address = r.Address,
                                   City = r.City,
                                   ContactID = r.ContactID,
                                   DaysRemaining = (r.ExpiryDate.Value - DateTime.Now).Days > 0 ? (r.ExpiryDate.Value - DateTime.Now).Days.ToString() : "Expired",
                                   ExpiryDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.ExpiryDate.HasValue ? r.ExpiryDate.Value : DateTime.Now),
                                   ID = r.ID,
                                   PolicyNumber = r.PolicyNumber,
                                   PolicyTypeID = r.PolicyTypeID,
                                   PostCode = r.PostCode,
                                   StartDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.StartDate.HasValue ? r.StartDate.Value : DateTime.Now),
                                   State = r.State,
                                   PolicyTypeName = r.PolicyTypeID.HasValue ? tlist.Where(o => o.ID == r.PolicyTypeID).FirstOrDefault().Title : string.Empty,
                                   DateCreated = string.Format(Deffinity.systemdefaults.GetStringDateformat(), r.LoggedDatetime.HasValue ? r.LoggedDatetime.Value : (r.StartDate.HasValue ? r.StartDate.Value : DateTime.Now)),
                               }).ToList();

            }
        }
        return addressList;
    }

    protected void btnDownloadAddressList_Click(object sender, EventArgs e)
    {
        try
        {
            //Get Contactlist
            var list = GetContactList();
            //get addresslist
            var addresslist = GetAddressList(list).ToList();



            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("worksheet1");

            int x = 2;
            // Title
            
            ws.Cell("A1").Value = "Name";
            ws.Cell("B1").Value = "Email Address";
            ws.Cell("C1").Value = "Mobile Number";
            ws.Cell("D1").Value = "Date Created";

            ws.Columns(1, 4).AdjustToContents();
            var rngTable = ws.Range("A1:K1");
            // var rngHeaders = rngTable.Range("A2:I2");
            rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngTable.Style.Font.Bold = true;
            rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;
            //Supplier List
            if (list.Count >0)
            {
                
                int sCount = 2;
                //int tCount = 1;
                //int mCount = 1;
                foreach (var item in list)
                {
                    if (sCount != 2)
                    {
                        sCount++;
                        ws.Cell("A" + sCount.ToString()).Value = string.Empty;
                        sCount++;
                    }
                    ws.Cell("A" + sCount.ToString()).Value = item.Name;
                    ws.Cell("A" + sCount.ToString()).Style.Font.Bold = true;
                    ws.Cell("B" + sCount.ToString()).Value = item.Email; 
                    ws.Cell("C" + sCount.ToString()).Value = item.Mobile; 
                    ws.Cell("D" + sCount.ToString()).Value = string.Format(Deffinity.systemdefaults.GetStringDateformat(),  item.DateLogged.HasValue ? item.DateLogged.Value : DateTime.Now);
                    ws.Column(1).Width = 150;




                    if (addresslist.Where(o => o.ContactID == item.ID).ToList().Count > 0)
                    {
                        sCount++;
                        //create header 

                        ws.Cell("B" + sCount.ToString()).Value = "Address";
                        ws.Cell("C" + sCount.ToString()).Value = "City";
                        ws.Cell("D" + sCount.ToString()).Value = "State ";
                        ws.Cell("E" + sCount.ToString()).Value = "Postcode";
                        ws.Cell("F" + sCount.ToString()).Value = "Policy Type";
                        ws.Cell("G" + sCount.ToString()).Value = "Policy Number";
                        ws.Cell("H" + sCount.ToString()).Value = "Start Date";
                        ws.Cell("I" + sCount.ToString()).Value = "Expiry Date";
                        ws.Cell("J" + sCount.ToString()).Value = "Days Remaining";
                        ws.Cell("K" + sCount.ToString()).Value = "Date Created";
                        

                        //Create sub header
                        var rngTable_sub = ws.Range("B" +sCount.ToString()+ ":k"+sCount.ToString());
                        // var rngHeaders = rngTable.Range("A2:I2");
                        rngTable_sub.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rngTable_sub.Style.Font.Bold = true;
                        rngTable_sub.Style.Fill.BackgroundColor = XLColor.LightBlue;
                        foreach (var a in addresslist.Where(o => o.ContactID == item.ID).ToList())
                        {
                            sCount++;
                            ws.Cell("B" + sCount.ToString()).Value = a.Address;
                            ws.Cell("C" + sCount.ToString()).Value = a.City;
                            ws.Cell("D" + sCount.ToString()).Value = a.State;
                            ws.Cell("E" + sCount.ToString()).Value = a.PostCode;
                            ws.Cell("F" + sCount.ToString()).Value = a.PolicyTypeName;
                            ws.Cell("G" + sCount.ToString()).Value = a.PolicyNumber;
                            ws.Cell("H" + sCount.ToString()).Value = a.StartDate;
                            ws.Cell("I" + sCount.ToString()).Value = a.ExpiryDate;
                            ws.Cell("J" + sCount.ToString()).Value = a.DaysRemaining;
                            ws.Cell("K" + sCount.ToString()).Value = a.DateCreated;
                        }
                    }
                    else
                    {
                        sCount++;
                    }

                  
                }
                ws.Columns().AdjustToContents();
            }
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Address List.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    private void BindMaintenanceType()
    {
        try
        {

            ddlMaintenanceType.DataSource = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_Select(sessionKeys.PortfolioID).OrderBy(o=>o.Name);
            ddlMaintenanceType.DataTextField = "Name";
            ddlMaintenanceType.DataValueField = "ID";
            ddlMaintenanceType.DataBind();
            ddlMaintenanceType.Items.Insert(0,new ListItem("Please select...", "0"));

        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnSelect_OnClick(object sender, EventArgs e)
    {
        try
        {
            var mData = new PortfolioMgt.Entity.MaintenanceSchedule();
            mData.DateOfReminder = Convert.ToDateTime(txtDateOfReminder.Text.Trim());
            mData.EquipmentName = txtEquipment.Text.Trim();
            mData.MaintenanceTypeID = Convert.ToInt32(ddlMaintenanceType.SelectedValue);
            mData.PortfolioID = sessionKeys.PortfolioID;
            mData.ReminderDescription = txtReminderDescription.Text.Trim();
            mData.RenewalAmount = Convert.ToDouble(txtRenewalAmount.Text.Trim());
            mData.RequesterID = Convert.ToInt32(hcontactid.Value);
            PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Add(mData);

            lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;

            txtDateOfReminder.Text = string.Empty;
            txtEquipment.Text = string.Empty;
            ddlMaintenanceType.SelectedValue = "0";
            txtReminderDescription.Text = string.Empty;
            txtRenewalAmount.Text = "0.00";
            hcontactid.Value = "0";
            mdlExnter.Hide();
            BindGrid();
            // Storage_AddUpdate(Convert.ToInt32(hbomid.Value), Convert.ToInt32(ddlsiteInSearch.SelectedValue), Convert.ToInt32(ddlWareshouse.SelectedValue), Convert.ToDouble(txtQtyReceived.Text));
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    public string SetTagCss(string tags)
    {
        string retval = string.Empty;
        if (tags != null)
        {
            if (tags.Trim().Length > 0)
            {
                retval = "<div class='bootstrap-tagsinput'>";
                var s = string.Empty;
                string[] str = tags.Split(',');
                foreach (var s1 in str)
                {
                    if (!string.IsNullOrEmpty(s1))
                        s = s + "<span class='tag label label-dark'>" + s1 + "</span> ";
                }
                retval = retval + s + "</div>";
            }
        }
        return retval;
    }
}


