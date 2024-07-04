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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class GanttReport3 : System.Web.UI.Page
{
    ReportDocument rpt;
    int _Pref;
    protected void Page_Load(object sender, EventArgs e)
    {

        _Pref = Convert.ToInt32(Request.QueryString["ProjectReference"]);
        if (_Pref==0)
        {
            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
        }
        else
        {
            BindReport();
           
        }      
        
   }

   private void BindReport()
   {


       rpt = new ReportDocument();
       CrystalReportViewer1.Enabled = true;
       CrystalReportViewer1.EnableParameterPrompt = false;
       CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

       //Load the selected report file.

       string str = "GanttReport3.rpt";
       rpt.Load(Server.MapPath(str));

       //Set the Database Login Information
       string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
       string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
       string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
       string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

       DataTable dt = new DataTable();

       string strConn = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
       SqlConnection MyCon = new SqlConnection(strConn);
       SqlCommand myCommand = new SqlCommand("AMPS_ProjectforecastContractorsDetails", MyCon);
       myCommand.CommandType = CommandType.StoredProcedure;

       myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = _Pref;
       myCommand.Parameters.Add("@LID", SqlDbType.VarChar, 100).Value = "null";
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
