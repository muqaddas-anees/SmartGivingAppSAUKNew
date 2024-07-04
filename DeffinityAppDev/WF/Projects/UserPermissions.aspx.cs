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
using Deffinity.ProgrammeManagers;

public partial class UserPermissions : System.Web.UI.Page
{
    Qstring Qval = new Qstring();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Management";
        CheckUserRole();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/IPD.aspx?Project={0}", Qval.Pref.ToString()));
        Response.Redirect(PermissionManager.GetBackURL(PermissionManager.Feature.ProjectProfiler) + "?Project=" + Qval.Pref.ToString());
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/ProjectPipeline.aspx?Status=2&list=all");
        Response.Redirect(PermissionManager.GetNextURL(PermissionManager.Feature.ProjectProfiler) + "?Project=" + Qval.Pref.ToString());
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
        //btnDelete.Enabled = false;
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
