using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Admin_ResourcePlanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Master.PageHead = "Resource Planner";
        if (!IsPostBack)
        {
            txt_sFromdate.Text = DateTime.Now.AddDays(-15).ToShortDateString();
            txt_sTodate.Text = DateTime.Now.AddDays(15).ToShortDateString();

            RadioButtonList_Bindings(lstShift);
            RadioButtonList_Bindings(listShift_insert);     
         
            //ddlTeams.DataSourceID =

        }

        //add css dynamically
        //HtmlLink css = new HtmlLink();
        //css.Href = ResolveClientUrl("~/stylcss/ext-all.css");
        //css.Attributes["rel"] = "stylesheet";
        //css.Attributes["type"] = "text/css";
        ////css.Attributes["media"] = "all";
        //Page.Header.Controls.Add(css);
        
    }
    private void RadioButtonList_Bindings(RadioButtonList lstShift)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "Select ID, (Shift + ' '+StartTime+' - '+EndTime) AS Shift,Colour FROM Shift Where Deleted=0 AND PortfolioID=@PortfolioID", new SqlParameter("@PortfolioID", sessionKeys.PortfolioID)).Tables[0];
        lstShift.DataSource = dt;
        lstShift.DataTextField = "Shift";
        lstShift.DataValueField = "ID";
        lstShift.DataBind();
        //set
        if (lstShift.Items.Count > 0)
        {
            lstShift.SelectedIndex = 0;
        }

        setShiftColor(dt, lstShift);
        
    }
    private void setShiftColor(DataTable shiftDetails, RadioButtonList lstShift)
    {
        foreach (DataRow row in shiftDetails.Rows)
        {
            for (int i = 0; i < lstShift.Items.Count; i++)
            {
                ListItem listItem = lstShift.Items[i];
                string StrColor = "";
                if (listItem.Value.ToString() == row["ID"].ToString())
                {
                    string alteredColor = string.Empty;
                    switch (row["Colour"].ToString())
                    {
                        case "Olive":
                            alteredColor = "#667C26";
                            break;
                        case "Blue":
                            alteredColor = "#6698FF";
                            break;
                        case "Green":
                            alteredColor = "#4CC417";
                            break;
                        case "Teal":
                            alteredColor = "#4C7D7E";
                            break;
                        case "Maroon":
                            alteredColor = "#E3319D";
                            break;
                        case "Red":
                            alteredColor = "#E55451";
                            break;
                        case "Gray":
                            alteredColor = "#4C4646";
                            break;
                        case "Lime":
                            alteredColor = "#41A317";
                            break;
                        case "Aqua":
                            alteredColor = "#B6FFFF;color:black";
                            break;
                        case "Yellow":
                            alteredColor = "#FFF380;color:black";
                            break;
                        case "Purple":
                            alteredColor = "#8E35EF";
                            break;
                        default:
                            alteredColor = row["Colour"].ToString();
                            break;
                    }
                    //<span style="width:10px;background-color:Red;">&nbsp;</span>
                    if (!string.IsNullOrEmpty(alteredColor))
                        listItem.Text = "&nbsp;<span style=padding:0px;background-color:" + alteredColor + ";>&nbsp;&nbsp;&nbsp;</span>&nbsp;" + listItem.Text.Trim();
                    //listItem.Attributes.Add("style", "color:white;background-color:" + alteredColor);
                    else
                        listItem.Text = "&nbsp;<span style=padding:0px;background-color:" + row["Colour"].ToString() + ";>&nbsp;&nbsp;&nbsp;</span>&nbsp;" + listItem.Text.Trim();
                    //listItem.Attributes.Add("style", "font-color:white;background-color:" + row["Colour"].ToString());
                }
            }
        }
    }
    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            if (!ddlTeams.SelectedItem.Text.Contains("SD-"))
            {

                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, string.Format("SELECT TeamMember.ID,(select ContractorName from Contractors where ID = TeamMember.Name) as Name FROM TeamMember INNER JOIN Team ON TeamMember.TeamID = Team.ID where Team.ID = {0} and PortfolioID = {1}", ddlTeams.SelectedValue.Trim(), sessionKeys.PortfolioID)).Tables[0];

            }
            else
            {
                string SDID = ddlTeams.SelectedValue.Trim("SD-".ToCharArray()).Trim();
                dt = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETSD_TEAMMEMBERS", new SqlParameter[] { new SqlParameter("@PORTFOLIOID", sessionKeys.PortfolioID), new SqlParameter("@TEAMID", SDID) }).Tables[0];

            }
            chkTeamMember.Items.Clear();
            chkTeamMember.DataSource = dt;
            chkTeamMember.DataTextField = "Name";
            chkTeamMember.DataValueField = "ID";
            chkTeamMember.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    
    }
    protected void btnInsertAll_Click(object sender, EventArgs e)
    {
       
        int i = chkTeamMember.Items.Count;
        foreach (ListItem item in chkTeamMember.Items)
        {
            if (item.Selected)
            {
                ResourcePlanner_Service rs = new ResourcePlanner_Service();
                string ID = "0";
                string s = rs.Update_TeamMemberShift(item.Value, listShift_insert.SelectedItem.Value, txt_insert_fromdate.Text.Trim(), ddl_insert_site.SelectedValue, GetTeamType().ToString(), txt_insert_Notes.Text.Trim(), txt_insert_todate.Text.Trim(), ID,"2");
            }
            else
            { 
               
            }
        }
        ClearDataTab2_Fields();
    }

    private void ClearDataTab2_Fields()
    {
        txt_insert_fromdate.Text = string.Empty;
        txt_insert_Notes.Text = string.Empty;
        txt_insert_todate.Text = string.Empty;
        if (ddl_insert_site.Items.Count > 0)
        {
            ddl_insert_site.SelectedIndex = 0;
        }
        if (listShift_insert.Items.Count > 0)
        {
            listShift_insert.SelectedIndex = 0;
        }
        if (ddlTeams.Items.Count > 0)
        {
            ddlTeams.SelectedIndex = 0;
        }
        if (chkTeamMember.Items.Count > 0)
        {
            chkTeamMember.Items.Clear();
        }

    
    }

    private int GetTeamType()
    {
        int teamtype = 0;
        if (ddlTeams.SelectedItem.Text.Contains("SD-"))
        { teamtype = 2; }
        else
        { teamtype = 1; }
        return teamtype;

    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    
}
