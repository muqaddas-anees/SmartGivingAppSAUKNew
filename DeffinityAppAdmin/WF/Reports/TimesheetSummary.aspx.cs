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
public partial class Reports_TimeSheetSelectResource : System.Web.UI.Page
{
    ReportDocument rpt;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ////WebUserControl1.setSelection = 10;
            //string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            //SqlConnection con = new SqlConnection(strConn);
            //string selectsql = "SELECT    distinct TimesheetEntry.ContractorID, Contractors.ContractorName FROM  TimesheetEntry INNER JOIN Contractors ON TimesheetEntry.ContractorID = Contractors.ID";
            //SqlCommand cmd = new SqlCommand(selectsql, con);
            //con.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ddslectresource.DataSource = dt;
            //ddslectresource.DataTextField = "ContractorName";
            //ddslectresource.DataValueField = "ContractorID";
            //ddslectresource.DataBind();
            //ddslectresource.Items.Insert(0, "Please select");
            //dt.Clear();
            //con.Close();
            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
            fillResourcetime();
        }
        else
        {
            BindReport();
        }

        //fillResourcetime();
      
    }
    private void fillResourcetime()
    {
        try
        {
            //SqlConnection con1 = new SqlConnection(con);
            string selectsql = "SELECT    distinct TimesheetEntry.ContractorID, Contractors.ContractorName FROM  TimesheetEntry INNER JOIN Contractors ON TimesheetEntry.ContractorID = Contractors.ID";
            SqlCommand cmd = new SqlCommand(selectsql, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddslectresource.DataSource = dt;
            ddslectresource.DataTextField = "ContractorName";
            ddslectresource.DataValueField = "ContractorID";
            ddslectresource.DataBind();
            ddslectresource.Items.Insert(0, "Please select");
            dt.Clear();
            con.Close();
        }
        catch (Exception ex)
        {
            //Stemp = ex.ToString();
        }


    }


    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "TimeSheetSelectResource.rpt";
        rpt.Load(Server.MapPath(str));
        //string s = Server.MapPath(str);
        //CrystalReportViewer1.DataBind();

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_SelectResourceTimeReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ddslectresource.SelectedValue;
       // myCommand.Parameters.Add("@date1", SqlDbType.Char, 10).Value = txtFromDate.Text.Trim();
        //myCommand.Parameters.Add("@date2", SqlDbType.Char, 10).Value = txtToDate.Text.Trim();
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
    }
    protected void btn_view_Click(object sender, ImageClickEventArgs e)
    {
        BindReport();
    }
}
