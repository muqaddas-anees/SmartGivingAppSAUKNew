using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class ReportNavigationCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("Open Claims", "openclaims"));
                list.Add(new ListItem("Claim Pay-outs", "claimpay-outs"));
                list.Add(new ListItem("Completed Sales", "completedsales"));
                list.Add(new ListItem("Cancellations", "cancellations"));
                list.Add(new ListItem("Overall Sales Revenue (New Customers and Renewals)", "overallsalesrevenue"));
                list.Add(new ListItem("Sales Person Report", "salespersonreport"));
                list.Add(new ListItem("Product Type", "access"));
                list.Add(new ListItem("Policy Type", "producttype"));
                list.Add(new ListItem("Commission Reporting", "commissionreporting"));
                list.Add(new ListItem("Sales Person Leaderboard", "salespersonleaderboard"));
                list.Add(new ListItem("Average Cost per Ticket", "averagecostperticket"));
                list.Add(new ListItem("Total Claims by Month", "totalclaimsbymonth"));
                list.Add(new ListItem("Frequency of Covered Item Type by Category", "frequencyofcovereditemtypebycategory"));
                list.Add(new ListItem("Feedback from Surveys", "feedbackfromsurveys"));
                list.Add(new ListItem("Service Provider Average Review Score", "serviceprovideraveragereviewscore"));
                
                ddlReports.DataSource = list.ToList();
                ddlReports.DataTextField = "text";
                ddlReports.DataValueField = "value";
                ddlReports.DataBind();

                ddlReports.SelectedValue = "completedsales";
            }
        }

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReports.SelectedValue == "completedsales")
            {
                Response.Redirect("~/WF/DC/CompletedSalesJournal.aspx");
            }
        }
    }
}