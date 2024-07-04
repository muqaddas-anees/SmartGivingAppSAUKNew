using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

public partial class Reports_ProjectTasks : System.Web.UI.Page
{
     ReportDocument rpt;
     string ProjectReference = string.Empty;

     protected void Page_Load(object sender, EventArgs e)
     {
         if (Request.QueryString["Project"] != null)
             ProjectReference = Request.QueryString["Project"].ToString();
         BindReport();
     }

     private void BindReport()
     {
         rpt = new ReportDocument();

         //Load the selected report file.

         string str = @"ProjectTasks.rpt";
         rpt.Load(Server.MapPath(str));

         //Set the Database Login Information
         string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
         string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
         string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
         string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

         DataTable dt = new DataTable();
         string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
         SqlConnection MyCon = new SqlConnection(strConn);
         SqlCommand myCommand = new SqlCommand("RPT_ProjectTasks", MyCon);
         myCommand.CommandType = CommandType.StoredProcedure;

         myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
         SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
         myAdapter.Fill(dt);

         //Set the Crytal Report Viewer control's source to the report document.
         rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
         rpt.SetDataSource(dt);

         Response.Clear();
         rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Tasks");
         Response.End();
     }

     protected void Page_UnLoad(object sender, EventArgs e)
     {
         if (rpt != null)
         {
             rpt.Close();
             rpt.Dispose();
         }
     }
}
