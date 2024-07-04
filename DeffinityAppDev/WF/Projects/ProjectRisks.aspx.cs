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
public partial class ProjectRisks : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = Resources.DeffinityRes.ProjectManagement;// "Project Management";
        if (!IsPostBack)
        {
                           
                GridView1.DataSourceID = "MysqlSource";
                GridView1.DataBind();
                CheckUserRole();
        }

    }
    protected string getRiskRef(string Riskref)
    {
        string s;
        string Rpath = "";// Server.MapPath("riskReport.aspx");
        Rpath = "RiskRpt.aspx?RiskReference=" + Riskref.ToString();

        s = "<a href='#' onclick=(";
        s += "window.open('" + Rpath + "','mywindow','width=600,height=400,resizable=yes,scrollbars=yes')";
        s += ")><img src='images/reports.jpg' alt='Risk Report'/></a>";

        return s;
    }
    protected void ImgbtnRiskReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectRiskItems.aspx?project=" + QueryStringValues.Project.ToString()+ "&RiskRef=" + "0");
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
      
        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

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
