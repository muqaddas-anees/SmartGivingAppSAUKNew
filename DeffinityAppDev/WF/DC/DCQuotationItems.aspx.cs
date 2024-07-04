using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCQuotationItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Service Desk";

                if (Request.QueryString["callid"] != null)
                {
                    sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                }
                if (QueryStringValues.CCID > 0)
                    lblTitle.InnerText = "Job Reference " + QueryStringValues.CCID + ": " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID);
                else
                    lblTitle.InnerText =  Resources.DeffinityRes.ServiceDesk;

                if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link_return.HRef = "FLSResourceList.aspx?type=FLS";
                }
                else
                {
                    link_return.HRef = string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID,QueryStringValues.CallID);
                    //link_return.HRef = "FLSJlist.aspx?type=FLS";
                    ////}
                }
            }
        }
    }
}