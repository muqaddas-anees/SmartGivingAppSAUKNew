using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class sidemenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(sessionKeys.SID == 3)
            {

            }

            if (sessionKeys.SID == 4)
            {

                link_settings.Visible = false;
                link_members.Visible = false;
                link_eventmanagement.Visible = false;
                link_expenses.Visible = false;
                link_timesheets.Visible = false;//
                link_pagebuilder.Visible = false;
                a_tithing.HRef = "~/App/Donations.aspx";
                a_tithing.Title = "Tithing";
            }

            if(sessionKeys.IsService)
            {
                a_tithing.HRef = "~/App/Donations.aspx";
                a_tithing.Title = "Tithing";
            }
        }
        private void DisplayandHideSerives()
        {
            using(var context=new PortfolioDataContext())
            {
                var portolfiosettings = context.PortfolioActiveProducts.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);
                link_eventmanagement.Visible = portolfiosettings.IsProjectManagement ?? false;
                pnlFundCamp.Visible = portolfiosettings.IsPeerToPeerFundraising ?? false;
            }
        }
    }
}