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

public partial class Reports_ProjectStatusReportViewer : System.Web.UI.Page
{
    #region ReportMethods

    ReportDocument rpt;
    string ProjectReference = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        int programmeid = 0;
        int countryid = 0;
        int subprogrammeid = 0;
        if (Request.QueryString["programmeid"] != null)
            programmeid = int.Parse(Request.QueryString["programmeid"].ToString());
        if (Request.QueryString["Projects"] != null)
            ProjectReference = Request.QueryString["Projects"].ToString();
        if (Request.QueryString["countryid"] != null)
            countryid = int.Parse(Request.QueryString["countryid"].ToString());
        if (Request.QueryString["subprogrammeid"] != null)
            subprogrammeid = int.Parse(Request.QueryString["subprogrammeid"].ToString());
        BindReport(programmeid, countryid, subprogrammeid);
    }
    private void BindReport(int programmeid, int countryid, int subprogrammeid)
    {
        rpt = new ReportDocument();

        //Load the selected report file.

        string str = "ProjectStatusReport.rpt";
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

        ////Set the Crytal Report Viewer control's source to the report document.
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
        //rpt.Subreports["KeyRisksRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["KeyRisksRpt"].SetDataSource(dt10);

        #endregion


        #region ProjectProposals.rpt

        DataTable dt6 = new DataTable();
        SqlDataAdapter adp_sub5 = new SqlDataAdapter("DN_RPT_ProjectProposals", MyCon);
        adp_sub5.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub5.SelectCommand.Parameters.Add("@ProgrammeID", SqlDbType.Int).Value = sessionKeys.ProgrammeID;
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
        #region IssueComments.rpt
        DataTable dtIssueComments = new DataTable();
        SqlDataAdapter adp_subIssueComments = new SqlDataAdapter("Project_IssueCommentsRpt", MyCon);
        adp_subIssueComments.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_subIssueComments.SelectCommand.Parameters.Add("@ProjectRef", SqlDbType.VarChar).Value = ProjectReference;
        adp_subIssueComments.Fill(dtIssueComments);
        rpt.Subreports["IssueComments.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["IssueComments.rpt"].SetDataSource(dtIssueComments);

        #endregion



        #region newlyAddedCode

        //DataTable dtnew = new DataTable();

        DataTable dt2 = new DataTable();
        SqlDataAdapter adp1 = new SqlDataAdapter("Programme_rptRisk_new", MyCon);
        adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp1.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp1.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp1.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        //@subprogramme
        adp1.Fill(dt2);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDataSource(dt2);

        DataTable dt5 = new DataTable();
        SqlDataAdapter adp2 = new SqlDataAdapter("Programme_rptMilestone_new", MyCon);
        adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp2.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp2.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp2.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp2.Fill(dt5);
        rpt.Subreports["ProgrammeMilestones.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeMilestones.rpt"].SetDataSource(dt5);

        DataTable dt8 = new DataTable();
        SqlDataAdapter adp3 = new SqlDataAdapter("Programme_rptBenefit_new", MyCon);
        adp3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp3.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp3.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp3.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp3.Fill(dt8);
        rpt.Subreports["ProjectBenefit.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectBenefit.rpt"].SetDataSource(dt8);

        DataTable dt9 = new DataTable();
        SqlDataAdapter adp4 = new SqlDataAdapter("Programme_rptIssues_new", MyCon);
        adp4.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp4.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp4.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp4.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp4.Fill(dt9);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDataSource(dt9);


        DataTable dt11 = new DataTable();
        SqlDataAdapter adp10 = new SqlDataAdapter("DEFFINITY_ProjetReport_Tasklist", MyCon);
        adp10.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp10.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp10.Fill(dt11);
        rpt.Subreports["ProjectTask.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectTask.rpt"].SetDataSource(dt11);


        DataTable dt14 = new DataTable();
        SqlDataAdapter adp12 = new SqlDataAdapter("deffinity_projectmeetingselect", MyCon);
        adp12.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp12.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp12.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = null;
        adp12.Fill(dt14);
        rpt.Subreports["ProjectUpdates.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectUpdates.rpt"].SetDataSource(dt14);



        //DataTable dt15 = new DataTable();
        //SqlDataAdapter adp15 = new SqlDataAdapter("Project_FinancialRpt", MyCon);
        //adp15.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp15.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        ////adp15.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = null;
        //adp15.Fill(dt15);
        //rpt.Subreports["ProjectFinancial.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["ProjectFinancial.rpt"].SetDataSource(dt15);


        DataTable dt16 = new DataTable();
        SqlDataAdapter adp16 = new SqlDataAdapter("Project_TrackerRpt", MyCon);
        adp16.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp16.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp16.Fill(dt16);
        rpt.Subreports["ProjectTracker.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectTracker.rpt"].SetDataSource(dt16);
        #endregion



       // displayReport("pdf");






        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Program Report");
        Response.End();

        MyCon.Close();
        MyCon.Dispose();
    }
    private void displayReport(string Format)
    {
        System.IO.MemoryStream oStream;
        switch (Format)
        {
            case "pdf":
            default:
                oStream = (System.IO.MemoryStream)rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.ClearHeaders();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.AppendHeader("Content-Disposition", "attachment; FileName=Report.pdf");
                Response.End();
                rpt.Dispose();
                break;
        }
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
