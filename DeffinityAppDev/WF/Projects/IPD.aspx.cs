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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Flan.FutureControls;
using System.Data.Common;
using Deffinity.ProgrammeManagers;


public partial class IPD : BasePage
{
    Qstring Qval = new Qstring();
    string strProjRef = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
            lblError.Text = "";

            strProjRef = sessionKeys.Prefix + QueryStringValues.Project.ToString();
            CheckUserRole();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void ddlsubprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
        //BindCharts();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id;
        bool chk = false;
        bool chk_new = false;
        try
        {

            String IDs = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                //string s = row.Cells[0].Controls[1].ToString();
                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                if (chkNew.Checked)
                {
                    IDs = IDs + id.ToString() + ",";
                }
            }
            try
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_DELETE_PROJDEPENDENCY");
                db.AddInParameter(cmd, "@IDS", DbType.String, IDs);
                db.ExecuteNonQuery(cmd);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "Delete");
            }
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Delete");
        }
    }

    protected string GetProjectRef()
    {
        return strProjRef;
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //SqlDataSource1.Update();
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //int i = Convert.ToInt32(e.CommandArgument);
            //GridViewRow Row = GridView1.Rows[i];
            //string depOnProject = ((Label)Row.FindControl("DepOnProject")).Text;
            //RequiredFieldValidator depStart1, depEnd1;
            //CompareValidator depStart2, depEnd2;
            //if (depOnProject.Length > 0)
            //{
            //    depStart1 = (RequiredFieldValidator)Row.FindControl("OnStartdateval1");
            //    depEnd1 = (RequiredFieldValidator)Row.FindControl("OnEnddateval1");
            //    depStart2 = (CompareValidator)Row.FindControl("OnStartdateval2");
            //    depEnd2 = (CompareValidator)Row.FindControl("OnEnddateval2");
            //}
            //else
            //{
            //    depStart1 = (RequiredFieldValidator)Row.FindControl("DepStartdateval1");
            //    depEnd1 = (RequiredFieldValidator)Row.FindControl("DepEnddateval1");
            //    depStart2 = (CompareValidator)Row.FindControl("DepStartdateval2");
            //    depEnd2 = (CompareValidator)Row.FindControl("DepEnddateval2");
                
            //}
            //depStart1.Enabled = false;
            //depEnd1.Enabled = false;
            //depStart2.Enabled = false;
            //depEnd2.Enabled = false;
        }
        else if (e.CommandName == "Update")
        {
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            string id = ((HiddenField)Row.FindControl("HID")).Value;
            string depOnProject = ((Label)Row.FindControl("DepOnProject")).Text;
            string depProject = ((Label)Row.FindControl("DepProject")).Text;
            string depSDate, depEDate, depOnSDate, depOnEDate;
            if (depOnProject.Length > 0)
            {
                depSDate = ((TextBox)Row.FindControl("txtStartdate")).Text;
                depEDate = ((TextBox)Row.FindControl("txtEnddate")).Text;
                depOnSDate = ((TextBox)Row.FindControl("txtOnStartdate")).Text;
                depOnEDate = ((TextBox)Row.FindControl("txtonEnddate")).Text;
            }
            else
            {
                depSDate = ((TextBox)Row.FindControl("txtDepStartdate")).Text;
                depEDate = ((TextBox)Row.FindControl("txtDepEnddate")).Text;
                depOnSDate = ((TextBox)Row.FindControl("txtStartdate")).Text;
                depOnEDate = ((TextBox)Row.FindControl("txtEnddate")).Text;
            }

            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");

                DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_UPDATE_IPD_TASKS");
                db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(id));
                db.AddInParameter(cmd, "@DepSTARTDATE", DbType.DateTime, Convert.ToDateTime(depSDate.Trim()));
                db.AddInParameter(cmd, "@DepENDDATE", DbType.DateTime, Convert.ToDateTime(depEDate.Trim()));
                db.AddInParameter(cmd, "@DepOnSTARTDATE", DbType.DateTime, Convert.ToDateTime(depOnSDate.Trim()));
                db.AddInParameter(cmd, "@DepOnENDDATE", DbType.DateTime, Convert.ToDateTime(depOnEDate.Trim()));
                db.ExecuteNonQuery(cmd);
                cmd.Dispose();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void rowDragOE_RowDrop(object sender, RowDropEventArgs e)
    {
        try
        {
            if (CommandField() != false)
            {
                int id;
                int ProgrammeID;
                int newCnt;
                int i = 0;
                int retVal;
                //UpdateRowCount();
                RowDragOverlayExtender rde = (RowDragOverlayExtender)sender;

                id = Convert.ToInt32(((GridView)Page.FindControl(e.SourceGridViewID)).DataKeys[e.SourceRowIndex].Value.ToString());
                int id1;
                id1 = Convert.ToInt32(((GridView)Page.FindControl(rde.GridViewUniqueID)).DataKeys[rde.RowIndex].Value.ToString());

                //programme id
                //ProgrammeID = Convert.ToInt32(ddlGroups.SelectedValue);
                //grid to grid drag and drop    

                if (id1 == id)
                {
                    lblError.Text = Resources.DeffinityRes.TaskCantDependOnItself; // "A task cannot be dependent on itself. Please check and try again";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                retVal = AssociateTasks(id, id1);
                //if (rde.GridViewUniqueID.Contains("ProjectTasks1") == true)
                //{
                //    retVal = AssociateTasks(id, rde.RowIndex+1);
                //    //GridAssignedUser.DataBind();
                //}
                //else if (rde.GridViewUniqueID.Contains("ProjectTasks2") == true)
                //{
                //    retVal = AssociateTasks(id, rde.RowIndex + 1);
                //    //GridAssignedChecklist.DataBind();
                //}
                if (retVal == 1)
                {
                    lblError.Text = Resources.DeffinityRes.Dependencyaddedsuccessfully;// "Dependency added successfully";
                    lblError.ForeColor = System.Drawing.Color.Green;
                    GridView1.DataBind();
                }
                else if (retVal == 2)
                {
                    lblError.Text = Resources.DeffinityRes.DependencyExists;// "Dependency already exists";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else if (retVal == 3)
                {
                    lblError.Text = Resources.DeffinityRes.CanotCreateDependency;// "Cannot create dependency as it causes deadlock";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
                else if (retVal == 0)
                {
                    lblError.Text = Resources.DeffinityRes.ErrorOccuredInDB;// "Error occured in database, please try after sometime";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Programme management");
        }
    }
    public int AssociateTasks(int TaskDependent, int TaskDependingOn)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("Deffinity_InsertDependency");
            db.AddInParameter(cmd, "@TaskDependent", DbType.Int32, TaskDependent);
            db.AddInParameter(cmd, "@TaskDependingOn", DbType.Int32, TaskDependingOn);
            db.AddOutParameter(cmd, "@Output", DbType.Int32,4);
            db.ExecuteNonQuery(cmd);
            int getVal = (int)db.GetParameterValue(cmd, "Output");
            cmd.Dispose();
            return getVal;
        }
        catch (Exception e)
        {
            LogExceptions.LogException(e.Message);
            return 0;
        }
    }
    //protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlGroups.SelectedValue != "Select..." || ddlGroups.SelectedValue != "")
    //    //{
    //    //    GetEMail(ddlGroups.SelectedValue);
    //    //}
    //    //BindGrid();
    //    //AssociateGridBind();
    //}
    protected bool getVisibility(string st)
    {
        bool retVal = false;

        if (!string.IsNullOrEmpty(st.Trim()))
        {
            retVal = true;
        }
        return retVal;
    }
    protected void ddlProjGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProjects.SelectedIndex = 0;
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/ProjectMeetings.aspx?Project={0}", Qval.Pref.ToString()));
        Response.Redirect(PermissionManager.GetBackURL(PermissionManager.Feature.ProjectIPD) + "?Project=" + Qval.Pref.ToString());
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/UserPermissions.aspx?Project={0}", Qval.Pref.ToString()));
        Response.Redirect(PermissionManager.GetNextURL(PermissionManager.Feature.ProjectIPD) + "?Project=" + Qval.Pref.ToString());
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
       // btnAdd.Enabled = false;
        btnDelete.Enabled = false;
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
}
