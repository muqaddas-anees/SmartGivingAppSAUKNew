using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;

public partial class DCServiceUnits : System.Web.UI.Page
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
            //Master.PageHead = "FLS";
            if (Request.QueryString["callid"] != null)
            {
                sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                CallDetail cd= CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                sessionKeys.PortfolioID = cd.CompanyID.HasValue?cd.CompanyID.Value:0;
            }
            if (sessionKeys.IncidentID > 0)
                lblTitle.InnerText = "Service Units - Ticket Reference " + sessionKeys.IncidentID;
            else
                lblTitle.InnerText = "Service Units";
        }
    }
}