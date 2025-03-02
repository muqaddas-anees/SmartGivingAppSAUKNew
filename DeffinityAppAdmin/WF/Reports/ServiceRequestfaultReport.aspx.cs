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
public partial class Reports_ServiceRequestfaultReport : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["type"].ToString() == "1")
            {
                BindReport("Service Request");
            }
            else
            {
                BindReport("Fault");
            }
        }
    }
    private void BindReport(string type)
    {
        rpt = new ReportDocument();
        //CrystalReportViewer1.Enabled = true;
        //CrystalReportViewer1.EnableParameterPrompt = false;
        //CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ServviceRequestRPt.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_SRBySubCatincident", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@indidentType", SqlDbType.VarChar).Value = type;
        myCommand.Parameters.Add("@DateLogged", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : "01/01/1900");
       myCommand.Parameters.Add("@portfolioID", SqlDbType.Int).Value = Convert.ToInt32(sessionKeys.PortfolioID);




        //myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ContractorID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);


        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);


        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Volume of calls by category");

        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;
        //CrystalReportSource1.DataBind();
        //CrystalReportViewer1.DataBind();
    }
    protected void btn_Submitt_Click(object sender, ImageClickEventArgs e)
    {
        //BindReport();
    }
}
