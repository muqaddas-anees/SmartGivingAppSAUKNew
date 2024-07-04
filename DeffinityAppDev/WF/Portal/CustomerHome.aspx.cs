using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Deffinity.IssuesManager;
using Deffinity.ProjectMangers;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Deffinity.ProjectMeetingEntitys;
using Deffinity.ProjectMeetingManager;
using System.Linq;



public partial class CustomerHome : System.Web.UI.Page
{
    string chart_TaskStatus = string.Empty;
    string customer_sql = string.Empty;
    string User_sql = string.Empty;
    string Portfolio_sql = string.Empty;
    string Programme_sql = string.Empty;
    string  Portfolio = string.Empty;
    const string sortExpression = "SortExpression";
    const string sortDirection = "SortDirection";

    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

        //Master.PageHead = "Customer Portal";
        //Master.MasterRagLable = true;
        //Master.MasterRagPanel = true;
        SetSection = "customer";
        if (!Page.IsPostBack)
        {
            try
            {
                bindStatus();
                BindAssignedTask();
                BindIssues();
                
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText("Live"));
                // BindDatalists(Convert.ToInt32(ddlStatus.SelectedValue));
                DataSet ds = GetGridData(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue));
                bindgrid(Convert.ToInt32(ddlStatus.SelectedValue), ds);
                if (sessionKeys.IsCustomer)
                {
                    ddlCustomer.DataBind();
                    //ddlCustomer.SelectedIndex = ddlCustomer.Items.IndexOf(ddlCustomer.Items.FindByValue(sessionKeys.UID.ToString()));
                    ddlCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        string JavascriptBlock = @"
            <script type='text/javascript'>
            var checkedObject = null;
            function isChecked(what){
                if(checkedObject == null)
                {
                    checkedObject = what;
                    return true;
                }
                else{
                    if(what==checkedObject)
                    {
                        if(!what.checked)
                        {
                            checkedObject = null;
                            return true;
                        }
                    }
                    else{
                        alert('Please select one project');
                        return false;
                    }
                }       
            }
            </script>
        ";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "JavascriptBlock", JavascriptBlock);

    }

