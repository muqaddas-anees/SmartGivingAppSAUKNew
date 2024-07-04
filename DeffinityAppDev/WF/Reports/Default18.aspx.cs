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
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class Default18 : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Bindparameters();
        BindReport();
    }

    private void Bindparameters()
    {
        rpt = new ReportDocument();

        CrystalReportViewer1.Enabled = true;
       
        //Load the selected report file.
        string str = "Report18.rpt";
        rpt.Load(Server.MapPath(str));
                
        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.SetDatabaseLogon("sa", "ems", "pc18", "AMPS");

        //Set the Crytal Report Viewer control's source to the report document.
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
    }
    private void BindReport()
    {
        rpt = new ReportDocument();

        CrystalReportViewer1.Enabled = true;

        //Load the selected report file.
        string str = "Report18.rpt";
        rpt.Load(Server.MapPath(str));
        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand MyCommand = new SqlCommand("AMPS_OutstandingQAissues", MyCon);
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
        //CrystalReportSource1.Unload += new EventHandler(CrystalReportSource1_Unload);
        //CrystalReportViewer1.Unload += new EventHandler(CrystalReportViewer1_Unload);
        
        rpt.Close();
        rpt.Dispose();
        rpt = null;
        CrystalReportViewer1.Dispose();
        CrystalReportSource1.Dispose();

    }
}
