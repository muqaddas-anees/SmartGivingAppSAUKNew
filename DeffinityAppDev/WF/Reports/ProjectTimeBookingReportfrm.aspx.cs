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
public partial class Reports_ProjectTimeBookingReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string project = "0";
            string task = "0";
            string sdate = string.Empty;
            string edate = string.Empty;
            string type = string.Empty;
            //check the query sting values
            if (Request.QueryString["project"] != null)
                project = Request.QueryString["project"].ToString();
            if (Request.QueryString["task"] != null)
                task = Request.QueryString["task"].ToString();
            if (Request.QueryString["sdate"] != null)
                sdate = Request.QueryString["sdate"].ToString();
            if (Request.QueryString["edate"] != null)
                edate = Request.QueryString["edate"].ToString();
            if (Request.QueryString["type"] != null)
                type = Request.QueryString["type"].ToString();
            BindReport(project, task, sdate, edate, type);
        }
        catch (Exception ex)
        {
            if (!ex.Message.Contains("Unable to evaluate"))
                LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    private void BindReport(string project, string task, string sdate, string edate, string type)
    {
        rpt = new ReportDocument();

        //Load the selected report file.

        string str = "ProjectTimeBookingReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_TimesheetTaskBooking", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        if (project == "0")
        {

            myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = project;
        }
        if (task == "0")
        {
            myCommand.Parameters.Add("@ProjectTaskID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@ProjectTaskID", SqlDbType.Int).Value = task;
        }


        myCommand.Parameters.Add("@StatDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate);
        myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate);
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        if (type == "xsl")
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Timesheet Summary");
        else if (type == "xsl1")
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Timesheet Summary");
        else
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Timesheet Summary");

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
