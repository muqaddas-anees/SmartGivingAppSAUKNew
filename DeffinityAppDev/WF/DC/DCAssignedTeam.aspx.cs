using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DCAssignedTeam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "FLS";
            if (sessionKeys.IncidentID > 0)
                lblTitle.InnerText = "Assigned Team - Ticket Reference " + sessionKeys.IncidentID;
            else
                lblTitle.InnerText = "Assigned Team - FLS ";
        }
    }
}