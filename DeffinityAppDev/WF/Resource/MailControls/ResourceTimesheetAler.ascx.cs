using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resource_MailControls_ResourceTimesheetAler : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void BindControls(string ReciverName,string MaxHrs,string TotalHrs_Booked,string ResourceName,string ProjectReference)
    {

        lblMaxhrs.InnerText = MaxHrs;
        lblTotalHrsboked.InnerText = TotalHrs_Booked;
        lblResource.InnerText = ResourceName;
        lblReciver.InnerText = ReciverName;
        lblProjectReference.InnerText = ProjectReference;
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

    }
}
