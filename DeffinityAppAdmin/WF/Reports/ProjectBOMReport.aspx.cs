using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
public partial class Reports_ProjectBOMReport : System.Web.UI.Page
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
                BindReport(id,project);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindReport(string id,string project)
    {
        rpt = new ReportDocument();

        string str = "ProjectBOMReport.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection myConn = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand("ProjectBOM_WorksheetName ", myConn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = int.Parse(id);
      

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);

        DataTable dt2 = new DataTable();
        SqlDataAdapter subAdapter = new SqlDataAdapter("ProjectBOM_Service", myConn);
        subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter.SelectCommand.Parameters.Add("@workSheetID", SqlDbType.Int).Value = int.Parse(id);
       
        subAdapter.Fill(dt2);

        rpt.Subreports["ServiceRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ServiceRpt"].SetDataSource(dt2);


        DataTable dt3 = new DataTable();
        SqlDataAdapter subAdapter1 = new SqlDataAdapter("ProjectBOM_Misc ", myConn);
        subAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter1.SelectCommand.Parameters.Add("@workSheetID", SqlDbType.Int).Value = int.Parse(id);

        subAdapter1.Fill(dt3);

        rpt.Subreports["MiscRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["MiscRpt"].SetDataSource(dt3);


        DataTable dt4 = new DataTable();
        SqlDataAdapter subAdapter2 = new SqlDataAdapter("ProjectBOM_Labour ", myConn);
        subAdapter2.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter2.SelectCommand.Parameters.Add("@workSheetID", SqlDbType.Int).Value = int.Parse(id);

        subAdapter2.Fill(dt4);

        rpt.Subreports["LabourRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["LabourRpt"].SetDataSource(dt4);


        DataTable dt5 = new DataTable();
        SqlDataAdapter subAdapter3 = new SqlDataAdapter("ProjectBOM_Materials", myConn);
        subAdapter3.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter3.SelectCommand.Parameters.Add("@workSheetID", SqlDbType.Int).Value = int.Parse(id);

        subAdapter3.Fill(dt5);

        rpt.Subreports["MaterilasRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["MaterilasRpt"].SetDataSource(dt5);

        DataTable dt6 = new DataTable();
        SqlDataAdapter subAdapter4 = new SqlDataAdapter("DN_GetSummaryOfBOM", myConn);
        subAdapter4.SelectCommand.CommandType = CommandType.StoredProcedure;
        subAdapter4.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = int.Parse(project);
        subAdapter4.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = int.Parse(id);


        subAdapter4.Fill(dt6);

        rpt.Subreports["SummaryRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["SummaryRpt"].SetDataSource(dt6);


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
