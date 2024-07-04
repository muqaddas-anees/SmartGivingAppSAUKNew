using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DMain : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Resource settings

        //setting menu
        //if the user is admin 
        pnlSettings.Visible = false;
        btnSettings.Visible = false;

        if (sessionKeys.UID == 0)
        {
            Response.Redirect("~/WF/Default.aspx", false);
        }
        else
        {
            Page.Title = Deffinity.systemdefaults.GetInstanceTitle();
            img_user.Src = "~/WF/Admin/ImageHandler.ashx?type=user&id=0";
            //link_editprofile.HRef = string.Format("~/WF/Admin/AdminUsers.aspx?uid={0}", sessionKeys.UID);
            //link_editprofile1.HRef = string.Format("~/WF/Admin/AdminUsers.aspx?uid={0}", sessionKeys.UID);
        }

        //frm_setpage.Attributes.Add("src", ResolveClientUrl("~/WF/SessionKeepAlive.aspx"));
    }
}
