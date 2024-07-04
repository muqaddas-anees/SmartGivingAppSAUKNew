using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Deffinity.EmailService;
using System.Configuration;
using Health.Entity;
using Health.DAL;
using Health.StateManager;
using System.Data.SqlTypes;

public partial class Health_HealthCheckSchedule_CPortal : System.Web.UI.Page
{
    const string completedItems = "CompletedItemsInCache";
    const string sortExpression = "SortExpression";
    const string sortDirection = "SortDirection";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sessionKeys.pageid = 1;
            //Master.PageHead = "Customer Portal";
            HealthCheckListState.ClearHealthCheckItemsCache();
            gridIssueBinder();
            LoadPageControls();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.Browser.Browser.Equals("IE") && Request.Browser.Version.Equals("6.0"))
        {
            panel1.ScrollBars = ScrollBars.Horizontal;
            panel2.ScrollBars = ScrollBars.Horizontal;
            panel3.ScrollBars = ScrollBars.Horizontal;
        }
    }
    void LoadPageControls()
    {
        gridHealthChecks.DataSource = FilterRowsByCompletion();
        gridHealthChecks.DataBind();
    }
    HealthCheckListCollection FilterRowsByCompletion()
    {
        HealthCheckListCollection CompletedHealthCheckLists = new HealthCheckListCollection();
        foreach (HealthCheckList item in HealthCheckListHelper.LoadAllHealthCheckLists())
        {
            //if (item.Status == "Completed")
            CompletedHealthCheckLists.Add(item);
        }
        Cache.Insert(completedItems, CompletedHealthCheckLists, null, DateTime.MaxValue, TimeSpan.FromSeconds(90));
        return (HealthCheckListCollection)Cache[completedItems];
    }
    protected void gridIssueBinder()
    {
        if (ddlHealthCheckTitles.SelectedIndex != 0)
        {
            HealthCheckListItemsCollection healthCheckListItems = HealthCheckListItemsHelper.LoadAllHealthCheckListItemsByTitle(Convert.ToInt32(ddlHealthCheckTitles.SelectedValue));
            //HealthCheckListItemsHelper.SortByColumn("IssueStatus DESC", healthCheckListItems);
            //healthCheckListItems.Sort(new Health.HealthCheckListItemsSortHelper("IssueStatus"));
            //healthCheckListItems.Reverse();
            gridHealthCheckIssues.DataSource = FilterGridSource(healthCheckListItems);
            gridHealthCheckIssues.DataBind();
        }
    }
    HealthCheckListItemsCollection FilterGridSource(HealthCheckListItemsCollection healthCheckListItems)
    {
        HealthCheckListItemsCollection filteredCollection = new HealthCheckListItemsCollection();

        //Adds the item to the filteredCollection which needs to be removed later.
        for (int i = 0; i < healthCheckListItems.Count; i++)
        {
            HealthCheckListItems item = new HealthCheckListItems();
            item = healthCheckListItems[i];
            if (string.IsNullOrEmpty(item.Issues.Trim()))
                filteredCollection.Add(item);
        }

        //Removes the item from healthCheckListItems.
        for (int i = 0; i < filteredCollection.Count; i++)
        {
            HealthCheckListItems item = new HealthCheckListItems();
            item = filteredCollection[i];
            healthCheckListItems.Remove(item);
        }

        return healthCheckListItems;
    }
    protected void btnUpdateIssue_OnClick(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in gridHealthChecks.Rows)
        {
            bool hasIssue = ((CheckBox)gridRow.FindControl("chkIssue")).Checked;
            if (hasIssue)
            {
                int healthCheckID = Convert.ToInt32(((Label)gridRow.FindControl("lblID")).Text);
                GridView gridEmail = ((GridView)gridRow.FindControl("gridInner"));
                ArrayList emailArrayList = new ArrayList();
                foreach (GridViewRow emailRows in gridEmail.Rows)
                {
                    bool isSelectedMail = (((CheckBox)emailRows.FindControl("chkMailable")).Checked);
                    if (isSelectedMail)
                    {
                        string eMailAddress = (((Label)emailRows.FindControl("lblEmailID")).Text);
                        emailArrayList.Add(eMailAddress);
                    }
                }

                HealthCheckList healthCheckList = HealthCheckListHelper.LoadHealthcheckByID(healthCheckID);
                healthCheckList.Issue = txtIssue.Text;
                HealthCheckListHelper.Update(healthCheckList);
                lblMessage.Text = "Issue(s) added successfully.";
                SendIssueMail(emailArrayList, healthCheckID);
            }
        }
    }
    void SendIssueMail(ArrayList mailIDs, int healthCheckID)
    {
        Email eMail = new Email();
        HtmlHijacker htmlHiJacker = new HtmlHijacker();
        string errorString = string.Empty;
        string htmlTest = htmlHiJacker.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/HealthCheckDetails.aspx?Issue=true&healthcheckid=" + healthCheckID + "&PortfolioID=" + sessionKeys.PortfolioID, ref errorString);
        eMail.SendingMail(string.Empty, "Health Check", htmlTest, string.Empty, string.Empty, mailIDs);
    }
    protected void gridHealthChecks_Sorting(object sender, GridViewSortEventArgs e)
    {
        HealthCheckListCollection healthCheckLists = FilterRowsByCompletion();
        HealthCheckListHelper.SortByColumn(getSortExpression(e.SortExpression), healthCheckLists);
        gridHealthChecks.DataSource = healthCheckLists;
        gridHealthChecks.DataBind();
    }

    string getSortExpression(string sortExpn)
    {
        string finalSortExpression = string.Empty;

        if (ViewState[sortExpression] == null)
        {
            ViewState[sortExpression] = sortExpn;
            ViewState[sortDirection] = "DESC";
            finalSortExpression = sortExpn + " ASC";
        }
        else
            if (ViewState[sortExpression].ToString().Equals(sortExpn) && ViewState[sortDirection].ToString().Equals("ASC"))
            {
                ViewState[sortDirection] = "DESC";
                finalSortExpression = sortExpn + " DESC";
            }
            else
            {
                ViewState[sortDirection] = "ASC";
                finalSortExpression = sortExpn + " ASC";
            }
        ViewState[sortExpression] = sortExpn;
        return finalSortExpression;
    }

    protected string getRagUrl(string status)
    {
        string returnURL = string.Empty;
        switch (status)
        {
            case "Pending":
            case "In Progress":
                returnURL = @"/WF/images/indcate_amber.gif";
                break;
            case "Critical":
                returnURL = @"/WF/images/indcate_red.gif";
                break;
            case "Complete":
            case "Not Applicable":
            default:
                returnURL = @"/WF/images/indcate_green.png";
                break;
        }
        return returnURL;
    }
    protected void gridHealthChecks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridHealthChecks.PageIndex = e.NewPageIndex;
        LoadPageControls();
    }

    protected void gridHealthCheckIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridHealthCheckIssues.PageIndex = e.NewPageIndex;
        gridIssueBinder();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        HealthCheckListItemsState.ClearHealthCheckListItemsCache();
        gridIssueBinder();
    }

    protected string getDate(object date)
    {
        SqlDateTime issueDate = (SqlDateTime)date;
        string returnValue = string.Empty;
        if (issueDate.IsNull)
            returnValue = "No Date";
        else
            returnValue = issueDate.ToString().Remove(10);
        return returnValue;
    }
}