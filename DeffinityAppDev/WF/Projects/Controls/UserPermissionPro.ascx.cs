using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using ProgrammeMgt.Entity;
using ProgrammeMgt.DAL;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProgrammeManagers;
public partial class controls_UserPermissionPro : System.Web.UI.UserControl
{
    //LinQ Here...
    PortfolioDataContext portfolio = new PortfolioDataContext();
    UserDataContext userDB = new UserDataContext();
    projectTaskDataContext projectDB = new projectTaskDataContext();
    ProgrammeDataContext programmeDB = new ProgrammeDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindTeams();
                BindTeamsUser();
                BindTeams_Grid();
                BindUser_Grid();
                CheckUserRole();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    #region "Bind Team,Users & Role"
    private void BindTeams()
    {
        var arry = new int[100];
       // var getALLProgrammes =null ;
        try
        {
        //    var getProgramme = (from r in projectDB.Projects
        //                         where r.ProjectReference == QueryStringValues.Project
        //                         select new { r.OwnerGroupID }).ToList().FirstOrDefault();
        //    var getSubProgramme = (from r in projectDB.Projects
        //                        where r.ProjectReference == QueryStringValues.Project
        //                        select new { r.SubProgramme }).ToList().FirstOrDefault();

        //    if (getProgramme != null)
        //    {
        //         getALLProgrammes = (from r in programmeDB.OperationsOwners
        //                                where r.MasterProgramme == getProgramme.OwnerGroupID && r.Level == 2
        //                                select new { r.ID }).Union(from r in programmeDB.OperationsOwners
        //                                                           where r.MasterProgramme == 0 && r.Level == 1
        //                                                           select new { r.ID }).ToList();

        //        // var sid = new int[] { 1, 2, 3, 5 };

        //        for (int i = 0; i < getALLProgrammes.Count; i++)
        //        {
        //            arry[i] = getALLProgrammes[i].ID;


        //        }
        //    }
        //    else
        //    {
        //        var getALLProgrammes = (from r in programmeDB.OperationsOwners
        //                                where r.MasterProgramme == getProgramme.OwnerGroupID && r.Level == 2
        //                                select new { r.ID }).ToList();
        //    }

        //    var getTeams = (from r in portfolio.Teams
        //                    join p in portfolio.PermissionLevels on r.ID equals p.TeamID
        //                    where arry.Contains(p.ProgrammeID.Value)
        //                    select new { r.TeamName, r.ID }).ToList().Distinct();
            //DataTable dt=new DataTable();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_TeamPermissions", new
                SqlParameter("@ProjectRef", QueryStringValues.Project)).Tables[0];
            ddlTeam.DataSource = dt;
            ddlTeam.DataValueField = "ID";
            ddlTeam.DataTextField = "TeamName";
            ddlTeam.DataBind();
            ddlTeam.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    private void BindTeamsUser()
    {
        try
        {
            //var getProgramme = (from r in projectDB.Projects
            //                    where r.ProjectReference == QueryStringValues.Project
            //                    select new { r.OwnerGroupID }).ToList().FirstOrDefault();


            //var getALLProgrammes = (from r in programmeDB.OperationsOwners
            //                       where r.MasterProgramme == getProgramme.OwnerGroupID && r.Level == 2
            //                       select new { r.ID }).Union(from r in programmeDB.OperationsOwners
            //                                                  where r.MasterProgramme == 0 && r.Level == 1
            //                                                  select new { r.ID }).ToList();
            //var arry = new int[100];
            //for (int i = 0; i < getALLProgrammes.Count; i++)
            //{
            //    arry[i] = getALLProgrammes[i].ID;
                

            //}
            //var getContractors = (from r in userDB.Contractors
            //                      select r).ToList();
            //var getTeams = (from r in getContractors
            //                join p in portfolio.PermissionLevels on r.ID equals p.UserID
            //                where arry.Contains(p.ProgrammeID.Value)
            //                select new { r.ContractorName, r.ID }).ToList().Distinct();
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_UserPermissions", new
                SqlParameter("@ProjectRef", QueryStringValues.Project)).Tables[0];
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "ID";
            ddlUser.DataTextField = "ContractorName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    #endregion
    protected void imgRole_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectPermissionLevel insert = new ProjectPermissionLevel();
            var IsExist = (from r in portfolio.ProjectPermissionLevels
                           where r.TeamID == int.Parse(ddlTeam.SelectedValue)
                               //&& r.Role==int.Parse(ddlRole.SelectedValue)
                           && r.ProjectReference == QueryStringValues.Project
                           select r).ToList();
            if (IsExist != null)
            {
                if (IsExist.Count == 0)
                {
                    insert.ProjectReference = QueryStringValues.Project;
                    insert.Role = int.Parse(ddlRole.SelectedValue);
                    insert.TeamID = int.Parse(ddlTeam.SelectedValue);
                    insert.UserID = 0;
                    portfolio.ProjectPermissionLevels.InsertOnSubmit(insert);
                    portfolio.SubmitChanges();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Added successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sorry,selected group already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            BindTeams_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgApplyUser_Click(object sender, EventArgs e)
    {
        try
        {
            var IsExist = (from r in portfolio.ProjectPermissionLevels
                           where r.UserID == int.Parse(ddlUser.SelectedValue)
                               // && r.Role == int.Parse(ddlRoleUser.SelectedValue)
                           && r.ProjectReference == QueryStringValues.Project
                           select r).ToList();
            if (IsExist != null)
            {
                if (IsExist.Count == 0)
                {
                    ProjectPermissionLevel insert = new ProjectPermissionLevel();
                    insert.ProjectReference = QueryStringValues.Project;
                    insert.Role = int.Parse(ddlRoleUser.SelectedValue);
                    insert.TeamID = 0;
                    insert.UserID = int.Parse(ddlUser.SelectedValue); ;
                    portfolio.ProjectPermissionLevels.InsertOnSubmit(insert);
                    portfolio.SubmitChanges();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Added successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sorry, selected user already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            BindUser_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTeams_Grid()
    {
        try
        {
            var getTeams = (from r in portfolio.ProjectPermissionLevels
                            join t in portfolio.Teams on r.TeamID equals t.ID
                            where r.TeamID != 0 && r.ProjectReference == QueryStringValues.Project
                            select new { r.ID, t.TeamName, r.Role });
            grdTeam.DataSource = getTeams;
            grdTeam.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindUser_Grid()
    {
        try
        {

            var getContractors = (from r in userDB.Contractors
                                  select r).ToList();
            var getTeams = (from r in portfolio.ProjectPermissionLevels
                            where r.UserID != 0 && r.ProjectReference==QueryStringValues.Project
                            select r).ToList();
            var getUsers = (from r in getTeams
                            join c in getContractors on r.UserID equals c.ID
                            select new { r.ID, r.Role, c.ContractorName }).ToList();
            grdUsers.DataSource = getUsers;
            grdUsers.DataBind();
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string RoleType(string type)
    {
        string val = "";
        try
        {

            if (type == "1")
            {
                val = "Disabled";
            }
            if (type == "2")
            {
                val = "Manager";
            }
            if (type == "3")
            {
                val = "Viewer";
            }
            if (type == "4")
            {
                val = "Administrator";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return val;
    }
    protected void grdTeam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ProjectPermissionLevel delete = portfolio.ProjectPermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
            portfolio.ProjectPermissionLevels.DeleteOnSubmit(delete);
            portfolio.SubmitChanges();
            BindTeams_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ProjectPermissionLevel delete = portfolio.ProjectPermissionLevels.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
            portfolio.ProjectPermissionLevels.DeleteOnSubmit(delete);
            portfolio.SubmitChanges();
            BindUser_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdTeam_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Visible = false;
            grdTeam.EditIndex = -1;
            BindTeams_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Visible = false;
            grdTeam.EditIndex = -1;
            BindUser_Grid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
        imgApplyUser.Enabled = false;
        imgRole.Enabled = false;



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
