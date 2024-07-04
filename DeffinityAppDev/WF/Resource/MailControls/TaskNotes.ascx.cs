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


public partial class Resource_MailControls_TaskNotes : System.Web.UI.UserControl
{
    string task, notes, raisedby, receiver;
    public string Task
    {
        get { return task; }
        set { task = value; }
    }
    public string Notes
    {
        get { return notes; }
        set { notes = value; }
    }
    public string RaisedBy
    {
        get { return raisedby; }
        set { raisedby = value; }
    }
    public string Receiver
    {
        get { return receiver; }
        set { receiver = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //FillDetails();
    }
    public void FillDetails()
    {
        littask.Text  = Task.ToString();
        litnotes.Text = Notes.ToString();
        litraisedby.Text = RaisedBy.ToString();
        litdateraised.Text = DateTime.Now.ToShortDateString();
        lblReciver.InnerText = Receiver.ToString();

        linkWebsite.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        linkWebsiteFooter.Text = Deffinity.systemdefaults.GetWebSiteName();
        linkWebsiteFooter.NavigateUrl = Deffinity.systemdefaults.GetWebUrl();
        imgLogo.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"];
        ImgBorder.ImageUrl = Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"];
    }

}
