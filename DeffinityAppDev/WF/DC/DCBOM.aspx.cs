using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCBOM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (QueryStringValues.CCID > 0)
                    lblTitle.InnerText = "Job Reference " + QueryStringValues.CCID.ToString();
                else
                    lblTitle.InnerText = "Internal Costs";
                if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                {
                    link_return.HRef = "FLSResourceList.aspx?type=FLS";
                }
                else
                {
                    link_return.HRef = "FLSJlist.aspx?type=FLS";
                    ////}
                }
            }
        }
    }
}