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

public partial class Reports_ProjectVariancePage : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //WebUserControl1.setSelection = 1;
            //CrystalReportViewer1.Visible = false;
            //CrystalReportViewer1.Enabled = false;
           
        }
        else
        {
            BindReport();
        }
    }
      
    private void BindReport()
    {
        
        rpt = new ReportDocument();
        //CrystalReportViewer1.Enabled = true;
        //CrystalReportViewer1.EnableParameterPrompt = false;
        //CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

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
        myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = sessionKeys.UID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "CostVariance");

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
            rpt.Close();
            rpt.Dispose();
            rpt = null;
            //CrystalReportViewer1.Dispose();
            //CrystalReportSource1.Dispose();
        }
    }

    //void CrystalReportViewer1_Unload(object sender, EventArgs e)
    //{
    //    throw new Exception("The method or operation is not implemented.");
    //}

    //void CrystalReportSource1_Unload(object sender, EventArgs e)
    //{
    //    throw new Exception("The method or operation is not implemented.");
    //}
    //protected void WebUserControl1_Load(object sender, EventArgs e)
    //{

    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
        
    //}

    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        //DateTime sDate = Convert.ToDateTime(txtFromDate.Text);
        //DateTime eDate = Convert.ToDateTime(txtToDate.Text);
        //TimeSpan s1 = sDate - eDate;
        //if (s1.Days > 0)
        //{

        //    lblError.Visible = true;
        //    lblError.Text = "'From date' cannot be greater than 'To date'";
        //}
        //else
        //{
        //    BindReport();
        //}
        BindReport();
    }
}
