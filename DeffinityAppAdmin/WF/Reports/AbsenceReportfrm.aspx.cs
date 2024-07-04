using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
public partial class Reports_AbsenceReport : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string id = "0";
              
                string sdate = string.Empty;
                string edate = string.Empty;
                string type = string.Empty;
                 string atype = "0";
                 string ord1 = "0";
                 string team = "0";
                //check the query sting values
                
                if (Request.QueryString["id"] != null)
                    id = Request.QueryString["id"].ToString();
                if (Request.QueryString["sdate"] != null)
                    sdate = Request.QueryString["sdate"].ToString();
                if (Request.QueryString["edate"] != null)
                    edate = Request.QueryString["edate"].ToString();
                if (Request.QueryString["type"] != null)
                    type = Request.QueryString["type"].ToString();
                if (Request.QueryString["atype"] != null)
                    atype = Request.QueryString["atype"].ToString();
                if (Request.QueryString["ord"] != null)
                    ord1 = Request.QueryString["ord"].ToString();
                if (Request.QueryString["team"] != null)
                    team = Request.QueryString["team"].ToString();
                BindReport(id, sdate, edate, type, atype,ord1,team);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    private void BindReport(string id, string fromDate, string toDate, string type, string atype,string ord,string team1)
    {
        rpt = new ReportDocument();
       
        //string str = "AbsenceReport.rpt";
        //string str = "CrystalReport3Delete.rpt";
        string str = "AbsenceSummary.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


       

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("Project_AbsenceReport", myConn); // Timesheet_AbsenceReportSummary
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(id);
        cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);

        cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
            Convert.ToDateTime(toDate);
        cmd.Parameters.Add("@absencetype", SqlDbType.Int).Value =
           int.Parse(atype);
        cmd.Parameters.Add("@ord", SqlDbType.Int).Value =
          int.Parse(ord);
        cmd.Parameters.Add("@TeamID", SqlDbType.NVarChar).Value =
            int.Parse(team1);
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

        DataTable dt2 = new DataTable();
        SqlDataAdapter subAdapter = new SqlDataAdapter("Timesheet_AbsenceReportSummary", myConn);// Project_AbsenceReportSummary
        subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(id);
        subAdapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value =
           Convert.ToDateTime(fromDate);
        subAdapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
             Convert.ToDateTime(toDate);
        subAdapter.SelectCommand.Parameters.Add("@absencetype", SqlDbType.Int).Value =
          int.Parse(atype);
        subAdapter.SelectCommand.Parameters.Add("@ord", SqlDbType.Int).Value =
         int.Parse(ord);
        subAdapter.SelectCommand.Parameters.Add("@TeamID", SqlDbType.NVarChar).Value = team1;
        subAdapter.SelectCommand.Parameters.Add("@UID", SqlDbType.Int).Value = sessionKeys.UID;
        subAdapter.Fill(dt2);

        rpt.Subreports["AbsenceSummary1"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["AbsenceSummary1"].SetDataSource(dt2);
        //rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[1].SetDataSource(dt2);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);


        if (type == "xsl1")
        {

            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Absence Report");
        }
        else if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Absence Report");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Absence Report");
        }
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
