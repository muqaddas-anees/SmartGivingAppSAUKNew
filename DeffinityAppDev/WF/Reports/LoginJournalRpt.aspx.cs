using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Reports_LoginJournalRpt : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindReport();
    }
    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "LoginJournalRpt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
       
            string strConn = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            SqlConnection MyCon = new SqlConnection(strConn);
            SqlCommand myCommand = new SqlCommand("Deffinity_ProjectJournal", MyCon);
            myCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(dt);
       
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;

    }
}
