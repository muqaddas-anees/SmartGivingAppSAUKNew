using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.BAL;

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignNavigation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["eventid"] != null)
                {
                    var ret = CampaignTemplateBAL.CampaignTemplate_Schedule_SelectByID(Convert.ToInt32(Request.QueryString["eventid"].ToString()));
                    if (ret != null)
                    {
                        Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}&CSID={1}", + ret.TemplateID, Request.QueryString["eventid"].ToString()), false);
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}