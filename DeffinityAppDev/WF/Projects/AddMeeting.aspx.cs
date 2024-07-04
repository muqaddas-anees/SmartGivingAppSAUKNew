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
using Deffinity.ProjectMeetingManager;
using Deffinity.ProjectMeetingEntitys;
using Deffinity.ProjectTasksManagers;
using Deffinity.IssuesManager;
using Deffinity.Bindings;
using Deffinity.Project;
using Deffinity.ProgrammeManagers;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using UserMgt.DAL;
using UserMgt.Entity;
using Deffinity.ServiceCatalogManager;
using System.Text;
using System.IO;
using System.Net.Mail;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using System.Linq;
public partial class AddMeeting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Update";
        if (!IsPostBack)
        {
            //default Bindinds
            BindDdl();
            BindGrid();
            if (QueryStringValues.Meeting > 0)
            {
                //select meeting
                SelectMeeting(QueryStringValues.Meeting);
            }
            else
            {
                defaultFieldsValue();
            }
            //lblTitle.InnerText = "Project Update";
            CheckUserRole();
        }
    }
    private void SelectMeeting(int MeetingId)
    {
        ProjectMeetingEntity ProjectMeeting = new ProjectMeetingEntity();
        ProjectMeeting = ProjectMeetingManager.ProjectMeetingSelect(MeetingId);

        txtAttendees.Text = ProjectMeeting.Attendees;
        txtDate.Text = ProjectMeeting.MeetingDate;
        txtGeneralNotes.Text = ProjectMeeting.GeneralNotes;
        txtKeyAchievements.Text = ProjectMeeting.KeyAchievements;
        txtKeyTasks.Text = ProjectMeeting.KeyTasks;
        txtLessonLearnt.Text = ProjectMeeting.LessonsLearnt;
        //txtLocation.Text = ProjectMeeting.Location;
        txtTime.Text = ProjectMeeting.MeetingTime;
        chkVisibletoCustomer.Checked = ProjectMeeting.VisibletoCustomer;
        ddlRagAlert.SelectedValue = ProjectMeeting.RAGStatus.ToString();
    }
    private void defaultFieldsValue()
    {        
        txtDate.Text = DateTime.Now.Date.ToShortDateString();
        txtTime.Text = DateTime.Now.ToShortTimeString();

    }
    private void BindDdl()
    {
        //Bind drop down
        ddlRagStatus.DataSource = DefaultDatabind.b_RagStatus();
        ddlRagStatus.DataBind();
    }
    private void BindGrid()
    {
        try
        {
            //Bind grid
            GridTasks.DataSource = ProjectTasksManager.ProjectTaskList_SelectAll(QueryStringValues.Project, false, ddlRagStatus.SelectedValue);
            GridTasks.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGridBaseonRag()
    {
        try
        {
            //Bind grid
            GridTasks.DataSource = ProjectTasksManager.ProjectTaskList_SelectAll(QueryStringValues.Project, false, ddlRagStatus.SelectedValue);
            GridTasks.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected string lblResource(string Resource)
    {
        string res = "";
        if (Resource.Length > 0)
        {
            res = Resource.Substring(0, Resource.Length - 1);
        }


        return res;
    }
    protected string getItemDes( string indentLevel,string Description)
    {
        return ProjectTasksManager.DisplayIndentLevel(Description, int.Parse(string.IsNullOrEmpty(indentLevel)?"0":indentLevel));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //insert meeting
        InsertMeeting();
        Inser_UpdateLessonLearnt();
        Response.Redirect("~/WF/Projects/ProjectMeetings.aspx?Project="+ QueryStringValues.Project);
    }
    private void Inser_UpdateLessonLearnt()
    {
        SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference",QueryStringValues.Project),
                                                   new SqlParameter("@Description", txtLessonLearnt.Text),
                                                   new SqlParameter("@RemediationActions", ""),
                                                   new SqlParameter("@BusinessImpact",""),
                                                   new SqlParameter("@IdentifiedBy", sessionKeys.UID),
                                                   new SqlParameter("@AssignedTo",sessionKeys.UID),
                                                   new SqlParameter("@Status",1)
                                                    };

         SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_LessonsLearntInsert", sqlParams);
    }
    private void InsertMeeting()
    {
        try
        {
            string textKeyTask = txtKeyTasks.Text.Replace('<',' ');
            textKeyTask = textKeyTask.Replace('>', ' ');

            string textGeneralNotes = txtGeneralNotes.Text.Replace('<', ' ');
            textGeneralNotes = textGeneralNotes.Replace('>', ' ');

            string lessonLearnt = txtLessonLearnt.Text.Replace('<', ' ');
            lessonLearnt = lessonLearnt.Replace('>', ' ');

            string achivemnet = txtKeyAchievements.Text.Replace('<', ' ');
            achivemnet = achivemnet.Replace('>', ' ');
            ProjectMeetingEntity ProjectMeeting = new ProjectMeetingEntity();
            if (QueryStringValues.Meeting == 0)
            {
                ProjectMeeting.ID = 0;
            }
            else
            {
                ProjectMeeting.ID = QueryStringValues.Meeting;
            }
            ProjectMeeting.ProjectReference = QueryStringValues.Project;
            ProjectMeeting.Attendees = txtAttendees.Text.Trim();
            ProjectMeeting.GeneralNotes = textGeneralNotes;// txtGeneralNotes.Text;
            ProjectMeeting.LessonsLearnt = lessonLearnt;// txtLessonLearnt.Text.Trim();
            //ProjectMeeting.Location = txtLocation.Text.Trim();
            ProjectMeeting.MeetingDate = txtDate.Text.Trim();
            ProjectMeeting.MeetingTime = txtTime.Text.Trim();
            ProjectMeeting.KeyAchievements = achivemnet;// txtKeyAchievements.Text.Trim();
            ProjectMeeting.KeyTasks = textKeyTask;// txtKeyTasks.Text.Trim();
            ProjectMeeting.VisibletoCustomer = chkVisibletoCustomer.Checked ? true : false;
            ProjectMeeting.RAGStatus = int.Parse(ddlRagAlert.SelectedValue);
            int idVal=ProjectMeetingManager.ProjectMeetingSave(ProjectMeeting);
            if (QueryStringValues.Meeting == 0)
            {
                if (chkVisibletoCustomer.Checked == true)
                {
                    MailSection(idVal);
                }
            }
            else
            {
                ProjectMeeting.ID = QueryStringValues.Meeting;
                if (chkVisibletoCustomer.Checked == true)
                {
                    MailSection(ProjectMeeting.ID);
                }
            }
          
            //if any issue is raised
            //if (!string.IsNullOrEmpty(txtRaiseIssue.Text.Trim()))
            //{
            //    IssuesManager.InsertIssue(QueryStringValues.Project, txtRaiseIssue.Text.Trim(), DateTime.Parse(!string.IsNullOrEmpty(txtResolutionDate.Text.Trim()) ? DateTime.Now.Date.ToString() : txtResolutionDate.Text.Trim()));
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GridTasks_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridTasks.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GridTasks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int id = int.Parse(GridTasks.DataKeys[e.RowIndex].Values[0].ToString());
            Task taskentity = new Task();
            taskentity= ProjectTasksManager.ProjectTaskList_Select(id);
            //update   
            taskentity.ID = id;
            taskentity.ItemDescription = ((TextBox)GridTasks.Rows[e.RowIndex].FindControl("txtTask")).Text.Trim();
            taskentity.ProjectStartDate = ((TextBox)GridTasks.Rows[e.RowIndex].FindControl("txtStartDate")).Text.Trim();
            taskentity.ProjectEndDate = ((TextBox)GridTasks.Rows[e.RowIndex].FindControl("txtEndDate")).Text.Trim();
            taskentity.Notes = ((TextBox)GridTasks.Rows[e.RowIndex].FindControl("txtNotes")).Text.Trim();
            taskentity.ItemStatus = int.Parse(((DropDownList)GridTasks.Rows[e.RowIndex].FindControl("ddlStatus")).SelectedValue);
            ProjectTasksManager.ProjectTaskItemUpdate(taskentity);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        GridTasks.EditIndex = -1;
        BindGrid();
    }
    protected void GridTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];

                if (e.Row.FindControl("txtTask") != null)
                {
                    TextBox txtTask = e.Row.FindControl("txtTask") as TextBox;
                    txtTask.Text = objList[1].ToString();
                    TextBox txtStartDate = e.Row.FindControl("txtStartDate") as TextBox;
                    txtStartDate.Text = DateTime.Parse(objList[8].ToString()).ToShortDateString();
                    TextBox txtEndDate = e.Row.FindControl("txtEndDate") as TextBox;
                    txtEndDate.Text = DateTime.Parse(objList[9].ToString()).ToShortDateString();                    
                    TextBox txtNotes = e.Row.FindControl("txtNotes") as TextBox;
                    txtNotes.Text = objList[18].ToString();

                    DropDownList ddlStatus = e.Row.FindControl("ddlStatus") as DropDownList;
                    ddlStatus.DataSource = DefaultDatabind.b_ItemStatus();
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = Constants.ddlValField;
                    ddlStatus.DataBind();
                    ddlStatus.SelectedValue = objList[15].ToString();
                }
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridTasks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridTasks.EditIndex = -1;
        BindGrid();
    }
    protected void ddlRagStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRagStatus.InnerText = ddlRagStatus.SelectedValue;
        BindGridBaseonRag();
    }
    protected void clearfields()
    {
        txtAttendees.Text=string.Empty;
        txtDate.Text=string.Empty;
        txtGeneralNotes.Text=string.Empty;
        txtLessonLearnt.Text=string.Empty;
        //txtLocation.Text=string.Empty;
        //txtRaiseIssue.Text=string.Empty;
        //txtResolutionDate.Text=string.Empty;
        txtTime.Text=string.Empty;
        txtKeyAchievements.Text = string.Empty;
        txtKeyTasks.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearfields();
        Response.Redirect("~/WF/Projects/ProjectMeetings.aspx?Project="+ QueryStringValues.Project);
    }
    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
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
    }
    private void Disable()
    {
        btnAdd.Enabled = false;
       // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";


    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
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
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion
    #region "Mail Section"
    private void MailSection(int meetingID)
    {
        try
        {
            UserDataContext CustomersDB = new UserDataContext();
            projectTaskDataContext projectDB = new projectTaskDataContext();

            var projectTitle = (from r in projectDB.ProjectDetails
                                where r.ProjectReference == sessionKeys.Project
                                select r).ToList().FirstOrDefault();

            var listCustomers = (from r in projectDB.CustomerRagAlerts
                                 where r.ProjectReference == sessionKeys.Project
                                 && r.ProjectType == 1
                                 select r).ToList();
            var contractors = (from c in CustomersDB.Contractors
                               where
                                   c.Status == "Active"
                               select c).ToList();

            var emailIDs = (from c in contractors
                            join cp in listCustomers on c.ID equals cp.CustomerID
                            select c).ToList();
            if (emailIDs != null)
            {
                foreach (UserMgt.Entity.Contractor id in emailIDs)
                {
                    MailCnt.Visible = true;
                    MailCnt.setdata(meetingID, sessionKeys.Project, id.ContractorName);

                    ArrayList ToEmailIds = new ArrayList(0);
                    ToEmailIds.Add(id.EmailAddress);
                    //ToEmailIds.Add(txtEmail.Text.Trim());
                    //string[] EmailIds = txtemail.Text.Trim().Split(';');
                    //foreach (string EmailId in EmailIds)
                    //{
                    //    ToEmailIds.Add(EmailId);
                    //}

                    //ArrayList al = new ArrayList(0); ;

                    string htmlText = string.Empty;
                    StringWriter sw = new StringWriter();
                    Html32TextWriter htmlTW = new Html32TextWriter(sw);
                    MailCnt.RenderControl(htmlTW);
                    htmlText = htmlTW.InnerWriter.ToString();
                    string errorString = string.Empty;
                    Email eMail = new Email();
                    if (!string.IsNullOrEmpty(id.EmailAddress))
                    {
                        //eMail.SendingMail("info@deffinity.com", "Project-" + projectTitle.ProjectReferenceWithPrefix + ":" + projectTitle.ProjectTitle+"-Issue raised", htmlText, id.EmailAddress.ToString());
                        eMail.SendingMail(id.EmailAddress.ToString(), "Project-" + projectTitle.ProjectReferenceWithPrefix + ":" + projectTitle.ProjectTitle + "-Project updates", htmlText);
                        //lblMsg.Text = "Supplier requisition form has been sent successfully.";
                        //imgSaveEmail.Enabled = false;
                        //eMail.SendingMail(ToEmailIds, "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                        //eMail.SendingMail(txtemail.Text.Trim(), "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                    }
                    MailCnt.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #endregion
}
