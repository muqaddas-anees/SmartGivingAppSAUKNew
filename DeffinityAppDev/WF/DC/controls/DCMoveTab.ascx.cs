using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_controls_DCMoveTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbtnSDAssetsMove.NavigateUrl = string.Format("~/DC/DCMoveInformation.aspx?callid={0}", sessionKeys.IncidentID);
        lbtnSDMoveAsset.NavigateUrl = string.Format("~/DC/DCAssets.aspx?callid={0}", sessionKeys.IncidentID);
        lbtnSDMoveDealerVoice.NavigateUrl = string.Format("~/DC/DCDealerVoice.aspx?callid={0}", sessionKeys.IncidentID);
        lbtnSDMoveDashboard.NavigateUrl = string.Format("~/DC/DCDashboard.aspx?callid={0}", sessionKeys.IncidentID);

        if ((Request.Url.ToString().ToLower()).Contains("moveinformation.aspx") == true)
        {
            lbtnSDAssetsMove.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("assets.aspx") == true)
        {
            lbtnSDMoveAsset.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("dealervoice.aspx") == true)
        {
            lbtnSDMoveDealerVoice.BackColor = System.Drawing.Color.White;
        }
        else
        {
            lbtnSDMoveDashboard.BackColor = System.Drawing.Color.White;
        }
    }
}