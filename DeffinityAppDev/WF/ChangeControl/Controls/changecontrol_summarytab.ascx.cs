using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_changecontrol_summarytab : System.Web.UI.UserControl
{
    protected string[] Purl = { "ChangeControlManagement.aspx","ChangeControlDashBorad.aspx" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sessionKeys.Customer == "customer")
            {
                link1.HRef = "../ChangeControlManagementByCustomer.aspx?customer=0";
                link2.HRef = "../ChangeControlDashBorad.aspx?customer=0";
            }
            else
            {
                link1.HRef = "../ChangeControlManagement.aspx";
                link2.HRef = "../ChangeControlDashBorad.aspx";
            }
        }

    }
    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current2";
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;

    }
    protected string getUrl(int i)
    {
        string url = string.Empty;
        if (i < Purl.Length)
        {

            url = Purl[i];

            i++;
        }

        return url;
    }
}
