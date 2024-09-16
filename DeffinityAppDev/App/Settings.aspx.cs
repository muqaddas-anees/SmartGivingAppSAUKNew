using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnOpen_Command(object sender, CommandEventArgs e)
        {
            var b = "&back=~" + Request.Url.PathAndQuery;
            if (e.CommandName == "PortalBranding")
            {
                Response.Redirect("~/WF/CustomerAdmin/PortalBranding.aspx?type=PortalBranding" + b, false);
            }
            else if (e.CommandName == "Tax")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=vat" + b, false);
            }
            else if (e.CommandName == "FromMail")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=access" + b + "&pnl=JobTicketConfiguration", false);
            }
            else if (e.CommandName == "SourceofRequest")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=source" + b + "&pnl=JobTicketConfiguration", false);
            }
            else if (e.CommandName == "giftaid")
            {
                Response.Redirect("~/App/GiftAid.aspx?" + b + "&pnl=JobTicketConfiguration", false);
            }
            else if (e.CommandName == "TypeofJob")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=typeofrequest" + b + "&pnl=JobTicketConfiguration", false);
            }
            else if (e.CommandName == "ServiceCharge")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=servicecharge" + b + "&pnl=JobTicketConfiguration", false);
            }
            else if (e.CommandName == "FromMail")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=access" + b + "&pnl=EmailConfiguration", false);
            }
            else if (e.CommandName == "EmailFooter")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=footer" + b + "&pnl=EmailConfiguration", false);
            }
            else if (e.CommandName == "EmailNotification")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=notification" + b + "&pnl=EmailConfiguration", false);
            }
            else if (e.CommandName == "wordpress")
            {
                Response.Redirect("~/App/wordpress.aspx", false);
            }
            else if (e.CommandName == "mailchimp")
            {
                Response.Redirect("~/App/mailchimp.aspx?tab=fls&type=notification" + b + "&pnl=EmailConfiguration", false);
            }
            else if (e.CommandName == "MaintenancePlans")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=policy" + b + "&pnl=MaintenancePlans", false);
            }
            else if (e.CommandName == "MaintenancePlanNumberFormat")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=policynumberformat" + b + "&pnl=MaintenancePlans", false);
            }
            else if (e.CommandName == "OptionalAdd")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=addon" + b + "&pnl=MaintenancePlans", false);
            }
            else if (e.CommandName == "MaintenanceType")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=maintenance" + b + "&pnl=MaintenancePlans", false);
            }
            else if (e.CommandName == "InvoiceStartingPoint")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=invoiceseed" + b + "&pnl=Invoicing&Quotations", false);
            }
            else if (e.CommandName == "QuotationTemplates")
            {
                Response.Redirect("~/WF/DC/QuoteTemplate.aspx?tab=fls&&type=quotationtemplate" + b + "&pnl=Invoicing&Quotations", false);
            }
            else if (e.CommandName == "InventoryCategory")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=category&page=inventory" + b + "&pnl=Inventory", false);
            }
            else if (e.CommandName == "StorageLocations")
            {
                Response.Redirect("~/WF/WM/WMdetails.aspx?type=StorageLocations" + b + "&pnl=Inventory", false);
            }
            else if (e.CommandName == "SupplierManagement")
            {
                Response.Redirect("~/WF/Vendors/RFIVendors.aspx?type=SupplierManagement" + b + "&pnl=Inventory", false);
            }
            else if (e.CommandName == "CardPaymentSettings")
            {
                Response.Redirect("~/App/FLSDefault.aspx?tab=fls&type=paymentsettings" + b + "&pnl=Integration", false);
            }
            else if (e.CommandName == "Markup")
            {
                Response.Redirect("~/WF/DC/MarkupDefault.aspx?type=markup" + b + "&pnl=Quotations", false);
            }
            else if (e.CommandName == "Users")
            {
                Response.Redirect("~/WF/Admin/UserManagement.aspx?Type=SmartPros" + b + "&pnl=ManageUsers", false);
            }
            else if (e.CommandName == "SuppliersandCatalogues")
            {
                Response.Redirect("~/WF/Vendors/RFIVendors.aspx?type=SuppliersandCatalogues" + b + "&pnl=SuppliersandCatalogues", false);
            }
            else if (e.CommandName == "FormConfig")
            {
                Response.Redirect("~/WF/Health/HC/FormList.aspx?type=FormConfig" + b + "&pnl=FormConfig", false);
            }
            else if (e.CommandName == "Labor")
            {
                Response.Redirect("~/WF/DC/LabourRateSettingPage.aspx?type=Labor" + b + "&pnl=Labor", false);
            }
            else if (e.CommandName == "Timesheet")
            {
                Response.Redirect("~/WF/DC/Timesheets/TimesheetSettings.aspx?type=Timesheet" + b + "&pnl=Timesheet", false);
            }
            else if (e.CommandName == "Expenses")
            {
                Response.Redirect("~/WF/DC/Expenses/ExpensesSettings.aspx?type=Expenses" + b + "&pnl=Expenses", false);
            }
            else if (e.CommandName == "MaintenanceAdmin")
            {
                Response.Redirect("~/WF/CustomerAdmin/Maintenance/ChecklistSettings.aspx?type=MaintenanceAdmin" + b + "&pnl=MaintenanceAdmin", false);
            }
            else if (e.CommandName == "Internal")
            {
                Response.Redirect("~/WF/DC/InternalCostSettings.aspx?type=Internal" + b + "&pnl=Invoicing&Quotations", false);
            }
            else if (e.CommandName == "PageBuilder")
            {
                Response.Redirect("~/WF/PageBuilder.aspx?type=PageBuilder" + b + "&pnl=PageBuilder", false);
            }
            else if (e.CommandName == "Members")
            {
                Response.Redirect("~/App/users.aspx", false);
            }
            else if (e.CommandName == "tithing")
            {
                Response.Redirect("~/App/TithingCategorySettings.aspx?type=active" + b + "&pnl=tithing", false);
            }
            else if (e.CommandName == "thankyoumail")
            {
                Response.Redirect("~/App/ThankYouMailSettings.aspx?type=active" + b + "&pnl=tithing", false);
            }
            else if (e.CommandName == "custom")
            {
                Response.Redirect("~/WF/DC/FLSCustomFormDesigner.aspx?type=custom" + b + "&pnl=custom", false);
            }
            else if (e.CommandName == "company")
            {
                Response.Redirect("~/App/ManageCompany.aspx?type=company" + b + "&pnl=company", false);
            }
            else if (e.CommandName == "platform")
            {
                Response.Redirect("~/App/PlatformSupportReport.aspx?type=platform" + b + "&pnl=platform", false);
            }
            else if (e.CommandName == "product")
            {
                Response.Redirect("~/App/Products/CategorySetting.aspx", false);
            }
            //platform
            //thankyoumail
            //tithing
            //PageBuilder
            //Internal

        }
    }
}