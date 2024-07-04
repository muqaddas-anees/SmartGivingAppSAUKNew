using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;


public partial class DC_controls_RequestType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            Hide();
        }
    }  
    #region Hide Controls
    private void Hide()
    {
        ddl_Permit.Visible = true;
        txt_Permit.Visible = false;
        imb_SubmitPermit.Visible = false;
        imb_CancelPermit.Visible = false;
        imb_AddPermit.Visible = true;
       // imb_DeletePermit.Visible = true;
        imb_EditPermit.Visible = true;
    }
    #endregion
    #region Show Controls
    private void Show()
    {
        ddl_Permit.Visible = false;
        txt_Permit.Visible = true;
        imb_SubmitPermit.Visible = true;
        imb_CancelPermit.Visible = true;
        imb_AddPermit.Visible = false;
        //imb_DeletePermit.Visible = false;
        imb_EditPermit.Visible = false;
        lblmsg.Text = string.Empty;
      
    }
    #endregion
    protected void imb_AddPermit_Click(object sender, EventArgs e)
    {
        Show();
        txt_Permit.Text = string.Empty;
    }

    protected void imb_CancelPermit_Click(object sender, EventArgs e)
    {
        Hide();
        lblmsg.Text = string.Empty;
    }
    protected void imb_SubmitPermit_Click(object sender, EventArgs e)
    {
        try
        {
            RequestType type = new RequestType();
            type.Name = txt_Permit.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(h_rtId.Value) ? "0" : h_rtId.Value);
            if (id > 0)
            {
                bool exists = RequestTypeBAL.CheckbyIdUpdate(id, txt_Permit.Text.Trim());
                if (!exists)
                {
                    type.ID = id;
                    RequestTypeBAL.PermitUpdate(type);
                    lblmsg.Text = "Updated Successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ccdType.SelectedValue = id.ToString();
                    h_rtId.Value = "0";
                    txt_Permit.Text = string.Empty;
                }
                else
                {
                    lblmsg.Text = "Item Already Exists.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = RequestTypeBAL.CheckExists(txt_Permit.Text.Trim());
                if (!exists)
                {
                    RequestTypeBAL.AddPermits(type);
                    lblmsg.Text = "Added Successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();                   
                    ccdType.SelectedValue = type.ID.ToString();
                    h_rtId.Value = "0";
                    txt_Permit.Text = string.Empty;
                }
                else
                {
                    lblmsg.Text = "Item Already Exists.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditPermit_Click(object sender, EventArgs e)
    {
        try
        {            
            RequestType type = RequestTypeBAL.SelectbyId(int.Parse(ddl_Permit.SelectedValue));
            if (type != null)
            {
                txt_Permit.Text = type.Name;
                h_rtId.Value = type.ID.ToString();
                Show();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeletePermit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Permit.SelectedValue != "0")
            {
                RequestTypeBAL.PermitDelete(int.Parse(ddl_Permit.SelectedValue));
                lblmsg.Text = "Deleted Successfully.";
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}