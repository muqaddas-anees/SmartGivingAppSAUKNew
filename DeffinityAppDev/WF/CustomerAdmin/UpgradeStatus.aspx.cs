using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class UpgradeStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["bid"] != null)
                    {
                        var p = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Select(Convert.ToInt32(Request.QueryString["bid"].ToString()));
                        if (p != null)
                        {
                            p.IsPaid = true;

                            PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Update(p);
                            var m = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o => o.PlanID == p.PlanID && o.PortfolioID == p.PortfolioID).FirstOrDefault();
                            if (m != null)
                            {
                                m.IsPaid = true;

                                PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Update(m);
                                lblMsg.Text = "thank you for subscribing";
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}