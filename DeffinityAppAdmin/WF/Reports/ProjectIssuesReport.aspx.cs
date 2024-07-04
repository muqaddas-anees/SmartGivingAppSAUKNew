using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Reports_ProjectIssuesReport : System.Web.UI.Page
{
    ReportDocument rpt;
    int ProjectReference;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindReport();
    }
    private void BindReport()
    {
        //ProjectReference = QueryStringValues.Project; 
        ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
        rpt = new ReportDocument();

        string str = @"ProjectIssuesRpt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);        
        SqlDataAdapter adp = new SqlDataAdapter("select * from V_projectDetails where projectreference="+ProjectReference , MyCon);
        adp.SelectCommand.CommandType = CommandType.Text;     
        adp.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter(" select * from V_ProjectIssueDetails where projectreference=" + ProjectReference, MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.Text;        
        adp_sub.Fill(dt1);       
        rpt.Subreports["IssuesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["IssuesRpt"].SetDataSource(dt1);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
       
        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Issues Report");
        Response.End();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
}
