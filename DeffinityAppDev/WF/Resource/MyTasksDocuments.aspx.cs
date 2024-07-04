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
public partial class MyTasksDocuments : System.Web.UI.Page
{
    Qstring Qval = new Qstring();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Documents";
        if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString().ToLower() == "central")
        {
            //BuildProjectTabs1.Visible = false;
            ProjectRef1.Visible = false;
        }
    }
}
