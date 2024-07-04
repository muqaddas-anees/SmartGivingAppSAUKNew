using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Message : BasePage
{
    Email mail = new Email();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.Message ; //"Message";
        bool cont = true;
        if (Request.Url.ToString().ToLower().Contains(".axd"))
        { cont = false; }
        else if (Request.Url.ToString().ToLower().Contains(".asmx"))
        { cont = false; }
        
      
        if (cont)
        {
            string strReplace = "WF/Message.aspx?aspxerrorpath=/";
            string strSubject = Request.Url.ToString();            
            strSubject=strSubject.Replace(strReplace, "");
            string strError = "";
            if (Server.GetLastError() != null)
            {
                strError = Request.Url.ToString() + "<br>" + Server.GetLastError().Message;
            }
            else
            {

                strError = Request.Url.ToString();
            }
            mail.SendingMail("indra@deffinity.com", "ERROR : " + strSubject, strError);
        }
        
    }
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/logout.aspx");
    }
    
}
