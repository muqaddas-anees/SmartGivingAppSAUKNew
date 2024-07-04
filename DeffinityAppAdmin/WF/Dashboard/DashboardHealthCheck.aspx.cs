using System;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Deffinity.EmailService;
using System.Configuration;
using Health.Entity;
using Health.DAL;
using Health.StateManager;
using System.Data.SqlTypes;
using Deffinity.ProgrammeManagers;

public partial class DashboardHealthCheck1 : System.Web.UI.Page
{
    const string completedItems = "CompletedItemsInCache";
    const string sortExpression = "SortExpression";
    const string sortDirection = "SortDirection";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Master.PageHead = "Dashboard";
            //ddlPortfolio.DataBind();
            HealthCheckListState.ClearHealthCheckItemsCache();
            //sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
            CheckUserRole();
            LoadPageControls();

        }
        gridIssueBinder();
        

    }
    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
        ddlHealthCheckTitles.Items.Clear();
        //ListItem lstItem = new ListItem("Please Select..", "0");
        //ddlHealthCheckTitles.Items.Add(lstItem);
        //objHealthCheckDDLFiller.SelectParameters.Add("PortfolioID", ddlPortfolio.SelectedValue);
        //objHealthCheckDDLFiller.DataBind();
        ddlHealthCheckTitles.DataBind();
        ddlHealthCheckTitles.Items.Insert(0, Constants.ddlDefaultBind(true));
        LoadPageControls();
    }
    void LoadPageControls()
    {
        gridHealthChecks.DataSource = FilterRowsByCompletion();
        gridHealthChecks.DataBind();
    }

    HealthCheckListCollection FilterRowsByCompletion()
    {
        int Portfolio = Convert.ToInt32(string.IsNullOrEmpty(ddlPortfolio.SelectedValue) ? "0" : ddlPortfolio.SelectedValue);
        HealthCheckListCollection CompletedHealthCheckLists = new HealthCheckListCollection();
        HealthCheckListState.ClearHealthCheckItemsCache();
        foreach (HealthCheckList item in HealthCheckListHelper.LoadAllHealthCheckLists(Portfolio))
        {
            //if (item.Status == "Completed")
            CompletedHealthCheckLists.Add(item);
        }
        Cache.Insert(completedItems, CompletedHealthCheckLists, null, DateTime.MaxValue, TimeSpan.FromSeconds(90));
        return (HealthCheckListCollection)Cache[completedItems];
    }

    protected void gridIssueBinder()
    {
        if (ddlHealthCheckTitles.Items.Count>0 && ddlHealthCheckTitles.SelectedIndex != 0)
        {
            HealthCheckListItemsCollection healthCheckListItems = HealthCheckListItemsHelper.LoadAllHealthCheckListItemsByTitle(Convert.ToInt32(ddlHealthCheckTitles.SelectedValue));
            gridHealthCheckIssues.DataSource = FilterGridSource(healthCheckListItems);
            gridHealthCheckIssues.DataBind();
        }
    }

    //Filter the grid based on the requirements
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
                HealthCheckList healthCheckList = HealthCheckListHelper.LoadHealthcheckByID(healthCheckID);
                healthCheckList.Issue = txtIssue.Text;
                HealthCheckListHelper.Update(healthCheckList);
                lblMessage.Text = "Issue(s) added successfully.";
                SendIssueMail(healthCheckList, healthCheckID);
            }
        }
    }

    //Mail to the team.
    void SendIssueMail(HealthCheckList healthCheckList, int healthCheckID)
    {
        Email eMail = new Email();
        string sqlStatement = "SELECT HCN.* FROM HealthCheckNameMailID" +
                        " HCN INNER JOIN HealthCheckList_HealthCheckNameMailID HCL_HCN" +
                        " ON HCN.ID=HCL_HCN.MailID WHERE HCN.PortfolioHealthCheckID=" + healthCheckList.HealthCheckListID;
        DataTable teamMembers = DataHelperClass.GenericSelectMethodHelp(sqlStatement);
        ArrayList mailIDs = new ArrayList();
        foreach (DataRow row in teamMembers.Rows)
            mailIDs.Add(row["EmailID"].ToString());
        HtmlHijacker htmlHiJacker = new HtmlHijacker();

        string errorString = string.Empty;
        string htmlTest = htmlHiJacker.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/Health/MailTemplates/HealthCheckDetails.aspx?Issue=true&healthcheckid=" + healthCheckID + "&PortfolioID=" + sessionKeys.PortfolioID, ref errorString);
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
                returnURL = @"<span style='color:yellow;'><i class='fa fa-circle'></i></span>";
                break;
            case "Critical":
                returnURL = @"<span style='color:red;'><i class='fa fa-circle'></i></span>";
                break;
            case "Complete":
            case "Not Applicable":
            default:
                returnURL = @"<span style='color:green;'><i class='fa fa-circle'></i></span>";
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
        LoadPageControls();
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
    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
           if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }

            }
        
    }
    private void Disable()
    {
        btnUpdateIssue.Enabled = false;

    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                        // Disable();

                    }

                }
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion
}
