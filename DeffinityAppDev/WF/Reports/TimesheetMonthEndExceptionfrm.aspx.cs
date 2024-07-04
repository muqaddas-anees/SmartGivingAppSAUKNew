using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Microsoft.ApplicationBlocks.Data;

public partial class TimesheetMonthEndExceptionRept : System.Web.UI.Page
{
    ReportDocument rpt;
    DisBindings getData = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string id = "0";
                string etype = "0";
                string sdate = string.Empty;
                string edate = string.Empty;
                string type = string.Empty;
                string team = string.Empty;
                string ord = string.Empty;
                
                //check the query sting values
                if (Request.QueryString["etype"] != null)
                    etype = Request.QueryString["etype"].ToString();
                if (Request.QueryString["id"] != null)
                    id = Request.QueryString["id"].ToString();
                if (Request.QueryString["sdate"] != null)
                    sdate = Request.QueryString["sdate"].ToString();
                if (Request.QueryString["edate"] != null)
                    edate = Request.QueryString["edate"].ToString();
                if (Request.QueryString["type"] != null)
                    type = Request.QueryString["type"].ToString();
                if (Request.QueryString["team"] != null)
                    team = Request.QueryString["team"].ToString();
                if (Request.QueryString["ord"] != null)
                    ord = Request.QueryString["ord"].ToString();
                BindReports(sdate, edate, etype, type, id,team,ord);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    


    private void BindReports(string fromDate,string toDate,string entryType,string type,string ID,string TeamID,string ord)
    {
        rpt = new ReportDocument();

        //Load the selected report file.

        string str = "TimsheetMonthEndExceptionNew.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("TimesheetMonthEndException_New", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value =
           Convert.ToDateTime(fromDate);
        myCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
           Convert.ToDateTime(toDate);
        myCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = entryType;
        myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ID);
        myCommand.Parameters.Add("@TeamID", SqlDbType.Int).Value = int.Parse(TeamID);
        myCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = int.Parse(ord);
        myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        
        SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
        adapter.Fill(dt);

        DataTable dt3 = new DataTable();
        SqlDataAdapter subAdapter1 = new SqlDataAdapter("TimesheetMonthEndException_NewEqual", MyCon);
        subAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;

        subAdapter1.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value =
           Convert.ToDateTime(fromDate);
        subAdapter1.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value =
           Convert.ToDateTime(toDate);
        subAdapter1.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = entryType;
        subAdapter1.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ID);
        subAdapter1.SelectCommand.Parameters.Add("@TeamID", SqlDbType.Int).Value = int.Parse(TeamID);
        subAdapter1.SelectCommand.Parameters.Add("@OrderBY", SqlDbType.Int).Value = int.Parse(ord);
        subAdapter1.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
       
        subAdapter1.Fill(dt3);

        rpt.Subreports["SubMonthEndException"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["SubMonthEndException"].SetDataSource(dt3);
      
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        if (type == "xsl1")
            rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Month End Exception Report");
        else if(type=="xsl")
            rpt.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "Month End Exception Report");
            else
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Month End Exception Report");
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
