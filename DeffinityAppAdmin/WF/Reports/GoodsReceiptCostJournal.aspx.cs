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
using Microsoft.ApplicationBlocks.Data;

public partial class Reports_GoodsReceiptCostJournal : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindCustomer();
                BindProjects();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    #region Bind Data
    private void BindCustomer()
    {
        try
        {
            ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            ddlCustomer.DataTextField = "PortFolio";
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    // ddlCustomer.Items.Insert(0, new ListItem("All", "0"));
    }
    private void BindProjects()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "GoodsReceipt_ProjectList").Tables[0];
            ddlProject.Items.Clear();
            ddlProject.DataSource = dt;
            ddlProject.DataTextField = "ProjectReferenceWithPrefix";
            ddlProject.DataValueField = "ProjectReference";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    

    #endregion
    protected void imgView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            rpt = new ReportDocument();
            CrystalReportViewer1.Enabled = true;
            CrystalReportViewer1.EnableParameterPrompt = false;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

            string str = "GoodsReceiptCostJournal.rpt";
            rpt.Load(Server.MapPath(str));

            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
            string liveOrCompleted = "";
            if (chkLive.Checked == true)
            {
                liveOrCompleted = "2,";
            }
            else if (chkCompleted.Checked == true)
            {
                liveOrCompleted = "3,6,";
            }
            else
            {
                liveOrCompleted = "2,3,6,";
            }


            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
            SqlConnection myConn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("GoodsReceipt_CostJournal", myConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@customer", SqlDbType.Int).Value = ddlCustomer.SelectedValue;
            cmd.Parameters.Add("@project", SqlDbType.Int).Value = ddlProject.SelectedValue;
            cmd.Parameters.Add("@liveOrCompleted", SqlDbType.VarChar).Value = liveOrCompleted;
            cmd.Parameters.Add("@orderBy", SqlDbType.VarChar).Value = ddlOrderBy.SelectedValue;

            cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text);
            cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            DataTable dt2 = new DataTable();
            SqlDataAdapter subAdapter = new SqlDataAdapter("GoodsReceipt_CostJournalSummary", myConn);
            subAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //subAdapter.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ddlProjects.SelectedValue;

            subAdapter.SelectCommand.Parameters.Add("@customer", SqlDbType.Int).Value = ddlCustomer.SelectedValue;
            subAdapter.SelectCommand.Parameters.Add("@project", SqlDbType.Int).Value = ddlProject.SelectedValue;
            subAdapter.SelectCommand.Parameters.Add("@liveOrCompleted", SqlDbType.VarChar).Value = liveOrCompleted;
            subAdapter.SelectCommand.Parameters.Add("@orderBy", SqlDbType.VarChar).Value = ddlOrderBy.SelectedValue;

            subAdapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text);
            subAdapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text);

            subAdapter.Fill(dt2);

            rpt.Subreports["CostJournalSummary"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["CostJournalSummary"].SetDataSource(dt2);


            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Cost Journal By Period");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
