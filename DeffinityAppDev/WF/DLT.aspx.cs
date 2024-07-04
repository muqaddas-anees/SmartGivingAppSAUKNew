using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Dlt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        sqlds.ConnectionString = Constants.DBString;
        if (!Page.IsPostBack)
        {
            DataRow _dr = PermissionManager.GetUserLimit();
            txtMaxPMs.Text = _dr["PMs"].ToString();
            txtMaxResources.Text = _dr["Resources"].ToString();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
          
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
          
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void sqlds_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Page.MaintainScrollPositionOnPostBack = true;
            int index = GridView1.EditIndex;
            GridViewRow Grow = GridView1.Rows[index];
            DropDownList ddlReq = (DropDownList)Grow.FindControl("ddlStatus");
            sqlds.UpdateParameters["ID"].DefaultValue = e.Keys[0].ToString();
            sqlds.UpdateParameters["Status"].DefaultValue = ddlReq.SelectedValue;
            sqlds.Update();
            PermissionManager.UpdateFeaturesCache();
        }
        catch (Exception ex)
        {
        }
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

    protected void updateStatus(bool _status)
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
                PermissionManager.UpdateFeatureStatus(IDs, _status);
                PermissionManager.UpdateFeaturesCache();
                
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

    protected void btnEnable_Click(object sender, EventArgs e)
    {
        updateStatus(true);
    }
    protected void btnDisable_Click(object sender, EventArgs e)
    {
        updateStatus(false);
    }
    protected void imgupdateLimit_Click(object sender, EventArgs e)
    {
        PermissionManager.UpdateUserLimit(Convert.ToInt32(txtMaxPMs.Text), Convert.ToInt32(txtMaxResources.Text));
    }
}
