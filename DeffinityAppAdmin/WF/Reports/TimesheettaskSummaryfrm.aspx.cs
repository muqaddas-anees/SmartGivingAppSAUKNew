using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
public partial class Reports_TimesheettaskSummaryfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try{
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

        BindReport(teamid, resourceid, sdate, edate.Trim(), type1,custid);
         }
        catch (Exception ex)
        { 
            if(!ex.Message.Contains("Unable to evaluate"))
            LogExceptions.WriteExceptionLog(ex); }
    }
    private void BindReport(string teamid,string resourceid,string sdate,string edate,string type,string custid)
    {
        rpt = new ReportDocument();
       

        //Load the selected report file.

        string str = "TimesheetTasksSummary3.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(Constants.DBString);
        SqlCommand myCommand = new SqlCommand("DN_TimeSheetTasksummaryReport_New", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        if (custid == "0")
        {
            myCommand.Parameters.Add("@Customer", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@Customer", SqlDbType.Int).Value = custid;
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
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Summary");
        }
        else if(type=="xsl")
            {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Service Request Summary");
        }
            else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Service Request Summary");
        }

    }
}
