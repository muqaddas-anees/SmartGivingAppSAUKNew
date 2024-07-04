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
    public partial class DCServicesMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
                lblTitle.InnerText = "Job Reference " + Deffinity.systemdefaults.GetCallPrefix() + FLSDetailsBAL.GetCallIDByCustomerWithoutCompany(QueryStringValues.CallID).ToString();
                if (Request.QueryString["callid"] != null)
                {
                    sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                }
            }
        }
    }
}