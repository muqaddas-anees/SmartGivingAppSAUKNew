using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class sidemenu_fundriser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            linkProfile.HRef = string.Format("~/App/Member.aspx?type=myprofile&mid={0}", sessionKeys.UID.ToString());
        }

        protected string getMyProfileUrl()
        {
            return "";
             
        }
    }
}