using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using System.Web.UI.HtmlControls;
public partial class InventoryCustomFormDesigner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Customer Admin";
                ccdCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
                VisibleControls(ddlTypeOfField.SelectedValue);
                imgUpdate.Visible = false;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void VisibleControls(string type)
    {

        if (type.ToLower() == "instruction")
        {
            SetValues(true, false, false, false, false, false);
        }
        else if (type.ToLower() == "text box" || type.ToLower() == "text area")
        {
            SetValues(true, true, false, false, false, true);
        }
        else if (type.ToLower() == "dropdown list" || type.ToLower() == "radio button")
        {
            SetValues(true, false, false, false, true, true);
        }
        else if (type.ToLower() == "number field")
        {
            SetValues(true, true, true, true, false, true);
        }
        else if (type.ToLower() == "date")
        {
            SetValues(true, false, false, false, false, true);
        }
        else if (type.ToLower() == "url")
        {
            SetValues(true, false, false, false, false, true);
        }
        else if (type.ToLower() == "checkbox")
        {
            SetValues(true, false, false, false, false, true);
        }
    }
    private void BindGrid()
    {
        string[] customer = Regex.Split(ccdCustomer.SelectedValue, ":::");
        int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
        using (InventoryDataContext Idb = new InventoryDataContext())
        {
             var result = Idb.inventoryCustomFields.Where(f => f.CustomerID == customerId).ToList();
             gvForm.DataSource = result;
             gvForm.DataBind();
        }
    }
    private void SetValues(bool label, bool defaultText, bool minValue, bool maxValue, bool listValue, bool mandatory)
    {
        //Labels 

        lblLableName.Visible = label;
        lblDefaultText.Visible = defaultText;
        lblMinimumValue.Visible = minValue;
        lblMaximumValue.Visible = maxValue;
        lblListValues.Visible = listValue;
        lblMandatoryField.Visible = mandatory;

        //Texboxs

        txtLabelName.Visible = label;
        txtDefaultText.Visible = defaultText;
        txtMinimumValue.Visible = minValue;
        txtMaximumValue.Visible = maxValue;
        txtListValues.Visible = listValue;
        chkMandatoryField.Visible = mandatory;

        //validation 
        rfvMin.Enabled = minValue;
        rfvMax.Enabled = maxValue;
        cvMin.Enabled = minValue;
        cvMax.Enabled = maxValue;
    }
    protected void ddlTypeOfField_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleControls(ddlTypeOfField.SelectedValue);
    }
    private void ClearFields()
    {
        ddlTypeOfField.SelectedValue = "Text Box";
        txtLabelName.Text = string.Empty;
        txtDefaultText.Text = string.Empty;
        txtListValues.Text = string.Empty;
        txtMaximumValue.Text = string.Empty;
        txtMinimumValue.Text = string.Empty;
        //chkListPostion.SelectedValue = "";
        chkMandatoryField.Checked = false;
    }

    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // bool positionExists = CustomFormDesignerBAL.CheckPosition(Convert.ToInt32(ddlCustomer.SelectedValue), chkListPostion.SelectedValue);

            inventoryCustomField InventoryCustomField = new inventoryCustomField();
            InventoryCustomField.CustomerID = int.Parse(ddlCustomer.SelectedValue);
            InventoryCustomField.TypeOfField = ddlTypeOfField.SelectedValue;
            InventoryCustomField.LabelName = txtLabelName.Text;
            InventoryCustomField.DefaultText = txtDefaultText.Text;
            InventoryCustomField.MinimumValue = txtMinimumValue.Text;
            InventoryCustomField.MaximumValue = txtMaximumValue.Text;
            InventoryCustomField.Mandatory = chkMandatoryField.Checked;
            InventoryCustomField.FieldPosition = "";
            InventoryCustomField.ListValue = txtListValues.Text;
            InventoryMgr InMgrCls=new InventoryMgr();
            InMgrCls.AddFields(InventoryCustomField);
            BindGrid();
            ClearFields();
            VisibleControls(ddlTypeOfField.SelectedValue);
            lblMsg.Text = "Field added successfully.";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            //else
            //{
            //    lblMsg.Text = "Position already exists. Please check and try again.";
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }

    protected void gvForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                imgAdd.Visible = false;
                imgUpdate.Visible = true;
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                hfId.Value = id.ToString();
                using (InventoryDataContext db = new InventoryDataContext())
                {
                    var data = db.inventoryCustomFields.Where(f => f.ID == id).FirstOrDefault();
                    if (data != null)
                    {
                        string type = data.TypeOfField;
                        VisibleControls(type);
                        ddlTypeOfField.SelectedValue = data.TypeOfField;
                        ddlTypeOfField.Enabled = false;
                        txtLabelName.Text = data.LabelName;
                        txtDefaultText.Text = data.DefaultText;
                        txtMaximumValue.Text = data.MaximumValue;
                        txtMinimumValue.Text = data.MinimumValue;
                        //chkListPostion.SelectedValue = data.FieldPosition;
                        chkMandatoryField.Checked = Convert.ToBoolean(data.Mandatory);
                        txtListValues.Text = data.ListValue;
                    }
                }


            }
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                InventoryMgr InMgrCls = new InventoryMgr();
                InMgrCls.DeleteField(id);
                BindGrid();
                ClearFields();
                VisibleControls(ddlTypeOfField.SelectedValue);
                imgAdd.Visible = true;
                imgUpdate.Visible = false;
                ddlTypeOfField.Enabled = true;
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }



    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void imgUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(hfId.Value);
            if (id > 0)
            {
                // bool positionExists = CustomFormDesignerBAL.CheckPosition(Convert.ToInt32(ddlCustomer.SelectedValue), id, chkListPostion.SelectedValue);

                InventoryMgr InMgrCls = new InventoryMgr();
                var flsCustomField = InMgrCls.GetFieldByID(id);
                if (flsCustomField != null)
                {
                    flsCustomField.LabelName = txtLabelName.Text;
                    flsCustomField.DefaultText = txtDefaultText.Text;
                    flsCustomField.MinimumValue = txtMinimumValue.Text;
                    flsCustomField.MaximumValue = txtMaximumValue.Text;
                    flsCustomField.Mandatory = chkMandatoryField.Checked;
                    flsCustomField.FieldPosition = chkListPostion.SelectedValue;
                    flsCustomField.ListValue = txtListValues.Text;
                    InMgrCls.UpdateFields(flsCustomField);
                    BindGrid();
                    ClearFields();
                    VisibleControls(ddlTypeOfField.SelectedValue);
                    imgAdd.Visible = true;
                    imgUpdate.Visible = false;
                    lblMsg.Text = "Field updated successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ddlTypeOfField.Enabled = true;
                }

                //else
                //{
                //    lblMsg.Text = "Position already exists. Please check and try again.";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void imgCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearFields();
        VisibleControls(ddlTypeOfField.SelectedValue);
        imgAdd.Visible = true;
        imgUpdate.Visible = false;
        ddlTypeOfField.Enabled = true;
    }
    protected void gvForm_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void gvForm_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}