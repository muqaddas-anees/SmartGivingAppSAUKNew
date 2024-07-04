using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class Reports_DefaultPage : System.Web.UI.Page
{

    #region ReportMethods

    ReportDocument rpt;
    string ProjectReference = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Projects"] != null)
            ProjectReference = Request.QueryString["Projects"].ToString();
        BindReport();
    }
    private void BindReport()
    {
        rpt = new ReportDocument();

        //Load the selected report file.

   string str = "~/Reports/ProjectSummaryReport.rpt";
     //   string str = "ProjectSummaryReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];



        #region For Main Report

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_RPT_ProjectSummary", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);


        #endregion

        #region For RAGSummary Sub Report

        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter("DN_RPT_RAGSummary", MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub.Fill(dt1);
        rpt.Subreports["RAGSummary.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["RAGSummary.rpt"].SetDataSource(dt1);

        #endregion

        #region KeyRisks.rpt

        DataTable dt2 = new DataTable();
        SqlDataAdapter adp_sub1 = new SqlDataAdapter("DN_RPT_KeyRisks", MyCon);
        adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub1.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub1.Fill(dt2);
        rpt.Subreports["KeyRisks.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["KeyRisks.rpt"].SetDataSource(dt2);

        #endregion

        #region For KeyTasks.rpt

        DataTable dt3 = new DataTable();
        SqlDataAdapter adp_sub2 = new SqlDataAdapter("DN_RPT_KeyTasks", MyCon);
        adp_sub2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub2.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub2.Fill(dt3);
        rpt.Subreports["Tasks.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["Tasks.rpt"].SetDataSource(dt3);

        #endregion

        #region For Achievements.rpt

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp_sub3 = new SqlDataAdapter("DN_RPT_Achievements", MyCon);
        adp_sub3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub3.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub3.SelectCommand.Parameters.Add("@Days", SqlDbType.Int).Value = 0;
        adp_sub3.Fill(dt4);
        rpt.Subreports["Achievements.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["Achievements.rpt"].SetDataSource(dt4);

        #endregion

        #region Financials.rpt

        DataTable dt5 = new DataTable();
        SqlDataAdapter adp_sub4 = new SqlDataAdapter("DN_RPT_Financials", MyCon);
        adp_sub4.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub4.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub4.Fill(dt5);
        rpt.Subreports["Financials.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["Financials.rpt"].SetDataSource(dt5);

        #endregion

        #region ProjectProposals.rpt

        DataTable dt6 = new DataTable();
        SqlDataAdapter adp_sub5 = new SqlDataAdapter("DN_RPT_ProjectProposals", MyCon);
        adp_sub5.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub5.Fill(dt6);
        rpt.Subreports["ProjectProposals.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectProposals.rpt"].SetDataSource(dt6);

        #endregion

        #region CostAndSummary.rpt

        DataTable dt7 = new DataTable();
        SqlDataAdapter adp_sub6 = new SqlDataAdapter("DN_RPT_CostAndResourceSummary", MyCon);
        adp_sub6.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub6.Fill(dt7);
        rpt.Subreports["CostAndSummary.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["CostAndSummary.rpt"].SetDataSource(dt7);

        #endregion

        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Summary Report");
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
    #endregion
}