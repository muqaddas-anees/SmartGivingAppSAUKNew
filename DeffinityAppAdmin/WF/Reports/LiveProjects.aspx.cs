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

public partial class Reports_Default5 : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
            BindCountries();
        }
        else
        {
            BindReport();
        }

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //CrystalReportViewer1.Visible = false;
            //CrystalReportViewer1.Enabled = false;
            //BindCountries();
        }
        else
        {
            //BindReport();

        }

    }
    private void BindCountries()
    {

        SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBString"]);
        MyCon.Open();
        SqlCommand myCommand = new SqlCommand("AMPS_Countries", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Connection = MyCon;
        SqlDataAdapter da = new SqlDataAdapter(myCommand);
        DataTable dt = new DataTable();
        da.Fill(dt);
        
        //dr = myCommand.ExecuteReader();
        ddlCountries.DataSource = dt;
        ddlCountries.DataTextField = "Country";
        ddlCountries.DataValueField = "ID";
        ddlCountries.DataBind();
        MyCon.Close();
        MyCon.Dispose();

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //Session["CountryID"] = Convert.ToInt32(ddlCountries.SelectedValue);
        BindReport();
        
    }
    private void BindReport()
    {
         rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "Report5.rpt";
        rpt.Load(Server.MapPath(str));
        //string s = Server.MapPath(str);
        //CrystalReportViewer1.DataBind();

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBString"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("AMPS_ReportByCountry", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        //if (ddlCountries.SelectedValue == null)
        //{
            myCommand.Parameters.Add("@CountryID", SqlDbType.Int).Value = Convert.ToInt32(ddlCountries.SelectedValue);
        //}
        //else
        //{
        //    myCommand.Parameters.Add("@CountryID", SqlDbType.Int).Value = Convert.ToInt32(Session["CountryID"]);
        //}
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
    }
    private void Bindparameters()
    {


        rpt = new ReportDocument();

        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;

        //Declare the parameter related objects.

        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;
        ParameterFieldDefinition crParameterFieldLocation;
        ParameterValues crParameterValues = new ParameterValues();

        //Load the selected report file.

        string str = "Report5.rpt";
        rpt.Load(Server.MapPath(str));

        // Get the report's parameters collection.

        crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields;

        //Set the first parameter
        //- Get the parameter, tell it to use the current values vs default value.
        //- Tell it the parameter contains 1 discrete value vs multiple values.
        //- Set the parameter's value.
        //- Add it and apply it.
        //- Repeat these statements for each parameter.       


        crParameterFieldLocation = crParameterFieldDefinitions[0];
        crParameterValues = crParameterFieldLocation.CurrentValues;
        crParameterDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
        crParameterDiscreteValue.Value = ddlCountries.SelectedValue.ToString();
        crParameterValues.Add(crParameterDiscreteValue);
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues);

       
        string s = Server.MapPath(str);
        CrystalReportViewer1.DataBind();


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


    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

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
