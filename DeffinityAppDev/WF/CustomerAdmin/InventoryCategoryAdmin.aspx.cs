using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class InventoryCategoryAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        private void SetCategoryEditMode(bool setval)
        {
            txtCategory.Visible = setval;
            ddlCategory.Visible = !setval;
            btnAddCategory.Visible = !setval;
            btnEditCategory.Visible = !setval;
            btnDelCategory.Visible = !setval;
            btnSubmitCategory.Visible = setval;
            btnCancelCategory.Visible = setval;
        }
        private void SetSubCategoryEditMode(bool setval)
        {
            txtSubCategory.Visible = setval;
            ddlSubCategory.Visible = !setval;
            btnAddSubCategory.Visible = !setval;
            btnEditSubCategory.Visible = !setval;
            btnDelSubCategory.Visible = !setval;
            btnSubmitSubcategory.Visible = setval;
            btnCancelSubcategory.Visible = setval;
        }
        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            SetCategoryEditMode(true);
            hcid.Value = ddlCategory.SelectedValue.ToString();
            txtCategory.Text = ddlCategory.SelectedItem.Text;
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            SetCategoryEditMode(true);
            hcid.Value = "0";
        }

        protected void btnDelCategory_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(ddlCategory.SelectedValue);
                if (id > 0)
                {
                    InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_CategoryDelete(id);
                    lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                    ddlCategory.DataBind();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text.Trim() != string.Empty)
                {
                    if (Convert.ToInt32(hcid.Value) > 0)
                    {
                        InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_CategoryUpdate(txtCategory.Text.Trim(), Convert.ToInt32(hcid.Value));
                        ddlCategory.DataBind();
                        lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                    else
                    {
                        InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_CategoryAdd(txtCategory.Text.Trim());
                        ddlCategory.DataBind();
                        lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    }

                    SetCategoryEditMode(false);
                    txtCategory.Text = string.Empty;
                }
                else
                {
                    lblError.Text = "Please enter category";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnCancelCategory_Click(object sender, EventArgs e)
        {
            SetCategoryEditMode(false);
            txtCategory.Text = string.Empty;
        }

        protected void btnEditSubCategory_Click(object sender, EventArgs e)
        {
            SetSubCategoryEditMode(true);
            hsid.Value = ddlSubCategory.SelectedValue.ToString();
            txtSubCategory.Text = ddlSubCategory.SelectedItem.Text;
        }

        protected void btnAddSubCategory_Click(object sender, EventArgs e)
        {
            SetSubCategoryEditMode(true);
            txtSubCategory.Text = string.Empty;
            hsid.Value = "0";
        }

        protected void btnDelSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(ddlSubCategory.SelectedValue);
                if (id > 0)
                {
                    InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_SubCategoryDelete(id);
                    lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                    ddlSubCategory.DataBind();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSubcategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory.Text.Trim() != string.Empty)
                {
                    if (Convert.ToInt32(hsid.Value) > 0)
                    {
                        InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_SubCategoryUpdate(txtSubCategory.Text.Trim(), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(hsid.Value));
                        ddlSubCategory.DataBind();
                        lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                    else
                    {
                        InventoryMgt.BAL.InventoryCategoryBAL.InventoryCategoryBAL_SubCategoryAdd(txtSubCategory.Text.Trim(), Convert.ToInt32(ddlCategory.SelectedValue));
                        ddlSubCategory.DataBind();
                        lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    }

                    SetSubCategoryEditMode(false);
                    txtSubCategory.Text = string.Empty;
                }
                else
                {
                    lblError.Text = "Please enter sub category";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }

        protected void btnCancelSubcategory_Click(object sender, EventArgs e)
        {
            SetSubCategoryEditMode(false);
            txtSubCategory.Text = string.Empty;
        }
    }
}