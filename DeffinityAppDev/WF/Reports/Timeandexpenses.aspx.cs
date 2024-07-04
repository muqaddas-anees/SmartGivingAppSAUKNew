using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
public partial class Reports_Timeandexpenses : System.Web.UI.Page
{
    ReportDocument rpt;
    int Project;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Project = Convert.ToInt32(Request.QueryString["Project"]);
        
    }
    private void BindReport()
    {
        rpt = new ReportDocument();
        CrystalReportViewer1.Enabled = true;
        CrystalReportViewer1.EnableParameterPrompt = false;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;

        //Load the selected report file.

        string str = "ExpensesReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];

        DataTable dt = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DN_SumoftotalTimeandExpenses", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
        myCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
        myCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
        myCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
          
 


        //myCommand.Parameters.Add("@ContractorID", SqlDbType.Int).Value = ContractorID;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt);
        //rpt.Subreports["Expenses.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["Expenses.rpt"].SetDataSource(dt);
        DataTable dt2 = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter("DN_ExpensesReport", MyCon);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
         if (RadioButtonList1.SelectedItem.Text == "Buying Price")
        {
            adp.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp.Fill(dt2);


            rpt.Subreports["Expenses.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["Expenses.rpt"].ReportDefinition.ReportObjects[5].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["Expenses.rpt"].ReportDefinition.ReportObjects[7].ObjectFormat.EnableSuppress = true;


            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[2].ReportObjects[4].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[2].ReportObjects[6].ObjectFormat.EnableSuppress = true;

            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[3].ReportObjects[2].ObjectFormat.EnableSuppress = true;
                        
            rpt.Subreports["Expenses.rpt"].SetDataSource(dt2);
        }
        else if (RadioButtonList1.SelectedItem.Text == "Selling Price")
        {
            adp.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp.Fill(dt2);

            rpt.Subreports["Expenses.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["Expenses.rpt"].ReportDefinition.ReportObjects[4].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["Expenses.rpt"].ReportDefinition.ReportObjects[6].ObjectFormat.EnableSuppress = true;

            
            

            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[2].ReportObjects[3].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[2].ReportObjects[5].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["Expenses.rpt"].ReportDefinition.Sections[3].ReportObjects[1].ObjectFormat.EnableSuppress = true;
                        
            rpt.Subreports["Expenses.rpt"].SetDataSource(dt2);
        }
        else if (RadioButtonList1.SelectedItem.Text == "All")
        {
            adp.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp.Fill(dt2);

            rpt.Subreports["Expenses.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
            rpt.Subreports["Expenses.rpt"].SetDataSource(dt2);
        }

        



        DataTable dt9= new DataTable();
        SqlDataAdapter adp9 = new SqlDataAdapter("DN_ExternalExpensesReport", MyCon);
        adp9.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp9.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
        adp9.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
        adp9.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
        adp9.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
        

        adp9.Fill(dt9);
        rpt.Subreports["ExternalExpensesReport.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ExternalExpensesReport.rpt"].SetDataSource(dt9);




        DataTable dt6 = new DataTable();
        SqlDataAdapter adp_sub6 = new SqlDataAdapter("DN_TimeSheetReportBasedonpref", MyCon);
        adp_sub6.SelectCommand.CommandType = CommandType.StoredProcedure;



        if (RadioButtonList1.SelectedItem.Text =="Buying Price")
        {
            
            adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp_sub6.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp_sub6.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@SellingPrice", SqlDbType.Int).Value = 0;
            adp_sub6.Fill(dt6);
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);

           
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.ReportObjects[6].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.ReportObjects[8].ObjectFormat.EnableSuppress = true;
            
           
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[2].ReportObjects[5].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[2].ReportObjects[6].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[3].ReportObjects[1].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDataSource(dt6);
           


        }
        else if (RadioButtonList1.SelectedItem.Text =="Selling Price")
        {
            adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp_sub6.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp_sub6.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@SellingPrice", SqlDbType.Int).Value = 1;
            adp_sub6.Fill(dt6);
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
             rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.ReportObjects[5].ObjectFormat.EnableSuppress = true;
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.ReportObjects[7].ObjectFormat.EnableSuppress = true;

           
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[2].ReportObjects[3].ObjectFormat.EnableSuppress = true;
          
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[2].ReportObjects[4].ObjectFormat.EnableSuppress = true;
          
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].ReportDefinition.Sections[3].ReportObjects[2].ObjectFormat.EnableSuppress = true;

            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDataSource(dt6);
          



        }
        else if (RadioButtonList1.SelectedItem.Text=="All")
        {
            adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = Project;
            adp_sub6.SelectCommand.Parameters.Add("@ResourceID", SqlDbType.Int).Value = ddlcustomer_Timesheet.SelectedValue;
            adp_sub6.SelectCommand.Parameters.Add("@StartDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_StartDate.Text) ? "01/01/1900" : txt_StartDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@EndDateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(string.IsNullOrEmpty(txt_EndDate.Text) ? "01/01/1900" : txt_EndDate.Text);
            adp_sub6.SelectCommand.Parameters.Add("@SellingPrice", SqlDbType.Int).Value = 0;
            adp_sub6.Fill(dt6);
            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);

            rpt.Subreports["TimeSheet_BuyingPrice.rpt"].SetDataSource(dt6);
        }

       

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt);
        if (RadioButtonList1.SelectedItem.Text == "Selling Price")
        {

            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[2].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[7].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[13].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[14].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[4].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[5].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[16].ObjectFormat.EnableSuppress = true;
           rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[17].ObjectFormat.EnableSuppress = true;
            
        }
        else if (RadioButtonList1.SelectedItem.Text == "Buying Price")
        {

            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[8].ObjectFormat.EnableSuppress = true;
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[9].ObjectFormat.EnableSuppress = true;
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[6].ObjectFormat.EnableSuppress = true;
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[3].ObjectFormat.EnableSuppress = true;

            //// these are selling price values---start
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[10].ObjectFormat.EnableSuppress = true;
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[11].ObjectFormat.EnableSuppress = true;
            rpt.ReportDefinition.Sections["DetailSection1"].ReportObjects[12].ObjectFormat.EnableSuppress = true;

            //------------------end

         

        }
        
        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false,"Service Request Summary");

        //Response.Write(rpt.DataSourceConnections.Count.ToString());
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.Visible = true;
        //CrystalReportSource1.DataBind();
        //CrystalReportViewer1.DataBind();
    }
    protected void btn_Submitt_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        //if (rpt != null)
        //{
        //    rpt.Close();
        //    rpt.Dispose();
        //    //rpt.Close();
        //    //   rpt.Dispose();
        //    rpt = null;
        //    CrystalReportViewer1.Dispose();
        //    CrystalReportSource1.Dispose();
        //    CrystalReportViewer1 = null;
      
        //}
    }
}
