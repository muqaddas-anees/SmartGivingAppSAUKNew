using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCInvoiceMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTitle.InnerText = "Invoice " + "- Ticket Reference " + FLSDetailsBAL.GetCallIDByCustomerWithoutCompany(QueryStringValues.CallID).ToString();
                if (Request.QueryString["callid"] != null)
                {
                    sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                }
            }
        }
    }
}