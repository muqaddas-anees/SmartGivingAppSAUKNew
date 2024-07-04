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

public partial class RAGOverviewReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnViewrpt_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer(@"~/Reports/RAGReportViewer.aspx?Program=" + ddlprogram.SelectedValue, false);
    }
}
