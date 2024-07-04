using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
public partial class Reports_ProjectBOMSupplierInv : System.Web.UI.Page
{
    ReportDocument rpt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            string project = "0";
            string wrshtid = "0";
            //check the query sting values

           
            if (Request.QueryString["project"] != null)
                project = Request.QueryString["project"].ToString();
            if (Request.QueryString["wrkshtId"] != null)
                wrshtid = Request.QueryString["wrkshtId"].ToString();
            BindReport(int.Parse(project),int.Parse(wrshtid));
        }
    }
    private void BindReport(int ProjectRef,int wrkshtID)
    {
        //linkRpt.NavigateUrl = "~/Reports/ProjectBOMReport.aspx?id=" + HD_Type.Value + "&project=" + QueryStringValues.Project;

        rpt = new ReportDocument();
        string str = "ProjectBOMSRInv.rpt";
        rpt.Load(Server.MapPath(str));

        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand MyCommand = new SqlCommand("ProjectBOMReport_Mainrpt", MyCon);
        MyCommand.CommandType = CommandType.StoredProcedure;

        MyCommand.Parameters.AddWithValue("@ProjectRef", ProjectRef);
        MyCommand.Parameters.AddWithValue("@WorkSheetId", wrkshtID);
        SqlDataAdapter myAdapter = new SqlDataAdapter(MyCommand);
        myAdapter.Fill(dt);

        DataTable dt1 = new DataTable();
        SqlCommand MyCommand1 = new SqlCommand("ProjectBOMReport_Subrpt", MyCon);
        MyCommand1.CommandType = CommandType.StoredProcedure;

        MyCommand1.Parameters.AddWithValue("@ProjectRef", ProjectRef);
        MyCommand1.Parameters.AddWithValue("@WorkSheetId", wrkshtID);
        SqlDataAdapter myAdapter1 = new SqlDataAdapter(MyCommand1);
        myAdapter1.Fill(dt1);



        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);

        rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports[0].SetDataSource(dt1);
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Supplier Invoice Report");

    }
}
