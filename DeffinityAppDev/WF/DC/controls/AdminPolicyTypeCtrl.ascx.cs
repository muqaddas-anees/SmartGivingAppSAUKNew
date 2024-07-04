using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using PortfolioMgt.Entity;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class AdminPolicyTypeCtrl : System.Web.UI.UserControl
    {
        PolicyTypeBAL pbal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDropDown();
            }
        }

        private void BindDropDown()
        {
            try
            {
                pbal = new PolicyTypeBAL();
                ddlPolicyType.DataSource = pbal.PolicyType_SelectAll().OrderBy(o => o.Title).ToList();
                ddlPolicyType.DataTextField = "Title";
                ddlPolicyType.DataValueField = "ID";
                ddlPolicyType.DataBind();
                ddlPolicyType.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddPolicy_Click(object sender, EventArgs e)
        {
            txtPolicyType.Text = string.Empty;
            txtPolicyDetails.Text = string.Empty;
            txtPolicyTypePrefix.Text = string.Empty;
            hid.Value = "0";
            ddlPolicyType.Visible = false;
            txtPolicyType.Visible = true;
            btnAddPolicy.Visible = false;
            btnEditPolicy.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = true;
        }

        protected void btnEditPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ddlPolicyType.SelectedValue);
                if (id > 0)
                {
                    ddlPolicyType.Visible = false;
                    txtPolicyType.Visible = true;
                    btnAddPolicy.Visible = false;
                    btnEditPolicy.Visible = false;
                    btnCancel.Visible = true;
                    hid.Value = id.ToString();
                    pbal = new PolicyTypeBAL();
                    var pEntity = pbal.PolicyType_SelectBYID(id);
                    txtPolicyType.Text = pEntity.Title;
                    txtPolicyTypePrefix.Text = pEntity.PolicyTypePrefix;
                    var sval = btnft.SelectedValue;
                    if (sval == "0")
                    {
                        txtMonthly.Text = string.Format("{0:F2}", pEntity.Monthly.HasValue ? pEntity.Monthly.Value : 0);
                        txtYearly.Text = string.Format("{0:F2}", pEntity.Yearly.HasValue ? pEntity.Yearly.Value : 0);
                    }
                    else
                    {
                        txtMonthly.Text = string.Format("{0:F2}", pEntity.Monthly_G5000.HasValue ? pEntity.Monthly_G5000.Value : 0);
                        txtYearly.Text = string.Format("{0:F2}", pEntity.Yearly_G5000.HasValue ? pEntity.Yearly_G5000.Value : 0);
                    }

                    txtDiscount.Text = string.Format("{0:F2}", pEntity.DiscountPercent.HasValue ? pEntity.DiscountPercent.Value : 0);
                }
                else
                {
                    lblError.Text = "Please select maintenance plan";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hid.Value);
                pbal = new PolicyTypeBAL();
                var retval = pbal.PolicyType_DeleteBYID(id);
                if (retval)
                {
                    lblSuccess.Text = Resources.DeffinityRes.Deletedsuccessfully;
                    BindDropDown();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SaveData()
        {
            try
            {

                bool isSuccess = false;
                pbal = new PolicyTypeBAL();
                ProductPolicyType pt = new ProductPolicyType();
                int id = Convert.ToInt32(hid.Value);
                if (id == 0)
                {
                    if (txtPolicyType.Text.Trim() != string.Empty)
                    {
                        //check already exists
                        if (!pbal.PolicyType_IsExists(txtPolicyType.Text.Trim()))
                        {
                            pt.Title = txtPolicyType.Text.Trim();
                            pt.Description = txtPolicyDetails.Text.Trim();
                            pt.PolicyTypePrefix = txtPolicyTypePrefix.Text.Trim();
                              var sval = btnft.SelectedValue;
                              if (sval == "0")
                              {
                                  pt.Monthly = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                                  pt.Yearly = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                              }
                              else
                              {
                                  pt.Monthly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                                  pt.Yearly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                              }
                            pt.DiscountPercent = Convert.ToDouble(!string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? txtDiscount.Text.Trim() : "0.00"); 
                            pbal.PolicyType_Add(pt);
                            lblSuccess.Text = "Added successfully";
                            isSuccess = true;
                            id = pt.ID;
                            txtPolicyType.Text = string.Empty;
                        }
                        else
                        {
                            lblError.Text = "Maintenance plan already exists";
                        }
                    }
                    else
                    {
                        lblError.Text = "Please enter maintenance plan";
                    }
                }
                else if (id > 0)
                {
                    var pEntity = pbal.PolicyType_SelectBYID(id);
                    if (txtPolicyType.Visible)
                    {
                        if (!pbal.PolicyType_IsExistsOnUpdate(txtPolicyType.Text.Trim(), id))
                        {

                            pEntity.Title = txtPolicyType.Text.Trim();
                            pEntity.Description = txtPolicyDetails.Text;
                            pEntity.PolicyTypePrefix = txtPolicyTypePrefix.Text.Trim();
                            var sval = btnft.SelectedValue;
                            if (sval == "0")
                            {
                                pEntity.Monthly = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                                pEntity.Yearly = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                            }
                            else
                            {
                                pEntity.Monthly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                                pEntity.Yearly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                            }
                            pEntity.DiscountPercent = Convert.ToDouble(!string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? txtDiscount.Text.Trim() : "0.00");

                            pbal.PolicyType_Edit(pEntity);
                            lblSuccess.Text = "Updated successfully";
                            isSuccess = true;
                            id = pEntity.ID;
                            txtPolicyType.Text = string.Empty;
                        }
                        else
                        {
                            lblError.Text = "Maintenance plan already exists";
                        }
                    }
                    else
                    {
                        pEntity.Description = txtPolicyDetails.Text;
                        pEntity.PolicyTypePrefix = txtPolicyTypePrefix.Text.Trim();
                        var sval = btnft.SelectedValue;
                        if (sval == "0")
                        {
                            pEntity.Monthly = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                            pEntity.Yearly = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                        }
                        else
                        {
                            pEntity.Monthly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtMonthly.Text.Trim()) ? txtMonthly.Text.Trim() : "0.00");
                            pEntity.Yearly_G5000 = Convert.ToDouble(!string.IsNullOrEmpty(txtYearly.Text.Trim()) ? txtYearly.Text.Trim() : "0.00");
                        }
                        pEntity.DiscountPercent = Convert.ToDouble(!string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? txtDiscount.Text.Trim() : "0.00");

                        pbal.PolicyType_Edit(pEntity);
                        lblSuccess.Text = "Updated successfully";
                        isSuccess = true;
                        id = pEntity.ID;
                        txtPolicyType.Text = string.Empty;
                    }
                }

                if (isSuccess == true)
                {
                    ddlPolicyType.Visible = true;
                    txtPolicyType.Visible = false;
                    btnAddPolicy.Visible = true;
                    btnEditPolicy.Visible = true;
                    btnCancel.Visible = false;
                    btnDelete.Visible = true;
                    BindDropDown();
                    //set value
                    ddlPolicyType.SelectedValue = id.ToString();
                    SetControlValues();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnBottomSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void ddlPolicyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlValues();
        }

        private void SetControlValues()
        {
            try
            {
                int id = Convert.ToInt32(ddlPolicyType.SelectedValue);
                if (id > 0)
                {
                    hid.Value = id.ToString();
                    pbal = new PolicyTypeBAL();
                    var pEntity = pbal.PolicyType_SelectBYID(id);
                    txtPolicyDetails.Text = pEntity.Description;
                    txtPolicyTypePrefix.Text = pEntity.PolicyTypePrefix;

                    var sval = btnft.SelectedValue;
                    if (sval == "0")
                    {
                        txtMonthly.Text = string.Format("{0:F2}", pEntity.Monthly.HasValue ? pEntity.Monthly.Value : 0);
                        txtYearly.Text = string.Format("{0:F2}", pEntity.Yearly.HasValue ? pEntity.Yearly.Value : 0);
                    }
                    else
                    {
                        txtMonthly.Text = string.Format("{0:F2}", pEntity.Monthly_G5000.HasValue ? pEntity.Monthly_G5000.Value : 0);
                        txtYearly.Text = string.Format("{0:F2}", pEntity.Yearly_G5000.HasValue ? pEntity.Yearly_G5000.Value : 0);
                    }
                    txtDiscount.Text = string.Format("{0:F2}", pEntity.DiscountPercent.HasValue ? pEntity.DiscountPercent.Value : 0);
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void ClearData()
        {
            txtPolicyDetails.Text = string.Empty;
            txtPolicyTypePrefix.Text = string.Empty;
            txtMonthly.Text = "0.00";
            txtYearly.Text = "0.00";
            txtDiscount.Text = "0.00";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlPolicyType.Visible = true;
            txtPolicyType.Visible = false;
            btnAddPolicy.Visible = true;
            btnEditPolicy.Visible = true;
            btnCancel.Visible = false;
            BindDropDown();
        }

        protected void btnft_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetControlValues();
        }

        
    }
}