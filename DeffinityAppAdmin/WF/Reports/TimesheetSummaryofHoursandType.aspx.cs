using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Reports_TimesheetSummaryofHoursandType : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            defaultBindings();
            DateTime startdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime enddate = startdate.AddMonths(1).AddDays(-1);
            txt_StartDate.Text = startdate.ToString("d");
            txt_EndDate.Text = enddate.ToString("d");
        }
        lblError.Text = "";
    }

    private void defaultBindings()
    {
        Bind_Customers();
        Bind_Teams();
        Bind_Resources();

    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        try
        {
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetSummaryofHoursfrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3}&customer={4}&order={5}&userid={6} &type=pdf", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(),ddlCustomers.SelectedValue,ddlSort.SelectedValue,sessionKeys.UID));



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Teams();



    }
    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Resources();



    }

    private void Bind_Teams()
    {
        DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomers.SelectedValue))).Tables[0];
        ddlTeam.DataSource = dt;
        ddlTeam.DataTextField = "TeamName";
        ddlTeam.DataValueField = "SDID";
        ddlTeam.DataBind();
        ddlTeam.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Bind_Customers()
    {
        //customer
        ddlCustomers.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomers.DataTextField = "PortFolio";
        ddlCustomers.DataValueField = "ID";
        ddlCustomers.DataBind();
    }
    private void Bind_Resources()
    {

        DataTable dt = new DataTable();
        if (ddlTeam.SelectedItem.Text == "All")
        {
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID as ID ,ContractorName as Name from Contractors where  status ='Active' and SID not in (7,8,10) order by Name", ddlTeam.SelectedValue)).Tables[0];
        }
        else if (!ddlTeam.SelectedItem.Text.Contains("SD-"))
        {
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT distinct t.Name id, c.ContractorName Name FROM TeamMember as t INNER JOIN Contractors c ON t.Name = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and t.TeamID = {0} order by ContractorName", ddlTeam.SelectedValue)).Tables[0];
        }
        else
        {
            string SDID = ddlTeam.SelectedValue.Trim("SD-".ToCharArray()).Trim();
            dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "SELECT distinct c.ID, c.ContractorName as Name FROM SDteamToUsers s INNER JOIN Contractors c ON s.UserID = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and s.SDteamID = @TEAMID order by ContractorName", new SqlParameter("@TEAMID", Convert.ToInt32(SDID))).Tables[0];
        }
        ddlcontractors.Items.Clear();
        ddlcontractors.DataSource = dt;
        ddlcontractors.DataTextField = "Name";
        ddlcontractors.DataValueField = "ID";
        ddlcontractors.DataBind();
        ddlcontractors.Items.Insert(0, new ListItem("All", "0"));
    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetSummaryofHoursfrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3}&customer={4}&order={5} &userid={6} &type=xsl", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(), ddlCustomers.SelectedValue, ddlSort.SelectedValue,sessionKeys.UID));



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetSummaryofHoursfrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3} &customer={4}&order={5} &userid={6} &type=xsl1", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(), ddlCustomers.SelectedValue, ddlSort.SelectedValue,sessionKeys.UID));



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
