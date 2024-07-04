using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
public partial class Reports_TimesheetMonthEndExceptionfrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Label1.Visible = false;
                defaultBindings();
                BindChkBoxType();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
   
    private void BindChkBoxType()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "TimeSheet_ListOfEntryTypes").Tables[0];
        chkEntryType.DataSource = dt;
        chkEntryType.DataValueField = "ID";
        chkEntryType.DataTextField = "EntryType";
        chkEntryType.DataBind();
    }
    private string  EntryTyeps()
    {
        string val = "";
        foreach (ListItem type in chkEntryType.Items)
        {
            if (type.Selected == true)
            {
                val += type.Value + ",";
            }
        }
        return val;
    }
    //protected void btnExportExcel_Click(object sender, EventArgs e)
    //{
       
    //}
    protected void btn_Submitt_Click(object sender, ImageClickEventArgs e)
    {
        string val = EntryTyeps();
        if (val != "")
        {
            Label1.Visible = false;
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetMonthEndExceptionfrm.aspx?etype={0}&id={1}&sdate={2}&edate={3}&team={4}&ord={5}&type=pdf", val, ddlResource.SelectedValue, Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text).ToShortDateString()
                , Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_EndDate.Text).ToShortDateString(), ddlTeam.SelectedValue, ddlSortOption.SelectedValue));
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select entry type";
        }
    }

    private void defaultBindings()
    {
        Bind_Customers();
        Bind_Teams();
        Bind_Resources();


    }
    //protected void btn_Submitt_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {

    //        TimesheetSummary.Attributes.Add("src", string.Format("Timesheetsummarycompletefrm.aspx?teamid={0}&resourceid={1}&sdate={2}&edate={3}", ddlTeam.SelectedValue, ddlcontractors.SelectedValue, txt_StartDate.Text.Trim(), txt_EndDate.Text.Trim()));

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
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
        // DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomers.SelectedValue))).Tables[0];
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
        //DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS", new SqlParameter("@PORTFOLIOID", int.Parse(ddlCustomers.SelectedValue))).Tables[0];
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
            //dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("select ID as ID ,ContractorName as Name from Contractors where  status ='Active' and SID not in (7,8,10) order by Name", ddlTeam.SelectedValue)).Tables[0];
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
        ddlResource.Items.Clear();
        ddlResource.DataSource = dt;
        ddlResource.DataTextField = "ContractorName";
        ddlResource.DataValueField = "ID";
        ddlResource.DataBind();
        ddlResource.Items.Insert(0, new ListItem("All", "0"));
       // ddlResource.Items.RemoveAt(1);
    }
    protected void lnkButtonExcel_Click(object sender, EventArgs e)
    {
        string val = EntryTyeps();
        if (val != "")
        {
            Label1.Visible = false;
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetMonthEndExceptionfrm.aspx?etype={0}&id={1}&sdate={2}&edate={3}&team={4}&ord={5}&type=xsl1", val, ddlResource.SelectedValue, Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text).ToShortDateString()
                , Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_EndDate.Text).ToShortDateString(), ddlTeam.SelectedValue, ddlSortOption.SelectedValue));
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select entry type";
        }
    }
    protected void lnkButtonExcel1_Click(object sender, EventArgs e)
    {
        string val = EntryTyeps();
        if (val != "")
        {
            Label1.Visible = false;
            TimesheetSummary.Attributes.Add("src", string.Format("TimesheetMonthEndExceptionfrm.aspx?etype={0}&id={1}&sdate={2}&edate={3}&team={4}&ord={5}&type=xsl", val, ddlResource.SelectedValue, Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text).ToShortDateString()
                , Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_EndDate.Text).ToShortDateString(), ddlTeam.SelectedValue, ddlSortOption.SelectedValue));
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select entry type";
        }
    }
}
