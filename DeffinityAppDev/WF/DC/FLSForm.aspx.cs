using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //int[] sids = { 4, 9 };
        //if (sids.Contains(sessionKeys.SID))
        //{
        //    this.Page.MasterPageFile = "~/DeffinityResourceTab.master";

        //}

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
            //if (QueryStringValues.CCID > 0)
            //    lblTitle.InnerText = Resources.DeffinityRes.ServiceDesk+" - Ticket Reference " + QueryStringValues.CCID;
            //else
            //    lblTitle.InnerText = Resources.DeffinityRes.ServiceDesk;
            //if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            //{
            //    link_return.HRef = "FLSResourceList.aspx?type=FLS";
            //}
            //else
            //{
            //    link_return.HRef = "FLSJlist.aspx?type=FLS";
            //    ////}
            //}

        }
    }
}