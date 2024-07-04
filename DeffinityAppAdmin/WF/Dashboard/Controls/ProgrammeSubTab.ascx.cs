using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class controls_ProgrammeSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        for (int i=1;i<=8;i++)
        {
            HyperLink HyperLinkprogram =(HyperLink ) this.FindControl("HyperLink" + i.ToString());
            HyperLinkprogram.NavigateUrl = "~/WF/Dashboard/Programme.aspx?Panel=" + i.ToString();
        }

        //lbtnProjects.NavigateUrl = "~/AdminDropdown.aspx?Panel=0"; //+Request.QueryString["1"];
        //lbtnTS.NavigateUrl = "~/AdminDropdown.aspx?Panel=1";// +Request.QueryString["2"];
        //lbtnResource.NavigateUrl = "~/AdminDropdown.aspx?Panel=2";// +Request.QueryString["3"];
        //lbtnSD.NavigateUrl = "~/AdminDropdown.aspx?Panel=3";// +Request.QueryString["4"];
        //lbtnIssRisks.NavigateUrl = "~/AdminDropdown.aspx?Panel=4";// +Request.QueryString["5"];
        //lbtnAssets.NavigateUrl = "~/AdminDropdown.aspx?Panel=5";// +Request.QueryString["6"];
        if (Request.QueryString["Panel"] != null)
        {
            HyperLink HyperLinkprogram = (HyperLink)this.FindControl("HyperLink" + Request.QueryString["Panel"].ToString());
            HyperLinkprogram.BackColor = System.Drawing.Color.White;
        }
        else
        {
            HyperLink1.BackColor = System.Drawing.Color.White;
        }
        
    
    }
}
