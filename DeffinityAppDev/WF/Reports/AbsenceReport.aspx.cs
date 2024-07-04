using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
public partial class Reports_AbsenceReportfrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                defaultBindings();
                BindAbsenceType();
                TimesheetSummary.Attributes.Add("src", string.Format("AbsenceReportfrm.aspx?id={0}&sdate={1}&edate={2}&atype={3}&type=pdf&ord={4} &team={5}", ddlcontractors.SelectedValue.ToString(),
          Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
          , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), ddlAbsenseType.SelectedValue.ToString(), ddlSortOption.SelectedValue.ToString(),ddlTeam.SelectedValue.ToString()));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void defaultBindings()
    {
        // getData.DdlBindSelect(ddlcontractors, "DN_DisplayResourcebyName", "ID", "ContractorName", true, true);

        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_DisplayResourcebyName").Tables[0];
        //ddlResource.DataSource = dt;
        //ddlResource.DataValueField = "ID";
        //ddlResource.DataTextField = "ContractorName";
        //ddlResource.DataBind();
        
        Bind_Customers();
        Bind_Teams();
        Bind_Resources();


    }
    private void BindAbsenceType()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select  ID, Type  from  VT.AbsenseType  order by Type").Tables[0];
        ddlAbsenseType.DataSource = dt;
        ddlAbsenseType.DataValueField = "ID";
        ddlAbsenseType.DataTextField = "Type";
        ddlAbsenseType.DataBind();
        ddlAbsenseType.Items.Insert(0, new ListItem("ALL", "0"));
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        TimesheetSummary.Attributes.Add("src", string.Format("AbsenceReportfrm.aspx?id={0}&sdate={1}&edate={2}&atype={3}&type=xsl1&ord={4} &team={5}", ddlcontractors.SelectedValue,
          Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
          , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), ddlAbsenseType.SelectedValue.ToString(), ddlSortOption.SelectedValue.ToString(), ddlTeam.SelectedValue.ToString()));
    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        TimesheetSummary.Attributes.Add("src", string.Format("AbsenceReportfrm.aspx?id={0}&sdate={1}&edate={2}&atype={3}&type=pdf&ord={4} &team={5}", ddlcontractors.SelectedValue,
           Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
           , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), ddlAbsenseType.SelectedValue.ToString(), ddlSortOption.SelectedValue, ddlTeam.SelectedValue.ToString()));
    }
    protected void btnExportExcel0_Click(object sender, EventArgs e)
    {
        TimesheetSummary.Attributes.Add("src", string.Format("AbsenceReportfrm.aspx?id={0}&sdate={1}&edate={2}&atype={3}&type=xsl&ord={4} &team={5}", ddlcontractors.SelectedValue,
          Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text).ToShortDateString()
          , Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text).ToShortDateString(), ddlAbsenseType.SelectedValue.ToString(), ddlSortOption.SelectedValue.ToString(), ddlTeam.SelectedValue.ToString()));
    }

    #region newlyaddedCode --Giri
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Bind_Teams();



    }
    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Resources();



    }

    private void Bind_Teams()
    {
        //DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomers.SelectedValue))).Tables[0];
        DataTable dt = new DataTable();
        // DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomers.SelectedValue))).Tables[0];
        if (sessionKeys.SID != 1)
        {
            dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select ID,TeamName from team where id in (select TeamID from TeamMember where name=" + sessionKeys.UID + ")").Tables[0];
        }
        else
        {
            dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "select ID,TeamName from team").Tables[0];
        }
        ddlTeam.DataSource = dt;
        ddlTeam.DataTextField = "TeamName";
        ddlTeam.DataValueField = "ID";
        ddlTeam.DataBind();
        ddlTeam.Items.Insert(0, new ListItem("All", "0"));
    }
    private void Bind_Customers()
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
    private void Bind_Resources()
    {

        DataTable dt = new DataTable();
        if (ddlTeam.SelectedItem.Text == "All")
        {
           // dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID as ID ,ContractorName as Name from Contractors where  status ='Active' and SID not in (7,8,10) order by Name", ddlTeam.SelectedValue)).Tables[0];
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        }
        else if (!ddlTeam.SelectedItem.Text.Contains("SD-"))
        {
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT distinct t.Name id, c.ContractorName ContractorName FROM TeamMember as t INNER JOIN Contractors c ON t.Name = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and t.TeamID = {0} order by ContractorName", ddlTeam.SelectedValue)).Tables[0];
        }
        else
        {
            string SDID = ddlTeam.SelectedValue.Trim("SD-".ToCharArray()).Trim();
            dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.Text, "SELECT distinct c.ID, c.ContractorName as ContractorName FROM SDteamToUsers s INNER JOIN Contractors c ON s.UserID = c.ID where c.Status = 'Active' and c.SID not in (7,8,10) and s.SDteamID = @TEAMID order by ContractorName", new SqlParameter("@TEAMID", Convert.ToInt32(SDID))).Tables[0];
        }
        ddlcontractors.Items.Clear();
        ddlcontractors.DataSource = dt;
        ddlcontractors.DataTextField = "ContractorName";
        ddlcontractors.DataValueField = "ID";
        ddlcontractors.DataBind();
        ddlcontractors.Items.Insert(0, new ListItem("All", "0"));
        //ddlcontractors.Items.RemoveAt(1);
    }
    #endregion
}
