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
using HealthCheckMgt.BAL;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Net;
using UserMgt.DAL;
using Location.DAL;
using DC.DAL;
using System.Web.UI.HtmlControls;

public partial class HC_HealthCheckForm : System.Web.UI.Page
{
    public string section = "healthcheck";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
        try
        {
            BindControls(healthCheckListID);
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        if (!IsPostBack)
        {
            try
            {
                HealthCheckListItemsState.ClearHealthCheckListItemsCache();
                HealthCheckList healthCheckList = new HealthCheckList();
               
                healthCheckList = GetHealthCheckSchedule(healthCheckListID);
                LoadPageControls(healthCheckList);
                Cache.Remove("MailList");
                //Bind the form
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                Response.Redirect("~/WF/Health/HC/HealthCheckSchedule.aspx?R=Y" + retString(), false);
            }
        }
      
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
             int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
           // UpdateHealthCheckItems(false);
            UpdateHealthCheckSchedule();
            SavePlaceholderData(healthCheckListID);
            Response.Redirect("~/WF/Health/HC/HealthCheckSchedule.aspx?R=Y"+retString(), false);
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
        //healthCheckList.Status = GetStatusOfTheHealthCheckList(healthCheckID);
        healthCheckList.Assignmember = Convert.ToInt32(ddlTeammember.SelectedValue);
        healthCheckList.Status = ddlStatus.SelectedValue;
        //foreach (GridViewRow row in gridHealthChecks.Rows)
        //{
        //    string issue = ((TextBox)row.FindControl("txtIssues")).Text;
        //    if (!string.IsNullOrEmpty(issue))
        //    {
        //        healthCheckList.IssueStatus = "Issues";
        //        break;
        //    }
        //    else
        //        healthCheckList.IssueStatus = "Good";

        //    string Duedate = ((TextBox)row.FindControl("txtDuedate")).Text;
        //    if (!string.IsNullOrEmpty(Duedate.Trim()))
        //    {
        //        healthCheckList.DueDate = Convert.ToDateTime(Duedate);
        //    }
        //    else
        //        healthCheckList.DueDate = Convert.ToDateTime("1/1/1900");

            
        //    string rag = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;

        //    if (rag == "Please select..." || rag == string.Empty)
        //    {
        //        healthCheckList.RAG = string.Empty;
        //    }
        //    else
        //    {
        //        healthCheckList.RAG = ((DropDownList)row.FindControl("ddlRag")).SelectedItem.Text;
        //    }
        //}
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
        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(healthCheckList.Status.ToString()));
       // ddlTeammember.SelectedIndex = Convert.ToInt32(healthCheckList.Assignmember.ToString())-1;
       
    }

    //private void UpdateHealthCheckItems(bool isCompleteAll)
    //{
    //    HealthCheckListItems healthCheckListItems = new HealthCheckListItems();
    //    for (int i = 0; i < gridHealthChecks.Rows.Count; i++)
    //    {
    //        UpdateSingleItem(isCompleteAll, healthCheckListItems, i);
    //    }
    //}

    /// <summary>
    /// Updates a single item
    /// </summary>
    /// <param name="isCompleteAll">"True" makes the status as "Complete" for the item.</param>
    /// <param name="healthCheckListItems"></param>
    /// <param name="i">Index of the grid to be updated</param>
   
    protected void lnkCompleteItems_Click(object sender, EventArgs e)
    {
        try
        {
            //UpdateHealthCheckItems(true);
            UpdateHealthCheckSchedule();
            Response.Redirect("~/WF/Health/HC/HealthCheckSchedule.aspx?R=Y" + retString(), false);
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
    //void updateSingleRecordAndGetMailDetails(int rowIndex)
    //{
    //    ArrayList mailIds=new ArrayList();
    //    GridViewRow row = gridHealthChecks.Rows[rowIndex];
        

    //    //Find the grid view in that particular row. And add the mail ids to the arraylist mailIds to send mails.
    //    if (gridInner!=null && gridInner.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow innerGridRow in gridInner.Rows)
    //        {
    //            bool isMailable = ((CheckBox)innerGridRow.FindControl("chkMailable")).Checked;
    //            //Get the mail id only if marked for mail and add to ArrayList.
    //            if (isMailable)
    //            {
    //                string eMailID = ((Label)innerGridRow.FindControl("lblEmailID")).Text;
    //                mailIds.Add(eMailID);
    //            }
    //        }
    //    }

    //    //Update the selected item to the database.
    //    HealthCheckListItems healthCheckListItems = new HealthCheckListItems();
    //    MailSingleItem(rowIndex, mailIds, healthCheckListItems);
    //}

    //private void MailSingleItem(int rowIndex, ArrayList mailIds, HealthCheckListItems healthCheckListItems)
    //{
        
    //    if (mailIds.Count > 0)
    //    {
    //        int itemID = UpdateSingleItem(false, healthCheckListItems, rowIndex);
    //        int healthCheckListID = Convert.ToInt32(((Label)gridHealthChecks.Rows[rowIndex].FindControl("lblID")).Text);
    //        int healthCheckID = Convert.ToInt32(Request.QueryString["HealthCheckId"]);
    //        HtmlHijacker htmlHiJacker = new HtmlHijacker();
    //        string errorString = string.Empty;
    //        Email eMail = new Email();
    //        string htmlTest = htmlHiJacker.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/HealthCheckListItem.aspx?healthcheckid=" + healthCheckID + "&PortfolioID=" + sessionKeys.PortfolioID + "&ListItemID=" + itemID, ref errorString);
    //        eMail.SendingMail(string.Empty, "Health Check", htmlTest, string.Empty, string.Empty, mailIds);
    //    }
    //}

    protected string FormatDefaultDate(DateTime date)
    {
        if (date.Year ==1900)
            return " ";
        else
            return date.ToShortDateString();// .ToString("{0:d}");
        
    }

    //protected void Page_Error(object sender, EventArgs e)
    //{
    //    Exception ex = Server.GetLastError();
    //    LogExceptions.WriteExceptionLog(ex);
    //    Email eMail = new Email();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<br/>Message: " + ex.Message);
    //    sb.Append("<br/>Source: " + ex.Source);
    //    sb.Append("<br/>TargetSite: " + ex.TargetSite);
    //    sb.Append("<br/>Data: " + ex.Data);
    //    sb.Append("<br/>Inner Exception: " + ex.InnerException);
    //    sb.Append("<br/>Stack Trace:<br/><hr/>" + ex.StackTrace);
    //    ArrayList mailIds = new ArrayList();
    //    mailIds.Add("chandra.sekhar@emsysindia.com");
    //    mailIds.Add("goverdhan@emsysindia.com");
    //    mailIds.Add("indra@emsysindia.com");
    //    eMail.SendingMail(string.Empty, "Deffinity Error", sb.ToString(), string.Empty, string.Empty, mailIds);
    //    Response.Redirect("~/Message.aspx?aspxerrorpath=/HealthCheckForm.aspx?R=Y",false);
    //}
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
                if (string.Format(Deffinity.systemdefaults.GetStringDateformat(), entity.StartDate) == "01/01/1900")
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

    #region Form 
   
    public void BindControls(int HealthCheckID)
    {
        try
        {
            bool Isfirsttime = false;
            if (ViewState["state"] == null)
            {
                ViewState["state"] = 1;
                Isfirsttime = true;
            }
            else
            {
                Isfirsttime = false;
            }
            HealthCheckBAL hbal = new HealthCheckBAL();
            var hcData = hbal.HealthCheckList_SelectByID(HealthCheckID);
            int formid = hcData.PortfolioHealthCheckID.Value;
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID, section);
            //start table
            Table tbl = new Table();
            tbl.EnableViewState = true;
            tbl.Style.Add("width", "98%");
            tbl.Style.Add("background-color", hform.FormBackColor);
            tbl.CssClass = "tblform";
            //check header is exists
            var pHeader = hpanels.Where(o => o.PanelName == "Header").FirstOrDefault();
            TableRow tr;
            TableCell td;
            TableHeaderRow th_row;
            TableHeaderCell th_cell;
            LiteralControl lc;
            Image img;
            Table pnltbl;
            RequiredFieldValidator rf;
            if (pHeader != null)
            {
                pnltbl = new Table();
                pnltbl.Style.Add("width", "100%");
                pnltbl.Style.Add("background-color", pHeader.PanelBackColor);
                pnltbl.CssClass = "tblheader";
                // var td = null;
                for (int row = 1; row <= pHeader.PanelRows; row++)
                {
                    tr = new TableRow();
                    var colCnt = pHeader.PanelColumns;
                    for (int col = 1; col <= pHeader.PanelColumns; col++)
                    {
                        td = new TableCell();
                        td.Style.Add("width", (100 / colCnt).ToString() + "%");
                        var cval = hcontrols.Where(o => o.PanelID == pHeader.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                        if (cval != null)
                        {

                            if (!string.IsNullOrEmpty(cval.ControlValue))
                            {
                                img = new Image();
                                img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                td.Controls.Add(img);
                            }
                            else
                            {
                                var lblHead = new Label();
                                lblHead.Text = string.Empty;
                                td.Controls.Add(lblHead);

                            }
                        }
                        //add image
                        tr.Cells.Add(td);

                    }
                    pnltbl.Rows.Add(tr);
                }
                // ph.Controls.Add(tbl);
                tr = new TableRow();
                td = new TableCell();
                td.Controls.Add(pnltbl);
                tr.Cells.Add(td);
                tbl.Controls.Add(tr);
            }

            //get list of existing panels
            var plist = hpanels.Where(o => o.PanelName != "Header" && o.PanelName != "Signature Panel").OrderBy(a => a.PnlPosition).ToList();
            foreach (var pnl in plist)
            {
                if (pnl != null)
                {
                    var LblPanel = new Label();
                    LblPanel.Font.Bold = true;
                    LblPanel.Text = pnl.PanelName;
                    LblPanel.Font.Size = 10;

                    pnltbl = new Table();
                    pnltbl.Style.Add("width", "100%");
                    pnltbl.Style.Add("background-color", pnl.PanelBackColor);
                    pnltbl.CssClass = "tblcontrol";
                    // var td = null;
                    th_row = new TableHeaderRow();
                    th_cell = new TableHeaderCell();
                    th_cell.HorizontalAlign = HorizontalAlign.Left;
                    th_cell.Controls.Add(LblPanel);
                    th_row.Cells.Add(th_cell);
                    pnltbl.Controls.Add(th_row);
                    for (int row = 1; row <= pnl.PanelRows; row++)
                    {
                        tr = new TableRow();
                        var colCnt = pnl.PanelColumns;
                        for (int col = 1; col <= pnl.PanelColumns; col++)
                        {
                            var cval = hcontrols.Where(o => o.PanelID == pnl.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();

                            if (cval != null)
                            {
                                var cdata = hControlValues.Where(o => o.ControlID == cval.ControlID).FirstOrDefault();
                                td = new TableCell();
                                td.Style.Add("width", (100 / colCnt).ToString() + "%");
                                lc = new LiteralControl(cval.ControlLabelName + " <br/>");
                                td.Controls.Add(lc);
                                //if (cval.TypeOfField.ToLower() == "textbox")
                                //{

                                //    td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), cval.DefaultText,(cdata != null ?cdata.ControlValue:string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}
                                //else if (cval.TypeOfField.ToLower() == "dropdown")
                                //{
                                //    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}
                                //else if (cval.TypeOfField.ToLower() == "checkbox")
                                //{
                                //    td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                //    tr.Cells.Add(td);
                                //}
                                if (cval.TypeOfField.ToLower() == "date")
                                {
                                    td.Controls.Add(GenerateTextboxDate(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    tr.Cells.Add(td);
                                }
                                if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    td.Controls.Add(GenerateTextbox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "dropdown")
                                {
                                    td.Controls.Add(GenerateDropDown(cval.ListValues, cval.ControlID.ToString(), cval.DefaultText, Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cdata.ControlValue));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_dropdown_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "checkbox")
                                {
                                    td.Controls.Add(GenerateCheckBox(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "textarea")
                                {
                                    td.Controls.Add(GenerateTextarea(cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    if (cval.Mandatory.HasValue ? cval.Mandatory.Value : false)
                                    {
                                        rf = Add_validation(cval);
                                        //txtid = txtid + 1;
                                        td.Controls.Add(rf);
                                    }
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "label")
                                {
                                    td.Controls.Add(GenerateLable(cval.ControlID.ToString(), cval.DefaultText, cval.ControlWidth, cval.Height, cval.ControlPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "image")
                                {
                                    if (cdata != null)
                                    {
                                        if (!string.IsNullOrEmpty(cdata.ControlValue))
                                        {
                                            img = new Image();
                                            img.ID = cdata.ControlValue.ToString();
                                            img.ImageUrl = "~/WF/UploadData/HC/" + cdata.ControlValue + ".png";
                                            img.Style.Add("width", "25%");
                                            img.Style.Add("height", "25%");
                                            td.Controls.Add(img);
                                       //   td.Controls.Add(GenerateFileupload(cval.ControlID.ToString()));
                                            tr.Cells.Add(td);
                                        }
                                    }
                                    else
                                    {
                                        img = new Image();
                                        img.ID = cval.ControlValue.ToString();
                                        img.ImageUrl = "~/WF/UploadData/HC/" + cval.ControlValue + ".png";
                                        img.EnableViewState = true;
                                        img.Style.Add("float", string.IsNullOrEmpty(cval.CblPosition) ? "left" : cval.CblPosition);
                                        img.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.Value.ToString() + "%" : "100%");
                                        td.Controls.Add(img);
                                        tr.Cells.Add(td);
                                    }
                                }
                                else if (cval.TypeOfField.ToLower() == "checkboxlist")
                                {
                                    td.Controls.Add(GenerateChecklistbox(cval.DefaultText, cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "radiobutton")
                                {
                                    td.Controls.Add(GenerateRadioBtnlistbox(cval.ListValues, cval.ControlID.ToString(), (cdata != null ? cdata.ControlValue : string.Empty), Isfirsttime, cval.ControlWidth, cval.ControlPosition, cval.Height, cval.CblPosition));
                                    tr.Cells.Add(td);
                                }
                                else if (cval.TypeOfField.ToLower() == "table")
                                {
                                    int colLength = cval.columnlist.Split(',').Length;
                                    int RowLength = cval.ListValues.Split(',').Length;
                                    td.Controls.Add(GenerateTable(row,col, cval.TypeofFieldInTbl, cval.ControlID.ToString(), cval.ControlWidth, cval.ListValues.ToString(), cval.columnlist.ToString(),cval.PanelID));
                                    tr.Cells.Add(td);
                                }
                                if (!string.IsNullOrEmpty(cval.Helptext))
                                {
                                    var span = new HtmlGenericControl("span");
                                    span.ID = "ImageToolTip" + cval.ControlID.ToString();
                                    span.Attributes["title"] = cval.Helptext;
                                    span.Attributes["class"] = "fa-info";
                                    span.Attributes["style"] = "padding-left:10px;font-size:medium;vertical-align:-webkit-baseline-middle";
                                    td.Controls.Add(span);
                                }
                            }
                        }
                        pnltbl.Rows.Add(tr);
                    }
                    tr = new TableRow();
                    td = new TableCell();

                    td.Controls.Add(pnltbl);
                    tr.Cells.Add(td);
                    tbl.Controls.Add(tr);
                }
            }
            //get signature panel
            var SignatureList = hpanels.Where(o => o.PanelName == "Signature Panel").ToList();
            foreach (var Signature in SignatureList)
            {
                if (Signature != null)
                {
                    pnltbl = new Table();
                    pnltbl.Style.Add("width", "100%");
                    pnltbl.Style.Add("background-color", Signature.PanelBackColor);
                    pnltbl.CssClass = "tblcontrol";
                    pnltbl.CellPadding = 8;
                    pnltbl.CellSpacing = 3;
                    for (int row = 1; row <= Signature.PanelRows; row++)
                    {
                        tr = new TableRow();
                        var colCnt = Signature.PanelColumns;
                        for (int col = 1; col <= Signature.PanelColumns; col++)
                        {
                            td = new TableCell();
                            td.Style.Add("width", (100 / colCnt).ToString() + "%");
                            var cval = hcontrols.Where(o => o.PanelID == Signature.PanelID && o.RowIndex == row && o.ColumnIndex == col).FirstOrDefault();
                            if (cval != null)
                            {
                                var Sdata = hControlValues.Where(o => o.ControlID == cval.ControlID).FirstOrDefault();
                                td.Controls.Add(GenerateLable(cval.ControlID.ToString(), cval.ControlLabelName, 10, 22, cval.ControlPosition));
                                tr.Cells.Add(td);
                                if (cval.TypeOfField.ToLower() == "textbox")
                                {
                                    if (!string.IsNullOrEmpty(cval.TypeOfField))
                                    {
                                        var txt = new TextBox();
                                        txt.ID = cval.ControlID.ToString();
                                        txt.Text = Sdata != null ? Sdata.ControlValue : string.Empty;
                                        if (Sdata != null)
                                        {
                                            if (Sdata.ControlValue != "")
                                            {
                                                txt.ReadOnly = true;
                                            }
                                        }
                                        txt.Style.Add("width", cval.ControlWidth.HasValue ? cval.ControlWidth.ToString() + "%" : "100%");
                                        txt.Style.Add("float", "left");
                                        td.Controls.Add(txt);
                                        tr.Cells.Add(td);
                                    }
                                }
                            }
                        }
                        pnltbl.Rows.Add(tr);
                    }
                    tr = new TableRow();
                    td = new TableCell();
                    td.Controls.Add(pnltbl);
                    tr.Cells.Add(td);
                    tbl.Controls.Add(tr);
                }
               
            }
            ph.Controls.Add(tbl);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }


    RadioButtonList Rbl;
    public RadioButtonList GenerateRadioBtnlistbox(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height, string Rblist)
    {
        Rbl = new RadioButtonList();
        try
        {
            Rbl.ID = id;
            //ddl.Width = 200;
            Rbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            Rbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            Rbl.RepeatLayout = RepeatLayout.Table;
            if (Rblist == "Horizontal")
            {
                Rbl.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                Rbl.RepeatDirection = RepeatDirection.Vertical;
            }
            int num1 = 2;

            if (int.TryParse(setvalue, out num1))
            {

            }
            Rbl.RepeatColumns = num1;
            //if (setvalue != string.Empty)
            //{
            //    chl.RepeatColumns = int.Parse(setvalue);
            //}
            //else
            //{
            //    chl.RepeatColumns = 2;
            //}
            Rbl.CellSpacing = 3;
            Rbl.CellPadding = 3;
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                Rbl.Items.Add(s);
            Rbl.EnableViewState = true;
            //chl.SelectedIndexChanged += ddl_SelectedIndexChanged;
            //Rbl.AutoPostBack = true;
            if (Isfirsttime && !string.IsNullOrEmpty(setvalue))
            {
                Rbl.Items.FindByValue(setvalue).Selected = true;
            }
            //ddlid = ddlid + 1;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Rbl;
    }


    public TextBox GenerateTextboxDate(string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.CssClass = "dateclass";
        txt.Style.Add("width", "121px");
        // txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }


    Label lbl1 = null;
    public Label GenerateLableSp(string id, string lblName, string ControlPosition, int? width, int? Height)
    {
        lbl1 = new Label();
        lbl1.ID = "lbl" + id.ToString();
        lbl1.Text = lblName;
        lbl1.EnableViewState = true;
        lbl1.Style.Add("float", string.IsNullOrEmpty(ControlPosition) ? "left" : ControlPosition);
        lbl1.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        //   lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        return lbl;
    }
    private static RequiredFieldValidator Add_validation(HealthCheckMgt.Entity.HealthCheck_FormControl cval)
    {
        RequiredFieldValidator rf;
        rf = new RequiredFieldValidator();
        rf.EnableViewState = true;
        rf.ControlToValidate = cval.ControlID.ToString();
        rf.ErrorMessage = "Please enter " + cval.ControlLabelName;
        rf.Text = "*";
        rf.ValidationGroup = "Form";
        return rf;
    }
    private static RequiredFieldValidator Add_dropdown_validation(HealthCheckMgt.Entity.HealthCheck_FormControl cval)
    {
        RequiredFieldValidator rf;
        rf = new RequiredFieldValidator();
        rf.EnableViewState = true;
        rf.ControlToValidate = cval.ControlID.ToString();
        rf.ErrorMessage = "Please select " + cval.ControlLabelName;
        rf.Text = "*";
        rf.InitialValue = "0";
        rf.ValidationGroup = "Form";
        return rf;
    }
    Label lbl = null;
    public Label GenerateLable(string id, string lblName, int? width, int? Height, string position)
    {
        lbl = new Label();
        lbl.ID = "lbl" + id.ToString();
        lbl.Text = lblName;
        lbl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
    //    lbl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        lbl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        lbl.EnableViewState = true;
        return lbl;
    }
    Table t = null;
    TableRow trow = null;
    TableCell tc = null;
    TableHeaderRow HeadR = null;
    TableHeaderCell thc = null;
    public Table GenerateTable(int? row, int? col, string typeoffieldInTbl, string id, int? width, string Rlist, string Clist, int? PanelId)
    {
        string[] Rstr = Rlist.Split(',').ToArray();
        string[] Cstr = Clist.Split(',').ToArray();
        int r = Rlist.Split(',').Length;
        int c = Clist.Split(',').Length;

        t = new Table();
        trow = new TableRow();
        tc = new TableCell();

        HeadR = new TableHeaderRow();
        for (int HList = 0; HList <= c; HList++)
        {
            if (HList != 0)
            {
                thc = new TableHeaderCell();
                var l = new Label();
                l.ID = HList.ToString() + "" + id.ToString();
                l.Text = Cstr[HList - 1].ToString();
                thc.Controls.Add(l);
                HeadR.Cells.Add(thc);
                t.Rows.Add(HeadR);
            }
            else
            {
                thc = new TableHeaderCell();
                var l1 = new Label();
                l1.ID = HList.ToString() + "" + id.ToString();
                l1.Text = "";
                thc.Controls.Add(l1);
                HeadR.Cells.Add(thc);
                t.Rows.Add(HeadR);
            }
        }
        HealthCheckBAL hbal = new HealthCheckBAL();
        var formid = Request.QueryString["PID"].ToString();

        var Tblcontrols = hbal.HealthCheck_FormControl_SelectByFormID(int.Parse(formid));
        var TblCntlList = Tblcontrols.Where(o => o.PanelID == PanelId && o.RowIndex == row && o.ColumnIndex == col).ToList();
        int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
        var TblControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(healthCheckListID, section);
        int count = 0;
        for (int i = 1; i <= r; i++)
        {
            trow = new TableRow();
            for (int j = 0; j <= c; j++)
            {
                if (j != 0)
                {
                    var TblCntldata = TblControlValues.Where(o => o.ControlID == TblCntlList[count].ControlID).FirstOrDefault();
                    tc = new TableCell();
                    if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "textbox")
                    {
                        var txt = new TextBox();
                        txt.ID = TblCntlList[count].ControlID.ToString();
                        txt.Text = TblCntldata != null ? TblCntldata.ControlValue : string.Empty;
                        txt.Style.Add("width", width.HasValue ? width.ToString() + "%" : "100%");
                        txt.Style.Add("float", "right");
                        tc.Controls.Add(txt);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "checkbox")
                    {
                        var checkBox = new CheckBox();
                        checkBox.ID = TblCntlList[count].ControlID.ToString();
                        checkBox.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
                        checkBox.Style.Add("float", "right");
                        tc.Controls.Add(checkBox);
                        trow.Cells.Add(tc);
                    }
                    else if (TblCntlList[count].TypeofFieldInTbl.ToLower() == "radiobutton")
                    {
                        var Rbtn = new RadioButton();
                        Rbtn.ID = TblCntlList[count].ControlID.ToString();
                        Rbtn.Checked = Convert.ToBoolean(TblCntldata != null ? TblCntldata.ControlValue : "false");
                        Rbtn.Style.Add("float", "right");
                        tc.Controls.Add(Rbtn);
                        trow.Cells.Add(tc);
                    }
                    count = count + 1;
                }
                else
                {
                    tc = new TableCell();
                    var lb = new Label();
                    lb.ID = i.ToString() + "" + j.ToString() + "" + id.ToString();
                    lb.Text = Rstr[i - 1].ToString();
                    tc.Controls.Add(lb);
                    trow.Cells.Add(tc);
                }
            }
            t.Rows.Add(trow);
        }
        return t;
    }
    Button Btn = null;
    public Button GenerateButton(string id)
    {
        
        Btn = new Button();
        Btn.ID = id.ToString();
        Btn.Text = "Upload";
       // Btn.OnClientClick = HCs.UploadImage();
        Btn.EnableViewState = true;
        return Btn;
    }
    FileUpload Fload = null;
    public FileUpload GenerateFileupload(string id)
    {
        Fload = new FileUpload();
        Fload.ID = "File" + id.ToString();
        Fload.EnableViewState = true;
        return Fload;
    }
    TextBox txt;
    public TextBox GenerateTextarea(string id, string setvalue, bool Isfirsttime, int? width, string position,int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "90%");
        txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.TextMode = TextBoxMode.MultiLine;
        txt.Height = 70;
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
       
        return txt;
    }
    public TextBox GenerateTextbox(string id, string setvalue, bool Isfirsttime, int? width, string position,int? Height)
    {
        txt = new TextBox();
        txt.ID = id;
        txt.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "90%");
       // txt.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        txt.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        txt.Text = setvalue;
        txt.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            txt.Text = setvalue;
        //txtid = txtid + 1;
        return txt;
    }
    CheckBoxList chl;
    public CheckBoxList GenerateChecklistbox(string DeafultColList,string Items, string id, string setvalue, bool Isfirsttime, int? width, string position,int? Height)
    {
        chl = new CheckBoxList();
        try
        {
            chl.ID = id;
            //ddl.Width = 200;
            chl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
            //  chl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
            chl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
            chl.RepeatLayout = RepeatLayout.Table;
            chl.RepeatDirection = RepeatDirection.Vertical;
            int num1 = 2;
           
            if (int.TryParse(DeafultColList, out num1))
            {
               
            }
            chl.RepeatColumns = num1;
            //if (DeafultColList != string.Empty)
            //{
            //    if (int.TryParse(DeafultColList))
            //    chl.RepeatColumns = int.type.Parse(DeafultColList);
            //}
            //else
            //{
            //    chl.RepeatColumns = 2;
            //}
            chl.CellPadding = 5;
            chl.CellSpacing = 3;
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
                chl.Items.Add(s);
            //set default values
            if (!string.IsNullOrEmpty(setvalue))
            {
                var srtsplit1 = setvalue.Split(',');
                var tcont = srtsplit1.Count();
                if (tcont > 0)
                {
                    var i = 0;
                    foreach (ListItem li in chl.Items)
                    {
                        if (i <= tcont)
                            li.Selected = Convert.ToBoolean(((srtsplit1[i] == null) ? "0" : srtsplit1[i]) == "1" ? "True" : "False");
                        i = i + 1;
                    }
                }
            }
            chl.EnableViewState = true;
            chl.SelectedIndexChanged += chk_SelectedIndexChanged;
            chl.AutoPostBack = true;
            if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            {
                //set updated values
                var srtsplit = setvalue.Split(',');
                var tcont1 = srtsplit.Count();
                if (srtsplit.Count() > 0)
                {
                    var j = 0;
                    foreach (ListItem li in chl.Items)
                    {
                        if (j <= tcont1)
                            li.Selected = Convert.ToBoolean(((srtsplit[j] == null) ? "0" : srtsplit[j]) == "1" ? "True" : "False");
                        j = j + 1;
                    }
                }
                //chl.SelectedValue = setvalue;
            }
            //ddlid = ddlid + 1;
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        return chl;
    }
    DropDownList ddl;
    public DropDownList GenerateDropDown(string Items, string id, string setvalue, bool Isfirsttime, int? width, string position, int? Height, string DropDownSelectdValue)
    {
        ddl = new DropDownList();
        ddl.ID = id;
        //ddl.Width = 200;
        ddl.Style.Add("width", width.HasValue ? width.Value.ToString() + "%" : "100%");
        ddl.Style.Add("float", string.IsNullOrEmpty(position) ? "left" : position);
        // ddl.Style.Add("height", Height.HasValue ? Height.Value.ToString() + "px" : "100px");
        if (string.IsNullOrEmpty(setvalue))
        {
            string[] str = Items.Split(',').ToArray();
            foreach (string s in str)
            {
                ddl.Items.Add(s);
            }
        }
        else
        {
            if (setvalue == "List of Project Managers")
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var ProjectManagersList = (from a in Udc.Contractors
                                               where (a.SID == 1 || a.SID == 2 || a.SID == 3) && (a.Status == "Active")
                                               select new
                                               {
                                                   Name = a.ContractorName,
                                                   Value = a.ID
                                               }).ToList();
                    ddl.DataSource = ProjectManagersList;
                }
            }
            else if (setvalue == "List of Customer Sites")
            {
                using (LocationDataContext Ldc = new LocationDataContext())
                {
                    var OurCustomerSitesList = (from a in Ldc.Sites
                                                where a.Visible == 'Y'
                                                select new
                                                {
                                                    Name = a.Site1,
                                                    Value = a.ID
                                                }).ToList();
                    ddl.DataSource = OurCustomerSitesList;
                }
            }
            else if (setvalue == "List of Our Sites")
            {
                using (DCDataContext Dc = new DCDataContext())
                {
                    var OurSitesList = (from a in Dc.OurSites
                                        select new
                                        {
                                            Name = a.Name,
                                            Value = a.ID
                                        }).ToList();
                    if (sessionKeys.PortfolioID > 0)
                    {
                        OurSitesList = (from a in Dc.OurSites
                                        where a.CustomerID == sessionKeys.PortfolioID
                                        select new
                                        {
                                            Name = a.Name,
                                            Value = a.ID
                                        }).ToList();
                    }
                    ddl.DataSource = OurSitesList;
                }
            }
            else if (setvalue == "List of Resources")
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    var ResourcesList = (from a in Udc.Contractors
                                         where (a.SID == 4 || a.SID == 9) && (a.Status == "Active")
                                         select new
                                         {
                                             Name = a.ContractorName,
                                             Value = a.ID
                                         }).ToList();
                    ddl.DataSource = ResourcesList;
                }
            }
            else if (setvalue == "List of Administrators")
            {
                UserDataContext Udc = new UserDataContext();
                //string[] AdministratorsList = Udc.Contractors.Where(a => a.SID == 1 || a.SID == 2 || a.SID == 3).Select(a=>a.ContractorName).ToArray();

                var AdministratorsList = (from a in Udc.Contractors
                                          where a.SID == 1 && a.Status == "Active"
                                          select new
                                          {
                                              Name = a.ContractorName,
                                              Value = a.ID
                                          }).ToList();
                ddl.DataSource = AdministratorsList;
            }
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        ddl.EnableViewState = true;
        ddl.SelectedIndexChanged += ddl_SelectedIndexChanged;
        //ddl.AutoPostBack = true;
        if (Isfirsttime && !string.IsNullOrEmpty(DropDownSelectdValue))
            ddl.Items.FindByText(DropDownSelectdValue).Selected = true;
        //ddlid = ddlid + 1;
        return ddl;
    }
    public void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dval = (DropDownList)sender;
        if (dval.SelectedIndex > 0)
        {
            string s = dval.SelectedValue;
        }
    }

    public void chk_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cval = (CheckBoxList)sender;
        if (cval.SelectedIndex > 0)
        {
            string s = cval.SelectedValue;
        }
    }

    CheckBox chk;
    public CheckBox GenerateCheckBox(string id, string setvalue, bool Isfirsttime)
    {
        chk = new CheckBox();
        chk.ID = id;
        //txt.Width = 200;
        //chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue)?"False":setvalue);
        chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue) ? "False" : setvalue);
        chk.EnableViewState = true;
        if (!Isfirsttime && !string.IsNullOrEmpty(setvalue))
            chk.Checked = Convert.ToBoolean(string.IsNullOrEmpty(setvalue) ? "False" : setvalue);
        //txtid = txtid + 1;
        return chk;
    }
    

    public void SavePlaceholderData(int HealthCheckID)
    {
        try
        {
            ViewState["state"] = "2";
            HealthCheckBAL hbal = new HealthCheckBAL();
            var hcData = hbal.HealthCheckList_SelectByID(HealthCheckID);
            int formid = hcData.PortfolioHealthCheckID.Value;
            var hform = hbal.HealthCheck_Form_SelectByID(formid);
            var hpanels = hbal.HealthCheck_FormPanel_SelectByFormID(formid);
            var hcontrols = hbal.HealthCheck_FormControl_SelectByFormID(formid);
            var hControlValues = hbal.HealthCheck_FormData_SelectByHealthCheckID(HealthCheckID,section);

            //pb = new ProjectAdditionalInfoBAL();
            //cb = new CustomFieldsBAL();
            //List<CustomField> clist = cb.CustomFields_SelectAll().ToList();

            foreach (HealthCheckMgt.Entity.HealthCheck_FormControl c in hcontrols.Where(o=>o.TypeOfField.ToLower() != "fileupload"))
            {
                var cVal = hControlValues.Where(p => p.ControlID == c.ControlID).FirstOrDefault();
                if (cVal == null)
                {
                    cVal = new HealthCheckMgt.Entity.HealthCheck_FormData();
                    cVal.HealthCheckID = HealthCheckID;

                    if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "date")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "dropdown")
                    {
                        var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                        if (ddl != null)
                        {
                            if (ddl.SelectedValue != "0")
                                cVal.ControlValue = ddl.SelectedItem.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "image")
                    {
                        var fl = ph.FindControl("File" + c.ControlID.ToString()) as FileUpload;
                        if (fl != null)
                        {
                            var Imagename = Guid.NewGuid().ToString();
                            if (fl.HasFile)
                            {
                                fl.SaveAs(Server.MapPath("~/WF/UploadData/HC" + "\\" + Imagename + ".png"));
                                cVal.ControlValue = Imagename.ToString();
                            }
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        var chk = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                        if (chk != null)
                        {
                            cVal.ControlValue = chk.Checked.ToString();
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkboxlist")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as CheckBoxList;
                        var chkValues = string.Empty;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                    chkValues = chkValues + "1,";
                                else
                                    chkValues = chkValues + "0,";
                            }
                            cVal.ControlValue = chkValues;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "radiobutton")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as RadioButtonList;
                        var chkValues = string.Empty;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                {
                                    chkValues = ci.Text;
                                }
                            }
                            cVal.ControlValue = chkValues;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "table")
                    {
                        if (c.TypeofFieldInTbl == "Textbox")
                        {
                            var txtInTbl = ph.FindControl(c.ControlID.ToString()) as TextBox;
                            if (txtInTbl != null)
                            {
                                cVal.ControlValue = txtInTbl.Text;
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "checkbox")
                        {
                            var chkInTbl = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                            if (chkInTbl != null)
                            {
                                cVal.ControlValue = chkInTbl.Checked.ToString();
                            }
                        }
                        //else if (c.TypeofFieldInTbl.ToLower() == "radiobutton")
                        //{
                        //    var RbInTbl = ph.FindControl(c.ControlID.ToString()) as RadioButton;
                        //    if (RbInTbl != null)
                        //    {
                        //        cVal.ControlValue = RbInTbl.Checked.ToString();
                        //    }
                        //}
                    }
                    cVal.ControlID = c.ControlID;
                    cVal.Section = section;
                    hbal.HealthCheck_FormData_Add(cVal);
                }
                else
                {
                    if (c.TypeOfField.ToLower() == "textbox" || c.TypeOfField.ToLower() == "textarea")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "date")
                    {
                        var txt = ph.FindControl(c.ControlID.ToString()) as TextBox;
                        if (txt != null)
                        {
                            cVal.ControlValue = txt.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "dropdown")
                    {
                        var ddl = ph.FindControl(c.ControlID.ToString()) as DropDownList;
                        if (ddl != null)
                        {
                            cVal.ControlValue = ddl.SelectedItem.Text;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "radiobutton")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as RadioButtonList;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                {
                                    cVal.ControlValue = ci.Text;
                                    break;
                                }
                            }
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "image")
                    {
                        var fl = ph.FindControl("File" + c.ControlID.ToString()) as FileUpload;
                        if (fl != null)
                        {
                            var Imagename = Guid.NewGuid().ToString();
                            if (fl.HasFile)
                            {
                                fl.SaveAs(Server.MapPath("~/WF/UploadData/HC" + "\\" + Imagename + ".png"));
                                cVal.ControlValue = Imagename.ToString();
                            }
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkbox")
                    {
                        var chk = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                        if (chk != null)
                        {
                            cVal.ControlValue = chk.Checked.ToString();
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "checkboxlist")
                    {
                        var chklist = ph.FindControl(c.ControlID.ToString()) as CheckBoxList;
                        var chkValues = string.Empty;
                        if (chklist != null)
                        {
                            foreach (ListItem ci in chklist.Items)
                            {
                                if (ci.Selected)
                                    chkValues = chkValues + "1,";
                                else
                                    chkValues = chkValues + "0,";
                            }
                            cVal.ControlValue = chkValues;
                        }
                    }
                    else if (c.TypeOfField.ToLower() == "table")
                    {
                        if (c.TypeofFieldInTbl.ToLower() == "textbox")
                        {
                            var txtInTbl = ph.FindControl(c.ControlID.ToString()) as TextBox;
                            if (txtInTbl != null)
                            {
                                cVal.ControlValue = txtInTbl.Text;
                            }
                        }
                        else if (c.TypeofFieldInTbl.ToLower() == "checkbox")
                        {
                            var chkInTbl = ph.FindControl(c.ControlID.ToString()) as CheckBox;
                            if (chkInTbl != null)
                            {
                                cVal.ControlValue = chkInTbl.Checked.ToString();
                            }
                        }
                        //else if (c.TypeofFieldInTbl.ToLower() == "radiobutton")
                        //{
                        //    var RbInTbl = ph.FindControl(c.ControlID.ToString()) as RadioButton;
                        //    if (RbInTbl != null)
                        //    {
                        //        cVal.ControlValue = RbInTbl.Checked.ToString();
                        //    }
                        //}
                    }
                    hbal.HealthCheck_FormData_update(cVal);
                }
            }
            //save the form to folder
            PrintAndSaveForm(HealthCheckID);
        }
        catch (Exception ex)
        { 
            LogExceptions.WriteExceptionLog(ex); 
        }

    }

    public void PrintAndSaveForm(int hid)
    {
        try
        {
            var wkhtmltopdfLocation = Server.MapPath("~/bin/") + "wkhtmltopdf.exe";
            var htmlUrl = string.Format(@"{0}/Health/HC/FormDataPreview.aspx?hcid=" + hid.ToString(),Deffinity.systemdefaults.GetWebUrl());
            var cfile = Server.MapPath("~/WF/UploadData/HC/HC") + hid.ToString() + ".pdf";
            if (System.IO.File.Exists(cfile))
            {
                System.IO.File.Delete(cfile);
            }

            var fpath = cfile+"\"";
            var pdfSaveLocation = "\"" + fpath;

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = wkhtmltopdfLocation;
                process.StartInfo.Arguments = htmlUrl + " " + pdfSaveLocation;
                process.Start();
                process.WaitForExit();
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            int healthCheckListID = Convert.ToInt32(Request.QueryString["HealthCheckID"]);
            SavePlaceholderData(healthCheckListID);
            Response.Redirect("Print.ashx?HealthCheckID=" + healthCheckListID);
            //string filename;
            //filename = Server.MapPath("~\\UploadData\\HC\\HC" + healthCheckListID.ToString() + ".pdf");
            //System.IO.FileInfo file = new System.IO.FileInfo(filename);
            //if ((file.Exists))
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.ContentType = "application/octet-stream";
            //    Response.WriteFile(file.FullName);
            //    Response.End();
            //    Response.Close();
            //    file = null;
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}