    public void bindStatus()
    {
        ddlStatus.Items.Clear();
        //ddlStatus.Items.Add(new ListItem("All", "0"));
        ddlStatus.Items.Add(new ListItem("Pending","1"));
        ddlStatus.Items.Add(new ListItem("Live","2"));
        ddlStatus.Items.Add(new ListItem("Archived", "5"));
       //ddlStatus.Items.Add(new ListItem("Complete(Requires QA)","3"));
        ddlStatus.Items.Add(new ListItem("Complete", "6"));
        ddlStatus.DataBind();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        int StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
        DataSet ds = GetGridData(StatusID, Convert.ToInt32(ddlCustomer.SelectedValue));
        bindgrid(StatusID, ds);
        BindDatalists(StatusID);
        ReturnStatus();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Binding the grids
   
    public void bindgrid(int StatusID,DataSet ds)
    {
        try
        {
            //If quote, bind the first grid
           // if (StatusID == 8)
           //{
                //GridView1.DataSource = ds;
                //GridView1.DataBind();
           //}
           //else
           //{
                GridView2.DataSource = ds;
                GridView2.DataBind();
           //}
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    private void bindGridFromDataView(int StatusID, DataView dv)
    {
        try
        {
            if (StatusID == 8)
            {
                //GridView1.DataSource = dv;
                //GridView1.DataBind();
            }
            else
            {
                GridView2.DataSource = dv;
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //If selected status is "Quote", hide the panel2 and show the panel1(quote) grid.

        //if (Convert.ToInt32(ddlStatus.SelectedValue) == 8)
        //{
        //    Panel1.Visible = true;
        //    Panel2.Visible = false;
        //}
        //else
        //{
        //    Panel2.Visible = true;
        //    Panel1.Visible = false;
        //}


        if (sessionKeys.SID == 7)
        {
            lblFiler.Visible = false;
            ddlCustomer.Visible = false;
        }
    }


    //Returns the dataset that is bindable to grid view
    private DataSet GetGridData(int StatusID,int resourceID)
    {
        SqlDataAdapter adp = new SqlDataAdapter("DN_CustomerProjects", con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //No need to pass the parameter of the customer id
       if (sessionKeys.SID != 7)
        adp.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Convert.ToInt32(ddlCustomer.SelectedValue);
       else
           adp.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = sessionKeys.UID;
        //If he is not a customer check portfolio wise
        adp.SelectCommand.Parameters.Add("@Portfolio", SqlDbType.Int).Value = sessionKeys.PortfolioID;
        adp.SelectCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = StatusID;
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int ID = Convert.ToInt32(e.CommandArgument.ToString());
    //    if (e.CommandName == "Edit")
    //        Response.Redirect("NewOrder.aspx?Project="+ID );
    //}

    private void BindDatalists(int status)
    {
        try
        {
            SqlDataAdapter adp = new SqlDataAdapter("DN_DisplayCustomerPage", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.Add("@status", SqlDbType.Int).Value = status;
            if(sessionKeys.SID != 7)
            adp.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = sessionKeys.UID;

            adp.SelectCommand.Parameters.Add("@Portfolio", SqlDbType.Int).Value = sessionKeys.PortfolioID;
            DataSet ds = new DataSet();
            adp.Fill(ds);
            //ListResources.DataSource = ds.Tables[0];
            //ListIsses.DataSource = ds.Tables[1];
            //ListCips.DataSource = ds.Tables[2];
            //ListRag.DataSource = ds.Tables[3];
            //Master.Red =ds.Tables[3].Rows[0][0].ToString();
            //Master.Amber = ds.Tables[3].Rows[0][1].ToString();
            //Master.Green = ds.Tables[3].Rows[0][2].ToString();
            //ListResources.DataBind();
            //ListIsses.DataBind();
            //ListCips.DataBind();
            //ListRag.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }

    protected string getIssueName(string status)
    {
        string stemp = string.Empty;
        if (string.IsNullOrEmpty(status))
        {
            stemp = string.Empty;
        }
        else
        {
            if (status == "1")
            { stemp = "QA"; }
            else
            { stemp = "H&S"; }
        }
        return stemp;
    }
    protected string ReturnDays(string days, string CR)
    {
        //string stemp = string.Empty;
        //if (CR != "")
        //{

        //    double val = Convert.ToDouble(days);
        //    if (val != 0)
        //    {
        //        //stemp = Math.Round(val, 1, MidpointRounding.AwayFromZero).ToString();
        //        stemp = Math.Round(val, 2).ToString();
        //    }
        //    else
        //    {
        //        stemp = "-";
        //    }
        //}
        //return stemp;


        //, string DDays
        string stemp = string.Empty;
        if (CR != "")
        {

            double val = Convert.ToDouble(days);
            if (val > 0.0)
            {
                stemp = Math.Round(val, 2).ToString();
            }
            else if (val < 0)
            {
                stemp = " ";
            }
            else
            {
                stemp = "-";
            }

        }
        return stemp;
    }
    protected System.Drawing.Color foreColor(string days)
    {
        System.Drawing.Color stemp = System.Drawing.Color.Gray;
        try
        {

            double val = Convert.ToDouble(days);
            if (val < 0)
            {
                stemp = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return stemp;
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int ProjectReference = Convert.ToInt32(e.CommandArgument.ToString());
            if (sessionKeys.PortfolioID != 0)
                Response.Redirect("CustomerServiceItems.aspx?Project=" + ProjectReference);
            else
                lblError.Text = "You dont have permissions to create service items";
        }
        if (e.CommandName == "CheckPoint")
        {
            int ProjectReference = Convert.ToInt32(e.CommandArgument.ToString());
            if (sessionKeys.PortfolioID != 0)
                Response.Redirect("CustomerCheckpointOverview.aspx?Customer=0&Project=" + ProjectReference);
        }
    }

    #region Sorting Helper Methods & Events

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataSet ds = GetGridData(Convert.ToInt32(ddlStatus.SelectedValue),sessionKeys.UID);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = getSortExpression(e.SortExpression);
        bindGridFromDataView(Convert.ToInt32(ddlStatus.SelectedValue), dv);
    }

    string getSortExpression(string sortExpn)
    {
        string finalSortExpression = string.Empty;

        if (ViewState[sortExpression] == null)
        {
            ViewState[sortExpression] = sortExpn;
            ViewState[sortDirection] = "ASC";
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

    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = GetGridData(Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue));
        bindgrid(Convert.ToInt32(ddlStatus.SelectedValue), ds);
    }

    //protected void Page_Error(object sender, EventArgs e)
    //{
    //    Exception ex = Server.GetLastError();
    //    LogExceptions.WriteExceptionLog(ex);
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<b><p>Message: " + ex.Message);
    //    sb.Append("</p><p>Target Site: " + ex.TargetSite);
    //    sb.Append("</p><p>Source: " + ex.Source);
    //    sb.Append("</p><p>Data: " + ex.Data);
    //    sb.Append("</p><p>Stack Trace: " + ex.StackTrace);
    //    sb.Append("</p></b>");
    //    Email eMail = new Email();
    //    ArrayList developerMailIDs = new ArrayList();
    //    developerMailIDs.Add("chandra.sekhar@emsysindia.com");
    //    developerMailIDs.Add("indra@emsysindia.com");
    //    developerMailIDs.Add("goverdhan@emsysindia.com");
    //    eMail.SendingMail(string.Empty, "Deffinity Exception - Customer Home", sb.ToString(), string.Empty, string.Empty, developerMailIDs);
    //    Response.Redirect("~/Message.aspx");
    //}
    protected int ReturnStatus()
    {
       return  Convert.ToInt32(ddlStatus.SelectedValue);
    }
    #region send an issue to project
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool Grid_Checked = false;
            int id = 0;
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chkNew = (CheckBox)row.FindControl("Chk_sendIssue");
                if (chkNew.Checked)
                {
                    id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                    //Insert into issues table
                    //IssuesManager.InsertFromCustomer(id, txtIssue.Text.Trim(), sessionKeys.UID);
                    
                    //sendMail
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Issue submitted to the project manager";
                    //clear the issue
                    //txtIssue.Text = string.Empty;
                    //un check the selected item
                    chkNew.Checked = false;
                    //update issue grid
                    BindIssues();
                    //send mail to project owner/PM
                    SendMail(id);
                    break;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Please select a project to update an issue";
                }
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    //mail
    private void SendMail(int projectreference)
    {
        ProjectIssue1.Visible = true;
        try
        {
            string name = string.Empty;
            string email = string.Empty;
            string projectStr = string.Empty;
            projectTaskDataContext pt = new projectTaskDataContext();
            ProjectDetails pd = new ProjectDetails();
            //SqlDataReader dr = ProjectManager.GetProjectDetails(projectreference);
            //while (dr.Read())
            //{
            //    name = dr["OwnerName"].ToString();
            //    email = dr["OwnerEmail"].ToString();
            //}
            //dr.Close();
            //dr.Dispose();
            pd = (from p1 in pt.ProjectDetails
                 where p1.ProjectReference == projectreference select p1).FirstOrDefault();
            name = pd.OwnerName;
            email = pd.OwnerEmail;
            projectStr = pd.ProjectReferenceWithPrefix;
            ProjectIssue1.BindData(IssuesManager.GetmaxID(projectreference),projectreference,name,true);
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ProjectIssue1.RenderControl(htmlWrite);
            Email ToEmail = new Email();
            ToEmail.SendingMail(email, "An issue has been raised against project " +projectStr, htmlWrite.InnerWriter.ToString());
            // Save to Inbox 
            InboxBAL.SaveInboxMessage("An issue has been raised against project " + projectStr, Convert.ToInt32(pd.OwnerID), email, htmlWrite.InnerWriter.ToString());
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally { ProjectIssue1.Visible = false; }
        
    }

    #endregion
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox c1 = (CheckBox)e.Row.Cells[0].FindControl("Chk_sendIssue");
            c1.Attributes.Add("onclick", "return isChecked(this)");
        }
    }

    #region Added new on 20th Jan 2011
    private void BindAssignedTask()
    {
        try
        {
            string str = " SELECT V_ProjectDetails.ProjectReferenceWithPrefix,pti.ID,pti.ItemStatus as StatusID , V_ProjectDetails.ProjectReference, ItemDescription AS TaskTitle, ProjectStartDate, " +
                        " pti.ProjectEndDate, ItemStatus.Status as ItemStatus, pti.Notes FROM ProjectTaskItems pti INNER JOIN" +
                        " V_ProjectDetails ON pti.ProjectReference = V_ProjectDetails.ProjectReference INNER JOIN ItemStatus ON " +
                        " pti.ItemStatus = ItemStatus.ID  where isnull(ViewCustomer,0) = 1 and pti.ProjectReference in" +
                        " (select ProjectReference from ProjectItems where ContractorID = " + sessionKeys.UID + " )";

            if (sessionKeys.SID == 7)
                str = str + getCustomersql() + getLiveProjectsql();
            if (sessionKeys.UID > 0 && sessionKeys.SID != 7)
                str = str + getUsersql() + getPortfoliosql() + getProgrammesql() + getLiveProjectsql();

            str = str + " order by TaskTitle";
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str).Tables[0];
            grdAssignedTask.DataSource = dt;
            grdAssignedTask.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdAssignedTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAssignedTask.PageIndex = e.NewPageIndex;
        BindAssignedTask();
    }
    protected void grdAssignedTask_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdAssignedTask.EditIndex = -1;
        BindAssignedTask();
    }
    #region Property
    private string section = string.Empty;
    public string SetSection
    {
        get { return section; }
        set { section = value; }
    }


    #endregion
    protected void grdAssignedTask_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                if (SetSection == "customer")
                    Response.Redirect("CustomerProjectOverview.aspx?Project=" + e.CommandArgument.ToString());
                else
                    Response.Redirect("ProjectOverviewV2.aspx?Project=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Update")
            {
                int i = grdAssignedTask.EditIndex;
                GridViewRow Row = grdAssignedTask.Rows[i];


                DropDownList ddlStatus = (DropDownList)Row.FindControl("ddlStatus");
                string str = "update ProjectTaskItems set ItemStatus=" + int.Parse(ddlStatus.SelectedValue) +
                    " where ID=" + Convert.ToInt32(e.CommandArgument.ToString());

                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, str);
                grdAssignedTask.EditIndex = -1;
                BindAssignedTask();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindStatus(DropDownList ddlStatus, int setvalue)
    {
        try
        {
            string str = " select * from  ItemStatus where ID in(1,2,3) order by Status";

            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str).Tables[0];
            ddlStatus.DataSource = dt;
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = setvalue.ToString();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdAssignedTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.FindControl("ddlStatus") != null)
                {
                    Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    BindStatus(ddlStatus, int.Parse(lblStatusID.Text));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdAssignedTask_RowEditing(object sender, GridViewEditEventArgs e)
    {

        grdAssignedTask.EditIndex = e.NewEditIndex;
        BindAssignedTask();
    }
    protected void grdAssignedTask_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    private string getCustomersql()
    {
        customer_sql = string.Format(" AND V_ProjectDetails.ProjectReference in (select ProjectReference from Customer_ProjectPermissions where UserId = {0}) ", sessionKeys.UID.ToString());
        return customer_sql;
    }
    private string getUsersql()
    {
        //if (sessionKeys.SID == 1)
        //    User_sql = " AND V_ProjectDetails.ProjectReference >0 ";
        //else
            User_sql = string.Format(" AND V_ProjectDetails.ProjectReference in (select ProjectReference from ProjectItems where ContractorID = {0}) ", sessionKeys.UID.ToString());

        return User_sql;
    }
    private string getPortfoliosql()
    {
        //if (sessionKeys.SID == 1)
        //    Portfolio_sql = " And V_ProjectDetails.Portfolio >0 ";
        //else
            Portfolio_sql = sessionKeys.PortfolioID > 0 ? string.Format(" And V_ProjectDetails.Portfolio = {0} ", sessionKeys.PortfolioID) : " And V_ProjectDetails.Portfolio >0 ";

        return Portfolio_sql;
    }
    private string getProgrammesql()
    {
        //if (ddlProgramme.SelectedValue == "0")
            Programme_sql = "";
       // else
         //   Programme_sql = string.Format(" And V_ProjectDetails.OwnerGroupID = {0}", ddlProgramme.SelectedValue);

        return Programme_sql;
    }
    private string getLiveProjectsql()
    {
        return string.Format(" And lower(V_ProjectDetails.ProjectStatusName)='live'"); ;
        //return string.Empty;
    }
    private void BindIssues()
    {
        string str = " SELECT ProjectIssues.ID IssueID, V_ProjectDetails.ProjectReferenceWithPrefix ,V_ProjectDetails.ProjectReference, V_ProjectDetails.ProjectTitle," +
                      " V_ProjectDetails.ProjectStatusName,ProjectIssues.Issue FROM  ProjectIssues INNER JOIN " +
                     " V_ProjectDetails ON ProjectIssues.Projectreference = V_ProjectDetails.ProjectReference" +
                     " WHERE  isnull(ViewCustomer,0) = 1 and  (LOWER(ProjectIssues.IssueSection) = 'project')   ";

        if (sessionKeys.SID == 7)
            str = str + getCustomersql() + getLiveProjectsql();
        if (sessionKeys.UID > 0 && sessionKeys.SID != 7)
            str = str + getUsersql() + getPortfoliosql() + getProgrammeList()+ getLiveProjectsql();

        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str.Replace("Table", "")).Tables[0];
        grdIssues.DataSource = dt;
        grdIssues.DataBind();
    }
    protected void grdIssues_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                Control ctrl = e.CommandSource as Control;

                if (ctrl != null)
                {

                    GridViewRow _currenrtrow = ctrl.Parent.NamingContainer as GridViewRow;
                    h_project.Value = (_currenrtrow.FindControl("hpid") as HiddenField).Value;
                }
                h_issue.Value = e.CommandArgument.ToString();
                //hpid
                ProjectIssue issue = ProjectMgt.BAL.ProjectIssuesBAL.ProjectIssues_selectByID(Convert.ToInt32(h_issue.Value));
                BindIssueData();
                txtIssueTitle.Text = issue.Issue;
                txtIssueTitle.ReadOnly = true;
                txtDateLogged.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), issue.ScheduledDate.HasValue ? issue.ScheduledDate.Value : DateTime.Now);
                txtDateLogged.ReadOnly = true;
                ddlIssueLocation.DataBind();
                ddlIssueLocation.SelectedIndex = ddlIssueLocation.Items.IndexOf(ddlIssueLocation.Items.FindByValue(issue.Location.HasValue ? issue.Location.Value.ToString() : "0"));
                ddlIssueLocation.Enabled = false;
                ddlStatus.DataBind();
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(issue.Status.HasValue ? issue.Status.Value.ToString() : "0"));
                ddlStatus.Enabled = false;
                ddlRag.SelectedIndex = ddlRag.Items.IndexOf(ddlRag.Items.FindByValue(issue.RAGStatus.HasValue ? issue.RAGStatus.Value.ToString() : "0"));
                ddlRag.Enabled = false;
                mdlIssue.Show();
                btnSubmitIssue.Visible = false;
                //if (SetSection == "customer")
                //    Response.Redirect("CustomerProjectOverview.aspx?Project=" + e.CommandArgument.ToString());
                //else
                //    Response.Redirect("ProjectOverviewV2.aspx?Project=" + e.CommandArgument.ToString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIssues.PageIndex = e.NewPageIndex;
        BindIssues();
    }
    private DataTable getProgrammeList()
    {
        string customer = sessionKeys.PortfolioID > 0 ? "= " + sessionKeys.PortfolioID.ToString() : ">0";
        string str = "select id,OperationsOwners as Programme from OperationsOwners where Level = 1  and PortfolioID" + customer + " order by OperationsOwners";
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str).Tables[0];
    }
    #endregion
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    private void progressBar(Label lblTxt,decimal Percentage)
    {
        string s="";
        lblTxt.Text="<TABLE cellspacing='0' cellpadding='0' border='1' width='200px' ID='Table1'><TR><TD bgcolor='#000066' width='" + Percentage.ToString() + "%'> </TD><TD bgcolor='#FFF7CE'></TD></TR></TABLE>";
       //return s;
    }
    protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProgress = (Label)e.Row.FindControl("lblProgress");
                Label lblDays = (Label)e.Row.FindControl("lblDays");
                Label lblPer = (Label)e.Row.FindControl("lblPer");
                Label lblDDays = (Label)e.Row.FindControl("lblDDays");
                Label lblTotalHrs = (Label)e.Row.FindControl("lblTotalDays");
                Label lblCR = (Label)e.Row.FindControl("lblCR");
                if (lblDays.Text != "-")
                {
                    SetProgress(lblProgress, Math.Round(Convert.ToDecimal(lblPer.Text), 2), Convert.ToDouble(lblDays.Text), Convert.ToDouble(lblDDays.Text), Convert.ToDouble(lblTotalHrs.Text), lblCR.Text);
                }



            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string ReturnDays(string days, string CR, string DDays, string totalHours)
    {
        string stemp = string.Empty;
        try
        {
            //, string DDays

            if (CR != "")
            {

                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (Convert.ToDouble(DDays) == 0)
                {
                    stemp = " ";
                }
                else if (Convert.ToDouble(DDays) < Convert.ToDouble(totalHours))
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else
                {
                    stemp = "-";
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return stemp;

    }
     public void SetProgress(Label lblProgress, decimal percent, double days, double ddays, double totldays,string CR)
    {
        if (CR != "")
        {
            if (ddays == 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>Setup&nbsp;Required</TD</TR></TABLE>";
            }
            if (ddays < totldays && days < 0 && ddays != 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>0%</TD</TR></TABLE>";
            }

            if (days != 0 && days > 0)
            {
                if (percent == 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD bgcolor=#FFF7CE width=100% align=center>100%</TD</TR></TABLE>";
                }
                if (percent >= 50)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD  bgcolor=#99FF66 width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 49 && percent > 10)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD  bgcolor=#FFCC00  width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 10 && percent > 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD bgcolor=red width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
            }
        }

        

    }
   protected bool POVisible(string val)
   {
       bool visible = false;
       if (int.Parse(val) != 0)
       {
           visible = true;
       }

       return visible;
   }
   protected bool CheckPointVisible(string projectRef)
   {
       bool visible = false;
       try
       {
           using (projectTaskDataContext pd = new projectTaskDataContext())
           {
               ProjectTaskItem projectTaskItem = pd.ProjectTaskItems.Where(p => p.ProjectReference == int.Parse(projectRef) && p.QA.ToLower() == "y" && p.isMilestone == true && p.CompletionDate < DateTime.Now && p.ItemStatus != 3 && p.AssignedToCustomerUser == sessionKeys.UID).FirstOrDefault();
               if (projectTaskItem != null)
               {
                   visible = true;
               }
           }

       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
       return visible;
   }
   #region Issue

   public void BindIssueData()
   {
       ddlIssueStatus.DataBind();
       if (ddlIssueStatus.Items.Count > 1)
       {
           ddlIssueStatus.Items.RemoveAt(0);
           //ddlIssueStatus.Items.Insert(0,new ListItem("Please select","0"));
       }

       ddlIssueLocation.DataBind();
       ddlIssueLocation.Items.Insert(0, new ListItem("Please select", "0"));
   }

   protected void btnSubmitIssue_Click(object sender, EventArgs e)
   {
       try
       {
           //bool Grid_Checked = false;
           int id = 0;
           //foreach (GridViewRow row in GridView2.Rows)
           //{
               //CheckBox chkNew = (CheckBox)row.FindControl("Chk_sendIssue");
           if (!string.IsNullOrEmpty(h_project.Value))
               {
                   id = Convert.ToInt32(h_project.Value); //Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                   //Insert into issues table
                   IssuesManager.InsertFromCustomer(id, txtIssueTitle.Text.Trim(), sessionKeys.UID, Convert.ToInt32(ddlIssueStatus.SelectedValue), Convert.ToInt32(ddlIssueLocation.SelectedValue), Convert.ToDateTime(txtDateLogged.Text.Trim()), Convert.ToInt32(ddlRag.SelectedValue));

                   //sendMail
                   lblMsg.Visible = true;
                   lblMsg.ForeColor = System.Drawing.Color.Green;
                   lblMsg.Text = "Issue submitted to the project manager";
                   //clear the issue
                   //txtIssue.Text = string.Empty;
                   //un check the selected item
                   //chkNew.Checked = false;
                   //update issue grid
                   BindIssues();
                   //send mail to project owner/PM
                   SendMail(id);
                   //break;
               }
               else
               {
                   lblMsg.Visible = true;
                   lblMsg.ForeColor = System.Drawing.Color.Red;
                   lblMsg.Text = "Please select a project to update an issue";
               }
           }
      // }
       catch (Exception ex)
       { LogExceptions.WriteExceptionLog(ex); }
   }
   protected void btnRaiseIssue_Click(object sender, EventArgs e)
   {
       bool isChecked = false;
       foreach (GridViewRow row in GridView2.Rows)
       {
           CheckBox chkNew = (CheckBox)row.FindControl("Chk_sendIssue");
           if (chkNew.Checked)
           {
               isChecked = true;
               h_project.Value = ((HiddenField)row.FindControl("HID")).Value;
               lit_projectTitle.Text = sessionKeys.Prefix + h_project.Value;
               txtDateLogged.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
               BindIssueData();
               mdlIssue.Show();
           }
       }

       if (!isChecked)
       {
           lblMsg.Visible = true;
           lblMsg.ForeColor = System.Drawing.Color.Red;
           lblMsg.Text = "Please select a project to raise an issue";
       }

   }
   protected void btnIssueCancel_Click(object sender, EventArgs e)
   {
       txtIssueTitle.Text = string.Empty;
       txtDateLogged.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
       txtDateLogged.ReadOnly = false;
       txtIssueTitle.ReadOnly = false;
       ddlIssueLocation.SelectedIndex = 0;
       ddlStatus.SelectedIndex = 0;
       ddlIssueLocation.Enabled = true;
       ddlStatus.Enabled = true;
       ddlRag.SelectedIndex = 0;
       ddlRag.Enabled = true;
       btnSubmitIssue.Visible = true;
       mdlIssue.Hide();
   }
   #endregion
  
}
