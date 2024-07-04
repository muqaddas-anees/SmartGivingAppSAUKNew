using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class MaintenancePlanSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    //get maintence plan data
                    BindPlanData();
                    BindCustomFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindPlanData()
        {
            var p = PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_SelectByAddressID(QueryStringValues.AddressID).FirstOrDefault();
            if (p != null)
            {
                txtPlanPrice.Text = string.Format("{0:N}", p.Amount.HasValue ? p.Amount.Value : 0);
                hplanid.Value = p.MaintenacePlanID.ToString();
            }
            else
                hplanid.Value = "0";
        }

        private int UpdateGetPlanID()
        {
            int planID = 0;
            var p = PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_SelectByAddressID(QueryStringValues.AddressID).FirstOrDefault();
            if (p == null)
            {

                var r=   PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_Add(new PortfolioMgt.Entity.PartnerMaintenacePlan() { AddressID = QueryStringValues.AddressID, Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtPlanPrice.Text.Trim()) ? txtPlanPrice.Text.Trim() : "0") });
                planID = r.MaintenacePlanID;

                lblSuccess.Text = Resources.DeffinityRes.Addedsuccessfully;
            }
            else
            {
                p.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtPlanPrice.Text.Trim()) ? txtPlanPrice.Text.Trim() : "0");
                PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_Update(p);
                planID = p.MaintenacePlanID;
                lblSuccess.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }


            return planID;
        }

        protected void btnAddEquipment_Click(object sender, EventArgs e)
        {
            try
            {
                //QueryStringValues.pla
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}",QueryStringValues.ContactID,QueryStringValues.AddressID, UpdateGetPlanID()),false);

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindCustomFields()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.v_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll().Where(o => o.MaintenacePlanID == Convert.ToInt32(hplanid.Value)).ToList();
                list_Customfields.DataSource = rlist;
                list_Customfields.DataBind();
                //var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();
                //var qList = QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).ToList();
                //var pList = QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID);
                //var policyList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID);
                ////var policyDetails = policyList.FirstOrDefault();
                ////var discountPercent = policyDetails.DiscountPercent.HasValue?policyDetails.DiscountPercent.Value:0.00;

                //var paBal = new PortfolioMgt.BAL.PortfolioContactBAL();
                //var CallDetails = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                //var addressID = CallDetails.ContactAddressID;
                //var addressDetails = paBal.v_PortfolioContactAddress_SelectByID(addressID).FirstOrDefault();
                //var selectedpolicyID = addressDetails.PolicyTypeID.HasValue ? addressDetails.PolicyTypeID.Value : 0;
                //if (oList.Count > 0)
                //{
                //    var dList = (from o in oList
                //                 select new
                //                 {
                //                     o.CallID,
                //                     o.ID,
                //                     o.IsActive,
                //                     o.LoggedDate,
                //                     o.ModifiedDate,
                //                     o.OptionName,
                //                     o.Description,
                //                     ItemsCount = qList.Where(p => p.QuotationOptionID == o.ID).Count(),
                //                     Price = (pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault().OriginalPrice : 0.00),
                //                     IsAplied = pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault().IsOptionActive : false

                //                 }).ToList();
                //    var rLIst = (from o in dList
                //                 select new
                //                 {
                //                     o.CallID,
                //                     o.ID,
                //                     o.IsActive,
                //                     o.LoggedDate,
                //                     o.ModifiedDate,
                //                     o.OptionName,
                //                     o.Price,
                //                     o.IsAplied,
                //                     o.ItemsCount,
                //                     ItemsCountName = o.ItemsCount.ToString() + " Items",
                //                     o.Description,
                //                     d_IsAplied = (o.IsAplied.HasValue ? o.IsAplied.Value : false) ? "Sold" : "",
                //                     //MemberCost = string.Format("{0:F2}", (discountPercent > 0) ? o.Price - (o.Price * (discountPercent / 100)) : o.Price),
                //                     //Savings = string.Format("{0:F2}", (discountPercent > 0) ?  (o.Price * (discountPercent / 100)) : 0.00),
                //                     mdata = GetPolicyData(selectedpolicyID, policyList, o.Price)
                //                 }).ToList();
                //    list_Customfields.DataSource = rLIst;
                //    list_Customfields.DataBind();
                //  }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void list_Customfields_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            list_Customfields.EditIndex = -1;
            //BindCustomFields();
        }
        protected void list_Customfields_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            list_Customfields.EditIndex = e.NewEditIndex;
            //BindCustomFields();
            BindCustomFields();
        }
        protected void list_Customfields_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName == "UpdateItem")
                //{
                //    cb = new CustomFieldsBAL();
                //    CustomField dc = cb.CustomFields_SelectByID(Convert.ToInt32(e.CommandArgument.ToString()));
                //    TextBox txteDescription = (TextBox)e.Item.FindControl("txtLable");
                //    TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                //    DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                //    dc.FieldLable = txteDescription.Text.Trim();
                //    //dc.Cost = Convert.ToDouble(txteCost.Text.Trim());
                //    dc.FieldType = ddltype.SelectedValue;
                //    dc.FieldValue = txtValue.Text.Trim();
                //    cb.CustomFields_update(dc);
                //    lblMsg.Text = "Updated sucessfully";
                //    list_Customfields.EditIndex = -1;
                //    //BindCustomFields();
                //}
                //if (e.CommandName == "Item")
                //{
                //    var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                //    Response.Redirect(string.Format("/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindMaterialGrid(GridView gridMaterials,int eqid)
        {
            try
            {
                var eMaterial = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialMaterialBAL_SelectByEquipmentID(eqid);
                gridMaterials.DataSource = eMaterial.ToList();
                gridMaterials.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lbleqid = (Label)e.Item.FindControl("lbleqid");
                    GridView gvMaterial = (GridView)e.Item.FindControl("gridMaterials");

                    if(gvMaterial != null)
                    {
                        BindMaterialGrid(gvMaterial,Convert.ToInt32(lbleqid.Text));
                    }

                    //Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;
                    
                    HyperLink hlink = (HyperLink)e.Item.FindControl("hlink");
                    if (hlink != null)
                    {
                        var eqID = Convert.ToInt32(lbleqid.Text);
                        hlink.NavigateUrl = string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}",QueryStringValues.ContactID,QueryStringValues.AddressID, hplanid.Value, eqID);

                    }
                   

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGetPlanID();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}