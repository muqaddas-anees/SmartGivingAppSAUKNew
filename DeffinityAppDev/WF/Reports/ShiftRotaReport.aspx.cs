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

public partial class Reports_ShiftRotaReport : System.Web.UI.Page
{
    ReportDocument rpt;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        try
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
                BindReport();
            }
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
        string str = "ShiftRota_Teams.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_ShiftRotaReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = txtFrom.Text;
        myCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = txtTo.Text;
        myCommand.Parameters.Add("@PortfolioId", SqlDbType.Int).Value = Convert.ToInt32(ddlPortfolio.SelectedValue);
        myCommand.Parameters.Add("@TeamIDs", SqlDbType.NVarChar, 100).Value = GetTeamIDs();
        myCommand.Parameters.Add("@SDTeamIDs", SqlDbType.NVarChar, 100).Value = GetSDTeamIDs();
        //@SDTeamIDs
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlCommand myCommand1 = new SqlCommand("DN_GetShiftColors", MyCon);
        myCommand1.CommandType = CommandType.StoredProcedure;
        myCommand1.Parameters.Add("@PortfolioId", SqlDbType.Int).Value = Convert.ToInt32(ddlPortfolio.SelectedValue);
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCommand1);
        myAdapter1.Fill(dt1);
        rpt.Subreports["ColorLegend"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ColorLegend"].SetDataSource(dt1);
        

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        // rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Summary");

        rpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "ShiftRotaReport for" + txtFrom.Text + "-" + txtTo.Text);
        //ExportOptions exp = new ExportOptions();
        //exp.ExportFormatType = ExportFormatType.Excel;

        //ExportFormatOptions excelFormatOptions = new ExcelFormatOptions();
          //excelFormatOptions.ExcelUseConstantColumnWidth = True;
         // excelFormatOptions.ExcelUseConstantColumnWidth = False;
       

        //rpt.ExportToHttpResponse(exp, Response, false, "ShiftRotaReport for" + txtFrom.Text + "-" + txtTo.Text);
        MyCon.Close();
    }
    //private ExportFormatOptions GetFormatOptions()
    //{
    //    ExportFormatOptions excelFormatOptions = new ExcelFormatOptions();
    //   excelFormatOptions.ExcelTabHasColumnHeadings = True
    //    excelFormatOptions.ExcelUseConstantColumnWidth = False
    //    return excelFormatOptions;
    //}


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
        }
    }

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString.retrieveConnString(), CommandType.StoredProcedure, "DEFFINITY_GETALLTEAMS",  new SqlParameter("@PORTFOLIOID", int.Parse(ddlPortfolio.SelectedValue))).Tables[0];
        chkTeam.DataSource = dt;
        chkTeam.DataTextField = "TeamName";
        chkTeam.DataValueField = "SDID";
        chkTeam.DataBind();
    }
    private string GetTeamIDs()
    {
        string getteamids = string.Empty;
        int i = chkTeam.Items.Count;
        foreach (ListItem item in chkTeam.Items)
        {
            if ((item.Selected) && (item.Value.Contains("SD-") == false))
            {
                getteamids = getteamids + item.Value.ToString() + ",";               
            }
        }
        getteamids = getteamids + "0";
        return getteamids;
    }

    private string GetSDTeamIDs()
    {
        string getteamids = string.Empty;
        int i = chkTeam.Items.Count;
        foreach (ListItem item in chkTeam.Items)
        {
            if ((item.Selected) && (item.Value.Contains("SD-") == true))
            {
                getteamids = getteamids + item.Value.ToString().Replace("SD-", "") + ",";
            }
        }
        getteamids = getteamids + "0";
        return getteamids;
    }
}
