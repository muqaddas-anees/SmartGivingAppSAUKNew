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

public partial class Default4 : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindReport();
/*
        //Dim rpt As ReportDocument
        //rpt = New ReportDocument
        //CrystalReportViewer1.DataBind()
        //rpt.Load("c:\inetpub\wwwroot\west\reports\rep1.rpt", OpenReportMethod.OpenReportByDefault)
        //rpt.SetDatabaseLogon("sa", "123", "server1", "eastdb")
        //CrystalReportViewer1.ReportSource = rpt
        //CrystalReportViewer1.ShowFirstPage()


        //CrystalReportViewer1.Enabled = true;


        ReportDocument rpt = new ReportDocument();
        //CrystalReportViewer1.EnableParameterPrompt = false;
        //CrystalReportSource1.Report.Parameters[0].DefaultValue = txtFrom.Text;
        //CrystalReportSource1.Report.Parameters[1].DefaultValue = txtTo.Text;
        //rpt.SetParameterValue(0, txtFrom);
        //rpt.SetParameterValue(1, txtTo);



        //rpt.Load(@"c:\inetpub\wwwroot\AMPS\report2.rpt", OpenReportMethod.OpenReportByDefault);

        //rpt.Load(@"c:\inetpub\wwwroot\AMPS\report4.rpt");
        string str="Report4.rpt";
        rpt.Load(Server.MapPath(str));
        string s = Server.MapPath(str);
        CrystalReportViewer1.DataBind();
        //rpt.SetDatabaseLogon("sa", "ems");        
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.SetDatabaseLogon("sa", "ems", "pc18", "AMPS");
        
        //CrystalReportSource1.Report = (CrystalDecisions.Web.Report) rpt;
        CrystalReportViewer1.ReportSource = rpt;
        

     

        //CrystalReportSource1.ReportDocument.DataSourceConnections = ConfigurationSettings.AppSettings["connectionString"];            
        //CrystalReportSource1.ReportDocument.Database = ConfigurationSettings.AppSettings["connectionString"];
        //Report2         
        //string str = CrystalReportSource1.Report.DataSources.Count.ToString();

        //Response.Write(str.ToString());
        //SqlConnection mycon = new SqlConnection("Data Source=192.168.1.9;Initial Catalog=AMPS;User Id=sa;Password=ems;");
        //CrystalReportViewer1.Visible = true;
*/

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
        //CrystalReportSource1.Unload += new EventHandler(CrystalReportSource1_Unload);
        //CrystalReportViewer1.Unload += new EventHandler(CrystalReportViewer1_Unload);
        
        rpt.Close();
        rpt.Dispose();
        rpt = null;
        CrystalReportViewer1.Dispose();
        CrystalReportSource1.Dispose();

    }
}
