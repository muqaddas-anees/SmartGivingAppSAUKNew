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

public partial class Reports_Default2 : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false; 
        if (!Page.IsPostBack)
        {
            //WebUserControl1.setSelection = 1;
            CrystalReportViewer1.Visible = false;
            CrystalReportViewer1.Enabled = false;
        }
        else
        {
            //Bindparameters();
            BindReport();
        }
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

        string str = "report2.rpt";
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
        crParameterDiscreteValue.Value = txtFromDate.Text;
        crParameterValues.Add(crParameterDiscreteValue);
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues);

        //Set the second parameter
        crParameterFieldLocation2 = crParameterFieldDefinitions[1];
        crParameterValues2 = crParameterFieldLocation2.CurrentValues;
        crParameterDiscreteValue2 = new CrystalDecisions.Shared.ParameterDiscreteValue();
        crParameterDiscreteValue2.Value = txtToDate.Text;
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
    private void BindReport()
    {
        
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "report2.rpt";
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
        SqlCommand myCommand = new SqlCommand("AMPS_CostVarianceReport", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@date1",SqlDbType.Char,10).Value=txtFromDate.Text.Trim();
        myCommand.Parameters.Add("@date2",SqlDbType.Char,10).Value=txtToDate.Text.Trim();
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.Visible = true;
        //rpt.Dispose();
        //rpt.Close();
        //rpt = null;
        
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

    void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    void CrystalReportSource1_Unload(object sender, EventArgs e)
    {
        throw new Exception("The method or operation is not implemented.");
    }
    protected void WebUserControl1_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        DateTime sDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime eDate = Convert.ToDateTime(txtToDate.Text);
        TimeSpan s1 = sDate - eDate;
        if (s1.Days > 0)
        {

            lblError.Visible = true;
            lblError.Text = "To date can not be greater than From date";
        }
        else
        {
            BindReport();
        }
    }
}
/*
 *      DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand MyCommand = new SqlCommand("AMPS_PendingProjectReport", MyCon);
        MyCommand.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter MyAdapater = new SqlDataAdapter(MyCommand);
        MyAdapater.Fill(dt);
 * 
 * 
 * DataTable dt=new DataTable();
			SqlConnection MyCon=new SqlConnection(ConfigurationSettings.AppSettings["DBstring"]);	
			SqlCommand myCommand=new SqlCommand("AMPS_PendingProjectReport",MyCon);
			myCommand.CommandType=CommandType.StoredProcedure;
//			myCommand.Parameters.Add("@date1",SqlDbType.Char,10).Value=txtDate1.Text.Trim();
//			myCommand.Parameters.Add("@date2",SqlDbType.Char,10).Value=txtDate2.Text.Trim();
			SqlDataAdapter	myAdapter=new SqlDataAdapter(myCommand);
			myAdapter.Fill(dt);
*/