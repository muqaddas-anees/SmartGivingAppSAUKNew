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
using Deffinity.Bindings;


public partial class controls_ExportDataTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetCssClass(string strpage)
    {
        string CssClass = string.Empty;

        if (Page.Request.Path.ToLower().Contains(strpage.Trim().ToLower()))
        {

            CssClass = "current1";
        }

        return CssClass;
    }
}
