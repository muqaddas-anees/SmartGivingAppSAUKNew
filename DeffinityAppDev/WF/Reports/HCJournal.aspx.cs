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

public partial class Reports_HCJournal : System.Web.UI.Page
{
    ReportDocument rpt = new ReportDocument();

    protected void Page_Init(object sender, EventArgs e)
    {
     
    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    void BindReport()
    {
        DataTable table = new DataTable();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                string spName = string.Empty;
                if (ddlHealthCheck.SelectedValue.Equals("0"))
                    spName = "HealthCheckReportALL";
                else
                    spName = "HealthCheckReport";
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("PortfolioHealthCheckID", ddlHealthCheck.SelectedValue);
                    cmd.Parameters.AddWithValue("FromDate", Convert.ToDateTime(txtFromDate.Text));
                    cmd.Parameters.AddWithValue("ToDate", Convert.ToDateTime(txtToDate.Text));
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        rpt.Load(Server.MapPath("HCJournal.rpt"));
                        //Set the Database Login Information
                        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
                        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
                        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
                        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
                        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
                        table.Load(reader);
                        //CrystalReportViewer1.ReportSource = rpt;
                        rpt.SetDataSource(table);
                        //Response.Clear();
                        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "HCJournal");
                        //Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            table.Dispose();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Dispose();
            //CrystalReportViewer1.Dispose();
        }
    }
   
}
