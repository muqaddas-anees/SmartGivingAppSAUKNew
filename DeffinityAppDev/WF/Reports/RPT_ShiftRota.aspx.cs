using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.Web.Design;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class Reports_RPT_ShiftRota : System.Web.UI.Page
{
    ReportDocument rpt;
    
    protected void Page_Init(object sender, EventArgs e)
   {
        
    }

    private void BindReport()
    {

        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ShiftRota.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_Rpt_ShiftDetails", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("fromDate", Convert.ToDateTime(Request.QueryString["FromDate"]));
        myCommand.Parameters.AddWithValue("toDate", Convert.ToDateTime(Request.QueryString["ToDate"]));

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        myCommand = new SqlCommand("DN_Rpt_Shift", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
        }

        if (CrystalReportViewer1 != null)
            CrystalReportViewer1.Dispose();
        GC.Collect();
    }
}
