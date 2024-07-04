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

public partial class ReportsProgarmmeRptViwer_page : System.Web.UI.Page
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

        string str = @"ProgramReport_Portfolio.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];



        #region For Main Report

        DataTable dt12 = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DEFFINITY_ProjetReport_GETDeliverables", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt12);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt12);


        #endregion
        #region For Deliverables

        DataTable dt = new DataTable();        
        SqlCommand myCommanddel = new SqlCommand("DEFFINITY_ProjetReport_GETDeliverables", MyCon);
        myCommanddel.CommandType = CommandType.StoredProcedure;

        myCommanddel.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        SqlDataAdapter myAdapterdel = new SqlDataAdapter(myCommanddel);
        myAdapterdel.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.Subreports["KeyDeliverablesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["KeyDeliverablesRpt"].SetDataSource(dt);


        #endregion

        #region For RAGSummary Sub Report

        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter("DEFFINITY_ProjetReport_GETRAGDetails", MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub.Fill(dt1);
        rpt.Subreports["ProjectRAGStatusRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectRAGStatusRpt"].SetDataSource(dt1);

        #endregion

        #region For KeyTasks.rpt

        DataTable dt3 = new DataTable();
        SqlDataAdapter adp_sub2 = new SqlDataAdapter("DEFFINITY_ProjetReport_AchivementsLastweek", MyCon);
        adp_sub2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub2.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub2.Fill(dt3);
        rpt.Subreports["AchivementsLastWeekRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["AchivementsLastWeekRpt"].SetDataSource(dt3);

        #endregion

        #region For Achievements.rpt

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp_sub3 = new SqlDataAdapter("DEFFINITY_ProjetReport_AchivementsNextweek", MyCon);
        adp_sub3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub3.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub3.Fill(dt4);
        rpt.Subreports["AchivementsNextWeekRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["AchivementsNextWeekRpt"].SetDataSource(dt4);

        #endregion

        #region For KEY Risks.rpt

        DataTable dt10 = new DataTable();
        SqlDataAdapter adp_sub10 = new SqlDataAdapter("DN_RPT_KeyRisks", MyCon);
        adp_sub10.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub10.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub10.Fill(dt10);
        rpt.Subreports["KeyRisksRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["KeyRisksRpt"].SetDataSource(dt10);

        #endregion

        #region ProjectProposals.rpt

        DataTable dt6 = new DataTable();
        SqlDataAdapter adp_sub5 = new SqlDataAdapter("DN_RPT_ProjectProposals", MyCon);
        adp_sub5.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub5.SelectCommand.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = sessionKeys.PortfolioID;
        adp_sub5.Fill(dt6);
        rpt.Subreports["PojectProposalsRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["PojectProposalsRpt"].SetDataSource(dt6);

        #endregion

        #region CostAndSummary.rpt

        DataTable dt7 = new DataTable();
        SqlDataAdapter adp_sub6 = new SqlDataAdapter("DN_RPT_CostAndResourceSummary", MyCon);
        adp_sub6.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub6.Fill(dt7);
        rpt.Subreports["CostingAndResourcesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["CostingAndResourcesRpt"].SetDataSource(dt7);

        #endregion

        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Program Report");
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