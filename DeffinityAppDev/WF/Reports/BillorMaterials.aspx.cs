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
public partial class Reports_BillorMaterialsPage : System.Web.UI.Page
{
    ReportDocument rpt;
    int ProjectReference;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        BindReport();
    }
    private void BindReport()
    {
        //try
        //{
            ProjectReference = QueryStringValues.Project;
            rpt = new ReportDocument();

            string str = "~/Reports/ProjectBillofmaterialsRpt.rpt";
            rpt.Load(Server.MapPath(str));

            //Set the Database Login Information
            string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
            string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
            string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

            DataTable dt = new DataTable();
            string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
            SqlConnection MyCon = new SqlConnection(strConn);
            //SqlDataAdapter adp = new SqlDataAdapter("DN_ServiceCatalog_GetLabourDetails", MyCon);
            //DN_ServiceCatalog_GetServiceDetails
            SqlDataAdapter adp = new SqlDataAdapter("DN_GetBOMType", MyCon);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp.Fill(dt);

            DataTable dt1 = new DataTable();
            SqlDataAdapter adp_sub = new SqlDataAdapter("DN_ServiceCatalog_GetMaterialDetails", MyCon);
            adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp_sub.Fill(dt1);
            // rpt.Subreports[1].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            //rpt.Subreports[1].SetDataSource(dt1);
            rpt.Subreports["BOM_GetMaterialDetailsRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["BOM_GetMaterialDetailsRpt"].SetDataSource(dt1);

            //if (dt1.Rows[0]["ProjectTitle"] != null)
            //    lblReportTitle.Text = "Bill of Materials for " + ProjectReference.ToString() + " " + dt1.Rows[0]["ProjectTitle"].ToString();

            DataTable dt2 = new DataTable();
            SqlDataAdapter adp_sub1 = new SqlDataAdapter("DN_ServiceCatalog_GetLabourDetails", MyCon);
            adp_sub1.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_sub1.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp_sub1.Fill(dt2);
            //rpt.Subreports[0].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            //rpt.Subreports[0].SetDataSource(dt2);
            //rpt.Subreports["BOMTest"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            //rpt.Subreports["BOMTest"].SetDataSource(dt2);
            rpt.Subreports["BOM_GetLabourDetailsRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["BOM_GetLabourDetailsRpt"].SetDataSource(dt2);

            DataTable dt3 = new DataTable();
            SqlDataAdapter adp_sub2 = new SqlDataAdapter("DN_ServiceCatalog_GetServiceDetails", MyCon);
            adp_sub2.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_sub2.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp_sub2.Fill(dt3);
            rpt.Subreports["BOM_GetServiceDetailsRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["BOM_GetServiceDetailsRpt"].SetDataSource(dt3);

            DataTable dt4 = new DataTable();
            SqlDataAdapter adp_sub3 = new SqlDataAdapter("DN_ServiceCatalog_GetSubsidiaries", MyCon);
            adp_sub3.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_sub3.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp_sub3.Fill(dt4);
            rpt.Subreports["BOM_GetSubsidiariesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["BOM_GetSubsidiariesRpt"].SetDataSource(dt4);

            DataTable dt5 = new DataTable();
            SqlDataAdapter adp_sub4 = new SqlDataAdapter("DN_GetSummaryOfBOM", MyCon);
            adp_sub4.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_sub4.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
            adp_sub4.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
            adp_sub4.Fill(dt5);
            rpt.Subreports["BOM_SummaryRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["BOM_SummaryRpt"].SetDataSource(dt5);

            //Set the Crytal Report Viewer control's source to the report document.
            rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.SetDataSource(dt);
            //Response.Write(rpt.DataSourceConnections.Count.ToString());
            //CrystalReportViewer1.ReportSource = rpt;
            //CrystalReportViewer1.Visible = true;
            Response.Clear();
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ProjectBOM Report");
            Response.Flush();
            Response.End();
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
        //finally 
        //{
           
        //    if (rpt != null)
        //    {
        //        rpt.Close();
        //        rpt.Dispose();
        //    }
        //}
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
