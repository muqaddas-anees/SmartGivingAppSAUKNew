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
public partial class Reports_SRReportBysite : System.Web.UI.Page
{
    ReportDocument rpt;
    ReportDocument rpt1;

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
        try
        {
            rpt = new ReportDocument();
            rpt1 = new ReportDocument();

            //Load the selected report file.

            string str = "VolofCallsBySiteandMonth.rpt";
            string str1 = "VolofCallsByYear.rpt";
            rpt.Load(Server.MapPath(str));
            rpt1.Load(Server.MapPath(str1));

            //Set the Database Login Information
            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

            DataTable dt = new DataTable();

            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            SqlConnection MyCon = new SqlConnection(strConn);

            SqlCommand myCommand = new SqlCommand("DN_SRBySite_Month", MyCon);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@Portfolio", SqlDbType.Int, 32).Value = sessionKeys.PortfolioID;
            //myCommand.Parameters.AddWithValue("@Portfolio", sessionKeys.PortfolioID);
            //@SDTeamIDs
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(dt);

            DataTable dt1 = new DataTable();
            SqlCommand myCommand1 = new SqlCommand("DN_SRByYear_Portfolio", MyCon);
            myCommand1.CommandType = CommandType.StoredProcedure;
            myCommand1.Parameters.Add("@Portfolio", SqlDbType.Int).Value = sessionKeys.PortfolioID;
            SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
            myAdapter1.Fill(dt1);
            //Set the Crytal Report Viewer control's source to the report document.
            rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports[0].SetDataSource(dt1);

            //rpt1.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            //rpt1.SetDataSource(dt1);

            //Set the Crytal Report Viewer control's source to the report document.
            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);
          
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Annual Breakdown Report");

            MyCon.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
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
