using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class InternalCostSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetLabourRates();
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

        private void GetLabourRates()
        {
            try
            {
                var t = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                if (t != null)
                {
                    chkInternalcost.Checked = t.EnableInternalCost.HasValue?t.EnableInternalCost.Value:false;
                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var t = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                if (t != null)
                {
                    t.EnableInternalCost = chkInternalcost.Checked;
                    PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(t);
                }
                   
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}