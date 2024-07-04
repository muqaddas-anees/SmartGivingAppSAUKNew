using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class QuoteAndAssignSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (QueryStringValues.CCID > 0)
                    lblTitle.InnerText = "Job Reference " + QueryStringValues.CCID;
                link_Assign_Sales_rep1.HRef = link_Assign_Sales_rep2.HRef = "~/WF/DC/DCAssignSalesRep.aspx" + "?CCID=" + QueryStringValues.CCID + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID;
                link_create_Quotation1.HRef = link_create_Quotation2.HRef = "~/WF/DC/DCQuotationCompare.aspx"+ "?CCID=" + QueryStringValues.CCID + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID+ "&Option=0&tab=quote";

            }
        }
    }
}