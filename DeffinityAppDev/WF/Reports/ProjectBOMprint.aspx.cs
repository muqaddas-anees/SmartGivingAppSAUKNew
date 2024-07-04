using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
public partial class Reports_ProjectBOMprint : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string id = "0";

                string project = "0";
                string edate = string.Empty;
                string type = string.Empty;
                //check the query sting values

                if (Request.QueryString["id"] != null)
                    id = Request.QueryString["id"].ToString();
                if (Request.QueryString["project"] != null)
                    project = Request.QueryString["project"].ToString();
                BindReport(id, project);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindReport(string id, string project)
    {
        rpt = new ReportDocument();

        string str = "ProjectBOMprint.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("ProjectBOM_Report ", myConn);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@Type", SqlDbType.Int).Value = int.Parse(id);
        cmd.Parameters.Add("@ProjectRef", SqlDbType.Int).Value = int.Parse(project);
        cmd.Parameters.Add("@WorkSheetID", SqlDbType.Int).Value = int.Parse(id);

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

        DataTable dt2 = new DataTable();
        SqlDataAdapter subAdapter = new SqlDataAdapter("ProjectBOM_Summary", myConn);
        subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter.SelectCommand.Parameters.Add("@ProjectRef", SqlDbType.Int).Value = int.Parse(project);
        subAdapter.SelectCommand.Parameters.Add("@WorkSheetID", SqlDbType.Int).Value = int.Parse(id);

        subAdapter.Fill(dt2);

        rpt.Subreports["SubReport"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["SubReport"].SetDataSource(dt2);


      
      


        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);



        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "BOM Report");

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
        }
    }
}
