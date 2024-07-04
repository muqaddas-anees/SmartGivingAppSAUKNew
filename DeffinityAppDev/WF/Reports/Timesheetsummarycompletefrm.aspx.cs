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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class Reports_Timesheetsummarycompletefrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
       try{
        string teamid ="0";
        string resourceid ="0";
        string sdate= string.Empty;
        string edate= string.Empty;
        string type1 = string.Empty;
        string custid = "0";
        //check the query sting values
        if (Request.QueryString["teamid"] != null)
            teamid = Request.QueryString["teamid"].ToString();
        if (Request.QueryString["resourceid"] != null)
            resourceid = Request.QueryString["resourceid"].ToString();
        if (Request.QueryString["sdate"] != null)
            sdate = Request.QueryString["sdate"].ToString();
        if (Request.QueryString["edate"] != null)
            edate = Request.QueryString["edate"].ToString();
        if (Request.QueryString["type"] != null)
            type1 = Request.QueryString["type"].ToString();
        if (Request.QueryString["customer"] != null)
            custid = Request.QueryString["customer"].ToString();
        BindReport(teamid, resourceid, sdate, edate, type1, custid);
         }
        catch (Exception ex)
        { 
            if(!ex.Message.Contains("Unable to evaluate"))
            LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindReport( string teamid,string resourceid,string sdate,string edate,string type,string cusotmer)
    {
        rpt = new ReportDocument();
        //CrystalReportViewer1.Enabled = true;
        //CrystalReportViewer1.EnableParameterPrompt = false;
        //CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "TimeandTaskSummary.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = Constants.DBString;
        SqlConnection cn = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_TimeSheetsummaryReport_New", cn);
        myCommand.CommandType = CommandType.StoredProcedure;

        if (cusotmer == "0")
        {
            myCommand.Parameters.Add("@Customer", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@Customer", SqlDbType.Int).Value = cusotmer;
        }

        if (teamid == "0")
        {
            myCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = "0";
        }
        else
        {
            myCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = teamid;
        }
        if (resourceid == "0")
        {
            myCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = resourceid;
        }


        myCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);



        DataTable dt2 = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter("DN_SummaryofTimesheet", cn);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (teamid == "0")
        {
            adp.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = "0";
        }
        else
        {
            adp.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = teamid;
        }
        if (resourceid == "0")
        {
            adp.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = 0;
        }
        else
        {
            adp.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = resourceid;
        }

        adp.SelectCommand.Parameters.Add("StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        adp.SelectCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);


        adp.Fill(dt2);
        rpt.Subreports["SubReport_TimeSheetSummary.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["SubReport_TimeSheetSummary.rpt"].SetDataSource(dt2);
        //SubReport_TimesheetSummaryGroup.rpt
        DataTable dt3 = new DataTable();
        SqlDataAdapter adp3 = new SqlDataAdapter("DN_SummaryofTimesheetByResource", cn);
        adp3.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (teamid == "0")
        {
            adp3.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = "0";
        }
        else
        {
            adp3.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = teamid;
        }
        if (resourceid == "0")
        {
            adp3.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = 0;
        }
        else
        {
            adp3.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = resourceid;
        }

        adp3.SelectCommand.Parameters.Add("StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        adp3.SelectCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);


        adp3.Fill(dt3);
        //rpt.Subreports["SubReport_TimesheetSummaryGroup.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["SubReport_TimesheetSummaryGroup.rpt"].SetDataSource(dt3);
        rpt.Subreports["subEntryType.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["subEntryType.rpt"].SetDataSource(dt3);

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp4 = new SqlDataAdapter("DN_SummaryofDeclineTimesheetJournalByResource", cn);
        adp4.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (teamid == "0")
        {
            adp4.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = "0";
        }
        else
        {
            adp4.SelectCommand.Parameters.Add("@Team", SqlDbType.NVarChar).Value = teamid;
        }
        if (resourceid == "0")
        {
            adp4.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = 0;
        }
        else
        {
            adp4.SelectCommand.Parameters.Add("ResourceID", SqlDbType.Int).Value = resourceid;
        }

        adp4.SelectCommand.Parameters.Add("StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        adp4.SelectCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);


        adp4.Fill(dt4);
        rpt.Subreports["Decline.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["Decline.rpt"].SetDataSource(dt4);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Timesheet Completed");
        }
        else if (type == "xsl1")
        {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Timesheet Completed");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Timesheet Completed");
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
