using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class SignupResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var orgData = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.PortfolioID).FirstOrDefault();
                hurl.Value = orgData.OrgUniqID;
                sessionKeys.PortfolioID = orgData.ID;
                lblOrgname.Text = orgData.PortFolio;
                lblResult.Text= string.Format("Thank you for joining the {0} community. We’ll keep you posted with regular events in your area.",sessionKeys.PortfolioName);
            }
        }

        protected void btnBacktologin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Deffinity.systemdefaults.GetWebUrl() + "/web/" + hurl.Value, false);
        }
    }
}