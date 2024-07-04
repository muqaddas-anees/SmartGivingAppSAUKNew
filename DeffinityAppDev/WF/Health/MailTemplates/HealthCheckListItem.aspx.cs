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

public partial class MailTemplates_HealthCheckListItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HealthCheckListState.ClearHealthCheckItemsCache();
        HealthCheckListItemsState.ClearHealthCheckListItemsCache();
        sessionKeys.PortfolioID = Convert.ToInt32(Request.QueryString["PortfolioID"]);
        if (Request.QueryString["healthCheckID"] != null)
        {
            int healthCheckID = Convert.ToInt32(Request.QueryString["healthCheckID"]);

            sessionKeys.pageid = 1;

            HealthCheckList healthCheckList = HealthCheckListHelper.LoadHealthcheckByID(healthCheckID);
            if (healthCheckList != null)
            {
                lblTeam.Text = healthCheckList.AssignedTeamName;
                lblIssueDetails.Text = healthCheckList.Issue;
                lblHealthCheckTitle.Text = healthCheckList.HealthCheckTitle;
            }
            lblDate.Text = DateTime.Now.Date.ToString(Deffinity.systemdefaults.GetDateformat()) + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            BindGrid(healthCheckID);

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
    private void BindGrid(int healthCheckID)
    {
        int healthCheckListItemId = 0;
        if (Request.QueryString["ListItemID"] != null)
        {
            healthCheckListItemId = Convert.ToInt32(Request.QueryString["ListItemID"]);
            HealthCheckListItemsCollection coll = new HealthCheckListItemsCollection();
            coll = HealthCheckListItemsHelper.LoadAllHealthCheckListItems(healthCheckID);
            HealthCheckListItemsCollection newColl = new HealthCheckListItemsCollection();
            foreach (HealthCheckListItems item in coll)
            { 
                //Remove if not selected
                if (item.Id == healthCheckListItemId)
                    newColl.Add(item);
            }
            gridHealthCheckListItems.DataSource = newColl;
            gridHealthCheckListItems.DataBind();
        }
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

    protected void Page_Error(object sender, EventArgs e)
    {
        LogExceptions.WriteExceptionLog(Server.GetLastError());
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
        return li;
    }
    public void GridColumns()
    {
        gridHealthCheckListItems.Columns[1].Visible = VisibilityInGridColumns("Status");
        gridHealthCheckListItems.Columns[4].Visible = VisibilityInGridColumns("Yes No value");
        gridHealthCheckListItems.Columns[5].Visible = VisibilityInGridColumns("Issues");
        gridHealthCheckListItems.Columns[6].Visible = VisibilityInGridColumns("Notes");
    }
    protected void gridHealthCheckListItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
}
