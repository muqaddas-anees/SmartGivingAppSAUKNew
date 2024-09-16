using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Marketplace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnOpen_Command(object sender, CommandEventArgs e)
        {
            // Command logic based on the CommandName
            switch (e.CommandName)
            {
                case "PortalBranding":
                    // Add your logic for the PortalBranding command
                    OpenPortalBranding();
                    break;

                // You can add more cases here for other buttons with different CommandNames if needed

                default:
                    break;
            }
        }

        private void OpenPortalBranding()
        {
            // Logic for opening portal branding (or any other functionality you need)
            Response.Redirect("~/PortalBranding.aspx"); // Redirect example
        }
    }
  
}

