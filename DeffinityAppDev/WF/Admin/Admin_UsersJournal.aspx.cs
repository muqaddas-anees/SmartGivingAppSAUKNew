using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UsersJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["OnlineUsers"] != null)
        {
            // Master.PageHead = "Journal";
            lblNoOfUsersOnline.Text = Application["OnlineUsers"].ToString();
        }
    }
    protected void Grid_Journal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "');");
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = string.Empty;
            string ids_param = string.Empty;
            foreach (GridViewRow row in Grid_Journal.Rows)
            {
                //string s = row.Cells[0].Controls[1].ToString();
                CheckBox chkNew = (CheckBox)row.Cells[0].Controls[1];
                ids = ((HiddenField)row.FindControl("HID")).Value;
                if (chkNew.Checked)
                {
                    ids_param = ids_param + ids + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids_param))
            {
                PageJornal.PageJournal_Delete(ids_param);
                Grid_Journal.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtPageTitle.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlUsers.SelectedValue = "0";
    }
}
