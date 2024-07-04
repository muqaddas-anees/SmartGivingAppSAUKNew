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
public partial class Reports_ResourcePlanner : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        lblmsg.Text = "";
    }

    private void CheckDates(int type)
    {

        DataTable tbl1;

        DateTime FromDate = Convert.ToDateTime(txtFrom.Text);
        DateTime ToDate = Convert.ToDateTime(txtTo.Text);
        int Datediff;
        // Datediff = Convert.ToInt32(SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure,"DN_GetDateDiff",new SqlParameter[]{new SqlParameter("@fromDate",txtFrom.Text),new SqlParameter("@ToDate",txtTo.Text)} ).Tables[0].Rows[0][0]);
        tbl1 = SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DN_GetDateDiff", new SqlParameter[] { new SqlParameter("@fromDate", FromDate), new SqlParameter("@ToDate", ToDate) }).Tables[0];
        Datediff = Convert.ToInt32(tbl1.Rows[0][0]);
        if (Datediff >= 4)
        {
            lblmsg.Text = "Please select a period of less than or equal to 3 months";
        }
        else
        {
            BindReport(type);
        }
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        int type = 1;
        CheckDates(type);
    }
    private void BindReport(int printtype)
    {
        rpt = new ReportDocument();

        //Load the selected report file.
        string str = "ResourcePlanner.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("Deffinity_ResourcePlanner_rpt", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = txtFrom.Text;
        myCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = txtTo.Text;
        myCommand.Parameters.Add("@PortfolioId", SqlDbType.Int).Value = Convert.ToInt32(ddlPortfolio.SelectedValue);
        myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        //@SDTeamIDs
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlCommand myCommand1 = new SqlCommand("Deffinity_ResourcePlanner_AvailableResources_rpt", MyCon);
        myCommand1.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = txtFrom.Text;
        myCommand1.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = txtTo.Text;
        myCommand1.Parameters.Add("@PortfolioId", SqlDbType.Int).Value = Convert.ToInt32(ddlPortfolio.SelectedValue);
        myCommand1.Parameters.Add("@UserID", SqlDbType.Int).Value = sessionKeys.UID;
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
        myAdapter1.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);


        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        // rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Summary");
        if(printtype == 1)
        rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Resource Planner for" + txtFrom.Text + "-" + txtTo.Text);
        else
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Resource Planner for" + txtFrom.Text + "-" + txtTo.Text);
        //ExportOptions exp = new ExportOptions();
        //exp.ExportFormatType = ExportFormatType.Excel;

        //ExportFormatOptions excelFormatOptions = new ExcelFormatOptions();
        //excelFormatOptions.ExcelUseConstantColumnWidth = True;
        // excelFormatOptions.ExcelUseConstantColumnWidth = False;


        //rpt.ExportToHttpResponse(exp, Response, false, "ShiftRotaReport for" + txtFrom.Text + "-" + txtTo.Text);
        MyCon.Close();
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
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        int type = 2;
        CheckDates(type);
    }

    private void BindData()
    {
        //ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        //ddlPortfolio.DataTextField = "PortFolio";
        //ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
        ddlPortfolio.Items.RemoveAt(0);
        ddlPortfolio.Items.Insert(0, new ListItem("All", "0"));
        //ddlPortfolio.DataBind();
        
    }
}
