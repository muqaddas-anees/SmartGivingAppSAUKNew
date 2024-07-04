using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PageNotfound : System.Web.UI.Page
{
    Email mail = new Email();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblName.InnerText = sessionKeys.UName;
        lbldate.InnerText = System.DateTime.Now.ToShortDateString().ToString();
        Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
          bool cont = true;
        if (Request.Url.ToString().ToLower().Contains(".axd"))
        { cont = false; }
        else if (Request.Url.ToString().ToLower().Contains(".asmx"))
        { cont = false; }


        if (cont)
        {
            string emailText = Request.Url.ToString() + "<br/>" + string.Format("{0} - {1}", sessionKeys.UID, sessionKeys.UName);
            mail.SendingMail("indra@deffinity.com", "error", emailText);
        }
    }
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/logout.aspx");
    }
}
