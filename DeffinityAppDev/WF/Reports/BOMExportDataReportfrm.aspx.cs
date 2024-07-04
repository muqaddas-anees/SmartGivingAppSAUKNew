using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
public partial class Reports_BOMExportDataReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string Prjref = "0";

                string sdate = string.Empty;
                string edate = string.Empty;
                string type = string.Empty;
                string vend = "0";
               
                //check the query sting values

                if (Request.QueryString["Prjref"] != null)
                    Prjref = Request.QueryString["Prjref"].ToString();
                if (Request.QueryString["sdate"] != null)
                    sdate = Request.QueryString["sdate"].ToString();
                if (Request.QueryString["edate"] != null)
                    edate = Request.QueryString["edate"].ToString();
                if (Request.QueryString["vend"] != null)
                    vend = Request.QueryString["vend"].ToString();
                if (Request.QueryString["type"] != null)
                    type = Request.QueryString["type"].ToString();
                BindReport(Prjref, sdate, edate, vend,type);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindReport(string Prjref, string fromDate, string toDate, string vend, string type)
    {
        rpt = new ReportDocument();

        //string str = "AbsenceReport.rpt";
        //string str = "CrystalReport3Delete.rpt";
        string str = "BOMExportData.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("BOMExportDataReport", myConn); // Timesheet_AbsenceReportSummary
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = int.Parse(Prjref);
        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);

        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value =
            Convert.ToDateTime(toDate);
        cmd.Parameters.Add("@Supplier", SqlDbType.Int).Value =
           int.Parse(vend);
        
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

       
        //rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports[1].SetDataSource(dt2);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
       

        if (type == "xsl1")
        {

            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Project BOM Report");
        }
        else if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project BOM Report");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Project BOM Report");
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
