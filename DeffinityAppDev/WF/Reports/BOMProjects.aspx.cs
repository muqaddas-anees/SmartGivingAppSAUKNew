using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Reports_BOMProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("BillorMaterials.aspx?Project=" + Convert.ToInt32(ddlProjects.SelectedValue));
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}
