using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectTasksManagers;
using System.Web.UI.HtmlControls;
using Deffinity.GlobalIssues;
using Deffinity.ProgrammeManagers;
using System.IO;
using System.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Microsoft.ApplicationBlocks.Data;

public partial class ProjectOverviewV4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();   
       
        //Master.PageHead = "My Project";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");


       
        ddlCopyResourceFrom.SelectedIndex = -1;
        ddlCopyResourceTo.SelectedIndex = -1;
        ddlMoveResourceFrom.SelectedIndex = -1;
        ddlMoveResourceTo.SelectedIndex = -1;

        BindListpositions(ddlCopyResourceFrom, 0);
        BindListpositions(ddlCopyResourceTo, 0);
        BindListpositions(ddlMoveResourceFrom, 0);
        BindListpositions(ddlMoveResourceTo, 0);
     
        if (!IsPostBack)
        {

            try
            {
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //GetGanttFile();
            //PanelCsv1.Attributes.Add("onload", "hidechatdiv()");
        }

        //iframeResource.Attributes.Add("src", string.Format("ProjectResourceSearch.aspx?project={0}", QueryStringValues.Project));
    }
    #region set iframe url -project and projectID
    protected string RetUrl1()
    {
        string s = "";
        try
        {

            projectTaskDataContext _db = new projectTaskDataContext();
            var getDates = (from r in _db.Projects
                            where r.ProjectReference == int.Parse(Request.QueryString["Project"].ToString())
                            select r).ToList().FirstOrDefault();

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "getDates_newGantt", new SqlParameter("@projectreference", int.Parse(Request.QueryString["Project"].ToString()))).Tables[0];
            string sDate = string.Empty;
            string eDate = string.Empty;
            string status = "1";
            if (dt.Rows.Count != 0)
            {
                if (QueryStringValues.Project == 0)
                {
                    sDate = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                    eDate = string.Format("{0:MM/dd/yyyy}", DateTime.Now.AddYears(1));
                    status = "1";
                    s = string.Format("Gantt/index.aspx?project={0}&start={1}&end={2}&status={3}#en", QueryStringValues.Project, sDate, eDate, status);
                }
                else
                {
                    sDate = string.Format("{0:MM/dd/yyyy}", dt.Rows[0]["StartDate"]);
                    eDate = string.Format("{0:MM/dd/yyyy}", dt.Rows[0]["EndDate"]);
                    status = getDates.ProjectStatusID.ToString();
                    s = string.Format("Gantt/index.aspx?project={0}&start={1}&end={2}&status={3}#en", Request.QueryString["Project"], sDate, eDate, status);
                }
            }
            //if (QueryStringValues.Project == 0)
            //    s = string.Format("Gantt/index.aspx?project={0}#en", QueryStringValues.Project);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return s;
    }
    protected string ChitChatUrl()
    {
        string s = "";
        try
        {
            s = string.Format("ProjectChitchat.aspx?project=" + QueryStringValues.Project.ToString());
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return s;
    }

    protected string RetPID()
    {
        string proejectID = "";
        try
        {
            proejectID = Request.QueryString["Project"].ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return proejectID;
    }
    #endregion
    #region set iframe url-file upload
    protected string RetUrl()
    {
        return string.Format("ProjectMppFileUpload.aspx?project={0}", QueryStringValues.Project);
    }
    protected string RetUrl_Task()
    {
        return string.Format("ProjectTaskDocuments.aspx?project={0}", QueryStringValues.Project);
    }

    protected string RetUrl_Checklist()
    {
        return string.Format("ProjectChecklist.aspx?project={0}", QueryStringValues.Project);
    }
    protected string RetUrl_Resource()
    {
        return string.Format("ProjectResourceSearch.aspx?project={0}", QueryStringValues.Project);
    }
    //protected string RetUrl_RAGStatus()
    //{
    //    return string.Format("ProjectTaskRAGStatusConfig.aspx?project={0}", QueryStringValues.Project);
    //}
    
    //protected string RetUrl_MoveTasks()
    //{
    //    return string.Format("ProjectTaskMoveResources.aspx?project={0}", QueryStringValues.Project);
    //}
    #endregion


    #region "Move resources and Tasks"


    private void BindListpositions(DropDownList ddl, int type)
    {
        projectTaskDataContext _db = new projectTaskDataContext();

        int countlist0 = (from r in _db.ProjectTaskItems
                          where r.ProjectReference == QueryStringValues.Project
                          select r).ToList().Count();

        var listPos = (from r in _db.ProjectTaskItems
                       where r.ProjectReference == QueryStringValues.Project
                       orderby r.ListPosition
                       select new { ID = r.ListPosition, Name = (r.ListPosition) + "-" + r.ItemDescription }).ToList();
        if (countlist0 == 0)
        {
            listPos = (from r in _db.ProjectTaskItems
                       where r.ProjectReference == QueryStringValues.Project
                       orderby r.ListPosition
                       select new { ID = r.ListPosition, Name = (r.ListPosition) + "-" + r.ItemDescription }).ToList();
        }
        if (listPos != null)
        {

            if (type == 1)
            {
                ddl.DataSource = listPos;
                ddl.DataTextField = "ID";
                ddl.DataValueField = "ID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            else
            {
                ddl.DataSource = listPos;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "ID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Please select...", "0"));
            }

        }


    }
    private void BindListTasks(DropDownList ddl)
    {
        projectTaskDataContext _db = new projectTaskDataContext();

        var listTasks = (from r in _db.ProjectTaskItems
                         where r.ProjectReference == QueryStringValues.Project
                         orderby r.ListPosition
                         select new { ID = r.ID, Name = (r.ListPosition) + "-" + r.ItemDescription }).ToList();
        if (listTasks != null)
        {
            ddl.DataSource = listTasks;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Please select...", "0"));
        }


    }
   
    private string getTaskPosition(string Task)
    {


        string[] arr = Task.Split('-');
        return arr[0];
    }

    #endregion


    #region "Permission Code Here"
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
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
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
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }
                role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }

            }
        }
    }
    private void Disable()
    {
        //btnAdd_worksheet.Enabled = false;
        //btnaddtoquote.Enabled = false;
        //btnDel_worksheet.Enabled = false;
        //btnEdit_worksheet.Enabled = false;
        //btngenerate.Enabled = false;
        //btnAdd.Enabled = false;
        //btnAssign.Enabled = false;
        //btnCreate.Enabled = false;
        //btnDecrease.Enabled = false;
        //btnDeleteTask.Enabled = false;
        //btnDependency.Enabled = false;
        //btnIncrease.Enabled = false;
        //btnMovePosition.Enabled = false;
        //btnNav_resourcetype.Enabled = false;
        //btnResetType.Enabled = false;
        //btnUpdateTask.Enabled = false;
        //imgRecurrAdd.Enabled = false;
        //btnAdd_ResourceType.Enabled = false;
        //btnDeleteDependency.Enabled = false;
        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

    }
    #endregion 
    protected void lnkMoveResources_Click(object sender, EventArgs e)
    {
        mpopBOM.Show();
    }
    protected void lnkMoveTasks_Click(object sender, EventArgs e)
    {
       // mdpopMoves.Show();
    }
    protected void lnkTaskDocs_Click(object sender, EventArgs e)
    {
        mdlPopTaskDocs.Show();
    }
    protected void lnk_checklist_Click(object sender, EventArgs e)
    {
        try
        {
            
            ////mdlPopUpCheclist.TargetControlID = "lnk_checklist";
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "$find('btmm').show()", true);
            mdlPopUpCheclist.Show();
        }
        catch (Exception ex)
        {
            
        }
    }




    protected void btn_edit_search_Click(object sender, EventArgs e)
    {
        model_search_edit.Show();
    }
    protected void ImgCancel_Click(object sender, EventArgs e)
    {
        mpopBOM.Hide();
    }
    protected void lnk_RAGConfig_Click(object sender, EventArgs e)
    {
        mdlPopRAGStatus.Show();
    }
}