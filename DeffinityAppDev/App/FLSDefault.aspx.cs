using DC.DAL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeffinityManager.DAL;
using DeffinityManager;
using DC.BAL;
using DeffinityManager.DC.BLL;

namespace DeffinityAppDev.App
{
    public partial class FLSDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_video.Visible = false;
            vimeo.Visible = false;

            if (!string.IsNullOrEmpty(QueryStringValues.Type))
            {
                var h = string.Empty;
                if (QueryStringValues.Type == "vat")
                {
                    h = "VAT";
                    btn_video.Visible = true;
                    vimeo.Visible = true;
                    vimeo.Src = "https://player.vimeo.com/video/773358104?h=bbed38ae36";
                }
                else if (QueryStringValues.Type == "access")
                {
                    h = "From Email";
                    btn_video.Visible = true;
                    vimeo.Visible = true;
                    vimeo.Src = "https://player.vimeo.com/video/820505886?h=8041765b5e";
                }
                else if (QueryStringValues.Type == "source")
                    h = "Source of Request";
                else if (QueryStringValues.Type == "typeofrequest")
                    h = "Type of Job";
                else if (QueryStringValues.Type == "servicecharge")
                    h = "Service Charge";
                else if (QueryStringValues.Type == "footer")
                {
                    h = "Email footer";
                    btn_video.Visible = true;
                    vimeo.Visible = true;
                    vimeo.Src = "https://player.vimeo.com/video/820589614?h=57d6017484";
                }
                else if (QueryStringValues.Type == "notification")
                    h = "Email notification";
                else if (QueryStringValues.Type == "policy")
                    h = "Maintenance Plans";
                else if (QueryStringValues.Type == "policynumberformat")
                    h = "Maintenance Plan Number Format";
                else if (QueryStringValues.Type == "addon")
                    h = "Maintenance Plan Optional Add on Prices";
                else if (QueryStringValues.Type == "maintenance")
                    h = "Maintenance Type";
                else if (QueryStringValues.Type == "invoiceseed")
                    h = "Invoice Starting Point";
                else if (QueryStringValues.Type == "inventory")
                    h = "Inventory Category";
                else if (QueryStringValues.Type == "paymentsettings")
                    h = "Card Payment Settings";
                else if (QueryStringValues.Type == "category")
                    h = "Equipment Brand";
                //else if (QueryStringValues.Type == "")
                //    h = "";



                lblPanelTitle.Text = h;
            }
            //if (Request.QueryString["page"] != null)
            //{
            //    if (Request.QueryString["page"].ToString().ToLower() == "inventory")
            //    {
            //        linkBack.NavigateUrl = "~/WF/CustomerAdmin/InventoryItemslist.aspx";
            //        linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Inventory";
            //    }
            //}
            //if (Request.QueryString["nav"] != null)
            //{
            //    linkBack.NavigateUrl = "~/WF/Onboarding/Default.aspx";
            //    linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Onboarding";
            //    linkBack.Visible = true;
            //}

            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }
            // Master.PageHead = Resources.DeffinityRes.AdminDropdownLists;
            if (!IsPostBack)
            {
                //BindTypedropdown();
                //if (QueryStringValues.Type != string.Empty)
                //    ddlType.SelectedValue = QueryStringValues.Type;
                //else
                //    ddlType.SelectedValue = "category";
                PanelVisibility();

                SecurityAccess1.RequestTypeID = 6; //6 for FLS 
                BindRbList();
                BindCheckBox();
            }
        }

        //public void BindTypedropdown()
        //{
        //    List<ListItem> list = new List<ListItem>();
        //    //list.Add(new ListItem("Subject", "subject"));
        //    //list.Add(new ListItem("Assigned to Department", "adlist"));
        //    list.Add(new ListItem("Service Desk Distribution List", "dlist"));
        //    //list.Add(new ListItem("Priority", "priority"));
        //    list.Add(new ListItem("Email Footer", "footer"));
        //    list.Add(new ListItem("Equipment Category", "category"));
        //    list.Add(new ListItem("Our Site", "site"));
        //    list.Add(new ListItem("Source of Request", "source"));
        //    //list.Add(new ListItem("Requesters Department", "department"));
        //    list.Add(new ListItem("Service Desk Email", "access"));
        //    list.Add(new ListItem("Email Notification", "notification"));
        //    //list.Add(new ListItem("Show/Hide Document", "doc"));
        //    list.Add(new ListItem("Policy Type", "policy"));
        //    //list.Add(new ListItem("Job Acceptance Time", "jobtime"));
        //    list.Add(new ListItem("Fixed Rate Price Distribution List", "fixedrate"));
        //    //list.Add(new ListItem("Service Provider Scheduling Algorithm", "schedule"));
        //    list.Add(new ListItem("Service Type", "servicetype"));
        //    list.Add(new ListItem("Policy Number Format", "policynumberformat"));
        //    //list.Add(new ListItem("Ticket Management Notification Group", "ticketmanager"));

        //    list.Add(new ListItem("Maintenance Type", "maintenance"));
        //    list.Add(new ListItem("Payment Settings", "paymentsettings"));
        //    list.Add(new ListItem("Optional Add on Prices", "addon"));
        //    list.Add(new ListItem("Type of Request", "typeofrequest"));
        //    list.Add(new ListItem("TAX", "vat"));
        //    list.Add(new ListItem("Service Charge", "servicecharge"));
        //    list.Add(new ListItem("Quotation template", "quotationtemplate"));

        //    //pnlServiceType
        //    ddlType.DataSource = list.OrderBy(o => o.Text).ToList();
        //    ddlType.DataTextField = "text";
        //    ddlType.DataValueField = "value";
        //    ddlType.DataBind();

        //}
        public void BindRbList()
        {
            using (DCDataContext dc = new DCDataContext())
            {
                EmailSendingType E_MailType = dc.EmailSendingTypes.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                if (E_MailType != null)
                {
                    foreach (ListItem li in rbList.Items)
                    {
                        if (li.Value == E_MailType.EmailType.ToString())
                        {
                            li.Selected = true;
                        }
                    }
                }
            }
        }
        protected void btnSavetype_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                foreach (ListItem li in rbList.Items)
                {
                    if (li.Selected == true)
                    {
                        i = int.Parse(li.Value);
                    }
                }
                using (DCDataContext dc = new DCDataContext())
                {
                    EmailSendingType E_MailType = dc.EmailSendingTypes.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                    if (E_MailType == null)
                    {
                        EmailSendingType E_MailTypeNew = new EmailSendingType();
                        E_MailTypeNew.CustomerId = sessionKeys.PortfolioID;
                        E_MailTypeNew.EmailType = i;
                        dc.EmailSendingTypes.InsertOnSubmit(E_MailTypeNew);
                        dc.SubmitChanges();
                        lblMsg.Text = "Added successfully";
                    }
                    else
                    {
                        E_MailType.EmailType = i;
                        dc.SubmitChanges();
                        lblMsg.Text = "Updated successfully";
                    }
                    BindRbList();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindCheckBox()
        {
            try
            {
                using (DCDataContext Ddc = new DC.DAL.DCDataContext())
                {
                    var x = Ddc.DefaultConfigCustomers.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                    if (x != null)
                    {
                        Check1.Checked = true;
                    }
                    else
                    {
                        Check1.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void Check1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (DCDataContext Ddc = new DC.DAL.DCDataContext())
                {
                    DefaultConfigCustomer NewRecord = new DefaultConfigCustomer();
                    NewRecord = Ddc.DefaultConfigCustomers.FirstOrDefault();
                    if (Check1.Checked == true)
                    {
                        DefaultConfigCustomer NewRecord1 = new DefaultConfigCustomer();
                        NewRecord1.UserId = sessionKeys.UID;
                        NewRecord1.CustomerId = sessionKeys.PortfolioID;
                        Ddc.DefaultConfigCustomers.InsertOnSubmit(NewRecord1);
                        Ddc.SubmitChanges();
                        //lblDefaultCustomerMsg.ForeColor = System.Drawing.Color.Green;
                        lblDefaultCustomerMsg.Text = "Default data customer updated";
                    }
                    else
                    {
                        if (NewRecord != null)
                        {
                            Ddc.DefaultConfigCustomers.DeleteOnSubmit(NewRecord);
                            Ddc.SubmitChanges();
                            // lblDefaultCustomerMsg.ForeColor = System.Drawing.Color.Green;
                            lblDefaultError.Text = "Default data customer removed";
                        }
                    }
                }
                BindCheckBox();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void BtnApplyToAllCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                DefaultConfigurationToAllCustomer D_Configuration = new DefaultConfigurationToAllCustomer();
                D_Configuration.DataBindToTables();
                Response.Redirect(Page.Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        #region Visibility

        public void PanelVisibility()
        {
            if (QueryStringValues.Type == string.Empty)
            {
                pnlCategory.Visible = true;
            }
            else
            {
                pnlAdmin.Visible = QueryStringValues.Type == "subject" ? true : false;
                pnlAssignDep.Visible = QueryStringValues.Type == "adlist" ? true : false;
                pnlPrioity.Visible = QueryStringValues.Type == "priority" ? true : false;
                pnlSDesk.Visible = QueryStringValues.Type == "dlist" ? true : false;
                pnlEmailFooter.Visible = QueryStringValues.Type == "footer" ? true : false;
                pnlCategory.Visible = QueryStringValues.Type == "category" ? true : false;
                pnlOurSite.Visible = QueryStringValues.Type == "site" ? true : false;
                pnlSourceofRequest.Visible = QueryStringValues.Type == "source" ? true : false;
                pnlDepartment.Visible = QueryStringValues.Type == "department" ? true : false;
                pnlAccessEmail.Visible = QueryStringValues.Type == "access" ? true : false;
                pnlNotification.Visible = QueryStringValues.Type == "notification" ? true : false;
                pnlDocuments.Visible = QueryStringValues.Type == "doc" ? true : false;
                pnlPolicy.Visible = QueryStringValues.Type == "policy" ? true : false;
                //pnlPolicy.Visible = QueryStringValues.Type == "policy" ? true : false;
                pnlJobtime.Visible = QueryStringValues.Type == "jobtime" ? true : false;
                pnlFixedRate.Visible = QueryStringValues.Type == "fixedrate" ? true : false;
                pnlSchedule.Visible = QueryStringValues.Type == "schedule" ? true : false;
                pnlServiceType.Visible = QueryStringValues.Type == "servicetype" ? true : false;
                pnlPolicyFormat.Visible = QueryStringValues.Type == "policynumberformat" ? true : false;
                pnlTicketManager.Visible = QueryStringValues.Type == "ticketmanager" ? true : false;
                pnlAddon.Visible = QueryStringValues.Type == "addon" ? true : false;
                pnlMaintenanceType.Visible = QueryStringValues.Type == "maintenance" ? true : false;
                pnlPortfolioPaymentsettings.Visible = QueryStringValues.Type == "paymentsettings" ? true : false;
                pnlTypeofRequest.Visible = QueryStringValues.Type == "typeofrequest" ? true : false;
                pnlVat.Visible = QueryStringValues.Type == "vat" ? true : false;
                pnlServiceCharge.Visible = QueryStringValues.Type == "servicecharge" ? true : false;
                pnlInvoiceSeed.Visible = QueryStringValues.Type == "invoiceseed" ? true : false;

            }
        }


        #endregion
    }
}