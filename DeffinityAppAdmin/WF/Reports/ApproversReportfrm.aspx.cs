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
public partial class Reports_ApproversReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = string.Empty;
            string status = string.Empty;
            string ord = "0";
          
            //check the query sting values


            if (Request.QueryString["Status"] != null)
                status = Request.QueryString["Status"].ToString();
            if (Request.QueryString["Ord"] != null)
                ord = Request.QueryString["Ord"].ToString();
            
            if (Request.QueryString["type"] != null)
            {
                type = Request.QueryString["type"];
            }
            BindReport(type,status,int.Parse(ord));
        }
    }
    private void BindReport(string type,string status,int ord)
    {
        rpt = new ReportDocument();

        string str = "ApproversReport.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("Timesheet_ApproverReport ", myConn);
       
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@orderBy", SqlDbType.Int).Value = ord;
        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Approver Report");
        }
        else if (type != "xsl1")
        {
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Approver Report");
        }
        else
        {
             rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Approver Report");
        }
    }
}
