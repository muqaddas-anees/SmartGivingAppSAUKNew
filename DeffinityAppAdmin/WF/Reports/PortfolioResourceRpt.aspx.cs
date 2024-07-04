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

public partial class Reports_ResourceReport : System.Web.UI.Page
{
    ReportDocument rpt;
    int PortfolioID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlportfolio.DataBind();
        }
        CrystalReportViewer1.Visible = false;
        CrystalReportViewer1.Enabled = false;
        BindReport();
    }
    
    protected void btnViewrpt_Click(object sender, ImageClickEventArgs e)
    {
        Context.RewritePath(Request.FilePath + "?");  
        BindReport();
    }
    void BindReport()
    {
        CrystalReportViewer1.Visible = true;
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ResourceReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();    
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DEFFINITY_GETCollingOFFDDetails", MyCon);

        PortfolioID =Convert.ToInt32(ddlportfolio.SelectedValue);
        //if (Request.QueryString["Id"] != null)
        //    projectPlanID = Convert.ToInt32(Request.QueryString["Id"]);
        //else
        //    projectPlanID = 0;

        myCommand.Parameters.AddWithValue("@PortfolioID", PortfolioID);
        myCommand.CommandType = CommandType.StoredProcedure;
        

        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);       
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        if (rpt.Rows.Count == 0)
        {
            pnlrpt.Visible = false;
        }
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
        
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        
            //CrystalReportSource1.Unload += new EventHandler(CrystalReportSource1_Unload);
            //CrystalReportViewer1.Unload += new EventHandler(CrystalReportViewer1_Unload);
            if (rpt != null)
            {
                rpt.Close();
                rpt.Dispose();
                rpt = null;
            }
            CrystalReportViewer1.Dispose();
            CrystalReportSource1.Dispose();

    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {

    }
}
