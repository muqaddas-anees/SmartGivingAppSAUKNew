using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BLL;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class FLSCustomFormDesigner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Customer Admin";
               // BindForms();
                ccdCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
                VisibleControls(ddlTypeOfField.SelectedValue);
                imgUpdate.Visible = false;
                BindGrid();
                //set typeof request
               // BindTypeOfField();
              //  SetAssignedTypeOfRequest();
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
      
        //string[] customer = Regex.Split(ccdCustomer.SelectedValue, ":::");
        //int customerId = Convert.ToInt32(customer[0] == "" ? "0" : customer[0]);
        var result = CustomFormDesignerBAL.GetFieldList(sessionKeys.PortfolioID,0);
        gvForm.DataSource = result;
        gvForm.DataBind();

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

    protected void imgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            // bool positionExists = CustomFormDesignerBAL.CheckPosition(Convert.ToInt32(ddlCustomer.SelectedValue), chkListPostion.SelectedValue);

            FLSCustomField flsCustomField = new FLSCustomField();
            flsCustomField.CustomerID = sessionKeys.PortfolioID;
            flsCustomField.TypeOfField = ddlTypeOfField.SelectedValue;
            flsCustomField.LabelName = txtLabelName.Text;
            flsCustomField.DefaultText = txtDefaultText.Text;
            flsCustomField.MinimumValue = txtMinimumValue.Text;
            flsCustomField.MaximumValue = txtMaximumValue.Text;
            flsCustomField.Mandatory = chkMandatoryField.Checked;
            flsCustomField.FieldPosition = "";
            flsCustomField.ListValue = txtListValues.Text;
            flsCustomField.FormID = 0; //Convert.ToInt32(ddlForms.SelectedValue);
            CustomFormDesignerBAL.AddFields(flsCustomField);
            BindGrid();
            ClearFields();
            VisibleControls(ddlTypeOfField.SelectedValue);
            //lblMsg.Text = "Field added successfully";
            DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page,"Field added successfully", "Ok");
            //lblMsg.ForeColor = System.Drawing.Color.Green;

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
                using (DCDataContext db = new DCDataContext())
                {
                    var data = db.FLSCustomFields.Where(f => f.ID == id).FirstOrDefault();
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
                CustomFormDesignerBAL.DeleteField(id);
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

    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(hfId.Value);
            if (id > 0)
            {
                // bool positionExists = CustomFormDesignerBAL.CheckPosition(Convert.ToInt32(ddlCustomer.SelectedValue), id, chkListPostion.SelectedValue);


                var flsCustomField = CustomFormDesignerBAL.GetFieldByID(id);
                if (flsCustomField != null)
                {
                    flsCustomField.LabelName = txtLabelName.Text;
                    flsCustomField.DefaultText = txtDefaultText.Text;
                    flsCustomField.MinimumValue = txtMinimumValue.Text;
                    flsCustomField.MaximumValue = txtMaximumValue.Text;
                    flsCustomField.Mandatory = chkMandatoryField.Checked;
                    flsCustomField.FieldPosition = chkListPostion.SelectedValue;
                    flsCustomField.ListValue = txtListValues.Text;
                    CustomFormDesignerBAL.UpdateFields(flsCustomField);
                    BindGrid();
                    ClearFields();
                    VisibleControls(ddlTypeOfField.SelectedValue);
                    imgAdd.Visible = true;
                    imgUpdate.Visible = false;
                  // lblMsg.Text = "Field updated successfully";
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Field updated successfully", "Ok");
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
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
    protected void imgCancel_Click(object sender, EventArgs e)
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
    //private void BindForms()
    //{
    //   try
    //    {
    //        using (DCDataContext dc = new DCDataContext())
    //        {
    //            var flist = dc.FLSForms.Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
    //            ddlForms.DataSource = flist;
    //            ddlForms.DataTextField = "FormName";
    //            ddlForms.DataValueField = "ID";
    //            ddlForms.DataBind();
    //            ddlForms.Items.Insert(0, new ListItem("Please select...", "0"));
    //        }

    //    }
    //    catch(Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}

    //private void BindTypeOfField()
    //{
    //    try
    //    {
    //        using (DCDataContext dc = new DCDataContext())
    //        {
    //            var flist = dc.TypeOfRequests.Where(o => o.CustomerID == sessionKeys.PortfolioID).OrderBy(o=>o.Name).ToList();
    //            ddlTypeofRequest.DataSource = flist;
    //            ddlTypeofRequest.DataTextField = "Name";
    //            ddlTypeofRequest.DataValueField = "ID";
    //            ddlTypeofRequest.DataBind();
    //            ddlTypeofRequest.Items.Insert(0, new ListItem("Please select...", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //private void SetCategoryEditMode(bool setval)
    //{
    //    txtForms.Visible = setval;
    //    ddlForms.Visible = !setval;
    //    btnAddForm.Visible = !setval;
    //    btnEditForm.Visible = !setval;
    //    btnDelForm.Visible = !setval;
    //    btnSubmitForm.Visible = setval;
    //    btnCancelForm.Visible = setval;
    //}

    protected void btnEditForm_Click(object sender, EventArgs e)
    {
        //SetCategoryEditMode(true);
        //txtForms.Text = ddlForms.SelectedItem.Text;
        //hcid.Value = ddlForms.SelectedValue;
    }

    protected void btnAddForm_Click(object sender, EventArgs e)
    {
      
        //SetCategoryEditMode(true);
        //txtForms.Text = string.Empty;
        //hcid.Value = "0";
        pnlFormDesign.Visible = false;
    }

    protected void btnDelForm_Click(object sender, EventArgs e)
    {
        try
        {
            using (DCDataContext DC = new DCDataContext())
            {
                var dEntity = DC.FLSForms.Where(o => o.ID == 0).FirstOrDefault();
                if (dEntity != null)
                {
                    DC.FLSForms.DeleteOnSubmit(dEntity);
                    DC.SubmitChanges();
                    //SetCategoryEditMode(false);
                    //hcid.Value = "0";
                   // BindForms();
                    BindGrid();
                    pnlFormDesign.Visible = false;
                }
            }
            
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnSubmitForm_Click(object sender, EventArgs e)
    {
        using (DCDataContext DC = new DCDataContext())
        {
            //if (Convert.ToInt32(hcid.Value) > 0)
            //{
            //    var dEntity = DC.FLSForms.Where(o =>  o.ID == Convert.ToInt32(hcid.Value)).FirstOrDefault();
            //    if (dEntity != null)
            //    {
            //        var dEntity1 = DC.FLSForms.Where(o => o.FormName.ToLower() == txtForms.Text.ToLower() && o.ID != Convert.ToInt32(hcid.Value)).FirstOrDefault();
            //        if (dEntity1 == null)
            //        {
            //            dEntity.FormName = txtForms.Text.Trim();
            //            //DC.FLSForms.DeleteOnSubmit(dEntity);
            //            DC.SubmitChanges();
            //            lblMsg1.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            //           // BindForms();
            //            ddlForms.SelectedValue = dEntity.ID.ToString();
            //           // SetCategoryEditMode(false);
            //            BindGrid();
            //            pnlFormDesign.Visible = true;
            //           // hcid.Value = "0";
            //        }
            //        else
            //        {
            //            lblError.Text = "Form name already exists.Please try again";
            //        }
            //    }
            //}
            //else
            //{
            //    var dEntity = DC.FLSForms.Where(o => o.FormName.ToLower() == txtForms.Text.ToLower() && o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
            //    if (dEntity == null)
            //    {
            //        dEntity = new FLSForm();
            //        dEntity.FormName = txtForms.Text.Trim();
            //        dEntity.LoggedBy = sessionKeys.UID;
            //        dEntity.LoggedDate = DateTime.Now;
            //        dEntity.PortfolioID = sessionKeys.PortfolioID;
            //        DC.FLSForms.InsertOnSubmit(dEntity);
            //        DC.SubmitChanges();
            //        lblMsg1.Text = Resources.DeffinityRes.Addedsuccessfully;
            //        BindForms();
            //        ddlForms.SelectedValue = dEntity.ID.ToString();
            //        SetCategoryEditMode(false);
            //        BindGrid();
            //        pnlFormDesign.Visible = true;
            //        hcid.Value = "0";
            //    }
            //    else
            //    {
            //        lblError.Text = "Form name already exists.Please try again";
            //    }
            //}
              
        }
       
    }

    protected void btnCancelForm_Click(object sender, EventArgs e)
    {
        //SetCategoryEditMode(false);
        //hcid.Value = "0";
    }

    //protected void ddlForms_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlForms.SelectedValue) > 0)
    //    {
    //        BindGrid();
    //        pnlFormDesign.Visible = true;

    //        SetAssignedTypeOfRequest();
    //    }

    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    UpdateAssignedTypeOfRequest();
    //}

    //private void UpdateAssignedTypeOfRequest()
    //{
    //    try
    //    {
    //        using (DCDataContext DC = new DCDataContext())
    //        {
    //            var dEntity = DC.FLSForms.Where(o => o.ID == Convert.ToInt32(ddlForms.SelectedValue)).FirstOrDefault();
    //            if (dEntity != null)
    //            {
    //                dEntity.AssignedTypeOfRequestID = Convert.ToInt32(ddlTypeofRequest.SelectedValue);
    //                DC.SubmitChanges();
    //                lblMsg1.Text = Resources.DeffinityRes.UpdatedSuccessfully;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //private void SetAssignedTypeOfRequest()
    //{
    //    try
    //    {
    //        using (DCDataContext DC = new DCDataContext())
    //        {
    //            var dEntity = DC.FLSForms.Where(o => o.ID == Convert.ToInt32(ddlForms.SelectedValue)).FirstOrDefault();
    //            if (dEntity != null)
    //            {
    //                ddlTypeofRequest.SelectedValue = (dEntity.AssignedTypeOfRequestID.HasValue ? dEntity.AssignedTypeOfRequestID.Value : 0).ToString();

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
}