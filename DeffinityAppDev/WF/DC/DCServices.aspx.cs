using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DCServices : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //if (sessionKeys.SID == 9)
        //{
        //    this.Page.MasterPageFile = "~/DeffinityResourceTab.master";
        //}
        //else
        //    Response.Redirect("~/Default.aspx");
    }
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
                lblTitle.InnerText = "Invoice for "+sessionKeys.JobDisplayName+" Ref: " + QueryStringValues.CCID + " : " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID); 
            else
                lblTitle.InnerText = "Invoice for "+ sessionKeys.JobDisplayName;

            if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
            {
                link_return.HRef = "FLSResourceList.aspx?type=FLS";
            }
            else
            {
                //link_return.HRef = "FLSJlist.aspx?type=FLS";

                link_return.HRef = string.Format("DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID);
                ////}
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
   
}