using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class MaintenancePlanItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    if(sessionKeys.Message.Length >0)
                    {
                        lblMsg.Text = sessionKeys.Message;
                        sessionKeys.Message = string.Empty;
                    }
                    BindMaterial();
                    //set the values 
                    BindDataByEquipmentID();
                    BindMaterialGrid();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        private void BindMaterialGrid()
        {
            try
            {
                var eMaterial = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_SelectByEquipmentID(QueryStringValues.EQID);
                gridMaterials.DataSource = eMaterial.ToList();
                gridMaterials.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindDataByEquipmentID()
        {
            if (QueryStringValues.EQID > 0)
            {
                lblTitile.Text = "Edit Equipment";
                var eq = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.PartnerMaintenacePlanEquipmentEquipmentBAL_Select(QueryStringValues.EQID);
                if (eq != null)
                {
                    hCategory.Value = eq.TypeOfEquipmentID.ToString();
                    hSubCategory.Value = eq.ChecklistID.ToString();
                    hManufacturer.Value = eq.ManufacturerID.ToString();
                    //ddlManufacturer.SelectedValue = (eq.ManufacturerID.HasValue ? eq.ManufacturerID.Value : 0).ToString();
                    ddlMonth.SelectedValue = !string.IsNullOrEmpty(eq.StartMonth) ? eq.StartMonth.ToString() : string.Empty;
                    txtModelNumber.Text = eq.ModelNumber.ToString();
                    txtQTY.Text = eq.QTY.ToString();
                    txtSerialNumber.Text = eq.SerialNumber.ToString();
                    txtTimeperyear.Text = (eq.TimePerYear.HasValue ? eq.TimePerYear.Value : 0).ToString();
                }
            }
        }

        private void BindMaterial()
        {
            try
            {
                var p = PortfolioMgt.BAL.PartnerMaterialBAL.PartnerMaterialBAL_SelectByPortfolioID().OrderBy(o => o.MaterialTitle).ToList();
                ddlMaterial.DataSource = p;
                ddlMaterial.DataTextField = "MaterialTitle";
                ddlMaterial.DataValueField = "ID";
                ddlMaterial.DataBind();
                ddlMaterial.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnAddMaterialtoPlan_Click(object sender, EventArgs e)
        {
            try
            {
                var mid = ddlMaterial.SelectedValue;

                var mdata = PortfolioMgt.BAL.PartnerMaterialBAL.PartnerMaterialBAL_Select(Convert.ToInt32(mid));
                if(mdata != null)
                {
                    var m = new PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial();
                    m.EquipmentID = QueryStringValues.EQID;
                    m.Material = mdata.MaterialTitle;
                    m.Price = mdata.Price;
                    m.QtyPerVisit = 1;

                    PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_Add(m);
                    BindMaterialGrid();
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //save material data
        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty( huid.Value))
                {

                    var m = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialBAL_Select(Convert.ToInt32(huid.Value));
                    m.EquipmentID = QueryStringValues.EQID;
                    m.Material = txtMaterial.Text.Trim();
                    m.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Text.Trim())? txtPrice.Text.Trim():"0");
                    m.QtyPerVisit = Convert.ToInt32(!string.IsNullOrEmpty(txtQtyPerVisit.Text.Trim()) ? txtQtyPerVisit.Text.Trim() : "0");

                    PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_Update(m);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
                else
                {

                    var m = new PortfolioMgt.Entity.PartnerMaintenacePlanEquipmentMaterial();
                    m.EquipmentID = QueryStringValues.EQID;
                    m.Material = txtMaterial.Text.Trim();
                    m.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Text.Trim()) ? txtPrice.Text.Trim() : "0");
                    m.QtyPerVisit = Convert.ToInt32(!string.IsNullOrEmpty(txtQtyPerVisit.Text.Trim()) ? txtQtyPerVisit.Text.Trim() : "0");

                    PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_Add(m);
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                }
                huid.Value = string.Empty;
                BindMaterialGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            try
            {
                huid.Value = "";
                txtPrice.Text = "0.00";
                txtQtyPerVisit.Text = "1";
                txtMaterial.Text = string.Empty;
                mdlMaterial.Show();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void gridMaterials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                if (e.CommandName == "item_edit")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());
                    var d= PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialBAL_Select(id);
                    if (d != null)
                    {
                        huid.Value = id.ToString();
                        txtMaterial.Text = d.Material;
                        txtQtyPerVisit.Text = (d.QtyPerVisit.HasValue ? d.QtyPerVisit.Value : 0).ToString();
                        txtPrice.Text = string.Format("{0:N2}", (d.Price.HasValue ? d.Price.Value : 0));

                        mdlMaterial.Show();
                    }

                }
                else if(e.CommandName== "item_delete")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());
                    PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_delete(id);
                    BindMaterialGrid();
                    lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveEquipmentData();
        }

        private void SaveEquipmentData()
        {
            try
            {
                PortfolioMgt.Entity.PartnerMaintenacePlanEquipment eq = new PortfolioMgt.Entity.PartnerMaintenacePlanEquipment();
                if (QueryStringValues.PlanID > 0 && QueryStringValues.EQID == 0)
                {
                    //add equipment

                    eq.ChecklistID = Convert.ToInt32(hSubCategory.Value);
                    //eq.EquipmentTypeID = Convert.ToInt32(hCategory.Value);
                    eq.MaintenacePlanID = QueryStringValues.PlanID;
                    eq.ManufacturerID = Convert.ToInt32(hManufacturer.Value);
                    eq.ModelNumber = txtModelNumber.Text.Trim();
                    eq.QTY = Convert.ToInt32(!string.IsNullOrEmpty(txtQTY.Text.Trim()));
                    eq.SerialNumber = txtSerialNumber.Text.Trim();
                    eq.StartMonth = ddlMonth.SelectedValue;
                    eq.TimePerYear = Convert.ToInt32(!string.IsNullOrEmpty(txtTimeperyear.Text.Trim()) ? txtTimeperyear.Text.Trim() : "0");
                    eq.TypeOfEquipmentID = Convert.ToInt32(hCategory.Value); ;
                    eq= PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.PartnerMaintenacePlanEquipmentEquipmentBAL_Add(eq);
                    sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                    Response.Redirect(string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}",QueryStringValues.PlanID,QueryStringValues.AddressID, QueryStringValues.PlanID,eq.EquipmentID), false);


                }
                else
                {
                    eq = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.PartnerMaintenacePlanEquipmentEquipmentBAL_Select(QueryStringValues.EQID);
                    if (eq != null)
                    {
                        eq.ChecklistID = Convert.ToInt32(hSubCategory.Value);
                        //eq.EquipmentTypeID = Convert.ToInt32(hCategory.Value);
                        eq.MaintenacePlanID = QueryStringValues.PlanID;
                        eq.ManufacturerID = Convert.ToInt32(hManufacturer.Value);
                        eq.ModelNumber = txtModelNumber.Text.Trim();
                        eq.QTY = Convert.ToInt32(!string.IsNullOrEmpty(txtQTY.Text.Trim()));
                        eq.SerialNumber = txtSerialNumber.Text.Trim();
                        eq.StartMonth = ddlMonth.SelectedValue;
                        eq.TimePerYear = Convert.ToInt32(!string.IsNullOrEmpty(txtTimeperyear.Text.Trim()) ? txtTimeperyear.Text.Trim() : "0");
                        eq.TypeOfEquipmentID = Convert.ToInt32(hCategory.Value); ;
                        PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.PartnerMaintenacePlanEquipmentEquipmentBAL_Update(eq);
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}", QueryStringValues.PlanID, QueryStringValues.AddressID, QueryStringValues.PlanID, eq.EquipmentID), false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnBackEq_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanSetup.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}", QueryStringValues.PlanID, QueryStringValues.AddressID, QueryStringValues.PlanID, 0), false);

        }
    }
}