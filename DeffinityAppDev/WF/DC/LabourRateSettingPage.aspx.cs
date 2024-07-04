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
    public partial class LabourRateSettingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                var t = LabourRateSettingBAL.LabourRateSettingBAL_Select(sessionKeys.PortfolioID);
                if (t != null)
                {
                    txtCost.Text = string.Format("{0:F2}", t.CostRate);
                    txtSell.Text = string.Format("{0:F2}", t.SellRate);
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
                LabourRateSettingBAL.LabourRateSettingBAL_Update(Convert.ToDouble(txtCost.Text.Trim()), Convert.ToDouble(txtSell.Text.Trim()), sessionKeys.PortfolioID);
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}