using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCCustomerChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["callid"] != null && Request.QueryString["callid"] != "0")
            {
                int tid = int.Parse(Request.QueryString["callid"].ToString());
                sessionKeys.IncidentID = tid;
            }
            else
            {
                sessionKeys.IncidentID = 0;
            }
            if (sessionKeys.IncidentID > 0)
                lblTitle.InnerText = "Service Desk - Ticket Reference " + sessionKeys.IncidentID;
            else
                lblTitle.InnerText = "Service Desk";
        }
    }
}