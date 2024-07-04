using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using InventoryUse.BAL;

public partial class controls_Use_inventory : System.Web.UI.UserControl
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
        ddlUse.Visible = true;
        txtUse.Visible = false;
        txtCode.Visible = false;
        imb_SubmitUse.Visible = false;
        imb_CancelUse.Visible = false;
        imb_AddUse.Visible = true;
        imb_DeleteUse.Visible = true;
        imb_EditUse.Visible = true;
    }
    #endregion
    #region Show Controls
    private void Show()
    {
        ddlUse.Visible = false;
        txtUse.Visible = true;
        txtCode.Visible = true;
        imb_SubmitUse.Visible = true;
        imb_CancelUse.Visible = true;
        imb_AddUse.Visible = false;
        imb_DeleteUse.Visible = false;
        imb_EditUse.Visible = false;
        lblmsg.Text = string.Empty;

    }
    #endregion


   
    protected void imb_AddUse_Click(object sender, EventArgs e)
    {
        Show();
        txtUse.Text = string.Empty;
        txtCode.Text = string.Empty;
    }
    protected void imb_SubmitUse_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory_Use iuse = new Inventory_Use();
            iuse.Name = txtUse.Text.Trim();
            iuse.Code = txtCode.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(hfUseId.Value) ? "0" : hfUseId.Value);
            if (id > 0)
            {
                bool exists = InventoryUseBAL.CheckbyIdUpdate(id, txtUse.Text.Trim(),txtCode.Text.Trim());
                if (!exists)
                {
                    iuse.ID = id;
                    InventoryUseBAL.UpdateInventoryUse(iuse);
                    lblmsg.Text = "Use updated successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();

                    hfUseId.Value = "0";
                    txtUse.Text = string.Empty;
                    txtCode.Text = string.Empty;
                }
                else
                {
                    lblmsg.Text = "Use already exists.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = InventoryUseBAL.CheckExists(txtUse.Text.Trim(),txtCode.Text.Trim());
                if (!exists)
                {
                    InventoryUseBAL.AddInventoryUse(iuse);
                    lblmsg.Text = "Use added successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ccdUse.SelectedValue = iuse.ID.ToString();
                    hfUseId.Value = "0";
                    txtUse.Text = string.Empty;
                    txtCode.Text = string.Empty;

                   

                }
                else
                {
                    lblmsg.Text = "Use already exists.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditUse_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory_Use iuse = InventoryUseBAL.SelectbyId(int.Parse(ddlUse.SelectedValue));
            if (iuse != null)
            {
                txtUse.Text = iuse.Name;
                txtCode.Text = iuse.Code;
                hfUseId.Value = iuse.ID.ToString();
                Show();
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_CancelUse_Click(object sender, EventArgs e)
    {
        Hide();
        lblmsg.Text = string.Empty;
        hfUseId.Value = "0";
    }
    protected void imb_DeleteUse_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlUse.SelectedValue != "0")
            {
                InventoryUseBAL.DeleteInventoryUse(int.Parse(ddlUse.SelectedValue));
                lblmsg.Text = "Use deleted successfully.";
                lblmsg.ForeColor = System.Drawing.Color.Green;
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}