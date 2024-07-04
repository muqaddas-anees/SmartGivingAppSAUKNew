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

public partial class Reports_HealthCheckItemspage : System.Web.UI.Page
{

    ReportDocument rpt = new ReportDocument();

    protected void Page_Init(object sender, EventArgs e)
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
                using (SqlCommand cmd = new SqlCommand(HealthCheckListItemsCommands.cmdSelectAll, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    int healthCheckListID = 0;
                    if (Request.QueryString["healthCheckID"] != null)
                        healthCheckListID = Convert.ToInt32(Request.QueryString["healthCheckID"]);
                    cmd.Parameters.AddWithValue("@HealthCheckID", healthCheckListID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        rpt.Load(Server.MapPath("HealthCheckListItems.rpt"));
                        //Set the Database Login Information
                        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
                        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
                        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
                        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
                        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);

                        table.Load(reader);
                        rpt.SetDataSource(table);
                        if (Request.QueryString["pdf"] != null)
                            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Health Check List Items");
                        else
                            CrystalReportViewer1.ReportSource = rpt;
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
            CrystalReportViewer1.Dispose();
        }
    }
}
