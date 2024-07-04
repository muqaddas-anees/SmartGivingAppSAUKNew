using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Welcome to " + Deffinity.systemdefaults.GetInstanceTitle();
            lblcopyrighttext.InnerText = Deffinity.systemdefaults.GetCopyrightText();
            lblyear.InnerText = System.DateTime.Now.Year.ToString();
            
            //uid.Attributes.Add("autocomplete", "off");
            //pwd.Attributes.Add("autocomplete", "off");

            //uid.Attributes.Add("placeholder", "Username");
            //pwd.Attributes.Add("placeholder", "Password");
        }

        protected void rbtton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtton.SelectedValue == "2")
                btnsubmit.PostBackUrl = "http://XACTold.deffinity.com";
            else
                btnsubmit.PostBackUrl = "~/WF/Default.aspx";
        }
    }
}