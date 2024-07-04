using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_Controls_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblyear.InnerText = System.DateTime.Now.Year.ToString();
        lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();
    }
}