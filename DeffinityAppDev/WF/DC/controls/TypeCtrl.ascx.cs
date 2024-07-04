using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class TypeCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTypedropdown();
                if (QueryStringValues.Type != string.Empty)
                    ddlType.SelectedValue = QueryStringValues.Type;
                else
                    ddlType.SelectedValue = "category";
            }
        }
        public void BindTypedropdown()
        {
            List<ListItem> list = new List<ListItem>();
            //list.Add(new ListItem("Subject", "subject"));
            //list.Add(new ListItem("Assigned to Department", "adlist"));
            list.Add(new ListItem("Service Desk Distribution List", "dlist"));
            //list.Add(new ListItem("Priority", "priority"));
            list.Add(new ListItem("Email Footer", "footer"));
            list.Add(new ListItem("Equipment Category", "category"));
            list.Add(new ListItem("Our Site", "site"));
            list.Add(new ListItem("Source of Request", "source"));
            //list.Add(new ListItem("Requesters Department", "department"));
            list.Add(new ListItem("Service Desk Email", "access"));
            list.Add(new ListItem("Email Notification", "notification"));
            //list.Add(new ListItem("Show/Hide Document", "doc"));
            list.Add(new ListItem("Maintenance Plan", "policy"));
            //list.Add(new ListItem("Job Acceptance Time", "jobtime"));
            list.Add(new ListItem("Fixed Rate Price Distribution List", "fixedrate"));
            //list.Add(new ListItem("Service Provider Scheduling Algorithm", "schedule"));
            list.Add(new ListItem("Service Type", "servicetype"));
            list.Add(new ListItem("Maintenance Plan Number Format", "policynumberformat"));
            //list.Add(new ListItem("Ticket Management Notification Group", "ticketmanager"));

            list.Add(new ListItem("Maintenance Type", "maintenance"));
            list.Add(new ListItem("Payment Settings", "paymentsettings"));
            list.Add(new ListItem("Optional Add on Prices", "addon"));
            list.Add(new ListItem("Type of Request", "typeofrequest"));
            list.Add(new ListItem("TAX", "vat"));
            list.Add(new ListItem("Service Charge", "servicecharge"));
            list.Add(new ListItem("Quotation Template", "quotationtemplate"));
            list.Add(new ListItem("Invoice Config", "invoiceseed"));

            //pnlServiceType
            ddlType.DataSource = list.OrderBy(o => o.Text).ToList();
            ddlType.DataTextField = "text";
            ddlType.DataValueField = "value";
            ddlType.DataBind();

        }
    }
}