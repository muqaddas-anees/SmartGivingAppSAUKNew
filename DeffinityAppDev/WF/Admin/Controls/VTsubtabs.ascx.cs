using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_VTsubtabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbtnBooking.NavigateUrl = "~/WF/Admin/vt.requestvacation.aspx";
        lbtnAwaiting.NavigateUrl = "~/WF/Admin/vt.approverequests.aspx";
        lbtnArchived.NavigateUrl = "~/WF/Admin/vt.archivedrequests.aspx";
        if ((Request.Url.ToString().ToLower()).Contains("vt.requestvacation.aspx") == true)
        {
            lbtnBooking.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("vt.approverequests.aspx") == true)
        {
            lbtnAwaiting.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("vt.archivedrequests.aspx") == true)
        {
            lbtnArchived.BackColor = System.Drawing.Color.White;
        }
    }
}
