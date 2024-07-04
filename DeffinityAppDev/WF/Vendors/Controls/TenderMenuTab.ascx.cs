using System;

public partial class controls_TenderMenuTab : System.Web.UI.UserControl
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
