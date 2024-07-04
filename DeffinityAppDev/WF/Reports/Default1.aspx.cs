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

public partial class Default1 : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            //Get the connection of the database
            SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
            SqlCommand cmd = new SqlCommand("AMPS_Projects", MyCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = MyCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Binds data to Contractor's dropdownlist
            ddlProjects.DataSource = dt;
            ddlProjects.DataTextField = "ProjectTitle";
            ddlProjects.DataValueField = "ProjectReference";
            ddlProjects.DataBind();

            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
        }
        else
        {
            //Bindparameters();
            BindReport();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        BindReport();
        //BindAddReport();

    }


    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "Report1.rpt";
        rpt.Load(Server.MapPath(str));
        //string s = Server.MapPath(str);
        //CrystalReportViewer1.DataBind();

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("AMPS_Report1", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ProjRef", SqlDbType.Int).Value = ddlProjects.SelectedValue.ToString();
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //string str_sub = "Report1_Additional.rpt";
        //rpt.Subreports[0].Load(str_sub);
        myCommand = new SqlCommand("AMPS_Report1_ProjectAdditionalcost", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ProjRef", SqlDbType.Int).Value = ddlProjects.SelectedValue.ToString();
        myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt1);
        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);
        

        myCommand = new SqlCommand("AMPS_Report1_Total", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ProjRef", SqlDbType.Int).Value = ddlProjects.SelectedValue.ToString();
        myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt2);
        rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[1].SetDataSource(dt2);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
        //rpt.Close();
        //rpt.Dispose();
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
            CrystalReportViewer1 = null;
        }

    }

 }
