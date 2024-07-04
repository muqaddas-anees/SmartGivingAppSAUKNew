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

public partial class Reports_SRSummaryByPortfolio : System.Web.UI.Page
{
    ReportDocument rpt;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        LogExceptions.WriteExceptionLog(ex);
        Server.ClearError();
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        rpt = new ReportDocument();

        //Load the selected report file.
        string str = "SRSummaryByPortfolio.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("CaseSummaryReportByPortfolio", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = Convert.ToInt32(ddlPortfolio.SelectedValue);
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Summary");
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
