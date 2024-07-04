using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VT.DAL;

public partial class Resource_MailControls_VTRequestMail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void Binddata(string UserName,string Type, string RecieveraName, string FromDate, string ToDate, string Days,string Notes,bool request,bool cancel)
    {

        try
        {
            lblrequest.Visible = request;
            if (!cancel)
            {
                lblcancel.Text = " requested";
                lbltype.Text = Type;
            }
            else
            {
                lblcancel.Text = " cancelled";
                lbltype.Text = Type+" leave request";
            }
            lbluser.Text = UserName;
            
            lblrecievername.Text = RecieveraName;
            litfromdate.Text = FromDate;
            littodate.Text = ToDate;
            litdays.Text = Days;           
            litnotes.Text = Notes;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "VT Mail");
        }

        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];

    }

}
