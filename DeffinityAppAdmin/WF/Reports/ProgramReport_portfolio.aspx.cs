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

public partial class Reports_ProgramReport_portfolio : System.Web.UI.Page
{

    string projectReferences = "0";

    string ProjectReferences
    {
        get
        {
            return projectReferences;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void btnViewrpt_Click(object sender, ImageClickEventArgs e)
    {
        try{
        DataTable dummytable = new DataTable();
        sessionKeys.PortfolioID = Convert.ToInt32(ddlportfolio.SelectedValue); 
        DataHelperClass.DDLHelper(dummytable, "Select projectreference as id from projects where portfolio=" + Convert.ToInt32(ddlportfolio.SelectedValue));
        

        foreach (DataRow row in dummytable.Rows)
        {
            projectReferences = projectReferences + "," + row["id"].ToString();
        }
        //Response.Redirect(@"~/Reports/ProgramReportViewer.aspx?Projects=" + projectReferences);
        Server.Transfer(@"~/Reports/ProgramReportViewer_Portfolio.aspx?Projects=" + projectReferences,false);
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
