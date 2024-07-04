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

public partial class Reports_OvertimeCutOffReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string etype = "";

                string sdate = string.Empty;
                string edate = string.Empty;
                string type = string.Empty;
               // string time = string.Empty;
                string ordby = string.Empty;
                //check the query sting values

                if (Request.QueryString["etype"] != null)
                    etype = Request.QueryString["etype"].ToString();
                if (Request.QueryString["sdate"] != null)
                    sdate = Request.QueryString["sdate"].ToString();
                if (Request.QueryString["edate"] != null)
                    edate = Request.QueryString["edate"].ToString();
                if (Request.QueryString["type"] != null)
                    type = Request.QueryString["type"].ToString();
                //if (Request.QueryString["time"] != null)
                //    time = Request.QueryString["time"].ToString();
                if (Request.QueryString["ord"] != null)
                    ordby = Request.QueryString["ord"].ToString();
                BindReport(sdate, edate, type, etype, ordby);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region BindReport
    private void BindReport(string fromDate,string toDate,string type,string EntryType,string ord)
    {
       
        rpt = new ReportDocument();

        string str = "OverTimeCutOffReport.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("TimeSheet_OvertimeCutOffReport ", myConn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);

        cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = Convert.ToDateTime(toDate);
           

       // cmd.Parameters.Add("@Hours", SqlDbType.Float).Value = Convert.ToDouble(Hours);
        cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = EntryType;
        cmd.Parameters.Add("@ord", SqlDbType.Int).Value = int.Parse(ord);
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

        DataTable dt3 = new DataTable();
        SqlDataAdapter subAdapter1 = new SqlDataAdapter("TimeSheet_OvertimeCutOffReportSummary", myConn);
        subAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;

        subAdapter1.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value =
            Convert.ToDateTime(fromDate);
        subAdapter1.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
             Convert.ToDateTime(toDate);
       // subAdapter1.SelectCommand.Parameters.Add("@Hours", SqlDbType.Float).Value = Convert.ToDouble(Hours);
        subAdapter1.SelectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = EntryType;
        subAdapter1.SelectCommand.Parameters.Add("@ord", SqlDbType.Int).Value = int.Parse(ord);
        subAdapter1.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        subAdapter1.Fill(dt3);

        rpt.Subreports["summaryCutOff"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["summaryCutOff"].SetDataSource(dt3);

        DataTable dt2 = new DataTable();
        SqlDataAdapter subAdapter = new SqlDataAdapter("TimeSheet_MissedCutOffReport ", myConn);
        subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
     
        subAdapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value =
            Convert.ToDateTime(fromDate);
        subAdapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
             Convert.ToDateTime(toDate);
        //subAdapter.SelectCommand.Parameters.Add("@Hours", SqlDbType.Float).Value =Convert.ToDouble(Hours);
        subAdapter.SelectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = EntryType;
        subAdapter.SelectCommand.Parameters.Add("ord", SqlDbType.Int).Value = int.Parse(ord);
        subAdapter.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        subAdapter.Fill(dt2);

        rpt.Subreports["MissedCutOff"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["MissedCutOff"].SetDataSource(dt2);


        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        if (type == "xsl1")
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Cutoff Report");
        else if (type == "pdf")
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Cutoff Report");
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Cutoff Report");
        }

    }
    #endregion
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
