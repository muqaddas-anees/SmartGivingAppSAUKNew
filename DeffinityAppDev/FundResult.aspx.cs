using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev
{
	public partial class FundResult : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    lblOrgname.Text = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID).PortFolio;
                    var unid = QueryStringValues.UNID;
                    var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                    var pE = pmRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();
                    if (unid.Length > 0)
                    {

                        if (pE.IsPaid.Value)
                        {

                            lblMsg.Text = "We're Doing More With Your Support. Thank you For Your Donation";
                        }
                        else
                        {
                            if (pE.CResult != null)
                            {
                                if (pE.CResult.Length > 0)
                                    lblMsg.Text = "Failed to process payment, Please try again ";
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnBacktologin_Click(object sender, EventArgs e)
        {
            var unid = QueryStringValues.UNID;
            var pmRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
            var pE = pmRep.GetAll().Where(o => o.unid == unid).FirstOrDefault();

            if (pE !=  null)
                Response.Redirect("~/FundraiserView.aspx?unid=" + pE.FundriserUNID, false);
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
    }
}