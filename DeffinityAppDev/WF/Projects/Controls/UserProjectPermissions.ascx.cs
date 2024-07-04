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
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.ProgrammeManagers;
public partial class controls_UserProjectPermissions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count == 0)
                imgbtnUpdatePermissions.Visible = false;
            CheckUserRole();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnUpdatePermissions_Click(object sender, EventArgs e)
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        foreach (GridViewRow gr in GridView1.Rows)
        {
            try
            {
                
                DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_UPDATE_PROJPERMISSIONS");
                db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(((HiddenField)gr.FindControl("HID")).Value));
                db.AddInParameter(cmd, "@ADMINRIGHTS", DbType.Boolean, ((CheckBox)gr.FindControl("chkAdmin")).Checked);
                db.AddInParameter(cmd, "@MODIFYPROJECT", DbType.Boolean, ((CheckBox)gr.FindControl("chkModifyProject")).Checked);
                db.AddInParameter(cmd, "@ALLOCATETASK", DbType.Boolean, ((CheckBox)gr.FindControl("chkAllocateTask")).Checked);
                db.AddInParameter(cmd, "@MANAGEISSUES", DbType.Boolean, ((CheckBox)gr.FindControl("chkManageIssues")).Checked);
                db.AddInParameter(cmd, "@MANAGERISK", DbType.Boolean, ((CheckBox)gr.FindControl("chkManageRisks")).Checked);
                db.AddInParameter(cmd, "@ADDASSETS", DbType.Boolean, ((CheckBox)gr.FindControl("chkAddAssets")).Checked);
                db.AddInParameter(cmd, "@ADDDOCUMENTS", DbType.Boolean, ((CheckBox)gr.FindControl("chkAddDocuments")).Checked);
                db.AddInParameter(cmd, "@MANAGEPROJECTFINANCIALS", DbType.Boolean, ((CheckBox)gr.FindControl("chkManageProjFinancials")).Checked);
                db.AddInParameter(cmd, "@APPROVEVARIATIONS", DbType.Boolean, ((CheckBox)gr.FindControl("chkApproveVariations")).Checked);
                db.AddInParameter(cmd, "@DELETEDOCUMENT", DbType.Boolean, ((CheckBox)gr.FindControl("chkDeleteDocument")).Checked);
                db.ExecuteNonQuery(cmd);
                cmd.Dispose();
            }
            catch (Exception eX)
            {
                LogExceptions.LogException(eX.Message);
            }
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
        imgbtnUpdatePermissions.Enabled = false;



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
