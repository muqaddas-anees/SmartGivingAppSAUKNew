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

public partial class controls_HtmlEditor : System.Web.UI.UserControl
{
    int _width, _height;    
    protected void Page_Load(object sender, EventArgs e)
    {        
        WebHtmlEditor1.Width = _width;
        WebHtmlEditor1.Height = _height;
    }
    #region properties
    public int width
    {
        get { return _width; }
        set { _width = value; }
    }
    public int height
    {
       get { return _height; }
       set { _height = value; }
    }
    public string Text
    {
        get { return WebHtmlEditor1.Text; }
        set { WebHtmlEditor1.Text = value; }
    }
    #endregion
}
