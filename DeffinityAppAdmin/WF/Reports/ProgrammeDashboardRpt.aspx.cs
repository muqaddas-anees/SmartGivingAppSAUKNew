using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class Reports_ProgrammeDashboardRpt : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        int programmeid = 0;
        int countryid = 0;
        int subprogrammeid = 0;
        if (Request.QueryString["programmeid"] != null)
            programmeid = int.Parse(Request.QueryString["programmeid"].ToString());
        if(Request.QueryString["countryid"] != null)
            countryid = int.Parse(Request.QueryString["countryid"].ToString());
        if (Request.QueryString["subprogrammeid"] != null)
            subprogrammeid = int.Parse(Request.QueryString["subprogrammeid"].ToString());

        BindReport(programmeid, countryid, subprogrammeid);
    }
    private void BindReport(int programmeid, int countryid,int subprogrammeid)
    {
        rpt = new ReportDocument();

        //Load the selected report file.
        string str = "ProgrammeDashboardReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("Programme_rptProjects", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        myCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        myCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);


        DataTable dt2 = new DataTable();
        SqlDataAdapter adp1 = new SqlDataAdapter("Programme_rptRisk", MyCon);
        adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp1.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp1.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp1.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        //@subprogramme
        adp1.Fill(dt2);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDataSource(dt2);

        DataTable dt3 = new DataTable();
        SqlDataAdapter adp2 = new SqlDataAdapter("Programme_rptMilestone", MyCon);
        adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp2.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp2.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp2.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp2.Fill(dt3);
        rpt.Subreports["ProgrammeMilestone.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeMilestone.rpt"].SetDataSource(dt3);

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp3 = new SqlDataAdapter("Programme_rptBenefit", MyCon);
        adp3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp3.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp3.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp3.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp3.Fill(dt4);
        rpt.Subreports["ProjectBenefit.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectBenefit.rpt"].SetDataSource(dt4);

        DataTable dt5 = new DataTable();
        SqlDataAdapter adp4 = new SqlDataAdapter("Programme_rptIssues", MyCon);
        adp4.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp4.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp4.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp4.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp4.Fill(dt5);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDataSource(dt5);

        //if (type == "xsl")
        //    rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Programme Summary");
        //else if (type == "xsl1")
        //    rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Programme Summary");
        //else
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Programme Summary");

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
