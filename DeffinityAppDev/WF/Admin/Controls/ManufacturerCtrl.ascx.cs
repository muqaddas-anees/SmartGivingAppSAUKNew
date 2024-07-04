using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.Entity;
using ProjectMgt.BLL;

public partial class controls_ManufacturerCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Hide();
                BindManufacturer();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }

    #region Hide Controls
    private void Hide()
    {
        ddlManufacturer.Visible = true;
        txtManufacturer.Visible = false;
        imb_Submit.Visible = false;
        imb_Cancel.Visible = false;
        imb_Add.Visible = true;
        imb_Delete.Visible = true;
        imb_Edit.Visible = true;

    }
    #endregion

    #region Show Controls
    private void Show()
    {
        ddlManufacturer.Visible = false;
        txtManufacturer.Visible = true;
        imb_Submit.Visible = true;
        imb_Cancel.Visible = true;
        imb_Add.Visible = false;
        imb_Delete.Visible = false;
        imb_Edit.Visible = false;
        lblMsg.Text = string.Empty;

    }
    #endregion


    private void BindManufacturer()
    {
        ddlManufacturer.DataSource = ManufacturerBAL.GetManufacturerList();
        ddlManufacturer.DataValueField = "Id";
        ddlManufacturer.DataTextField = "Name";
        ddlManufacturer.DataBind();
        ddlManufacturer.Items.Insert(0, new ListItem("Please select...", "0"));

    }
    protected void imb_Add_Click(object sender, EventArgs e)
    {
        Show();
        txtManufacturer.Text = string.Empty;
    }



    protected void imb_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Name = txtManufacturer.Text.Trim();
            int id = int.Parse(string.IsNullOrEmpty(hfId.Value) ? "0" : hfId.Value);
            if (id > 0)
            {
                bool exists = ManufacturerBAL.CheckManufacturer(id, txtManufacturer.Text.Trim());
                if (!exists)
                {
                    manufacturer.Id = id;
                    ManufacturerBAL.UpdateManufacturer(manufacturer);
                    lblMsg.Text = "Manufacturer updated successfully.";
                    BindManufacturer();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    ddlManufacturer.SelectedValue = id.ToString();
                    hfId.Value = "0";
                    txtManufacturer.Text = string.Empty;
                  
                }
                else
                {
                    lblMsg.Text = "Manufacturer already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                bool exists = ManufacturerBAL.CheckManufacturer(txtManufacturer.Text.Trim());
                if (!exists)
                {
                    ManufacturerBAL.AddManufacturer(manufacturer);
                    lblMsg.Text = "Manufacturer added successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Hide();
                    BindManufacturer();
                    ddlManufacturer.SelectedValue = manufacturer.Id.ToString();
                    hfId.Value = "0";
                    txtManufacturer.Text = string.Empty;
                    


                }
                else
                {
                    lblMsg.Text = "Manufacturer already exists.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            Manufacturer manufacturer = ManufacturerBAL.SelectByID(int.Parse(ddlManufacturer.SelectedValue));
            if (manufacturer != null)
            {
                txtManufacturer.Text = manufacturer.Name;
                hfId.Value = manufacturer.Id.ToString();
                Show();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imb_Cancel_Click(object sender, EventArgs e)
    {
        Hide();
        lblMsg.Text = string.Empty;
        hfId.Value = "0";
    }

    protected void imb_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlManufacturer.SelectedValue != "0")
            {
                ManufacturerBAL.DeleteByID(int.Parse(ddlManufacturer.SelectedValue));
                lblMsg.Text = "Manufacturer deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindManufacturer();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}