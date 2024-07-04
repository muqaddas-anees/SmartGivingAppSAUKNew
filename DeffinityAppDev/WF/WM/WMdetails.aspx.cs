using InventoryMgt.BAL;
using InventoryMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.WM
{
    public partial class WMdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindWarehouselist();
                BindWarehouseManager();
            }

            if (Request.QueryString["back"] != null)
            {
                if (Request.QueryString["pnl"] != null)
                    linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                else
                    linkBack.NavigateUrl = Request.QueryString["back"];
                linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                linkBack.Visible = true;
            }
        }


        #region warehouse data binding
        private void BindWarehouselist()
        {
            try
            {

                var wManagerlist = WarehouseDetailsBAL.WarehouseDetailsBAL_SelectAll().OrderBy(o => o.WarehouseName).ToList();
                ddlWareshouse.DataSource = wManagerlist;
                ddlWareshouse.DataValueField = "ID";
                ddlWareshouse.DataTextField = "WarehouseName";
                ddlWareshouse.DataBind();
                //To add Please select
                ddlWareshouse.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void BindWarehouseManager()
        {
            try
            {
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                var wManagerlist = cb.Contractor_Select_Admins().OrderBy(o => o.ContractorName).ToList();
                ddlWareshouseManager.DataSource = wManagerlist;
                ddlWareshouseManager.DataValueField = "ID";
                ddlWareshouseManager.DataTextField = "ContractorName";
                ddlWareshouseManager.DataBind();
                //To add Please select
                ddlWareshouseManager.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindWarehouseByID(int WarehouseID)
        {
            try
            {
                var wdetails = WarehouseDetailsBAL.WarehouseDetailsBAL_SelectByID(Convert.ToInt32(ddlWareshouse.SelectedValue));
                if (wdetails != null)
                {
                    txtAddress1.Text = wdetails.Address1;
                    txtAddress2.Text = wdetails.Address2;
                    txtcity.Text = wdetails.City;
                    txtEmail.Text = wdetails.Email;
                    txtMobile.Text = wdetails.Mobile;
                    txtPostcode.Text = wdetails.Postcode;
                    txtTown.Text = wdetails.Town;
                    txtWareshouse.Text = wdetails.WarehouseName;
                    ddlWareshouseManager.SelectedValue = wdetails.WarehouseManagerID.ToString();
                    
                }
                else
                {
                    ClearFilds();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion

        protected void ddlWareshouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindWarehouseByID(Convert.ToInt32(ddlWareshouse.SelectedValue));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ClearFilds()
        {
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtPostcode.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtWareshouse.Text = string.Empty;
            ddlWareshouseManager.SelectedValue = "0";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFilds();
            ddlWareshouse.SelectedValue = "0";
            pnlDDLWarehouse.Visible = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                BindWarehouseByID(Convert.ToInt32(ddlWareshouse.SelectedValue));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlWareshouse.SelectedValue) > 0)
                {
                    WarehouseDetailsBAL.WarehouseDetailsBAL_Delete(Convert.ToInt32(ddlWareshouse.SelectedValue));
                    BindWarehouselist();
                    ClearFilds();
                    ddlWareshouse.SelectedValue = "0";
                }
                else
                {
                    lblError.Text = "Please select storage location";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsExists = WarehouseDetailsBAL.WarehouseDetailsBAL_WarehouseNameExists(txtWareshouse.Text.Trim(), Convert.ToInt32(ddlWareshouse.SelectedValue));
                if (!IsExists)
                {
                    if (Convert.ToInt32(ddlWareshouse.SelectedValue) > 0)
                    {
                        var wdetails = WarehouseDetailsBAL.WarehouseDetailsBAL_SelectByID(Convert.ToInt32(ddlWareshouse.SelectedValue));
                        if (wdetails != null)
                        {
                            wdetails.Address1 = txtAddress1.Text;
                            wdetails.Address2 = txtAddress2.Text;
                            wdetails.City = txtcity.Text;
                            wdetails.Email = txtEmail.Text;
                            wdetails.Mobile = txtMobile.Text;
                            wdetails.Postcode = txtPostcode.Text;
                            wdetails.Town = txtTown.Text;
                            wdetails.WarehouseName = txtWareshouse.Text;
                            wdetails.WarehouseManagerID = Convert.ToInt32(ddlWareshouseManager.SelectedValue);
                            WarehouseDetailsBAL.WarehouseDetailsBAL_Update(wdetails);
                            lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                            BindWarehouselist();
                            ddlWareshouse.SelectedValue = wdetails.ID.ToString();
                            pnlDDLWarehouse.Visible = true;
                        }
                    }
                    else
                    {
                        var wdetails = new WarehouseDetail();
                        wdetails.Address1 = txtAddress1.Text;
                        wdetails.Address2 = txtAddress2.Text;
                        wdetails.City = txtcity.Text;
                        wdetails.Email = txtEmail.Text;
                        wdetails.Mobile = txtMobile.Text;
                        wdetails.Postcode = txtPostcode.Text;
                        wdetails.Town = txtTown.Text;
                        wdetails.WarehouseName = txtWareshouse.Text;
                        wdetails.WarehouseManagerID = Convert.ToInt32(ddlWareshouseManager.SelectedValue);
                        var retval = WarehouseDetailsBAL.WarehouseDetailsBAL_Add(wdetails);
                        lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                        BindWarehouselist();
                        if (retval != null)
                            ddlWareshouse.SelectedValue = retval.ID.ToString();

                        pnlDDLWarehouse.Visible = true;

                    }
                }
                else
                {
                    lblError.Text = "Storage location already exists";
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFilds();
            ddlWareshouse.SelectedValue = "0";
            pnlDDLWarehouse.Visible = true;
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            ClearFilds();
            ddlWareshouse.SelectedValue = "0";
            pnlDDLWarehouse.Visible = true;
        }
    }
}