using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Reports_TimesheetSummaryofHoursfrm : System.Web.UI.Page
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
            string order = "1";
            string userid = "0";
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
            if (Request.QueryString["order"] != null)
                order = Request.QueryString["order"].ToString();
            if (Request.QueryString["userid"] != null)
                userid = Request.QueryString["userid"].ToString();
            BindReport(teamid, resourceid, sdate, edate, type1, custid,order,userid);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.End();
            if (!ex.Message.Contains("Unable to evaluate"))
                LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindReport(string teamid, string resourceid, string sdate, string edate, string type, string custid,string order,string userid)
    {
        rpt = new ReportDocument();


        //Load the selected report file.
        string str = "TimesheetSummaryHours.rpt";
        //string str = "TimesheetSummaryHoursType.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = Constants.DBString;
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_TimeSheetsummaryofHoursReport", MyCon);
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
        if (order == "1")
        {
            myCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = 1;
        }
        else
        {
            myCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = 2;
        }
        if (userid == "0")
        {
            myCommand.Parameters.Add("@LgdUserID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@LgdUserID", SqlDbType.Int).Value = userid;
        }
            



        myCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);
        myCommand.CommandTimeout = 1000;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);




        DataTable dt4 = new DataTable();

        myCommand = new SqlCommand("DN_TimeSheetsummaryofHoursSubReport", MyCon);
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
        if (order == "1")
        {
            myCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = 1;
        }
        else
        {
            myCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = 2;
        }
        if (userid == "0")
        {
            myCommand.Parameters.Add("@LgdUserID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@LgdUserID", SqlDbType.Int).Value = userid;
        }




        myCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);
        myCommand.CommandTimeout = 1000;
        myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt4);
        rpt.Subreports["subReport.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["subReport.rpt"].SetDataSource(dt4);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        MyCon.Close();

        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Timesheet summary of Hours");
        }
        else if (type == "xsl")
        {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Timesheet summary of Hours");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Timesheet summary of Hours");
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
