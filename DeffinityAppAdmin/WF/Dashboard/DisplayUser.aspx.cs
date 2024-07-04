using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplayUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["userid"] != null)
        {
            DisplayImage(int.Parse(Request.Params["userid"].ToString()));
        }
    }
    private void DisplayImage(int UserID)
    {
        string filepath = Server.MapPath("~/WF/UploadData/Users/OriginalData/") + "user_" + UserID.ToString() + ".png";
        if (System.IO.File.Exists(filepath))
        {
            UserImage.Visible = true;
            UserImage.ImageUrl = string.Format("~/WF/UploadData/Users/OriginalData/user_{0}.png", UserID.ToString());
        }
        else
        {
            UserImage.Visible = false;
        }
    }
}
