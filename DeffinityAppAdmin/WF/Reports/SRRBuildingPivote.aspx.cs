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
public partial class Reports_SRRBuildingPivote : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ServiceRequestBuildingpivot.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_SRRBuildingPivote", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        

        if (ddlmainCategory.SelectedValue == "0")
        {
            myCommand.Parameters.Add("@ProjectCategoryMasterID", SqlDbType.Int).Value = 0;
        }
        else
        {
            myCommand.Parameters.Add("@ProjectCategoryMasterID", SqlDbType.Int).Value = ddlmainCategory.SelectedValue;
        }
        myCommand.Parameters.Add("@portfolioID", SqlDbType.Int).Value = 1;//Convert.ToInt32(sessionKeys.PortfolioID);
        myCommand.Parameters.Add("@DateLogged", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);





        //myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ContractorID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);


        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
     

      //This is for PDF format:-  rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service category");
//Below one is the Ecelformat

        rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Service Building Pivote");
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;
        //CrystalReportSource1.DataBind();
        //CrystalReportViewer1.DataBind();
    }
    protected void btn_Submitt_Click(object sender, ImageClickEventArgs e)
    {
        BindReport();
    }
}
