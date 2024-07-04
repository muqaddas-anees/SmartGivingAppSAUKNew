using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System.Linq;

public partial class Reports_TimeSheetSummaryBasedonEntryType : System.Web.UI.Page
{
    PortfolioDataContext portfolioDB = new PortfolioDataContext();
    DisBindings getData = new DisBindings();
    //ReportDocument rpt;
       SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {

                defaultBindings();
            }
            lblError.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    private void defaultBindings()
    {
        try
        {
            Bind_Customers();
            Bind_Teams();
            Bind_Resources();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        

    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        try
        {

            TimesheetSummary.Attributes.Add("src", string.Format("Timesheetsummarycompletefrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3}&customer={4} &type=pdf", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(),ddlCustomers.SelectedValue));
                
        }
        catch (Exception ex)
        { 
             LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind_Teams();
    }
    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Resources();
    }

    private void Bind_Teams()
    {
        try
        {
            DataTable dt = new DataTable();
            //old//DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS").Tables[0];
            if (sessionKeys.SID != 1)
            {
                 dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select ID,TeamName from team where id in (select TeamID from TeamMember where name=" + sessionKeys.UID + ")").Tables[0];
            }
            else
            {
                 dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select ID,TeamName from team").Tables[0];
            }
            //var teamsList = (from r in portfolioDB.Teams
            //                 select new { r.ID, r.TeamName }).ToList();
            ddlTeam.DataSource = dt;
            ddlTeam.DataTextField = "TeamName";
            ddlTeam.DataValueField = "ID";
            ddlTeam.DataBind();
            ddlTeam.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Bind_Customers()
    {
        try
        {
            //customer
            ddlCustomers.DataBind();
            ddlCustomers.Items.RemoveAt(0);
            ddlCustomers.Items.Insert(0, new ListItem("All", "0"));
           
            //ddlCustomers.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            //ddlCustomers.DataTextField = "PortFolio";
            //ddlCustomers.DataValueField = "ID";
            //ddlCustomers.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Bind_Resources()
    {
        try
        {

            DataTable dt = new DataTable();
            if (ddlTeam.SelectedItem.Text == "All")
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
               // dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID as ID ,ContractorName as Name from Contractors where  status ='Active' and SID not in (7,8,10) order by Name", ddlTeam.SelectedValue)).Tables[0];
               // dt.Rows.Remove(;
            }
            else if (!ddlTeam.SelectedItem.Text.Contains("SD-"))
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT distinct t.Name id, c.ContractorName ContractorName FROM TeamMember as t INNER JOIN Contractors c ON t.Name = c.ID where c.Status = 'Active' and c.SID not in (7,8) and t.TeamID = {0} order by ContractorName", ddlTeam.SelectedValue)).Tables[0];
                ddlcontractors.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            else
            {
                string SDID = ddlTeam.SelectedValue.Trim("SD-".ToCharArray()).Trim();
                dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "SELECT distinct c.ID, c.ContractorName as ContractorName FROM SDteamToUsers s INNER JOIN Contractors c ON s.UserID = c.ID where c.Status = 'Active' and c.SID not in (7,8) and s.SDteamID = @TEAMID order by ContractorName", new SqlParameter("@TEAMID", Convert.ToInt32(SDID))).Tables[0];
                ddlcontractors.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            ddlcontractors.Items.Clear();
            ddlcontractors.DataSource = dt;
            ddlcontractors.DataTextField = "ContractorName";
            ddlcontractors.DataValueField = "ID";
            ddlcontractors.DataBind();
            ddlcontractors.Items.Insert(0, new ListItem("All", "0"));
            //ddlcontractors.Items.RemoveAt(1);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            TimesheetSummary.Attributes.Add("src", string.Format("Timesheetsummarycompletefrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3} &customer={4} &type=xsl", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(),ddlCustomers.SelectedValue));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {

            TimesheetSummary.Attributes.Add("src", string.Format("Timesheetsummarycompletefrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3} &customer={4} &type=xsl1", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim(),ddlCustomers.SelectedValue));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
