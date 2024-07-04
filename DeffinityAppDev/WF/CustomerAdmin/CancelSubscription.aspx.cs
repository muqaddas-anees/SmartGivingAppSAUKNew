using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class CancelSubscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var p = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Select(QueryStringValues.PlanID);
                    if (p != null)
                    {
                        pnlDisplay.Visible = false;
                        pnlInput.Visible = true;
                    }
                    else
                    {

                        pnlDisplay.Visible = true;
                        pnlInput.Visible = false;
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(QueryStringValues.PlanID >0)
                {
                   var p = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Select(QueryStringValues.PlanID);
                    if(p != null)
                    {
                        p.CancellationReason = txtReason.Text.Trim();
                        p.SubscriptionCancelDate = DateTime.Now;
                        PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Update(p);
                        lblmsg.Text = "Thank you for your response";
                        pnlDisplay.Visible = true;
                        pnlInput.Visible = false;
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