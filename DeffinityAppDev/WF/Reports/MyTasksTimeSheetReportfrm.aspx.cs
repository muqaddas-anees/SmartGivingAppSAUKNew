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
public partial class Reports_MyTasksTimeSheetReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                

                string sdate = string.Empty;
                string edate = string.Empty;
                string type = string.Empty;
                string time = string.Empty;
                //check the query sting values

                
                if (Request.QueryString["sdate"] != null)
                    sdate = Request.QueryString["sdate"].ToString();
                if (Request.QueryString["edate"] != null)
                    edate = Request.QueryString["edate"].ToString();
                if (Request.QueryString["type"] != null)
                    type = Request.QueryString["type"].ToString();
                
                BindReport(sdate, edate,type);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindReport(string fromDate,string toDate,string type)
    {
        rpt = new ReportDocument();

        string str = "MyTasksTimeSheetReport.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("TimesSheet_MyTaskSummaryRpt", myConn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value =sessionKeys.UID;
        cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fromDate);
        cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = Convert.ToDateTime(toDate);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);


        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        if (type == "xsl1")
        {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "My Tasks Timesheet Report");
        }
        else
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "My Tasks Timesheet Report");
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
