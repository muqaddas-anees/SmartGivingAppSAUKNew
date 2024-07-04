using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DC_controls_Status : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hide();
        }
    }
   
    #region Hide Controls
    private void hide()
    {
        ddlPermit.Visible = true;
        ddl_status.Visible = true;
        txt_status.Visible = false;
        imb_SubmitStatus.Visible = false;
        imb_CancelStatus.Visible = false;
        imb_AddStatus.Visible = true;
        //imb_DeletePermit.Visible = true;
        imb_EditStatus.Visible = true;
    }
    #endregion
    #region Show Controls
    private void show()
    {
        ddlPermit.Visible = true;
        ddl_status.Visible = false;
        txt_status.Visible = true;
        imb_SubmitStatus.Visible = true;
        imb_CancelStatus.Visible = true;
        imb_AddStatus.Visible = false;
        //imb_DeletePermit.Visible = false;
        imb_EditStatus.Visible = false;
        lblmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_AddStatus_Click(object sender, EventArgs e)
    {
        show();
        txt_status.Text = string.Empty;
    }
    protected void imb_SubmitStatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPermit.SelectedValue != "0")
            {
                int rid = int.Parse(ddlPermit.SelectedValue);
                Status s = new Status();
                s.Name = txt_status.Text.Trim();
                s.RequestTypeID = rid;
                int id = int.Parse(string.IsNullOrEmpty(h_sId.Value) ? "0" : h_sId.Value);
                if (id > 0)
                {
                    bool exists = StatusBAL.CheckbyIdUpdate(id, rid, txt_status.Text.Trim());
                    if (!exists)
                    {
                        s.ID = id;
                        StatusBAL.StatusUpdate(s);
                        lblmsg.Text = "Updated successfully";
                        //lblmsg.ForeColor = System.Drawing.Color.Green;
                        hide();
                        cddType.SelectedValue = rid.ToString();
                        ccdStatus.SelectedValue = id.ToString();
                        h_sId.Value = "0";
                        txt_status.Text = string.Empty;
                    }
                    else
                    {
                        lblEror.Text = "Item already exists";
                        //lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    bool exists = StatusBAL.CheckExists(txt_status.Text.Trim(), rid);
                    if (!exists)
                    {
                        StatusBAL.AddStatus(s);
                        lblmsg.Text = "Added successfully";
                        //lblmsg.ForeColor = System.Drawing.Color.Green;
                        hide();
                        ccdStatus.SelectedValue = s.ID.ToString();
                        h_sId.Value = "0";
                        txt_status.Text = string.Empty;
                    }
                    else
                    {
                        lblEror.Text = "Item Already Exists";
                        //lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                lblEror.Text = "Please select permit";
                //lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditStatus_Click(object sender, EventArgs e)
    {
        try
        {
            Status type = StatusBAL.SelectbyId(int.Parse(ddl_status.SelectedValue));
            if (type != null)
            {
                txt_status.Text = type.Name;
                h_sId.Value = type.ID.ToString();
                show();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_CancelStatus_Click(object sender, EventArgs e)
    {
        hide();
       lblmsg.Text = string.Empty;
    }
    protected void imb_DeletePermit_Click(object sender, EventArgs e)
    {
        try
        {
            string[] arr = { "New", "In Hand/WIP", "Completed", "Resolved", "Awaiting Information" };
            if (ddlPermit.SelectedValue != "")
            {
                if (ddl_status.SelectedValue != "")
                {
                    string Sname = ddl_status.SelectedItem.ToString();
                    if (!arr.Contains(Sname))
                    {
                        StatusBAL.StatusDelete(int.Parse(ddl_status.SelectedValue));
                        lblmsg.Text = "Deleted successfully";
                        //lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else 
                    {
                        lblEror.Text = "Unable to delete this status";
                        //lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblEror.Text = "Please select status";
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblEror.Text = "Please select Type of Permit";
                //lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}