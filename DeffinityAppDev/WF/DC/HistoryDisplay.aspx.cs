using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_HistoryDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            string type = Request.QueryString["type"].ToString().ToLower();

            if (type == "fls")
                FlsHistory1.Visible = true;
            else if (type == "delivery")
                DeliveryHistory1.Visible = true;
            else if (type == "accesscontrol")
                AccessControlHistory1.Visible = true;
            else if (type == "permittowork")
                PermitToWorkHistory1.Visible = true;

        }
    }
}