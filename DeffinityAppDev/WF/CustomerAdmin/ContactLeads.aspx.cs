using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactLeads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToShortDateString();
                    // hTimeYear.Value = sessionKeys.PartnerID.ToString();
                    // hfdid.Value = sessionKeys.PortfolioID.ToString();
                    //Master.PageHead = "Customer Admin";

                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}