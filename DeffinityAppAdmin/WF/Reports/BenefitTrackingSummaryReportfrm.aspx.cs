using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Reports_BenefitTrackingSummaryReportfrm : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void BindReport()
    {
        rpt = new ReportDocument();
        //Load the selected report file.

        string str = "~/Reports/BenefitTrackingSummaryReport.rpt";
        try
        {
            string path = Server.MapPath(str);
            rpt.Load(path);
        }
        catch (Exception)
        {
            rpt.Dispose();
        }

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);

        SqlCommand myCommand = new SqlCommand("Deffinity_BenefitTrackingSummary1", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.AddWithValue("@UserID", sessionKeys.UID);
        myCommand.Parameters.AddWithValue("@PortfolioID", Convert.ToInt32(Request.QueryString["PortfolioID"].ToString()));
        myCommand.Parameters.AddWithValue("@ProgrammeID", Convert.ToInt32(Request.QueryString["ProgID"].ToString()));
        myCommand.Parameters.AddWithValue("@SubprogrammeID", Convert.ToInt32(Request.QueryString["SubprogID"].ToString()));
        myCommand.Parameters.AddWithValue("@BenfitID", Convert.ToInt32(Request.QueryString["BenefitID"].ToString()));

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        DataTable dt = new DataTable();
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        string type = string.Empty;
        type = Request.QueryString["type"].ToString();
        if (type == "pdf")
        {
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "BenefitTrackingSummaryReport.pdf");
        }
        else if (type == "xls")
        {
            displayReport("xls");
        }

    }
    private void displayReport(string Format)
    {
        System.IO.MemoryStream oStream;
        switch (Format)
        {
            case "xls":
            default:
                oStream = (System.IO.MemoryStream)rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                Response.ClearHeaders();
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/xls";
                Response.BinaryWrite(oStream.ToArray());
                Response.AppendHeader("Content-Disposition", "attachment; FileName=BenefitTrackingSummaryReport.xls");
                Response.End();
                rpt.Dispose();
                break;
        }
    }
}
