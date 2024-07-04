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
public partial class Reports_Tasks : System.Web.UI.Page
{
    ReportDocument rpt;
    int ProjectReference;
    int ContractorID;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ProjectReference = Convert.ToInt32(Request.QueryString["Project"]);
        ContractorID = Convert.ToInt32(Session["UID"]);
        BindReport();
    }
    private void BindReport()
    {
        rpt = new ReportDocument();
        //CrystalReportViewer1.Enabled = true;
        //CrystalReportViewer1.EnableParameterPrompt = false;
        //CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "TasksReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_OutstandingTasksReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ContractorID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter("DN_rpt_ProjectDetails", MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        adp_sub.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        DataTable dt2 = new DataTable();
        SqlDataAdapter adp_sub1 = new SqlDataAdapter("DN_TasksByStatus", MyCon);
        adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub1.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        adp_sub1.Fill(dt2);
        rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[1].SetDataSource(dt2);


        //DataTable dt2 = new DataTable();
        //SqlDataAdapter adp_sub1 = new SqlDataAdapter("DN_getTotals1", MyCon);
        //adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub1.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //adp_sub1.Fill(dt2);
        //rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[1].SetDataSource(dt2);
        ////DN_VarianceReport
        ////

        //DataTable dt3 = new DataTable();
        //SqlDataAdapter adp_sub2 = new SqlDataAdapter("DN_ProjectRisk4", MyCon);
        //adp_sub2.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub2.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //adp_sub2.Fill(dt3);
        //rpt.Subreports[2].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[2].SetDataSource(dt3);



        //DataTable dt4 = new DataTable();
        //SqlDataAdapter adp_sub3 = new SqlDataAdapter("DN_ProjectQAScheduleReport", MyCon);
        //adp_sub3.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub3.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //adp_sub3.Fill(dt4);
        //rpt.Subreports[3].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[3].SetDataSource(dt4);


        //DataTable dt5 = new DataTable();
        //SqlDataAdapter adp_sub4 = new SqlDataAdapter("DN_ProjectRecommendations", MyCon);
        //adp_sub4.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub4.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //adp_sub4.Fill(dt5);
        //rpt.Subreports[4].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[4].SetDataSource(dt5);


        //DataTable dt6 = new DataTable();
        //SqlDataAdapter adp_sub5 = new SqlDataAdapter("DN_VarianceReport", MyCon);
        //adp_sub5.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub5.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        //adp_sub5.Fill(dt6);
        //rpt.Subreports[5].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[5].SetDataSource(dt6);

      


      

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Projects");
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
        }
    }
}
