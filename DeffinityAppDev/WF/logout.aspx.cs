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


public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("Expires", new DateTime(1940, 1, 1).ToString("R")); 
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("R")); 
        Response.AddHeader("Cache-Control", "no-cache, must-revalidate");
        Response.AddHeader("Pragma", "no-cache");
       
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.CacheControl = "no-cache";
        Response.Expires = -1;
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
