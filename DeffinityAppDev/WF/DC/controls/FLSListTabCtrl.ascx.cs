using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class FLSListTabCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "FLS")
                {

                    // ccdType.SelectedValue = "6";
                    link_flsreport.Visible = true;
                    // Master.PageHead = "Service Desk";

                    // pnlRequestType.Visible = false;
                    //link_unitstatus.Visible = true;
                    link_Dashboard.Visible = true;

                }

                if (Request.QueryString["type"] == "Delivery")
                {

                    // ccdType.SelectedValue = "1";
                    link_deliveryreport.Visible = true;
                    //Master.PageHead = "Delivery";

                }

                if (Request.QueryString["type"] == "AccessControl")
                {

                    // ccdType.SelectedValue = "3";
                    link_accessreport.Visible = true;
                    //Master.PageHead = "Access Control";
                }

                if (Request.QueryString["type"].ToLower() == "permittowork")
                {

                    // ccdType.SelectedValue = "3";
                    //link_accessreport.Visible = true;
                    //Master.PageHead = "Permit to Work";
                }
            }

            if (sessionKeys.SID == 9 || sessionKeys.SID == 4)
            {
                link_accessreport.Visible = false;
                link_deliveryreport.Visible = false;
                link_flsreport.Visible = false;
                //link_unitstatus.Visible = false;
                link_menu.Visible = false;
            }

            if(Request.Url.AbsoluteUri.ToLower().Contains("resourceschedular.aspx"))
            {
                link_menu.Visible = false;
            }
        }
    }
}