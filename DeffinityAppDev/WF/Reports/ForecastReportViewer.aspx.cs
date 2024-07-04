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

public partial class Reports_ForecastReportViewer : System.Web.UI.Page
{
    ReportDocument rpt;
    int ProjectReference;
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
        BindReport();
    }
    private void BindReport()
    {
        //ProjectReference = QueryStringValues.Project; 
        ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
        rpt = new ReportDocument();

        string str = @"ForcastDetailsRpt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand1 = new SqlCommand("Deffinity_GetProjectDetails", MyCon);
        myCommand1.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.Add("@Projectreference", SqlDbType.Int).Value = ProjectReference;
        SqlDataAdapter adp = new SqlDataAdapter(myCommand1); 
        adp.Fill(dt);

        DataTable dt1 = new DataTable();             
        SqlCommand myCommand = new SqlCommand("DEFFINITY_GETFORECASTDETAILS_Rpt", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@Projectreference", SqlDbType.Int).Value = ProjectReference;
        SqlDataAdapter adp1 = new SqlDataAdapter(myCommand);              
        adp1.Fill(dt1);
        rpt.Subreports["ForecastRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ForecastRpt"].SetDataSource(dt1);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Forecast Report");
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
