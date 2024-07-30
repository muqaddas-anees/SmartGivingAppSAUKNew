using ClosedXML.Excel;
using DeffinityManager.BLL;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace DeffinityAppDev.App
{
    public partial class PendingOrganizations : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //firt time
                    Session[users] = null;
                    //check users and update the ower id in Project portfoliio table
                    BindCountry();
                    BingGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
      
        private void BindCountry()
        {
            try
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

                // ddlCountry.SelectedValue = "190";
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public List<UserMgt.Entity.v_contractor> GetUsers(bool getNewdata = false)
        {
            //if (getNewdata)
            //    Session[users] = null;
            //if (Session[users] == null)
            //{
            //    Session[users] = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
            //}

            //return (Session[users] as List<UserMgt.Entity.v_contractor>).ToList();
            return UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
        }

        private double GetTFee(PortfolioMgt.Entity.PortfolioPaymentSetting tFee)
        {
            double retval = 0.00;

            if (tFee != null)
            {
                if (tFee.TransactionFee.HasValue)
                    retval = tFee.TransactionFee.Value;
            }

            return retval;
        }

        public void BingGrid(bool getNewData = false)
        {
            try
            {

                string[] statusids = { "Pending", "Uploaded" };
                var iList = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o=> statusids.Contains( o.OrgarnizationStatus)).ToList();
                var tFee = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll();
                // var tList = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
                // var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().ToList();
                //  var glist = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select();

                //  var dlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().ToList();
                // var pb = new PortfolioMgt.BAL.PortfolioContactBAL();
                // var pdlist = pb.PortfolioContact_SelectAll().ToList();
                //var uList = GetUsers(getNewData);

                var rData = (from p in iList
                             orderby p.ID descending
                             //join u in uList on p.ID equals u.CompanyID
                             select new
                             {
                                 ID = p.ID,
                                 InstanceName = p.PortFolio,
                                 p.Address,
                                 p.EmailAddress,
                                 OrgarnizationStatus = p.OrgarnizationStatus ?? "",
                                 p.OrgarnizationGUID,
                                 p.OrgarnizationApproval,
                                 p.Town,
                                 p.State,
                                 p.CountryID,
                                 p.DenominationDetailsID,
                                 Religion = p.DenominationName,// p.DenominationDetailsID.HasValue ? (rlist.Where(o => o.ID == p.DenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Denomination = p.SubDenominationName, //p.SubDenominationDetailsID.HasValue ? (dlist.Where(o => o.ID == p.SubDenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Group = p.DenominationGroupName, //p.GroupDetailsID.HasValue ? (glist.Where(o => o.ID == p.GroupDetailsID).FirstOrDefault().Name) : string.Empty,
                                 Contact = p.BankName,//  pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Name : string.Empty,
                                 ContactEmail = p.EmailAddress,// //pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Email : string.Empty,
                                 ContactMobile = p.TelephoneNumber,// pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Mobile : string.Empty,
                                 LogoPath = p.LogoPath,
                                 p.SignatureSentToCardConnectOn,
                                 p.SignatureSentToCardConnect,
                                 p.SignatureSentToOrg,
                                 p.SignatureSentToOrgOn,
                                 p.SignatureSetLoginToOrg,
                                 p.SignatureSetLoginToOrgOn,
                                 ContactName = p.BankName,
                                 ContactNumber = p.TelephoneNumber,

                                 TransactionFee = GetTFee(tFee.Where(o => o.PortfolioID == p.ID).FirstOrDefault()),
                                 DateUploaded = p.StartDate.HasValue?p.StartDate.Value.ToShortDateString():"",

                             }).ToList(); ;//.ToList();

                // var ulist = GetUsers();

                //if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                //{
                //    rData = rData.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.EmailAddress != null ? p.EmailAddress.Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.Administrator != null ? p.Administrator.Contains(txtSearch.Text.ToLower()) : false)).ToList();
                //}


                if (QueryStringValues.Type == "all")
                {

                    var resultlist = rData.Where(o => o.InstanceName != "").OrderBy(o => o.InstanceName).ToList();

                    //lblNumberofInstances.Text = resultlist.Count().ToString();
                    // lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }
                else
                {
                    var resultlist = rData.Where(o => o.InstanceName != "").OrderBy(o => o.InstanceName).ToList();

                    if (Convert.ToInt32(ddlCountry.SelectedValue) > 0)
                        resultlist = resultlist.Where(o => o.CountryID == Convert.ToInt32(ddlCountry.SelectedValue)).ToList();

                    if (txtsearch.Value.Length > 0)
                    {
                        resultlist = resultlist.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                || (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    }

                    //    if (txtst.Value.Length > 0)
                    //    {
                    //        resultlist = resultlist.Where(p => (p.State != null ? p.State.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                    //|| (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    //    }
                    // lblNumberofInstances.Text = resultlist.Count().ToString();
                    //lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BingGrid();

        }
        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                GridViewRow row = e.Row;

                // Make sure we aren't in header/footer rows
                if (row.DataItem == null)
                {
                    return;
                }
                else
                {
                    //gv = (GridView)row.FindControl("gvInnerUsers");

                    //var dataRow = row.DataItem as dynamic;
                    //var r1 = dataRow.PortfolioID;
                    //var r = Convert.ToInt32(r1);
                    //if (r > 0)
                    //{
                    //    gv.DataSource = GetUsers().Where(o => o.CompanyID == r).OrderBy(o => o.ContractorName).ToList();
                    //    gv.DataBind();
                    //}

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Assuming your data source has a column named "SelectedValue" that contains the value to be selected in the DropDownList
                        string valueToSelect = DataBinder.Eval(e.Row.DataItem, "OrgarnizationStatus").ToString();

                        if (valueToSelect.Length > 0)
                        {
                            // Find the DropDownList and set its value
                            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlApproval");
                            if (ddl != null)
                            {
                                ddl.SelectedValue = valueToSelect;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if (gv.UniqueID == gvUniqueID)
            //{
            //    gv.PageIndex = gvNewPageIndex;
            //    gv.EditIndex = gvEditIndex;
            //    //Check if Sorting used


            //}


        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            BingGrid();
        }
        private void SetDefaultValues()
        {
            try
            {
                var pDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany(Convert.ToInt32(huid.Value.Trim()));
                if (pDetails != null)
                {

                    txtSaltPassphrase.Text = pDetails.consumerKey;
                    txtVendor.Text = pDetails.Vendor;
                    txtSecretKey.Text = pDetails.consumerSecret;
                }
                else
                {
                    txtSaltPassphrase.Text = string.Empty;
                    txtVendor.Text = string.Empty;
                    txtSecretKey.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var userid = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName == "email")
                {
                    try
                    {
                        Email ToEmail = new Email();
                        var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                        // var iList = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(user))
                        IPortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate> eREP = new PortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate>();
                        var r = eREP.GetAll().FirstOrDefault();

                        if (r != null)
                        {
                            var fromid = Deffinity.systemdefaults.GetFromEmail();
                            var subject = r.EmailSubject;
                            var body = r.EMailBody;

                            body = body.Replace("{{Instance Name}}", Deffinity.systemdefaults.GetInstanceTitle());
                            body = body.Replace("{{Instance URL}}", Deffinity.systemdefaults.GetWebUrl());

                            body = body.Replace("{{organization}}", c.PortFolio);
                            body = body.Replace("{{Organization}}", c.PortFolio);

                            body = body.Replace("{{Button1}}", string.Format("<a href='{1}' > <img ='{0}/assets/button1.png' /></a>", Deffinity.systemdefaults.GetWebUrl(), r.Button1Link + "?unid=" + c.OrgarnizationGUID));
                            body = body.Replace("{{Button2}}", string.Format("<a href='{1}' > <img ='{0}/assets/button2.png' /></a>", Deffinity.systemdefaults.GetWebUrl(), r.Button2Link));


                            ToEmail.SendingMail(fromid, subject, body, c.EmailAddress, "");
                            DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                        }

                        //SetDefaultValues();
                        //mdlMid.Show();
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }

                //else if (e.CommandName == "mid")
                //{
                //    huid.Value = userid.ToString();
                //    SetDefaultValues();
                //    mdlMid.Show();

                //}
                //else if (e.CommandName == "platform")
                //{
                //    Response.Redirect("~/App/PlatformSupportReport.aspx?orgid=" + userid, false);
                //}
                //else if (e.CommandName == "password")
                //{
                //    // huid.Value = userid.ToString();
                //    //  mdlManageOptions.Show();

                //}
                else if (e.CommandName == "status")
                {
                    var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(userid);

                    if (u.Status == UserMgt.BAL.ContractorStatus.Active)
                        u.Status = UserMgt.BAL.ContractorStatus.InActive;
                    else
                        u.Status = UserMgt.BAL.ContractorStatus.Active;
                    UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(userid, u.Status);
                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid(true);
                }
                else if (e.CommandName == "Instance")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.Visible.HasValue ? c.Visible.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if (e.CommandName == "Modules")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.AllowModules.HasValue ? c.AllowModules.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if (e.CommandName == "application")
                {
                    var orgid = e.CommandArgument.ToString();

                    Response.Redirect("~/midapplication", false);
                }
                else if (e.CommandName == "view")
                {
                    var orgid = e.CommandArgument.ToString();
                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(orgid)).FirstOrDefault();
                    if (rData != null)
                    {

                        Response.Redirect("~/midapplication.aspx?orgref=" + rData.OrgarnizationGUID, false);
                    }

                    // DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                }
                else if (e.CommandName == "sendtochurh")
                {
                    var orgid = e.CommandArgument.ToString();
                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(orgid)).FirstOrDefault();
                    if (rData != null)
                    {
                        MID_BAL.SendMailToOrganization(rData.OrgarnizationGUID);

                        rData.SignatureSentToOrg = true;
                        rData.SignatureSentToOrgOn = DateTime.Now;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(rData);


                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                    }

                }
                else if (e.CommandName == "sendtocardconnect")
                {
                    var orgid = e.CommandArgument.ToString();

                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(orgid)).FirstOrDefault();
                    if (rData != null)
                    {
                        MID_BAL.SendMailToCardConnect(rData.OrgarnizationGUID);

                        rData.SignatureSentToCardConnect = true;
                        rData.SignatureSentToCardConnectOn = DateTime.Now;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(rData);



                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                    }
                }
                else if (e.CommandName == "sendlogin")
                {
                    var orgid = e.CommandArgument.ToString();

                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(orgid)).FirstOrDefault();
                    if (rData != null)
                    {
                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var udetails = urRep.GetAll().Where(o => o.CompanyID == Convert.ToInt32(orgid)).OrderBy(o => o.ID).FirstOrDefault();
                        try
                        {
                            if (udetails != null)
                            {
                                MID_BAL.sendlogindetails(udetails.UserID);
                                rData.SignatureSetLoginToOrg = true;
                                rData.SignatureSetLoginToOrgOn = DateTime.Now;
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(rData);

                                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }

                }
                else if (e.CommandName == "members")
                {
                    var orgid = e.CommandArgument.ToString();

                    var rData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(orgid)).FirstOrDefault();
                    if (rData != null)
                    {
                        var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var udetails = urRep.GetAll().Where(o => o.CompanyID == Convert.ToInt32(orgid)).OrderBy(o => o.ID).FirstOrDefault();
                        try
                        {
                            if (udetails != null)
                            {
                                MID_BAL.SendMailToMembers(rData.OrgarnizationGUID);
                                rData.SignatureSetLoginToOrg = true;
                                rData.SignatureSetLoginToOrgOn = DateTime.Now;
                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(rData);

                                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                    }
                }
                else if (e.CommandName == "save")
                {

                    Button btn = (Button)e.CommandSource;
                    GridViewRow row = (GridViewRow)btn.NamingContainer;

                    // Find the DropDownList in the row.
                    DropDownList ddl = (DropDownList)row.FindControl("ddlApproval");


                    IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> pdRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();

                    var pd = pdRep.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    if (pd != null)
                    {
                        pd.OrgarnizationStatus = ddl.SelectedValue;
                       
                        pd.AcceptedDate = DateTime.Now;
                        pdRep.Edit(pd);
                        BingGrid();

                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnAddOrganization
        protected void btnAddOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Organization.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Organization.aspx", false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnSave_Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(huid.Value.Trim()) > 0)
                {
                    PortfolioMgt.Entity.PortfolioPaymentSetting p = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                    p.PortfolioID = Convert.ToInt32(huid.Value.Trim());
                    p.Host = "";
                    p.Notes = string.Empty;
                    //p.Partner = txtPartner.Text.Trim();
                    p.Password = "";
                    p.Username = "";
                    p.Vendor = txtVendor.Text.Trim();
                    p.consumerSecret = txtSecretKey.Text.Trim();
                    p.consumerKey = txtSaltPassphrase.Text.Trim();
                    p.IsActive = true;
                    p.PayType = "cardconnect";
                    var r = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(p);
                    if (r)
                    {
                        //lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                        //var d = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(Convert.ToInt32(huid.Value.Trim()));
                        // PasswordSendToMail(d, p.Vendor);
                    }

                    mdlManageOptions.Hide();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void PasswordSendToMail(ProjectPortfolio p, string mid)
        {
            try
            {
                //get contractor details
                var cdetails = UserMgt.BAL.ContractorsBAL.v_Contractor_SelectByID(p.Owner.HasValue ? p.Owner.Value : 0);
                var partnerDetails = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(p.PartnerID.HasValue ? p.PartnerID.Value : 0);
                var instance = p.PortFolio;
                Email em = new Email();
                string Body = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/Admin/EmailTemplates/midmail.html"));
                Body = Body.Replace("[Logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                Body = Body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                if (Body.Contains("[instance]"))
                {
                    Body = Body.Replace("[instance]", p.PortFolio);
                }

                if (Body.Contains("[Link]"))
                {
                    Body = Body.Replace("[Link]", Deffinity.systemdefaults.GetWebUrl());
                }
                if (Body.Contains("[contractorsname]"))
                {
                    Body = Body.Replace("[contractorsname]", cdetails.ContractorName);
                }
                if (Body.Contains("[companyname]"))
                {
                    Body = Body.Replace("[companyname]", p.PortFolio);
                }
                if (Body.Contains("[mid]"))
                {
                    Body = Body.Replace("[mid]", mid);
                }
                if (Body.Contains("[cell]"))
                {
                    Body = Body.Replace("[cell]", cdetails.ContactNumber);
                }
                // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                if (QueryStringValues.Type == ("all").ToLower())
                {
                    em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Deffinity.systemdefaults.GetInstanceTitle() + " " + instance + " MID has been updated", Body, "indra@deffinity.com", "");
                }
                else
                {
                    //em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Deffinity.systemdefaults.GetInstanceTitle() + " " + instance + " MID has been updated", Body, "nadeem.mohammed@123smartpro.com", "");
                    // em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Deffinity.systemdefaults.GetInstanceTitle() + " " + instance + " MID has been updated", Body, "mike.boreham@123smartpro.com", "");
                    //  em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Deffinity.systemdefaults.GetInstanceTitle() + " " + instance + " MID has been updated", Body, "middistribution@deffinity.com", "");
                    em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), Deffinity.systemdefaults.GetInstanceTitle() + " " + instance + " MID has been updated", Body, "indra@deffinity.com", "");
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
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Customers/") + "portfolio_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Customers/portfolio_{0}.png", contactsId.ToString());
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {


                var wb = new XLWorkbook();
                string prefix = "Organizations";

                // Create the worksheet name, truncating if necessary
                string worksheetName = prefix;
                var ws = wb.Worksheets.Add(worksheetName);

                // Title
                ws.Cell("A1").Value = "Organisations";
                ws.Cell("A2").Value = "Date Uploaded";//+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("B2").Value = "Charity Name";
                ws.Cell("C2").Value = "Registration Number";
                ws.Cell("D2").Value = "Phone Number";
                ws.Cell("E2").Value = "Email Address";
                ws.Cell("F2").Value = "Web Address";
                ws.Cell("G2").Value = "Address";
                ws.Cell("H2").Value = "Town";
                ws.Cell("I2").Value = "City";
                ws.Cell("J2").Value = "State";
                ws.Cell("K2").Value = "Postcode";
                ws.Cell("L2").Value = "Charity Type";
                ws.Cell("M2").Value = "Activity";

                // From worksheet
                var rngTable = ws.Range("A1:M2");

                var rngHeaders = rngTable.Range("A2:M2");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                rngTable.Cell(1, 1).Style.Font.FontSize = 15;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Row(1).Merge();

                ws.Columns(1, 9).AdjustToContents();

                string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Organisations");

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                var filename_download = string.Format("{1}_{0}.xlsx", DateTime.Now.Ticks, "Organizations");
                wb.SaveAs(path + "\\" + filename_download);

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + filename_download);
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + filename_download);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                Email ToEmail = new Email();
                foreach (GridViewRow row in GridInstances.Rows)
                {
                    CheckBox chk = row.FindControl("chk") as CheckBox;
                    Label lblid = row.FindControl("lblID") as Label;
                    if (chk != null && chk.Checked)
                    {
                       // string email = row.Cells[2].Text;
// Add your email sending logic here using the 'email' variable

                        var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(Convert.ToInt32( lblid.Text));
                        // var iList = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(user))
                        IPortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate> eREP = new PortfolioRepository<PortfolioMgt.Entity.tblEmailTemplate>();
                        var r = eREP.GetAll().FirstOrDefault();

                        if (r != null)
                        {
                            var fromid = Deffinity.systemdefaults.GetFromEmail();
                            var subject = r.EmailSubject;
                            var body = r.EMailBody;

                            body = body.Replace("{{Instance Name}}", Deffinity.systemdefaults.GetInstanceTitle());
                            body = body.Replace("{{Instance URL}}", Deffinity.systemdefaults.GetWebUrl());

                            body = body.Replace("{{organization}}", c.PortFolio);
                            body = body.Replace("{{Organization}}", c.PortFolio);

                            body = body.Replace("{{Button1}}", string.Format("<a href='{1}' > <img src='{0}/assets/button1.png' /></a>", Deffinity.systemdefaults.GetWebUrl(), r.Button1Link + "?unid=" + c.OrgarnizationGUID));
                            body = body.Replace("{{Button2}}", string.Format("<a href='{1}' > <img src='{0}/assets/button2.png' /></a>", Deffinity.systemdefaults.GetWebUrl(), r.Button2Link));


                            ToEmail.SendingMail(fromid, subject, body, c.EmailAddress, "");
                            //DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
                        }
                    }
                }

                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Mail has been sent successfully", "");
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }


        //private void Bind_Campaigning_Owner()
        //{
        //    var ownerlist = (from c in UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany()
        //                     orderby c.ContractorName
        //                     select new
        //                     {
        //                         ID = c.ID,
        //                         Name = c.ContractorName
        //                     }).ToList();
        //    ddlOwner.DataSource = ownerlist;
        //    ddlOwner.DataTextField = "Name";
        //    ddlOwner.DataValueField = "ID";
        //    ddlOwner.DataBind();
        //    ddlOwner.Items.Insert(0, new ListItem("Please select...", "0"));
        //}

    }


}