using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Servicedesk_SDServiceUnits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Master.PageHead = "Service Request";
            if (QueryStringValues.SDID > 0)
                lblTitle.InnerText = "Service Units - SR Reference " + QueryStringValues.SDID;
            else
                lblTitle.InnerText = "Service Units - Service Request";
            if (sessionKeys.Customer == "customer")
            {
                //ImageButton1.Visible = false;
                //sd_tabs1.Visible = false;
            }
            else
            {
                //ImageButton1.Visible = true;
                //sd_tabs1.Visible = true;
            }
            if (Request.QueryString["customer"] != null)
            {
                //ImageButton1.Visible = false;
                //sd_tabs1.Visible = false;
                //pnl_backtoCustomerHome.Visible = true;
            }
            else
            {
                //ImageButton1.Visible = true;
               // sd_tabs1.Visible = true;
                //pnl_backtoCustomerHome.Visible = false;
            }
        }
    }
    protected void rtrnSD_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("../SDsummary.aspx?sdid=" + QueryStringValues.SDID);
    }
}