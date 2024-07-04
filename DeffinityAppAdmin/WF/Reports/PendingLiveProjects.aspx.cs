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
using System.Data.SqlClient;

public partial class Reports_pendingLivePage : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //BindReport();

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        BindReport();

    }
    private void BindReport()
    {
        rpt = new ReportDocument();

        CrystalReportViewer1.Enabled = true;

        //Load the selected report file.
        string str = "Report4.rpt";
        rpt.Load(Server.MapPath(str));
        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand MyCommand = new SqlCommand("AMPS_LiveProjectReport", MyCon);
        MyCommand.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter MyAdapater = new SqlDataAdapter(MyCommand);
        MyAdapater.Fill(dt);
        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
        rpt = null;
        CrystalReportViewer1.Dispose();
        CrystalReportSource1.Dispose();

    }
}
