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
    public partial class DCQuoteMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cid"] != null)
                {
                    sessionKeys.PortfolioID = Convert.ToInt32(Request.QueryString["cid"].ToString());
                }

                lblTitle.InnerText = "Job Reference " + Deffinity.systemdefaults.GetCallPrefix() + FLSDetailsBAL.GetCallIDByCustomer(QueryStringValues.CallID).ToString();
                if (Request.QueryString["callid"] != null)
                {
                    sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);

                }

            }
        }
    }
}