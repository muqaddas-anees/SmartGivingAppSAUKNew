using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Health.DAL;
using Health.Entity;
using Health.StateManager;
using System.Data.SqlTypes;
using HealthCheckMgt.DAL;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public partial class MailTemplates_HealthCheckDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        sessionKeys.pageid = 1;
        HealthCheckListState.ClearHealthCheckItemsCache();
        HealthCheckListItemsState.ClearHealthCheckListItemsCache();
        sessionKeys.PortfolioID = Convert.ToInt32(Request.QueryString["PortfolioID"]);
        if (Request.QueryString["healthCheckID"] != null)
        {
            int healthCheckID = Convert.ToInt32(Request.QueryString["healthCheckID"]);
            HealthCheckList healthCheckList = HealthCheckListHelper.LoadHealthcheckByID(healthCheckID);
            lblTeam.Text = healthCheckList.AssignedTeamName;
            lblIssueDetails.Text = healthCheckList.Issue;
            lblHealthCheckTitle.Text = healthCheckList.HealthCheckTitle;
            lblDate.Text = DateTime.Now.Date.ToString(Deffinity.systemdefaults.GetDateformat()) + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }
        using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
        {
            var record = Hdc.DN_Customerlogos.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
            string InstanceLogoPath = ConfigurationManager.AppSettings["Weburl"].ToString() + "/media/deffinity_logo.gif";
            if (record != null)
            {
                if (record.EmailTypeId == 2)
                {
                    string LogoPath = Server.MapPath("~/WF/CustomerAdmin/images/Portfolio_" + sessionKeys.PortfolioID + ".gif");
                    if (File.Exists(LogoPath))
                    {
                        imgDeffinityLogo.Src = ConfigurationManager.AppSettings["Weburl"].ToString() + "/WF/CustomerAdmin/images/Portfolio_" + sessionKeys.PortfolioID + ".gif";
                    }
                    else
                    {
                        imgDeffinityLogo.Src = InstanceLogoPath;
                    }
                }
                else
                {
                    imgDeffinityLogo.Src = InstanceLogoPath;
                }
            }
            else
            {
                imgDeffinityLogo.Src = InstanceLogoPath;
            }
        }
        imgDeffinityLogo.Alt = "Logo";
    }

    protected string GetCheckedValue(object isChecked)
    {
       
        SqlBoolean chk = (SqlBoolean)isChecked;
        if (chk)
            return "Yes";
        else if (!chk)
            return "No";
        else
            return "Not Specified";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        GridColumns();
        if (Request.QueryString["Issue"] != null)
            pnlIssueText.Visible = true;
        else
            pnlIssueText.Visible = false;
    }
    public void GridColumns()
    {
        gridHealthCheckListItems.Columns[1].Visible = VisibilityInGridColumns("Status");
        gridHealthCheckListItems.Columns[3].Visible = VisibilityInGridColumns("Yes No value");
        gridHealthCheckListItems.Columns[4].Visible = VisibilityInGridColumns("RAG");
        gridHealthCheckListItems.Columns[5].Visible = VisibilityInGridColumns("Issues");
        gridHealthCheckListItems.Columns[6].Visible = VisibilityInGridColumns("Notes");

    }
    public bool VisibilityInGridColumns(string F_Name)
    {
        bool visible = true;
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var x = (from a in Hdc.HealthCheck_Configurators
                         where a.FieldId == int.Parse(Totalfields().Where(b => b.Text == F_Name).Select(b => b.Value).FirstOrDefault()) && a.CustomerId == sessionKeys.PortfolioID
                         select a).FirstOrDefault();
                visible = x.visibility.HasValue ? x.visibility.Value : true;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return visible;
    }
    public List<ListItem> Totalfields()
    {
        List<ListItem> li = new List<ListItem>();
        li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        li.Add(new System.Web.UI.WebControls.ListItem("Notes", "1"));
        li.Add(new System.Web.UI.WebControls.ListItem("Yes No value", "2"));
        li.Add(new System.Web.UI.WebControls.ListItem("RAG", "3"));
        li.Add(new System.Web.UI.WebControls.ListItem("Issues", "4"));
        li.Add(new System.Web.UI.WebControls.ListItem("Status", "5"));
        li.Add(new System.Web.UI.WebControls.ListItem("Email Icon Button", "6"));
        li.Add(new System.Web.UI.WebControls.ListItem("Save and email Button", "7"));
        li.Add(new System.Web.UI.WebControls.ListItem("Due Date", "8"));
        return li;
    }
    protected string getStatus(string status)
    {
        string alteredStatus = string.Empty;
        if (status.Length == 1)
            alteredStatus = "No Status";
        else
            alteredStatus = status;
        return alteredStatus;
    }

    protected string convertDate(object date)
    {
        string convertedDate = string.Empty;
        if (date != null)
        {
            convertedDate = date.ToString();
            if (convertedDate.Length >= 10)
            {
                convertedDate = convertedDate.Remove(11);
            }
        }
        return convertedDate;
    }
    protected void gridHealthCheckListItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "Red")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (e.Row.Cells[4].Text == "Amber")
            {
                e.Row.ForeColor = System.Drawing.Color.Orange;
            }
            else if (e.Row.Cells[4].Text == "Green")
            {
                e.Row.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}
