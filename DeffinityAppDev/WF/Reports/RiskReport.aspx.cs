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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class RiskReport : System.Web.UI.Page
{
    public string Project;
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"].ToString() != null)
        {
            Project = Request.QueryString["Project"].ToString();
            BindReport();
        }

    }
    protected void BindReport()
    {
        Project = Request.QueryString["Project"].ToString();
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "RiskSummaryreport1.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_ProjectRisk", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        // myCommand.Parameters.Add(@Project, SqlDbType.VarChar(50)).Value = Request.QueryString["Project"].ToString();

        myCommand.Parameters.Add("@Project", SqlDbType.VarChar, 50).Value = Request.QueryString["Project"].ToString();

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);


        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter("DN_rpt_ProjectDetails", MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
        adp_sub.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        //DataTable dt2 = new DataTable();
        //SqlDataAdapter adp_sub1 = new SqlDataAdapter("DN_ProjectRisk", MyCon);
        //adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub1.SelectCommand.Parameters.Add("@Project", SqlDbType.Int).Value = Project;
        //adp_sub1.Fill(dt2);
        //rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[0].SetDataSource(dt2);
        //Set the Crytal Report Viewer control's source to the report document.

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;



    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
        rpt = null;
        CrystalReportViewer1.Dispose();
        CrystalReportSource1.Dispose();

    }
}
