using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF
{
    public partial class CustomerMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (sessionKeys.UID == 0)
            {
                Response.Redirect("~/Default.aspx", false);
            }
            else
            {
                Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
            }
            //frm_setpage.Attributes.Add("src", ResolveClientUrl("~/WF/SessionKeepAlive.aspx"));
        }

        
    }
}