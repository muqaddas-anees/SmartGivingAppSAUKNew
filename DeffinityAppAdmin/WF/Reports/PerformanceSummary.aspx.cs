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

public partial class Reports_perform_summ_page : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
            BindCountries();
            CrystalReportViewer1.Enabled = true;
            //CrystalReportSource1.Report.Parameters[0].DefaultValue = ddlContractors.SelectedValue.ToString();
            //CrystalReportSource1.Report.Parameters[1].DefaultValue = ddlDays.SelectedValue.ToString();
            //CrystalReportViewer1.Visible = true;
        }
        else
        {
            //Bindparameters();
            BindReport();
        }
    }
    private void BindCountries()
    {
        //SqlConnection MyCon = new SqlConnection(ConfigurationSettings.AppSettings["DBstring"]);
        SqlConnection MyCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        MyCon.Open();
        SqlCommand myCommand = new SqlCommand("AMPS_Contractors", MyCon);
        //SqlDataReader dr;
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Connection = MyCon;
        SqlDataAdapter da = new SqlDataAdapter(myCommand);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //dr = myCommand.ExecuteReader();
        ddlContractors.DataSource = dt;
        ddlContractors.DataTextField = "ContractorName";
        ddlContractors.DataValueField = "ID";
        ddlContractors.DataBind();
        MyCon.Close();
        MyCon.Dispose();
    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        /*
         * Calling Function to Bind Parameters to stored Procedure 
         * and binding the data to the crystal reportviewer
         */
        //Bindparameters();
        BindReport();
    }


    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "Report9.rpt";
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
        SqlCommand myCommand = new SqlCommand("AMPS_RAGPerfmReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ddlContractors.SelectedValue.ToString();
        myCommand.Parameters.Add("@days", SqlDbType.Int).Value = ddlDays.SelectedValue.ToString();
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

        ParameterDiscreteValue crParameterDiscreteValue2;
        ParameterFieldDefinition crParameterFieldLocation2;
        ParameterValues crParameterValues2 = new ParameterValues();


        //Load the selected report file.

        string str = "Report9.rpt";
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
        crParameterDiscreteValue.Value = ddlContractors.SelectedValue.ToString();
        crParameterValues.Add(crParameterDiscreteValue);
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues);

        //Set the second parameter
        crParameterFieldLocation2 = crParameterFieldDefinitions[1];
        crParameterValues2 = crParameterFieldLocation2.CurrentValues;
        crParameterDiscreteValue2 = new CrystalDecisions.Shared.ParameterDiscreteValue();
        crParameterDiscreteValue2.Value = ddlDays.SelectedValue.ToString();
        crParameterValues2.Add(crParameterDiscreteValue2);
        crParameterFieldLocation2.ApplyCurrentValues(crParameterValues2);



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
