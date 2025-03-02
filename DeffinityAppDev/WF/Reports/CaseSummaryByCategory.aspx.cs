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

public partial class Reports_CaseSummaryByCategory : System.Web.UI.Page
{

    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        rpt = new ReportDocument();
        //crViewer.Enabled = true;
        //crViewer.EnableParameterPrompt = false;
        //crViewer.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "CaseSummaryByCategory.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];


        DataTable dt = new DataTable();

        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("CaseSummaryByCategory", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@SRType", SqlDbType.VarChar).Value = ddlSRType.SelectedValue;
        myCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = txtFrom.Text;
        myCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = txtTo.Text;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat,Response,false, "Service Request");
        //crViewer.ReportSource = rpt;
        //crViewer.Visible = true;
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
        }
        //if (crViewer != null)
        //{
        //    crViewer.Dispose();
        //    crViewer = null;
        //}
    }
}
