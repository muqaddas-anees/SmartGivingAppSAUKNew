using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class MailControls_CustomerOrderToSDteam : System.Web.UI.UserControl
{

    public string Type
    {
        set;
        get;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfType.Value = Type.ToString();
        }
    }

    public void BindControls(string ReciverName)
    {
        string ticketPrefix = string.Empty;
        if (hfType.Value == "FLS")
        {
            lblType.Text = "FLS Request";
            ticketPrefix = "";
            lblTicketPrefix.Text = "Ticket reference";
        }
        else
        {
            lblType.Text = "Service Request";
            ticketPrefix = "SR:";
            lblTicketPrefix.Text = "SR";
        }
        Gridview_services.DataBind();

        lblReciver.InnerText = ReciverName;
        lblIncidentID.InnerText = ticketPrefix + sessionKeys.IncidentID.ToString();
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
        
    }
}
