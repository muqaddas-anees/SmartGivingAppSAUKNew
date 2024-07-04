using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class SubscriptionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // link_cancel.HRef =  "~/WF/CustomerAdmin/CancelSubscription.aspx";

                var p = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_SelectAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();

                if(p != null)
                {
                    var b = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_SelectByPortfolioID(sessionKeys.PortfolioID);
                }

                else
                {
                    Response.Redirect("~/WF/CustomerAdmin/UpgradePlan.aspx", false);
                }
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WF/CustomerAdmin/SubscriptionDetails.aspx", false);
        }
    }
}