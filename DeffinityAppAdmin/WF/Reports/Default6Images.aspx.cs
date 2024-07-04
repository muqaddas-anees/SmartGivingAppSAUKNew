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

public partial class Default6Images : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Get the connection of the database
            SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connectionString"]);
            SqlCommand cmd = new SqlCommand("AMPS_Sites", MyCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Binds data to Owner's dropdownlist
            ddlSite.DataSource = dt;
            ddlSite.DataTextField = "Site";
            ddlSite.DataValueField = "ID";
            ddlSite.DataBind();

            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
        }
        else
        {
            //Bindparameters();
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            rpt = new ReportDocument();
            CrystalReportViewer1.Enabled = true;

            //Load the selected report file.
            string str = "Report6Images.rpt";
            rpt.Load(Server.MapPath(str));
            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
            SqlConnection MyCon = new SqlConnection(strConn);
            SqlCommand MyCommand = new SqlCommand("AMPS_RAGStatusImages", MyCon);
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.Parameters.Add("@siteID", SqlDbType.Int).Value = ddlSite.SelectedValue.ToString();

            SqlDataAdapter MyAdapater = new SqlDataAdapter(MyCommand);
            MyAdapater.Fill(dt);
            //Set the Database Login Information
            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            //Set the Crytal Report Viewer control's source to the report document.
            rpt.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.Visible = true;
            //rpt = null;
            //Session["index"] = 6;
        }
        catch(Exception _ex)
        {
            Response.Write(_ex.Message.ToString());
        }
        finally
        {
            
        }
    }
    private void Bindparameters()
    {
        

        CrystalReportViewer1.Enabled = true;

        //Load the selected report file.
        string str = "Report6.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.SetDatabaseLogon("sa", "ems", "pc18", "AMPS");

        //Set the Crytal Report Viewer control's source to the report document.
        CrystalReportViewer1.ReportSource = rpt;
        
        CrystalReportViewer1.Visible = true;
        
        
    }
    protected void WebUserControl1_Load(object sender, EventArgs e)
    {
        

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        else
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
            CrystalReportViewer1.Dispose();
            CrystalReportSource1.Dispose();
        }
    }

    void CrystalReportSource1_Unload(object sender, EventArgs e)
    {
        throw new Exception("The method or operation is not implemented.");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //BindReport();
    }
}
