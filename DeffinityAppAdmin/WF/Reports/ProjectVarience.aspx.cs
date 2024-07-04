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


public partial class Reports_ProjectVarience : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);
    string strConn = System.Configuration.ConfigurationManager.AppSettings["connectionString"];

    string listID = "";
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"] != null)
        {
            BindRpt(Request.QueryString["Project"].ToString());
        }
    }

    protected void BindRpt(string LID)
    {

        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ProjectVarience.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("Deffinity_ProjVarience", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;


        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Convert.ToInt32(LID);
        
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);



        myCommand = new SqlCommand("Deffinity_Scope", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Convert.ToInt32(LID);
        myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;

    }
}
