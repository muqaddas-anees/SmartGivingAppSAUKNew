using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MailControls_VTLeaveApproveRejectMail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void Binddata(string Type,string RecieveraName, string FromDate, string ToDate, string Days, string Status, string Notes)
    {
        try
        {
            lbltype.Text = Type;
            lblrecievername.Text = RecieveraName;
            litapprovereject.Text = Status;
            litfromdate.Text = FromDate;
            littodate.Text = ToDate;
            litdays.Text = Days;
            litstatus.Text = Status;
            litnotes.Text = Notes;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "VT Mail");
        }
        
        //linkWebsite.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsite.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        linkWebsiteFooter.Text = System.Configuration.ConfigurationManager.AppSettings["site"];
        linkWebsiteFooter.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
        imgLogo.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
      
    }
}
