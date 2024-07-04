using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Health.Entity;
using System.Data;
using Health.DAL;
using System.Collections;
using Health.StateManager;
using System.Data.SqlTypes;
using System.Text;
using Deffinity.EmailService;
using System.Configuration;
using HealthCheckMgt.DAL;
using System.Linq;
using System.Collections.Generic;

public partial class HealthCheckForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                HealthCheckListItemsState.ClearHealthCheckListItemsCache();
                HealthCheckList healthCheckList = new HealthCheckList();
                int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
                healthCheckList = GetHealthCheckSchedule(healthCheckListID);
                LoadPageControls(healthCheckList);
                Cache.Remove("MailList");

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                Response.Redirect("~/WF/Health/HealthCheckSchedule.aspx?R=Y" + retString(), false);
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            GridColumns();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void GridColumns()
    {
        try
        {
            gridHealthChecks.Columns[2].Visible = VisibilityInGridColumns("Yes No value");
            gridHealthChecks.Columns[3].Visible = VisibilityInGridColumns("Issues");
            gridHealthChecks.Columns[4].Visible = VisibilityInGridColumns("RAG");
            gridHealthChecks.Columns[5].Visible = VisibilityInGridColumns("Notes");
            gridHealthChecks.Columns[6].Visible = VisibilityInGridColumns("Due Date");
            gridHealthChecks.Columns[8].Visible = VisibilityInGridColumns("Status");
            gridHealthChecks.Columns[9].Visible = VisibilityInGridColumns("Email Icon Button");
            gridHealthChecks.Columns[10].Visible = VisibilityInGridColumns("Save and email Button");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
    private string retString()
    {
        string retStr = string.Empty;

        if (Request.QueryString["type"] != null)
        {
            retStr = "&type=" + Request.QueryString["type"].ToString();
        }
        else
        {
            retStr = "&type=main";
        }

        return retStr;
        
    }
    protected bool getYesOrNo(SqlBoolean isChecked, bool isYes)
    {
        if (isChecked.IsNull)
            return false;
        if (isYes)
        {
            if (isChecked == true)
                return true;
            else
                return false;
        }
        else
        {
            if (isChecked == true)
                return false;
            else
                return true;
        }
    }

    protected void gridHealthChecks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkYes = (CheckBox)e.Row.FindControl("chkYes");
            string chkYesID = e.Row.FindControl("chkYes").ClientID;
            CheckBox chkNo = (CheckBox)e.Row.FindControl("chkNo");
            chkNo.Attributes.Add("onclick", "javascript:CheckYes(" + ((CheckBox)e.Row.FindControl("chkNo")).ClientID + ","+chkYesID+")");
            chkYes.Attributes.Add("onclick", "javascript:CheckNo(" + ((CheckBox)e.Row.FindControl("chkNo")).ClientID + "," + chkYesID + ")");
            DropDownList ddlGridStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            try
            {
                string status = ((Label)e.Row.FindControl("lblStatus")).Text;
                ddlGridStatus.SelectedIndex = ddlGridStatus.Items.IndexOf(ddlGridStatus.Items.FindByValue(status));
            }
            

            catch 
            {
                ddlGridStatus.SelectedIndex = 0;
            }
            DropDownList ddlRag = ((DropDownList)e.Row.FindControl("ddlRAG"));
            try
            {
                string rag = ((Label)e.Row.FindControl("lblRag")).Text;
                ddlRag.SelectedIndex = ddlRag.Items.IndexOf(ddlRag.Items.FindByText(rag));
            }
            catch
            {
                ddlRag.SelectedIndex = 0;
            }
        }
    }

    protected void btnSubmitChanges_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateHealthCheckItems(false);
            UpdateHealthCheckSchedule();
            Response.Redirect("~/WF/Health/HealthCheckSchedule.aspx?R=Y"+retString(), false);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    void UpdateHealthCheckSchedule()
    {
        HealthCheckList healthCheckList = new HealthCheckList();
        int healthCheckID = Convert.ToInt32(Request.QueryString["HealthCheckId"]);
        healthCheckList = GetHealthCheckSchedule(healthCheckID);
        healthCheckList.AssignedTeam = Convert.ToInt32(ddlAssignedTeam.SelectedValue);
        healthCheckList.DateRaised = Convert.ToDateTime(txtDate.Text + " " + txtTime.Text);
        healthCheckList.Notes = txtNotes.Text;
        healthCheckList.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
        healthCheckList.Status = GetStatusOfTheHealthCheckList(healthCheckID);
        healthCheckList.Assignmember = Convert.ToInt32(ddlTeammember.SelectedValue);
        foreach (GridViewRow row in gridHealthChecks.Rows)
        {
            string issue = ((TextBox)row.FindControl("txtIssues")).Text;
            if (!string.IsNullOrEmpty(issue))
            {
                healthCheckList.IssueStatus = "Issues";
                break;
            }
            else
                healthCheckList.IssueStatus = "Good";

            string Duedate = ((TextBox)row.FindControl("txtDuedate")).Text;
            if (!string.IsNullOrEmpty(Duedate.Trim()))
            {
                healthCheckList.DueDate = Convert.ToDateTime(Duedate);
            }
            else
                healthCheckList.DueDate = Convert.ToDateTime("1/1/1900");

            
            string rag = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;

            if (rag == "Please select..." || rag == string.Empty)
            {
                healthCheckList.RAG = string.Empty;
            }
            else
            {
                healthCheckList.RAG = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;
            }
        }
        HealthCheckListHelper.Update(healthCheckList);
        //if(healthCheckList.Status=="Completed") SendMailOnComplete(healthCheckList);
    }

    //Return the status by verifying all the items related to a particular Health Check List
    private string GetStatusOfTheHealthCheckList(int healthCheckID)
    {
        string status = string.Empty;
        foreach (HealthCheckListItems item in HealthCheckListItemsHelper.LoadAllHealthCheckListItems(healthCheckID))
        {
            //If any of the item is critical then status of the entire health check is critical.
            if (item.Status == "Critical")
                return "Critical";
            if (item.Status != "Complete" && item.Status != "Not Applicable")
                status = "In Progress";
        }
        //If any of the item is "In Progress" state, then the entire health check is In Progress.
        if(status.Equals("In Progress"))
            return "In Progress";
        //If no items are either in Critical, In Progress or Not Applicable then the status is Completed.
        return "Completed";
      
    }

    private static HealthCheckList GetHealthCheckSchedule(int healthCheckID)
    {
        foreach (HealthCheckList healthCheckList in HealthCheckListHelper.LoadAllHealthCheckLists())
        {
            if (healthCheckID == healthCheckList.Id)
                return healthCheckList;
        }
        return null;
    }

    void LoadPageControls(HealthCheckList healthCheckList)
    {
        //Master.PageHead = healthCheckList.HealthCheckTitle;
        txtDate.Text = healthCheckList.DateRaised.Date.ToString("d");
        txtTime.Text = string.Format("{0:t hh:mm}", healthCheckList.DateRaised.ToShortTimeString()); 
        //ddlLocation.DataBind();
        //ddlAssignedTeam.DataBind();
        Location_Bind(healthCheckList.PortfolioID);
        Team_Bind(healthCheckList.PortfolioID);
        ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(healthCheckList.LocationID.ToString()));
        ddlAssignedTeam.SelectedIndex = ddlAssignedTeam.Items.IndexOf(ddlAssignedTeam.Items.FindByValue(healthCheckList.AssignedTeam.ToString()));
        txtNotes.Text = healthCheckList.Notes;
        txtIssueStatus.Text = healthCheckList.IssueStatus;
        ddlTeammember.DataBind();
        ddlTeammember.SelectedIndex = ddlTeammember.Items.IndexOf(ddlTeammember.Items.FindByValue(healthCheckList.Assignmember.ToString()));
       // ddlTeammember.SelectedIndex = Convert.ToInt32(healthCheckList.Assignmember.ToString())-1;
       
    }

    private void UpdateHealthCheckItems(bool isCompleteAll)
    {
        HealthCheckListItems healthCheckListItems = new HealthCheckListItems();
        for (int i = 0; i < gridHealthChecks.Rows.Count; i++)
        {
            UpdateSingleItem(isCompleteAll, healthCheckListItems, i);
        }
    }

    /// <summary>
    /// Updates a single item
    /// </summary>
    /// <param name="isCompleteAll">"True" makes the status as "Complete" for the item.</param>
    /// <param name="healthCheckListItems"></param>
    /// <param name="i">Index of the grid to be updated</param>
    private int UpdateSingleItem(bool isCompleteAll, HealthCheckListItems healthCheckListItems, int i)
    {
        GridViewRow row = gridHealthChecks.Rows[i];
        bool isYes = ((CheckBox)row.FindControl("chkYes")).Checked;
        bool isNo = ((CheckBox)row.FindControl("chkNo")).Checked;
        if (isYes)
            healthCheckListItems.IsChecked = true;
        else if (isNo)
            healthCheckListItems.IsChecked = false;
        else
            healthCheckListItems.IsChecked = SqlBoolean.Null;

        string issues = ((TextBox)row.FindControl("txtIssues")).Text;
        string previousIssues = ((Literal)row.FindControl("litPreviousIssues")).Text;
        string notes = ((TextBox)row.FindControl("txtNotes")).Text;
        string status = ((DropDownList)row.FindControl("ddlStatus")).SelectedValue;
        string s = ((TextBox)row.FindControl("txtDuedate")).Text.Trim();
        DateTime DueDate = Convert.ToDateTime(string.IsNullOrEmpty(s)?"01/01/1900":s);
        //Set Status to Complete when mark all as complete is clicked.
        status = (isCompleteAll) ? "Complete" : status;
        healthCheckListItems.Issues = string.IsNullOrEmpty(issues) ? string.Empty : issues;
        healthCheckListItems.Notes = string.IsNullOrEmpty(notes) ? string.Empty : notes;
        healthCheckListItems.DueDate = Convert.ToDateTime(DueDate);
        healthCheckListItems.AssignedTeam = Convert.ToInt32(ddlAssignedTeam.SelectedValue);
        healthCheckListItems.Id = Convert.ToInt32(((Label)row.FindControl("lblID")).Text);
        healthCheckListItems.HealthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckId"]);
        healthCheckListItems.HealthCheck = ((Label)row.FindControl("lblHealthCheck")).Text;
        healthCheckListItems.AssignedMember = Convert.ToInt32(ddlTeammember.SelectedValue);
        //If Issue text box in the grid is changed, then keep trap of the date that is changed.
        if (previousIssues != issues)
            healthCheckListItems.IssueDate = DateTime.Now;

        //Check the status of each item and mark the status of the health check list.
        //DateCompleted and Assignee needs to be updated only if the previous state of status is not "Complete"
        if (!string.Equals(healthCheckListItems.Status, "Complete"))
        {
            healthCheckListItems.DateCompleted = DateTime.Now.Date;
            healthCheckListItems.Assignee = sessionKeys.UID;
        }

        healthCheckListItems.Status = status;
        // need to add rag status here
        string rag = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;

        if (rag == "Please select..." || rag == string.Empty)
        {
            healthCheckListItems.RAG = string.Empty;
            
        }
        else
        {
            healthCheckListItems.RAG = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;
        }
        HealthCheckListItemsHelper.Update(healthCheckListItems);
        return healthCheckListItems.Id;
    }

    protected void lnkCompleteItems_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateHealthCheckItems(true);
            UpdateHealthCheckSchedule();
            Response.Redirect("~/WF/Health/HealthCheckSchedule.aspx?R=Y" + retString(), false);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gridHealthChecks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Mail"))
            {
                //Get's the number of the row that was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                updateSingleRecordAndGetMailDetails(rowIndex);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Updates the row selected
    /// </summary>
    /// <param name="rowIndex">row index of the gridview</param>
    void updateSingleRecordAndGetMailDetails(int rowIndex)
    {
        ArrayList mailIds=new ArrayList();
        GridViewRow row = gridHealthChecks.Rows[rowIndex];
        

        //Find the grid view in that particular row. And add the mail ids to the arraylist mailIds to send mails.
        if (gridInner!=null && gridInner.Rows.Count > 0)
        {
            foreach (GridViewRow innerGridRow in gridInner.Rows)
            {
                bool isMailable = ((CheckBox)innerGridRow.FindControl("chkMailable")).Checked;
                //Get the mail id only if marked for mail and add to ArrayList.
                if (isMailable)
                {
                    string eMailID = ((Label)innerGridRow.FindControl("lblEmailID")).Text;
                    mailIds.Add(eMailID);
                }
            }
        }

        //Update the selected item to the database.
        HealthCheckListItems healthCheckListItems = new HealthCheckListItems();
        MailSingleItem(rowIndex, mailIds, healthCheckListItems);
    }

    private void MailSingleItem(int rowIndex, ArrayList mailIds, HealthCheckListItems healthCheckListItems)
    {
        
        if (mailIds.Count > 0)
        {
            int itemID = UpdateSingleItem(false, healthCheckListItems, rowIndex);
            int healthCheckListID = Convert.ToInt32(((Label)gridHealthChecks.Rows[rowIndex].FindControl("lblID")).Text);
            int healthCheckID = Convert.ToInt32(Request.QueryString["HealthCheckId"]);
            HtmlHijacker htmlHiJacker = new HtmlHijacker();
            string errorString = string.Empty;
            Email eMail = new Email();
            string htmlTest = htmlHiJacker.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/WF/Health/MailTemplates/HealthCheckListItem.aspx?healthcheckid=" + healthCheckID + "&PortfolioID=" + sessionKeys.PortfolioID + "&ListItemID=" + itemID, ref errorString);
            eMail.SendingMail(string.Empty, "Health Check", htmlTest, string.Empty, string.Empty, mailIds);
        }
    }

    protected string FormatDefaultDate(DateTime date)
    {
        if (date.Year ==1900)
            return " ";
        else
            return date.ToShortDateString();// .ToString("{0:d}");
        
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        LogExceptions.WriteExceptionLog(ex);
        Email eMail = new Email();
        StringBuilder sb = new StringBuilder();
        sb.Append("<br/>Message: " + ex.Message);
        sb.Append("<br/>Source: " + ex.Source);
        sb.Append("<br/>TargetSite: " + ex.TargetSite);
        sb.Append("<br/>Data: " + ex.Data);
        sb.Append("<br/>Inner Exception: " + ex.InnerException);
        sb.Append("<br/>Stack Trace:<br/><hr/>" + ex.StackTrace);
        ArrayList mailIds = new ArrayList();
        mailIds.Add("chandra.sekhar@emsysindia.com");
        mailIds.Add("goverdhan@emsysindia.com");
        mailIds.Add("indra@emsysindia.com");
        eMail.SendingMail(string.Empty, "Deffinity Error", sb.ToString(), string.Empty, string.Empty, mailIds);
        Response.Redirect("~/WF/Message.aspx?aspxerrorpath=/HealthCheckForm.aspx?R=Y",false);
    }
    protected void gridHealthChecks_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }
    protected void ImgApply_Click(object sender, EventArgs e)
    {
        try
        {
            int chk = 0;
            if (Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text) != 0 && txtEndDate.Text != "")
            {
                chk = IsValidEndDate(int.Parse(txtRecur.Text));
            }
            if (chk == 0)
            {
                HealthCheckRecurr entity = new HealthCheckRecurr();
                entity.StartTime = Convert.ToDateTime("01/01/1900");
                entity.EndTime = Convert.ToDateTime("01/01/1900");
                entity.StartDate = Convert.ToDateTime(string.IsNullOrEmpty(txtStartDate.Text) ? "01/01/1900" : txtStartDate.Text);
                entity.EndDate = Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text) ? "01/01/1900" : txtEndDate.Text);
                entity.EndAfter = Convert.ToInt32(string.IsNullOrEmpty(txtEndOfOcurrences.Text) ? "0" : txtEndOfOcurrences.Text);
                string days = "";
                foreach (ListItem chkDay in chkDays.Items)
                {
                    if (chkDay.Selected)
                    {
                        days += chkDay.Value + ",";
                    }
                }
                entity.WeekDayName = days;
                entity.ReCurrencePattern = int.Parse(rdPattern.SelectedValue);
                entity.ReCurrenceRange = int.Parse(rdRangeOfRecurrence.SelectedValue);
                entity.RecurWeekOn = Convert.ToInt32(string.IsNullOrEmpty(txtRecur.Text) ? "0" : txtRecur.Text);
                entity.HealthCheckID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
                HealthCheckListItemsHelper.InsertUpdateRecurr(entity);
            }
             
           else
           {
               mpopRecurrence.Show();
           }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgRecurrence_Click(object sender, EventArgs e)
    {
        // imgRecurrence.Attributes.Add("onclick", "disableItem()");
        //string.Format("{0:t hh:mm}", healthCheckList.DateRaised.ToShortTimeString());
        try
        {
            int exist = HealthCheckListItemsHelper.IsExist(Convert.ToInt32(Request.QueryString["HealthCheckID"]));
            if (exist != 0)
            {
                HealthCheckRecurr entity = HealthCheckListItemsHelper.SelectById(Convert.ToInt32(Request.QueryString["HealthCheckID"]));
                //txtStartTime.Text = string.Format("{0:t hh:mm}", entity.StartTime.ToShortTimeString());
                //txtEndTime.Text = string.Format("{0:t hh:mm}", entity.EndTime.ToShortTimeString());
                if (string.Format("{0:d}", entity.StartDate) == "01/01/1900")
                {
                    txtStartDate.Text = "";
                }
                else
                {
                    txtStartDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.StartDate);
                }
                if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.EndDate) == "01/01/1900")
                {
                    txtEndDate.Text = "";
                }
                else
                {
                    txtEndDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.EndDate);
                }

                rdPattern.SelectedValue = entity.ReCurrencePattern.ToString();
                //if (rdPattern.SelectedValue != "1")
                //{
                //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "valid",
                //    //    "<script language='javascript'> function disableItem() { var chkDays = document.getElementById('<%=chkDays.ClientID%>');chkDays.disabled = true;}</script>");

                //    chkDays.Enabled = false;
                //}
                rdRangeOfRecurrence.SelectedValue = entity.ReCurrenceRange.ToString();

                string[] days = entity.WeekDayName.Split(',');
                foreach (ListItem chk in chkDays.Items)
                {
                    if (days.Length > 0)
                    {
                        for (int i = 0; i < days.Length; i++)
                        {
                            if (days[i] == chk.Value)
                            {
                                chk.Selected = true;
                            }
                        }

                    }
                }
                if (entity.RecurWeekOn != 0)
                {
                    txtRecur.Text = entity.RecurWeekOn.ToString();
                }
                else
                {
                    txtRecur.Text = string.Empty; 
                }
                txtEndOfOcurrences.Text = entity.EndAfter.ToString();

                mpopRecurrence.Show();
            }

            else
            {
                mpopRecurrence.Show();
            }
        }

        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private int IsValidEndDate(int weeks)
    {
        DateTime dt = Convert.ToDateTime(txtStartDate.Text);
        weeks = weeks * 7;
        DateTime dt1 = dt.AddDays(weeks);
        if (dt1 > Convert.ToDateTime(txtEndDate.Text) && weeks > 0)
        {
            lblRecurrMsg.Text = "Recur weeks exceeds end date";
            lblRecurrMsg.ForeColor = System.Drawing.Color.Red;
            return 1;
        }
        return 0;
    }

    private void Location_Bind(int portfolioID)
    { 
        objLocationDDLFiller.SelectParameters[0].DefaultValue=GetPortfolioid(portfolioID).ToString();
        ddlLocation.DataBind();
    }
    private void Team_Bind(int portfolioID)
    {
        objTeams.SelectParameters[0].DefaultValue = GetPortfolioid(portfolioID).ToString();
        ddlAssignedTeam.DataBind();
    }

    private int GetPortfolioid(int portfolioID)
    {
        int retVal = 0;
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["type"] != "main")
            {
                retVal=portfolioID;
            }
            else
            {
                retVal = sessionKeys.PortfolioID;
            }
        }
        else
        {
            retVal = sessionKeys.PortfolioID;
        }
        return retVal;
    }
    
   
}
