using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CustomerLogicPermissions;
using Deffinity.ProgrammeManagers;
public partial class controls_UserProjectRestrictions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindCustomer();
                BindGridPermissions();
                CheckUserRole();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindCustomer()
    {
        try
        {
            CLPermissionCS CLPClass = new CLPermissionCS();
            DataTable CTable = CLPClass.SelectPRCustomers(Convert.ToInt32(Request.QueryString["Project"]));
            ddlCustomer.DataSource = CTable;
            ddlCustomer.DataValueField = "ContractorID";
            ddlCustomer.DataTextField = "ContractorName";
            ddlCustomer.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGridPermissions()
    {
        try
        {
            CLPermissionCS CLPClass = new CLPermissionCS();
            //DataTable CTable = CLPClass.SelectByUIDPR(Convert.ToInt32(ddlCustomer.SelectedValue), Convert.ToInt32(Request.QueryString["Project"]));
            DataTable CTable = CLPClass.SelectUsersByPR( Convert.ToInt32(Request.QueryString["Project"]));
            grdUsersPermissions.DataSource = CTable;
            grdUsersPermissions.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnAllowView_Click(object sender, EventArgs e)
    {
        int retVal = -1;
        try
        {
            CLPermissionCS CLPClass = new CLPermissionCS();
            int ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
            int UserId = Convert.ToInt32(ddlCustomer.SelectedValue);
            DataTable _dt = CLPClass.SelectByUIDPR(UserId, ProjectReference);
                if(_dt.Rows.Count >0)
                {
                }
                else
                {
                    retVal= CLPClass.InsertCLPermission(UserId, ProjectReference,true);
                     BindGridPermissions();
                }

            if (retVal > 0)
            {
                string Add = "Added";
                //BindGridPermissions();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    protected void grdUsersPermissions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                CLPermissionCS CLPClass = new CLPermissionCS();
                int retVal = -1;
                DataTable _dt = CLPClass.SelectByID(ID);
                if (_dt.Rows.Count > 0)
                {
                    retVal = CLPClass.DeleteCLPermission(ID);
                    BindGridPermissions();
                }
            
                
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdUsersPermissions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUsersPermissions.PageIndex = e.NewPageIndex;
        BindGridPermissions();
    }
    protected void grdUsersPermissions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGridPermissions();

    }
    protected void grdUsersPermissions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdUsersPermissions.EditIndex = e.NewEditIndex;
        BindGridPermissions();
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
        btnAllowView.Enabled = false;
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
