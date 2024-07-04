using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class DCCustomerService : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        {
            //this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";
        }
    }

    private void SetMasterPageHead(string page_title)
    {
        //HtmlGenericControl ht = new HtmlGenericControl();
        //if (Master.FindControl("lblPageHead") != null)
        //{
        //    ((HtmlGenericControl)Master.FindControl("lblPageHead")).InnerText = page_title;
        //}
        //if (Master.Master.FindControl("lblPageHead") != null)
        //{
        //    ((HtmlGenericControl)Master.Master.FindControl("lblPageHead")).InnerText = page_title;
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetMasterPageHead("FLS");
           //this.Master.PageHead = "FLS";
            if (sessionKeys.IncidentID > 0)
                lblTitle.InnerText = "Services - Ticket Reference " + sessionKeys.IncidentID;
            else
                lblTitle.InnerText = "Services - FLS";

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}