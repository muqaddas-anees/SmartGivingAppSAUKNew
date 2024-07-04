using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Deffinity.ProjectMeetingManager;

using UserMgt.DAL;
using UserMgt.Entity;


using Microsoft.ApplicationBlocks.Data;
using System.IO;

public partial class CustomerProjectOverview : BasePage
{
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    Deffinity.GlobalIssues.Project objGlobal = new Deffinity.GlobalIssues.Project();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            SetCheckBoxes();
            BindMeetingData();
            BindResources(ddlIssueraisedBy);
            BindResources(ddlcheckedby);
            BindResources(ddlAssign);
            try
            {
                //lblTitle.InnerText = Deffinity.Bindings.DefaultDatabind.GetProjectTitle(QueryStringValues.Project);

                projectTaskDataContext pt = new projectTaskDataContext();

                ProjectDetails pd = (from p in pt.ProjectDetails
                                    where p.ProjectReference == QueryStringValues.Project
                                   select p).FirstOrDefault();

                lblTitle.InnerText = pd.ProjectTitle;
                lblEmail.InnerHtml = "<a href='mailto:" +pd.OwnerEmail +"' >"+pd.OwnerEmail+"</a>";
                lblOwner.InnerText =  pd.OwnerName;
              

            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        //Master.PageHead = Resources.DeffinityRes.MyProject;//"My Project";

        //add css dynamically
        //HtmlLink css = new HtmlLink();
        //css.Href = ResolveClientUrl("~/stylcss/ext-all.css");
        //css.Attributes["rel"] = "stylesheet";
        //css.Attributes["type"] = "text/css";
        ////css.Attributes["media"] = "all";
        //Page.Header.Controls.Add(css);
        
        GrdIssueBind(QueryStringValues.Project);
    }
    private void BindResources(DropDownList ddlResources)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo",
            new SqlParameter("@ProjectRefrence", 0),
               new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
        ddlResources.DataSource = dt;
        ddlResources.DataTextField = "ContractorName";
        ddlResources.DataValueField = "ID";
        ddlResources.DataBind();
        ddlResources.Items.Insert(0, new ListItem("Please select...", "0"));
        //int.Parse(string.IsNullOrEmpty(Request.QueryString["project"].ToString()) ? "0" : Request.QueryString["project"].ToString())
    }
    private void BindMeetingData()
     {
        GridMeetings.DataSource = ProjectMeetingManager.ProjectMeetingSelectByCustomer(sessionKeys.UID,QueryStringValues.Project);
        GridMeetings.DataBind();
    }
    protected string GetShortDescription(string Text, string ProjectRef, string meetingID)
    {
        string description = "";
        if (Text.Length > 200)
        {
            description = Text.ToString().Substring(0, 200) + "....<a href='WF/Reports/ProjectMeeting.aspx?meeting=" + meetingID + "&customer=0' Target='_blank'>Read More</a>..";
        }
        else
        {
            description = Text;
        }
        return description;
    }
    #region newcode for issues grid --- giri
    string _IssueSection = string.Empty;
    public string IssueSection
    {
        get { return _IssueSection; }
        set { _IssueSection = value; }
    }
    protected void gvBindIssues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.FindControl("lblSignOff") != null)
            {
                Label lblid = e.Row.FindControl("lblSignOff") as Label;
                if (lblid.Text == "0")
                {
                    
                }
            }
        }
        //if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
        //{

        //    gvBindIssues.Columns[10].Visible = false;
        //}
        //else
        //{
        //    gvBindIssues.Columns[10].Visible = true;
        //}

    }
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private void GrdIssueBind(int ProjectRef)
    {
        try
        {
            DbCommand cmd = db.GetStoredProcCommand("CustmrSectionProjIssues");
            db.AddInParameter(cmd, "@ProjectRef", DbType.Int32, QueryStringValues.Project);
            //db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
            
            DataSet ds = db.ExecuteDataSet(cmd);
            gvBindIssues.DataSource = ds;
            gvBindIssues.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected bool GetDocsEnable(string strStatus)
    {

        bool blnEnable = true;
        if (strStatus == "0")
        {
            blnEnable = false;
        }
        return blnEnable;

    }
    #endregion
    #region "Mail Alert-Sani-10thOct2011
    protected void imgIssuesSav_Click(object sender, EventArgs e)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var getLoginUser = (from r in projectDB.CustomerRagAlerts
                            where r.CustomerID == sessionKeys.UID
                            && r.ProjectReference == sessionKeys.Project
                            select r).ToList();
        if (getLoginUser != null)
        {
            if (getLoginUser.Count != 0)
            {
                CustomerRagAlert update = projectDB.CustomerRagAlerts.Single(P => P.CustomerID == sessionKeys.UID
                    && P.ProjectReference == sessionKeys.Project && P.Issue == 1);
                update.Issue = 0;
                projectDB.SubmitChanges();
            }
            else
            {
                CustomerRagAlert insert = new CustomerRagAlert();
                insert.ProjectReference = sessionKeys.Project;
                insert.Issue = 1;
                //insert.Issue = 0;
                insert.CustomerID = sessionKeys.UID;
                projectDB.CustomerRagAlerts.InsertOnSubmit(insert);
                projectDB.SubmitChanges();
            }
        }
    }
    protected void imgProjectUpdateSav_Click(object sender, EventArgs e)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var getLoginUser = (from r in projectDB.CustomerRagAlerts
                            where r.CustomerID == sessionKeys.UID
                            && r.ProjectReference == sessionKeys.Project
                            select r).ToList();
        if (getLoginUser != null)
        {
            if (getLoginUser.Count != 0)
            {
                CustomerRagAlert update = projectDB.CustomerRagAlerts.Single(P => P.CustomerID == sessionKeys.UID
                    && P.ProjectReference == sessionKeys.Project && P.ProjectType == 1);
                update.ProjectType = 0;
                projectDB.SubmitChanges();
            }
            else
            {
                CustomerRagAlert insert = new CustomerRagAlert();
                insert.ProjectReference = sessionKeys.Project;
                insert.ProjectType = 1;
                //insert.Issue = 0;
                insert.CustomerID = sessionKeys.UID;
                projectDB.CustomerRagAlerts.InsertOnSubmit(insert);
                projectDB.SubmitChanges();
            }
        }
    }
    public bool GetSignOffEnable(string status)
    {
        bool chk = true;
        if (status == "True")
        {
            chk = false;
        }
        return chk;
    }
    private void SetCheckBoxes()
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var getLoginUser = (from r in projectDB.CustomerRagAlerts
                            where r.CustomerID == sessionKeys.UID
                            && r.ProjectReference == sessionKeys.Project
                            select r).ToList().FirstOrDefault();
        if (getLoginUser != null)
        {
            if (getLoginUser.Issue == 0)
            {
                chkIssues.Checked = false;
            }
            else
            {
                chkIssues.Checked = true;
            }
            if (getLoginUser.ProjectType == 0)
            {
                chkProjectUpdate.Checked = false;
            }
            else
            {
                chkProjectUpdate.Checked = true;
            }
        }
    }
   
    protected void chkProjectUpdate_CheckedChanged(object sender, EventArgs e)
    {
        InsertUpdateMailAlert();
    }
   
    protected void chkIssues_CheckedChanged(object sender, EventArgs e)
    {
        InsertUpdateMailAlert();
    }
    private void InsertUpdateMailAlert()
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        var getLoginUser = (from r in projectDB.CustomerRagAlerts
                            where r.CustomerID == sessionKeys.UID
                            && r.ProjectReference == sessionKeys.Project
                            select r).ToList();
        if (getLoginUser != null)
        {
            if (getLoginUser.Count != 0)
            {
                CustomerRagAlert update = projectDB.CustomerRagAlerts.Single(P => P.CustomerID == sessionKeys.UID
                    && P.ProjectReference == sessionKeys.Project);
                update.ProjectType = int.Parse(chkProjectUpdate.Checked ? "1" : "0");
                update.Issue = int.Parse(chkIssues.Checked ? "1" : "0");
                projectDB.SubmitChanges();
            }
            else
            {
                CustomerRagAlert insert = new CustomerRagAlert();
                insert.ProjectReference = sessionKeys.Project;
                insert.ProjectType = int.Parse(chkProjectUpdate.Checked ? "1" : "0");
                insert.Issue = int.Parse(chkIssues.Checked ? "1" : "0");
                //insert.Issue = 0;
                insert.CustomerID = sessionKeys.UID;
                projectDB.CustomerRagAlerts.InsertOnSubmit(insert);
                projectDB.SubmitChanges();
            }
        }
    }

    protected string GetImage(string status)
    {
        string url = "";
        if (status == "1")
            url = "<Label style='color:green;'><i class='fa fa-circle'></i></Label>";
        if (status == "2")
            url = "<Label style='color:red;'><i class='fa fa-circle'></i></Label>";
        if (status == "3")
            url = "<Label style='color:yellow;'><i class='fa fa-circle'></i></Label>";
        return url;
    }
    protected string GetImageissues(string status)
    {
        string url = "";
        if (status == "3")
            url = "<Label style='color:green;'><i class='fa fa-circle'></i></Label>";
        //if (status == "2")
        //    url = "~/media/indcate_red.png";
        //if (status == "3")
        //    url = "~/media/indcate_amber.gif";
        return url;
    }
    protected bool SetVisible(string status)
    {
        bool vis = false;
        if (int.Parse(status) != 0)
        {
            vis = true;
        }
        return vis;
    }
    protected bool SetVisibleIss(string status)
    {
        bool vis = false;
        if (int.Parse(status) == 3)
        {
            vis = true;
        }
        return vis;
    }



    protected void GridMeetings_Sorting(object sender, GridViewSortEventArgs e)
    {
        //ProjectMeetingSelectAll(QueryStringValues.Project)
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridMeetings.PageIndex;
        GridMeetings.DataSource = SortDataTable(ProjectMeetingManager.ProjectMeetingSelectAll(QueryStringValues.Project), false);
        GridMeetings.DataBind();
        GridMeetings.PageIndex = pageIndex;
    }
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    #endregion
    #region set iframe url -project
    protected string RetUrl1()
    {
        string s = "";
        try
        {

            projectTaskDataContext _db = new projectTaskDataContext();
            var getDates = (from r in _db.Projects
                            where r.ProjectReference == int.Parse(Request.QueryString["Project"].ToString())
                            select r).ToList().FirstOrDefault();
            if (getDates != null)
            {
                string sDate = string.Format("{0:MM/dd/yyyy}", getDates.StartDate.Value);
                string eDate = string.Format("{0:MM/dd/yyyy}", getDates.ProjectEndDate.Value);
                string status = getDates.ProjectStatusID.ToString();
               // s = string.Format("ProjectNewGanttReadOnly.aspx?project={0}&start={1}&end={2}&status={3}", Request.QueryString["Project"], sDate, eDate, status);
                //s = string.Format("ProjectGanntNewReadOnly.aspx?ProjectReference={0}&start={1}&end={2}&status={3}", Request.QueryString["Project"], sDate, eDate, status);
                s = string.Format("/WF/Projects/Gantt/rindex.aspx?project={0}#en", Request.QueryString["Project"]);
            }
            

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return s;
    }
    #endregion
    protected void gvBindIssues_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Comments")
        {
               
                hfIssueID.Value = e.CommandArgument.ToString();
                BindCommentsGrid();
                popupComment.Show();
           
            
        }
         
        else if (e.CommandName == "cmdEdit")
        {

            DisplayCmdEdit(Convert.ToInt16(e.CommandArgument));

            popIssues1.Show();
            //btnAddnew.Visible = false;
            //lnkCancel.Visible = false;

        }
        else if (e.CommandName == "SignOff")
        {
            hfIssueID.Value = e.CommandArgument.ToString();
            lblDateTime.Text = DateTime.Now.ToString().Remove(16);
            popupSignOff.Show();
        }
        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void BindCommentsGrid()
    {
        using (projectTaskDataContext pi = new projectTaskDataContext())
        {
            using (UserDataContext ud = new UserDataContext())
            {
                int projectRef = sessionKeys.Project;
                var x = ud.Contractors.Select(c => c).ToList();
                var y = pi.ProjectIssueComments.Where(p => p.ProjectRef == projectRef && p.IssueRef == int.Parse(hfIssueID.Value)).Select(p => p).ToList();

                var result = (from p in y
                          join c in x on p.SubmitteBy equals c.ID
                          where p.ProjectRef == projectRef && p.IssueRef == int.Parse(hfIssueID.Value)
                          select new {p.ID,p.ProjectRef,p.IssueRef,p.Comments,p.CommentDate,c.ContractorName }).ToList();
                gvCustomerComments.DataSource = result;
                gvCustomerComments.DataBind();
                
            }
        }

    }
    protected void DisplayCmdEdit(int IssueIDArg)
    {



        //popIssues.Show();

        int IssueID = IssueIDArg;
        hidIssueID.Value = IssueID.ToString();
       
        DataTable dtIssues = objGlobal.SelectionWithID(IssueID);
        if (dtIssues.Rows.Count != 0)
        {
            lblRefID.Text = sessionKeys.Prefix + QueryStringValues.Project +"-" + IssueID;
            lblAssociatedTo.Visible = true;
            txtAssociatedTo.Visible = true;
            lblAssociatedTo0.Visible = false;
            ddlAssociatedTo.Visible = false;
            ddlProjectList.Visible = false;
            ddlHealthCheck.Visible = false;
            if (ddlAssociatedTo.Visible == true)
            {
                ddlAssociatedTo.SelectedItem.Text = HiddenIssueSection.Value;
            }

            txtDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dtIssues.Rows[0]["ScheduledDate"]);
            txtExpectedOutcome.Text = dtIssues.Rows[0]["ExpectedOutcome"].ToString();
            txtIssue.Text = dtIssues.Rows[0]["Issue"].ToString();
            txtNotes.Text = dtIssues.Rows[0]["Notes"].ToString();
            txtActionPlan.Text = dtIssues.Rows[0]["ActionPlan"].ToString();

            if (!string.IsNullOrEmpty(dtIssues.Rows[0]["IssueRaisedby"].ToString()))
            {
                ddlIssueraisedBy.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["IssueRaisedbyID"].ToString()) ? "0" : dtIssues.Rows[0]["IssueRaisedbyID"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(dtIssues.Rows[0]["AssignTo"].ToString()))
            {
                ddlAssign.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["AssignTo"].ToString()) ? "0" : dtIssues.Rows[0]["AssignTo"].ToString();
            }
            if (!string.IsNullOrEmpty(dtIssues.Rows[0]["CheckedBy"].ToString()))
            {
                ddlcheckedby.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["CheckedByID"].ToString()) ? "0" : dtIssues.Rows[0]["CheckedByID"].ToString().Trim();
            }

            ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(dtIssues.Rows[0]["Location"].ToString()));

            //ddllocation.SelectedItem.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["Location"].ToString()) ? "0" : dtIssues.Rows[0]["Location"].ToString();
            ddlStatus.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["Status"].ToString()) ? "0" : dtIssues.Rows[0]["Status"].ToString();
            ddlQAtype.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["IssuseType"].ToString()) ? "0" : dtIssues.Rows[0]["IssuseType"].ToString();
            txtdatechecked.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["DateChecked"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["DateChecked"].ToString()).ToShortDateString();
            txtDateCompleted.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["DateCompleted"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["DateCompleted"].ToString()).ToShortDateString();
            txtNextReviewDate.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["NextReviewDate"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["NextReviewDate"].ToString()).ToShortDateString();
            // txtAssociatedTo.Text = dtIssues.Rows[0]["ProjectTitleWithRef"].ToString();
            //Projectreference
            //txtAssociatedTo.Text = sessionKeys.Prefix + dtIssues.Rows[0]["ProjectReference"].ToString();
            ddlRagstatus.SelectedValue = "";
            ddlRagstatus.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["RAGStatus"].ToString()) ? "0" : dtIssues.Rows[0]["RAGStatus"].ToString();
            string val = string.IsNullOrEmpty(dtIssues.Rows[0]["EnableCust"].ToString()) ? "0" : dtIssues.Rows[0]["EnableCust"].ToString();
           
            if (hidIssueID.Value != "-99")
            {

                Panel_Insert.Visible = true;
                btnAddnew.Visible = true;
                lnkCancel.Visible = true;

                btnAddnew.CommandName = "Update";
                //btnAddnew.SkinID = "ImgUpdate";

                ddlAssociatedTo.Visible = false;
                ddlProjectList.Visible = false;
                ddlHealthCheck.Visible = false;
            }
            else
            {

                Panel_Insert.Visible = true;
                btnAddnew.Visible = true;
                lnkCancel.Visible = true;
                btnAddnew.CommandName = "Insert";
                //btnAddnew.SkinID = "ImgSubmit";

                txtAssociatedTo.Visible = false;
                lblAssociatedTo.Visible = false;
                ddlAssociatedTo.Visible = true;
                ddlProjectList.Visible = true;
            }
            //  if (HiddenIssueSection.Value.ToLower() == "project")
            if (dtIssues.Rows[0]["Projectreference"].ToString() != " ")
            {
                //lblAssociatedTo.Text = Resources.DeffinityRes.AssociatedtoProject;//"Associated to Project:";

            }
            else
            {
                lblAssociatedTo.Text = Resources.DeffinityRes.AssociatedtoAudit; //"Associated to Audit :";
            }

        }
    }
    public string Prefix()
    {
        return sessionKeys.Prefix;
    }
    protected void UpdateIssueClick()
    {
        try
        {
            DateTime asdate = Convert.ToDateTime(string.IsNullOrEmpty(txtdatechecked.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtdatechecked.Text.Trim());
            DateTime DateCompleted = Convert.ToDateTime(string.IsNullOrEmpty(txtDateCompleted.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtDateCompleted.Text.Trim());
            DateTime DateNextReviewDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtNextReviewDate.Text.Trim());
            int IssueId = Convert.ToInt32(hidIssueID.Value);

            string IssueSectionHidden = HiddenIssueSection.Value;
            int EnableCust = 0;
           
            object objResult = objGlobal.IssueUpdate(IssueId, txtIssue.Text.Trim(), Convert.ToInt32(ddlAssign.SelectedValue), Convert.ToDateTime(txtDate.Text.Trim()), txtNotes.Text.Trim(),
                Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlQAtype.SelectedValue),
                txtExpectedOutcome.Text.Trim(), DateCompleted, Convert.ToInt32(ddlIssueraisedBy.SelectedValue),
                DateNextReviewDate, EnableCust, int.Parse(ddlRagstatus.SelectedValue), txtActionPlan.Text.Trim());
           
            gvBindIssues.DataBind();




            //string file = System.IO.Path.GetFileName(fUploadUserImage.FileName);

           



            //clear the fieds
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        //only RAG Status update (customer only amend the RAG Status)
        using (projectTaskDataContext pi = new projectTaskDataContext())
        {
            int issueId = int.Parse(hidIssueID.Value);
            ProjectMgt.Entity.ProjectIssue pri = pi.ProjectIssues.Where(p => p.ID == issueId).FirstOrDefault();
            pri.RAGStatus = int.Parse(ddlRagstatus.SelectedValue);
            pi.SubmitChanges();
            
          
        }
       

        //else
        //{
        //    InsertIssueClick();

        //}

    }
    protected void btnIssueCancel_Click(object sender, EventArgs e)
    {

        popIssues1.Hide();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        using (projectTaskDataContext pi = new projectTaskDataContext())
        {
            ProjectIssueComment pc = new ProjectIssueComment();
            pc.CommentDate = DateTime.Now;
            pc.Comments = txtComments.Text;
            pc.ProjectRef = QueryStringValues.Project;
            pc.IssueRef = int.Parse(hfIssueID.Value);
            pc.SubmitteBy = sessionKeys.UID;
            pi.ProjectIssueComments.InsertOnSubmit(pc);
            pi.SubmitChanges();
            txtComments.Text = string.Empty;
            BindCommentsGrid();
            popupComment.Show();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        popupComment.Hide();
    }
    private void CustomerSignOffMail(int issueId)
    {
        try
        {
           
            if (sessionKeys.PortfolioID > 0)
            {
                using (projectTaskDataContext pt = new projectTaskDataContext())
                {
                    using (UserDataContext ud = new UserDataContext())
                    {
                        IssueCustomerSignOffMail1.Visible = true;
                       
                        ProjectDetails pd = (from p in pt.ProjectDetails
                                             where p.ProjectReference == QueryStringValues.Project
                                             select p).FirstOrDefault();

                        ProjectMgt.Entity.ProjectIssue pi = pt.ProjectIssues.Where(p => p.Projectreference == QueryStringValues.Project && p.ID == issueId).FirstOrDefault();
                        if (pi != null)
                        {
                            string name = ud.Contractors.Where(c => c.ID == pi.SignOffBy).Select(c => c.ContractorName).FirstOrDefault();
                            string projectTitle = pd.ProjectTitle;
                            string toMailId =  pd.OwnerEmail;//"dinesh.kalyan@emsysindia.com";
                            string ownerName = pd.OwnerName;
                            string subject = name + " has signed off Issue number:" + sessionKeys.Prefix + QueryStringValues.Project + "-" + pi.ID;
                           
                            IssueCustomerSignOffMail1.CustomerSignOffMail(issueId,ownerName,name,projectTitle,pi.Issue,pi.SignOffDate.ToString());
                            StringWriter stringWrite = new StringWriter();
                            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                            IssueCustomerSignOffMail1.RenderControl(htmlWrite);
                            Email ToEmail = new Email();
                            ToEmail.SendingMail(toMailId, subject, htmlWrite.InnerWriter.ToString());

                            // Save to Inbox 
                            InboxBAL.SaveInboxMessage(subject, Convert.ToInt32(pd.OwnerID), toMailId, htmlWrite.InnerWriter.ToString());

                        }
                    }
                }
               

            }

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        finally { IssueCustomerSignOffMail1.Visible = false; }
       
    }
    protected void btnSignOffSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (chkAccepted.Checked)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    int issueID = int.Parse(hfIssueID.Value);

                    ProjectMgt.Entity.ProjectIssue pi = pd.ProjectIssues.Where(p => p.ID == issueID).FirstOrDefault();
                    if (pi != null)
                    {
                        pi.SignOff = true;
                        pi.SignOffDate = DateTime.Now;
                        pi.SignOffBy = sessionKeys.UID;
                        pi.Status = 3;
                        pd.SubmitChanges();
                        CustomerSignOffMail(issueID);
                        GrdIssueBind(QueryStringValues.Project);
                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnSignOffCancel_Click(object sender, EventArgs e)
    {

    }
}
