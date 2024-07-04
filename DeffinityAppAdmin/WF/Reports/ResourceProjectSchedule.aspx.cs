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

public partial class Reports_ResourceProjectSchedule : System.Web.UI.Page
{
    ReportDocument rpt;
    int cid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //cid = 91; //Convert.ToInt32(Request.QueryString["CID"]);
        if (Request.QueryString["CID"] != null)
            cid = Convert.ToInt32(Request.QueryString["CID"]);
        else
            cid = 0;
        if (!Page.IsPostBack) 
        {
            //Get the connection of the database
            SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBString"]);
            SqlCommand cmd = new SqlCommand("AMPS_Contractors", MyCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Binds data to Contractor's dropdownlist
            ddlContractor.DataSource = dt;
            ddlContractor.DataTextField = "ContractorName";
            ddlContractor.DataValueField = "ID";
            ddlContractor.DataBind();

            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
        }
        if (cid != 0)
        {
            ddlContractor.SelectedIndex = ddlContractor.Items.IndexOf(ddlContractor.Items.FindByValue(cid.ToString()));
            BindReport();
        }
        else
        {
            BindReport();
        }
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        cid = 0;
        Context.RewritePath(Request.FilePath + "?");
        BindReport();
    }

    private void BindReport()
    {
        
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "GanttReport2.rpt";
        rpt.Load(Server.MapPath(str));
        
        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("AMPS_ProjectforecastContractor", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        //if (cid == 0)
        if((txtFromDate.Text.Trim()!="") && (txtToDate.Text.Trim()!=""))
        {
            myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ddlContractor.SelectedValue;
            myCommand.Parameters.Add("@date1", SqlDbType.Char, 10).Value = txtFromDate.Text.Trim();
            myCommand.Parameters.Add("@date2", SqlDbType.Char, 10).Value = txtToDate.Text.Trim();
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(dt);
        }
        else
        {
            myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = cid;
            myCommand.Parameters.Add("@date1", SqlDbType.Char, 10).Value = "*";
            myCommand.Parameters.Add("@date2", SqlDbType.Char, 10).Value = "*";
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(dt);
        }
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Resources");
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;
        
    }
    
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        else
        {
            //CrystalReportSource1.Unload += new EventHandler(CrystalReportSource1_Unload);
            //CrystalReportViewer1.Unload += new EventHandler(CrystalReportViewer1_Unload);           
            rpt.Close();
            rpt.Dispose();
            rpt = null;
            CrystalReportViewer1.Dispose();
            CrystalReportSource1.Dispose();
        }

    }
  
}
