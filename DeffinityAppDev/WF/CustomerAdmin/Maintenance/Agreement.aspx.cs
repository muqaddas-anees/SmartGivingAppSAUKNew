using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class Agreement : System.Web.UI.Page
    {
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGridView();
                    txtDate.Text = DateTime.Now.ToShortDateString();
                    lblTerms.Text = Server.HtmlDecode(PortfolioMgt.BAL.PartnerTermsandConditionBAL.PartnerTermsandConditionBAL_SelectByPortfolioID());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindGridView()
        {
            var gList = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.v_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll().Where(o => o.MaintenacePlanID == QueryStringValues.PlanID).ToList();
            GridView1.DataSource = gList;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridViewRow row = e.Row;
                //if (e.Row.RowType == DataControlRowType.Header)
                //{

                //    ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                //            ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
                //}
                // Make sure we aren't in header/footer rows
                if (row.DataItem == null)
                {
                    return;
                }


                //Find Child GridView control
                GridView gv = new GridView();
                gv = (GridView)row.FindControl("gvInnerTimeSheet");
                if (gv.UniqueID == gvUniqueID)
                {
                    gv.PageIndex = gvNewPageIndex;
                    gv.EditIndex = gvEditIndex;
                    //Check if Sorting used


                }
                var d = (PortfolioMgt.Entity.V_PartnerMaintenacePlanEquipment)e.Row.DataItem;
                //DataSet ds = ViewTimeSheet.ViewTimeSheet_SubmittApproval(Convert.ToInt32(((DataRowView)e.Row.DataItem)["WCDateID"]), 2, Convert.ToInt32(((DataRowView)e.Row.DataItem)["ContractorID"]), Convert.ToInt32(sessionKeys.UID));
                var ds = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialBAL_SelectAll().Where(o => o.EquipmentID == d.EquipmentID).ToList();
                gv.DataSource = ds;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}