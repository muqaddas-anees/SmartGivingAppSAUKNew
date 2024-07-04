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

public partial class Reports_ProjectPlanRpt : System.Web.UI.Page
{
    ReportDocument rpt;    
    int projectPlanID = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        BindReport();
        Page.Title = "Project Plan for Project Plan ID " + projectPlanID;

    }

    void BindReport()
    {
        rpt = new ReportDocument();
       

        //Load the selected report file.

        string str = "ProjectPlanNew.rpt";
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
        SqlCommand myCommand = new SqlCommand("DEFFINITY_ProjectplanRpt", MyCon);
        SqlCommand myCommand1 = new SqlCommand("DEFFINITY_ProjectplanActivityRpt", MyCon);


        if (Request.QueryString["Id"] != null)
            projectPlanID = Convert.ToInt32(Request.QueryString["Id"]);
        else
            projectPlanID = 0;

        myCommand.Parameters.AddWithValue("ProjectPlanID", projectPlanID);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.AddWithValue("ProjectPlanID", projectPlanID);
        myCommand1.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
        myAdapter1.Fill(dt1);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);
       

        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Proposal");
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
          
        }

    }
}
