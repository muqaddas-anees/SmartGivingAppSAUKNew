using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using DC.BLL;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hplanid.Value = BindPlanData(FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault().ContactAddressID).ToString();
                BindCustomFields();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private int BindPlanData(int addressID)
        {
            int planid = 0;
            var p = PortfolioMgt.BAL.PartnerMaintenacePlanBAL.PartnerMaintenacePlanBAL_SelectByAddressID(QueryStringValues.AddressID).FirstOrDefault();
            if (p != null)
            {
                planid = p.MaintenacePlanID;
            }
            else
                planid = 0;
            return planid;
        }
        private void BindCustomFields()
        {
            try
            {
                var rlist = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.v_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll().Where(o => o.MaintenacePlanID == Convert.ToInt32(hplanid.Value)).ToList();
                list_Customfields.DataSource = rlist;
                list_Customfields.DataBind();
                
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
        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lbleqid = (Label)e.Item.FindControl("lbleqid");
                    GridView gvMaterial = (GridView)e.Item.FindControl("gridMaterials");

                    if (gvMaterial != null)
                    {
                        BindMaterialGrid(gvMaterial, Convert.ToInt32(lbleqid.Text));
                    }

                    //Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;

                    HyperLink hlink = (HyperLink)e.Item.FindControl("hlink");
                    if (hlink != null)
                    {
                        var eqID = Convert.ToInt32(lbleqid.Text);
                        hlink.NavigateUrl = string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}", QueryStringValues.ContactID, QueryStringValues.AddressID, hplanid.Value, eqID);

                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindMaterialGrid(GridView gridMaterials, int eqid)
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

    }
}