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

public partial class Reports_Timesheetsummaryjournalcompletefrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string teamid = "0";
            string resourceid = "0";
            string sdate = string.Empty;
            string edate = string.Empty;
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
            BindReport(teamid, resourceid, sdate, edate, type1,custid);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.End();
            if(!ex.Message.Contains("Unable to evaluate"))
            LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindReport(string teamid, string resourceid, string sdate, string edate,string type,string custid)
    {
        rpt = new ReportDocument();


        //Load the selected report file.

        string str = "TimesheetSummaryComplete.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        
        string strConn = Constants.DBString;
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_TimeSheetsummaryJournalReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        MyCon.Close();
        MyCon.Open();
        if (custid == "0")
        {
            myCommand.Parameters.Add("Customer", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("Customer", SqlDbType.Int).Value = custid;
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
        myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        myCommand.CommandTimeout = 1000;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);



        DataTable dt2 = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter("DN_SummaryofTimesheetJournal", MyCon);
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
        adp.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;

        adp.Fill(dt2);
        rpt.Subreports["subEntry.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["subEntry.rpt"].SetDataSource(dt2);
        //SubReport_TimesheetSummaryGroup.rpt
        DataTable dt3 = new DataTable();
        SqlDataAdapter adp3 = new SqlDataAdapter("DN_SummaryofTimesheetJournalByResource", MyCon);
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
        adp3.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;

        adp3.Fill(dt3);
        //rpt.Subreports["SubReport_TimesheetSummaryGroup.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["SubReport_TimesheetSummaryGroup.rpt"].SetDataSource(dt3);
        rpt.Subreports["subResourceEntry.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["subResourceEntry.rpt"].SetDataSource(dt3);

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp4 = new SqlDataAdapter("DN_SummaryofDeclineTimesheetJournalByResource", MyCon);
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
        adp4.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;

        adp4.Fill(dt4);
        rpt.Subreports["DeclinedSummary"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["DeclinedSummary"].SetDataSource(dt4);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        MyCon.Close();

        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Timesheet Journal");
        }
        else if (type == "xsl")
        {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Timesheet Journal");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Timesheet Journal");
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
