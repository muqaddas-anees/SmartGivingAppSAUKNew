using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using GoodsReceiptClass;
using Microsoft.ApplicationBlocks.Data;
public partial class Reports_GoodsReceiptReport : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindProjects();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    private void BindReport()
    {
        try
        {
            rpt = new ReportDocument();
            CrystalReportViewer1.Enabled = true;
            CrystalReportViewer1.EnableParameterPrompt = false;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            string str = "GoodsReceiptReport.rpt";
            rpt.Load(Server.MapPath(str));

            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];




            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
            SqlConnection myConn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("GoodsReceipt_ReportSummary", myConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ddlProjects.SelectedValue;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            DataTable dt2 = new DataTable();
            SqlDataAdapter subAdapter = new SqlDataAdapter("GoodsReceipt_Report1", myConn);
            subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            subAdapter.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ddlProjects.SelectedValue;
            subAdapter.Fill(dt2);

            rpt.Subreports["SubReportItems"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["SubReportItems"].SetDataSource(dt2);


            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Goods Receipt Report");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProjects()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "GoodsReceipt_ProjectList").Tables[0];
            ddlProjects.Items.Clear();
            ddlProjects.DataSource = dt;
            ddlProjects.DataTextField = "ProjectReferenceWithPrefix";
            ddlProjects.DataValueField = "ProjectReference";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
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
}
