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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
public partial class PerformmanceReviewReport : System.Web.UI.Page
{
    DisBindings getdata = new DisBindings();
    ReportDocument rpt;
    int OldSid;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
      
    protected void Page_Load(object sender, EventArgs e)
    {
       
            OldSid = Convert.ToInt32(Session["UID"].ToString());
            BindReport();
        
     
    }
    private void BindReport()
    {

        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "PerformmanceReviewReportt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_DisplayOwnerResource", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@RecourceID", SqlDbType.Int).Value = OldSid;//Convert.ToInt32(Session["UID"].ToString());
          SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(dt);
              
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        else
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
  
}
