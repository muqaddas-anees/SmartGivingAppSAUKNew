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

public partial class controls_AdminDropdownTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbtnProjects.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=0"; //+Request.QueryString["1"];
        lbtnTS.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=1";// +Request.QueryString["2"];
        lbtnResource.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=2";// +Request.QueryString["3"];
        lbtnSD.NavigateUrl = "~/WF/DC/FLSDefault.aspx?tab=fls";// +Request.QueryString["4"];
        lbtnIssRisks.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=4";// +Request.QueryString["5"];
        lbtnAssets.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=5";// +Request.QueryString["6"];
        lbtnVendors.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=6";
        //lbtnBBBEE.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=7";
        lbtnInventory.NavigateUrl = "~/WF/Admin/AdminDropdown.aspx?Panel=9";
        lbtnContracts.NavigateUrl = "~/WF/Admin/AdminContractorGroup.aspx";
        //lbtnSA.NavigateUrl = "~/WF/DC/SecurityAccess.aspx?tab=SecurityAccess";

        if ((Request.Url.ToString().ToLower()).Contains("panel=0") == true)
        {
            lbtnProjects.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=1") == true)
        {
            lbtnTS.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=2") == true)
        {
            lbtnResource.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=3") == true)
        {
            lbtnSD.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=4") == true)
        {
            lbtnIssRisks.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=5") == true)
        {
            lbtnAssets.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("panel=6") == true)
        {
            lbtnVendors.BackColor = System.Drawing.Color.White;
        }
        //else if ((Request.Url.ToString().ToLower()).Contains("panel=7") == true)
        //{
        //    lbtnBBBEE.BackColor = System.Drawing.Color.White;
        //}
        else if ((Request.Url.ToString().ToLower()).Contains("panel=9") == true)
        {
            lbtnInventory.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("admincontractorgroup.aspx") == true)
        {
            lbtnContracts.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("tab=fls") == true)
        {
            lbtnSD.BackColor = System.Drawing.Color.White;
        }
        //else if ((Request.Url.ToString().ToLower()).Contains("tab=securityaccess") == true)
        //{
        //    lbtnSA.BackColor = System.Drawing.Color.White;
        //}
        else
        {
            lbtnProjects.BackColor = System.Drawing.Color.White;
        }
    }
}
