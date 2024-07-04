using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DC_PermitToWorkDefaults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Admin Dropdown Lists";
                HidePermitType();
                BindAssignedChecklist();
                sae1.RequestTypeID = 2; //2=permit to work
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  
    #region Hide permit Type Controls
    private void HidePermitType()
    {
        ddlType.Visible = true;
        txtType.Visible = false;
        imb_SubmitType.Visible = false;
        imb_CancelType.Visible = false;
        imb_AddType.Visible = true;
        imb_DeleteType.Visible = true;
        imb_EditType.Visible = true;
    }
    #endregion
    #region Show Permit Type Controls
    private void ShowpermitType()
    {
        ddlType.Visible = false;
        txtType.Visible = true;
        imb_SubmitType.Visible = true;
        imb_CancelType.Visible = true;
        imb_AddType.Visible = false;
        imb_DeleteType.Visible = false;
        imb_EditType.Visible = false;
        lblptmsg.Text = string.Empty;

    }
    #endregion
    protected void imb_AddType_Click(object sender, EventArgs e)
    {
        ShowpermitType();
        txtType.Text = string.Empty;
    }
    protected void imb_CancelType_Click(object sender, EventArgs e)
    {
        HidePermitType();
        lblptmsg.Text = string.Empty;
    }
    protected void imb_SubmitType_Click(object sender, EventArgs e)
    {
        try
        {
            PermitType type = new PermitType();
            type.Type = txtType.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(h_ptId.Value) ? "0" : h_ptId.Value);
            if (id > 0)
            {
                bool exists = PermitTypeBAL.CheckbyIdUpdate(id, txtType.Text.Trim());
                if (!exists)
                {
                    type.ID = id;
                    PermitTypeBAL.PermitTypeUpdate(type);
                    lblptmsg.Text = "Item Updated Successfully.";
                    lblptmsg.ForeColor = System.Drawing.Color.Green;
                    HidePermitType();
                    ccdPermitType.SelectedValue = id.ToString();
                    h_ptId.Value = "0";
                    txtType.Text = string.Empty;
                }
                else
                {
                    lblptmsg.Text = "Item Already Exists.";
                    lblptmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = PermitTypeBAL.CheckExists(txtType.Text.Trim());
                if (!exists)
                {
                    PermitTypeBAL.AddPermitType(type);
                    lblptmsg.Text = "Item Added Successfully.";
                    lblptmsg.ForeColor = System.Drawing.Color.Green;
                    HidePermitType();
                    ccdPermitType.SelectedValue = type.ID.ToString();
                    h_ptId.Value = "0";
                    txtType.Text = string.Empty;
                }
                else
                {
                    lblptmsg.Text = "Item Already Exists.";
                    lblptmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditType_Click(object sender, EventArgs e)
    {
        try
        {
            PermitType type = PermitTypeBAL.SelectbyId(int.Parse(ddlType.SelectedValue));
            if (type != null)
            {
                txtType.Text = type.Type;
                h_ptId.Value = type.ID.ToString();
                ShowpermitType();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteType_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedValue != "0")
            {
                PermitTypeBAL.PermitTypeDelete(int.Parse(ddlType.SelectedValue));
                lblptmsg.Text = "Item Deleted Successfully.";
                lblptmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void btnProjectClassApply_Click(object sender, EventArgs e)
    {
        try
        {
              AssignedChecklist al = AssignedChecklistsBAL.BindAssignedChecklist();
              if (al != null)
              {
                  al.TemplateID = int.Parse(ddlChecklist.SelectedValue);
                  AssignedChecklistsBAL.AssignedChecklistsUpdate(al);
              }
              else
              {
                  AssignedChecklist als = new AssignedChecklist();
                  als.TemplateID = int.Parse(ddlChecklist.SelectedValue);
                  AssignedChecklistsBAL.AddAssignedChecklists(als);
              }           
            lblcmsg.Text = "Checklist assigned successfully.";
            lblcmsg.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindAssignedChecklist()
    {
        try
        {
            AssignedChecklist al = AssignedChecklistsBAL.BindAssignedChecklist();
            if (al != null)
            {
                ccdChecklist.SelectedValue = al.TemplateID.Value.ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}