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
using Microsoft.ApplicationBlocks.Data;

public partial class Reports_SRMainRpt : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          BindReport();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindReport()
    {
        rpt = new ReportDocument();

        //Load the selected report file.
        string str = "SRMainRpt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_SDTestNew", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@Portfolio", sessionKeys.PortfolioID);
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        DataTable dt1 = new DataTable();
        SqlCommand myCommand1 = new SqlCommand("DN_SRByAreaBottom", MyCon);
        myCommand1.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.AddWithValue("@Portfolio", sessionKeys.PortfolioID);
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
        myAdapter1.Fill(dt1);
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);

        DataTable dt2 = new DataTable();
        SqlCommand myCommand2 = new SqlCommand("DN_SRByAreaFooter", MyCon);
        myCommand2.CommandType = CommandType.StoredProcedure;
        myCommand2.Parameters.AddWithValue("@Portfolio", sessionKeys.PortfolioID);
        SqlDataAdapter myAdapter2 = new SqlDataAdapter(myCommand2);
        myAdapter1.Fill(dt2);
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[1].SetDataSource(dt2);




        // rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Summary");
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "SR Report by Category summay");
        //ExportOptions exp = new ExportOptions();
        //exp.ExportFormatType = ExportFormatType.Excel;

       // ExportFormatOptions excelFormatOptions = new ExcelFormatOptions();
        
        // excelFormatOptions.ExcelUseConstantColumnWidth = True;
        //excelFormatOptions.ExcelUseConstantColumnWidth = False;
        //rpt.ExportToHttpResponse(exp, Response, false, "ShiftRotaReport for" + txtFrom.Text + "-" + txtTo.Text);
        MyCon.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        //CrystalReportSource1.Unload += new EventHandler(CrystalReportSource1_Unload);
        //CrystalReportViewer1.Unload += new EventHandler(CrystalReportViewer1_Unload);
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
        }
       

    }
}
