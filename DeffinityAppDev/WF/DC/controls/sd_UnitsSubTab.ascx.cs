using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

public partial class Servicedesk_sdcontrols_sd_UnitsSubTab : System.Web.DynamicData.FieldTemplateUserControl {
    protected void Page_Load(object sender, EventArgs e)
    {
        string r_type = string.Empty;
        if (Request.QueryString["type"] == null)
            r_type = "sr";
        else
            r_type = Request.QueryString["type"].ToString();

        lbtnUnitStatus1.NavigateUrl = "~/WF/DC/SDUnitStatus.aspx" + "?type=" + r_type;
        lbtnUnitAdministration1.NavigateUrl = "~/WF/DC/SDUnitAdministration.aspx" + "?type=" + r_type;
        //if ((Request.Url.ToString().ToLower()).Contains("sdunitstatus.aspx") == true)
        //{
        //    lbtnUnitStatus1.BackColor = System.Drawing.Color.White;
            
        //}
        //else if ((Request.Url.ToString().ToLower()).Contains("sdunitadministration.aspx") == true)
        //{

        //    lbtnUnitAdministration1.BackColor = System.Drawing.Color.White;
        //}

       
    }

   
}
