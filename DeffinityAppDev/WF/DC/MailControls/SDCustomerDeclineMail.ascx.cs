using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class MailControls_SDCustomerDeclineMail : System.Web.DynamicData.FieldTemplateUserControl {
    public string Type
    {
        set;
        get;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindControls(string reciverName, string description)
    {
        if (Type == "FLS")
        {
            lblTypeOfRequest.InnerText = "FLS Request";
            lblReciver.InnerText = "ALL";
            lblIncidentID.InnerText = string.Format("{0}", sessionKeys.IncidentID);
        }
        else
        {
            lblTypeOfRequest.InnerText = "Service Request";
            lblReciver.InnerText = reciverName;
            lbldiscription.InnerText = description;
            lblIncidentID.InnerText = string.Format("SR:{0}", sessionKeys.IncidentID);
        }
       
        
        
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        //linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

    }


}
