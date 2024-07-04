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

public partial class Reports_ProjectMeeting : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["customer"] != null)
            {
                BindReportByCustomer();
            }
            else
            { BindReport(); }
        }
    }
    private void BindReportByCustomer()
    {
        rpt = new ReportDocument();

        //Load the selected report file.

        string str = "ProjectMeetingByCustomer.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        #region For Main Report
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        DataTable dt2 = new DataTable();
        SqlDataAdapter adp_sub1 = new SqlDataAdapter("Deffinity_ProjectMeetingSelect", MyCon);
        adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub1.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = QueryStringValues.Meeting;
        adp_sub1.Fill(dt2);
        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt2);
        // Set the Crytal Report Viewer control's source to the report document.

        #endregion
       
        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Updates Report");
        Response.End();
    }
    private void BindReport()
    {
        rpt = new ReportDocument();

        //Load the selected report file.

        string str = "ProjectMeeting.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        #region For Main Report
        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("Deffinity_ProjectMeetingTaskItemsSelect", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = QueryStringValues.Meeting;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        #endregion
        #region For Main Report
        DataTable dt2 = new DataTable();
        SqlDataAdapter adp_sub1 = new SqlDataAdapter("Deffinity_ProjectMeetingSelect", MyCon);
        adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub1.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = QueryStringValues.Meeting;
        adp_sub1.Fill(dt2);
        rpt.Subreports["ProjectMeetingDetails.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectMeetingDetails.rpt"].SetDataSource(dt2);
       // Set the Crytal Report Viewer control's source to the report document.

        #endregion
        Response.Clear();
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Project Updates Report");
        Response.End();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
        }
    }
}
