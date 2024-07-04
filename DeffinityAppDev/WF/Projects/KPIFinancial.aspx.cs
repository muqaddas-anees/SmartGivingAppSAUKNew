using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;

public partial class KPIFinancial : System.Web.UI.Page
{
    projectTaskDataContext projectDB = new projectTaskDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Master.PageHead = "KPI";
                BindCustomers();
                BindProgramme();
                BuildTable();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }
    }
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomer.DataSource = portfolio;
            ddlCustomer.DataTextField = "PortFolio";
            ddlCustomer.DataValueField = "ID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProgramme()
    {
        try
        {
            ProgrammeDataContext ProgrammeContext = new ProgrammeDataContext();
            var Owner = from c in ProgrammeContext.OperationsOwners
                        where c.Level == 1
                        orderby c.OperationsOwners
                        select c;
            ddlProgramme.DataSource = Owner;
            ddlProgramme.DataTextField = "OperationsOwners";
            ddlProgramme.DataValueField = "ID";
            ddlProgramme.DataBind();
            ddlProgramme.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BuildTable()
    {
       
        var LabelsList = (from r in projectDB.KPI_LablesNames
                          join c in projectDB.KPI_Categories on r.KPICategoryID equals c.ID
                          where r.PageType==1
                          select new {
                          LabelID=r.LabelID,
                          LabelsName=r.LabelsName,
                          categoty=c.KpiCatogeryName,
                          ID=r.ID
                          }).ToList();

        string text = string.Empty;
        text += "<table  width='100%' class='table table-small-font table-bordered table-striped dataTable'  id='mainTable'  border='0' cellpadding='0' cellspacing='1' ><tr class='tab_header'><th  style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</th><td>Category</td><td>Target(" + int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text).ToString() + ")</td><td style='align:left'>Current Performance</td>";
        text += "<td style='align:left'>Variance</td><td></td><td style='align:left'>Performance(" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 1).ToString() + ")</td><th  style='align:left'>Performance (" + (int.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text) - 2).ToString() + ")</th>";
        text += "</tr>";
        //style='color:black;font-size:10px'
        if (LabelsList != null)
        {
            foreach (var  lblID in LabelsList)
            {
               //text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
                if (lblID.LabelID == 1)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(1).ToString("N2") + "</td><td align='right'>" + getCompletesProjects(1).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_Variance(1).ToString("N2") + "</td><td >" + VarianceRAG(getCompletesProjects(1), NoOfProjects_Target(1)) + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(1, 1).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(2, 1).ToString("N2") + "</td><tr>";
                    //text += "<tr><td>" + lblID.LabelsName + "</td><td>&nbsp;</td><td>" + getCompletesProjects().ToString("N2") + "</td>";//<td>" + getCompletesProjects_Variance().ToString("N2") + "</td><td>" + getCompletesProjects_PrivousYearPer(1) + "</td><td>" + getCompletesProjects_PrivousYearPer(2) + "</td><tr>";
                }
                if (lblID.LabelID == 2)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(2).ToString("N2") + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(0, 2).ToString("N2") + "</td><td align='right'>" + NoOfProjects_Variance(2).ToString("N2") + "</td><td >" + VarianceRAG(NoOfProjects_PrivousYearPer(0, 2), NoOfProjects_Target(2)) + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(1, 2).ToString("N2") + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(2, 2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 3)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + CompletedThisMonth_Target(0, 3).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Performance(0, 0, 3).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Variance(0, 3).ToString("N2") + "</td><td >" + VarianceRAG(CompletedThisMonth_Performance(0, 0, 3), NoOfProjects_Target(3)) + "</td><td align='right'>" + CompletedThisMonth_Performance(1, 0, 3).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Performance(2, 0, 3).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 4)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + CompletedThisMonth_Target(1, 4).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Performance(0, 1, 4).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Variance(1, 4).ToString("N2") + "</td><td >" + VarianceRAG(CompletedThisMonth_Performance(0, 1, 4), NoOfProjects_Target(4)) + "</td><td align='right'>" + CompletedThisMonth_Performance(1, 1, 4).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Performance(2, 1, 4).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 5)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(5).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_Perform(0).ToString("N2") + "</td><td align='right'>" + variance_RevCast(NoOfProjects_Target(5), ExpectedRevenue_Perf()).ToString("N2") + "</td><td >" + VarianceRAG(ExpectedRevenue_Perform(0), NoOfProjects_Target(5)) + "</td><td align='right'>" + ExpectedRevenue_Perform(1).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_Perform(2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 6)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(6).ToString("N2") + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(0, 6).ToString("N2") + "</td><td align='right'>" + NoOfProjects_Variance(6).ToString("N2") + "</td><td >" + VarianceRAG(NoOfProjects_PrivousYearPer(0, 6), NoOfProjects_Target(6)) + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(1, 6).ToString("N2") + "</td><td align='right'>" + NoOfProjects_PrivousYearPer(2, 6).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 7)
                {
                    text += "<tr ><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(7).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(0,7).ToString("N2") + "</td><td align='right'>" + InvoicedToDate(NoOfProjects_Target(7)).ToString("N2") + "</td><td>" + VarianceRAG(InvoicedToDate_PrivousYearPer(0,7), NoOfProjects_Target(7)) + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(1,7).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(2,7).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 8)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(8).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(0, 8).ToString("N2") + "</td><td align='right'>" + Variance(FinanceActuals_val(0, 8), NoOfProjects_Target(8)) + "</td><td >" + VarianceRAG(FinanceActuals_val(0, 8), NoOfProjects_Target(8)) + "</td><td align='right'>" + FinanceActuals_val(1, 8).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(2, 8).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 9)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(9).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(0, 9).ToString("N2") + "</td><td align='right'>" + Variance(FinanceActuals_val(0, 9), NoOfProjects_Target(9)) + "</td><td >" + VarianceRAG(FinanceActuals_val(0, 9), NoOfProjects_Target(9)) + "</td><td align='right'>" + FinanceActuals_val(1, 9).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(2, 9).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 10)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(10).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(0, 10).ToString("N2") + "</td><td align='right'>" + Variance(FinanceActuals_val(0, 10), NoOfProjects_Target(10)) + "</td><td >" + VarianceRAG(FinanceActuals_val(0, 10), NoOfProjects_Target(10)) + "</td><td align='right'>" + FinanceActuals_val(1, 10).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(2, 10).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 11)
                {
                   // text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(11).ToString("N2") + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 12)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(12).ToString("N2") + "</td><td align='right'> " + CompletedThisMonth_Performance(0, 0, 12).ToString("N2") + "</td><td align='right'>" + Variance(CompletedThisMonth_Performance(0, 0, 12), NoOfProjects_Target(12)) + "</td><td align='right'>" + VarianceRAG(CompletedThisMonth_Performance(0, 0, 12), NoOfProjects_Target(12)) + "</td><td align='right'>" + CompletedThisMonth_Performance(1, 0, 12).ToString("N2") + "</td><td align='right'>" + CompletedThisMonth_Performance(2, 0, 12).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 13)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(13).ToString("N2") + "</td><td align='right'>" + getCompletesProjects(13).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_Variance(13).ToString("N2") + "</td><td>" + VarianceRAG(getCompletesProjects(13), NoOfProjects_Target(13)) + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(1, 13).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(2, 13).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 14)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(14).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(0, 14).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_Variance(0, 14).ToString("N2") + "</td><td >" + VarianceRAG(ExpectedRevenue_By_Status(0, 14), NoOfProjects_Target(14)) + "</td><td align='right'>" + ExpectedRevenue_By_Status(1, 14).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(2, 14).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 15)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(15).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 15, 1).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 15, 1), NoOfProjects_Target(15)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 15, 1), NoOfProjects_Target(15)) + "</td><td align='right'>" + Performance_BOMs(1, 15, 1).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 15, 1).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 16)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(16).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 16, 1).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 16, 1), NoOfProjects_Target(16)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 16, 1), NoOfProjects_Target(16)) + "</td><td align='right'>" + Performance_BOMs(1, 16, 1).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 16, 1).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 17)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(17).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 17, 1).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 17, 1), NoOfProjects_Target(17)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 17, 1), NoOfProjects_Target(17)) + "</td><td align='right'>" + Performance_BOMs(1, 17, 1).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 17, 1).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 18)
                {
                   // text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(18).ToString("N2") + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 19)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(19).ToString("N2") + "</td><td align='right'>" + getCompletesProjects(19).ToString("N2") + "</td><td align='right'>" + Variance(getCompletesProjects(19), NoOfProjects_Target(19)) + "</td><td >" + VarianceRAG(getCompletesProjects(19), NoOfProjects_Target(19)) + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(1, 19).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(1, 19).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 20)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(20).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(0, 20).ToString("N2") + "</td><td align='right'>" + Variance(ExpectedRevenue_By_Status(0, 20), NoOfProjects_Target(20)) + "</td><td >" + VarianceRAG(ExpectedRevenue_By_Status(0, 20), NoOfProjects_Target(20)) + "</td><td align='right'>" + ExpectedRevenue_By_Status(1, 20).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(2, 20).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 21)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right' >" + NoOfProjects_Target(21).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 21, 2).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 21, 2), NoOfProjects_Target(21)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 21, 2), NoOfProjects_Target(21)) + "</td><td align='right'>" + Performance_BOMs(1, 21, 2).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 21, 2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 22)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(22).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 22, 2).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 22, 2), NoOfProjects_Target(22)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 22, 2), NoOfProjects_Target(22)) + "</td><td align='right'>" + Performance_BOMs(1, 22, 2).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 22, 2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 23)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(23).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(0, 23, 2).ToString("N2") + "</td><td align='right'>" + Variance(Performance_BOMs(0, 23, 2), NoOfProjects_Target(23)) + "</td><td >" + VarianceRAG(Performance_BOMs(0, 23, 2), NoOfProjects_Target(23)) + "</td><td align='right'>" + Performance_BOMs(1, 23, 2).ToString("N2") + "</td><td align='right'>" + Performance_BOMs(2, 23, 2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 24)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(24).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(0).ToString("N2") + "</td><td align='right'>" + Variance(FinanceActuals_val(0), NoOfProjects_Target(24)) + "</td><td >" + VarianceRAG(FinanceActuals_val(0), NoOfProjects_Target(24)) + "</td><td align='right'>" + FinanceActuals_val(1).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 25)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(25).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(0,25).ToString("N2") + "</td><td align='right'>" + Variance(FinanceActuals_val(0,25), NoOfProjects_Target(25)) + "</td><td >" + VarianceRAG(FinanceActuals_val(0,25), NoOfProjects_Target(25)) + "</td><td align='right'>" + FinanceActuals_val(1,25).ToString("N2") + "</td><td align='right'>" + FinanceActuals_val(2,25).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 26)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(26).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(0, 26).ToString("N2") + "</td><td align='right'>" + Variance(InvoicedToDate_PrivousYearPer(0, 26), NoOfProjects_Target(26)) + "</td><td>" + VarianceRAG(InvoicedToDate_PrivousYearPer(0, 26), NoOfProjects_Target(26)) + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(1, 26).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(2, 26).ToString("N2") + "</td><tr>";
                    //text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(26).ToString("N2") + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 27)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(27).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(0, 27).ToString("N2") + "</td><td align='right'>" + Variance(InvoicedToDate_PrivousYearPer(0, 27), NoOfProjects_Target(27)) + "</td><td>" + VarianceRAG(InvoicedToDate_PrivousYearPer(0, 27), NoOfProjects_Target(26)) + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(1, 27).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(2, 27).ToString("N2") + "</td><tr>";
                   // text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(27).ToString("N2") + "</td><td align='right'>&nbsp;</td><td>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 28)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(28).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(0, 28).ToString("N2") + "</td><td align='right'>" + Variance(InvoicedToDate_PrivousYearPer(0, 28), NoOfProjects_Target(28)) + "</td><td>" + VarianceRAG(InvoicedToDate_PrivousYearPer(0, 28), NoOfProjects_Target(28)) + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(1, 28).ToString("N2") + "</td><td align='right'>" + InvoicedToDate_PrivousYearPer(2, 28).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 29)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(29).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(0, 29).ToString("N2") + "</td><td align='right'>" + Variance(getCompletesProjects_PrivousYearPer(0, 29), NoOfProjects_Target(29)) + "</td><td >" + VarianceRAG(getCompletesProjects_PrivousYearPer(0, 29), NoOfProjects_Target(29)) + "</td><td align='right'> " + getCompletesProjects_PrivousYearPer(1, 29).ToString("N2") + "</td><td align='right'>" + getCompletesProjects_PrivousYearPer(2, 29).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 30)
                {
                    //POExp_val
                   // text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(30).ToString("N2") + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><tr>";
                }
                if (lblID.LabelID == 31)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(31).ToString("N2") + "</td><td align='right'>" + POExp_val(0, 31).ToString("N2") + "</td><td align='right'>" + Variance(POExp_val(0, 31), NoOfProjects_Target(31)) + "</td><td >" + VarianceRAG(POExp_val(0, 31), NoOfProjects_Target(31)) + "</td><td align='right'> " + POExp_val(1, 31).ToString("N2") + "</td><td align='right'>" + POExp_val(2, 31).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 32)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(32).ToString("N2") + "</td><td align='right'>" + Performance_OverDues(0, 32).ToString("N2") + "</td><td align='right'>" + Variance(Performance_OverDues(0, 32), NoOfProjects_Target(32)) + "</td><td>" + VarianceRAG(Performance_OverDues(0, 32), NoOfProjects_Target(32)) + "</td><td align='right'>" + Performance_OverDues(1, 32).ToString("N2") + "</td><td align='right'>" + Performance_OverDues(2, 32).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 33)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(33).ToString("N2") + "</td><td align='right'>" + Performance_OverDues(0, 33).ToString("N2") + "</td><td align='right'>" + Variance(Performance_OverDues(0, 33), NoOfProjects_Target(33)) + "</td><td>" + VarianceRAG(Performance_OverDues(0, 33), NoOfProjects_Target(33)) + "</td><td align='right'>" + Performance_OverDues(1, 33).ToString("N2") + "</td><td align='right'>" + Performance_OverDues(2, 33).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 34)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(34).ToString("N2") + "</td><td align='right'>" + Issues_Perf(0, 34).ToString("N2") + "</td><td td align='right'>" + Variance(Issues_Perf(0, 34), NoOfProjects_Target(34)) + "</td><td>" + VarianceRAG(Issues_Perf(0, 34), NoOfProjects_Target(34)) + "</td><td align='right'>" + Issues_Perf(1, 34).ToString("N2") + "</td><td align='right'>" + Issues_Perf(2, 34).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 35)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(35).ToString("N2") + "</td><td align='right'>" + Issues_Perf(0, 35).ToString("N2") + "</td><td align='right'>" + Variance(Issues_Perf(0, 35), NoOfProjects_Target(35)) + "</td><td >" + VarianceRAG(Issues_Perf(0, 35), NoOfProjects_Target(35)) + "</td><td align='right'>" + Issues_Perf(1, 35).ToString("N2") + "</td><td align='right'>" + Issues_Perf(2, 35).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 36)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(36).ToString("N2") + "</td><td align='right'>" + Issues_Perf(0, 36).ToString("N2") + "</td><td align='right'>" + Variance(Issues_Perf(0, 36), NoOfProjects_Target(36)) + "</td><td >" + VarianceRAG(Issues_Perf(0, 36), NoOfProjects_Target(36)) + "</td><td align='right'>" + Issues_Perf(1, 36).ToString("N2") + "</td><td align='right'>" + Issues_Perf(2, 36).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 37)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(37).ToString("N2") + "</td><td align='right'>" + Issues_Perf(0).ToString("N2") + "</td><td align='right'>" + Variance(Issues_Perf(0), NoOfProjects_Target(37)) + "</td><td >" + VarianceRAG(Issues_Perf(0), NoOfProjects_Target(37)) + "</td><td align='right'>" + Issues_Perf(1).ToString("N2") + "</td><td align='right'>" + Issues_Perf(2).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 75)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(75).ToString("N2") + "</td><td align='right'>" + Gps(0, 75).ToString("N2") + "</td><td align='right'>" + Variance(Gps(0, 75), NoOfProjects_Target(75)) + "</td><td >" + VarianceRAG(Gps(0, 75), NoOfProjects_Target(75)) + "</td><td align='right'>" + Gps(1, 75).ToString("N2") + "</td><td align='right'>" + Gps(2, 75).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 76)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(76).ToString("N2") + "</td><td align='right'>" + Gps(0, 76).ToString("N2") + "</td><td align='right'>" + Variance(Gps(0, 76), NoOfProjects_Target(76)) + "</td><td >" + VarianceRAG(Gps(0, 76), NoOfProjects_Target(76)) + "</td><td align='right'>" + Gps(1, 76).ToString("N2") + "</td><td align='right'>" + Gps(2, 76).ToString("N2") + "</td><tr>";
                }
                if (lblID.LabelID == 77)
                {
                    text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td align='right'>" + NoOfProjects_Target(77).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(0, 77).ToString("N2") + "</td><td align='right'>" + Variance(ExpectedRevenue_By_Status(0, 77), NoOfProjects_Target(77)) + "</td><td >" + VarianceRAG(ExpectedRevenue_By_Status(0, 77), NoOfProjects_Target(77)) + "</td><td align='right'>" + ExpectedRevenue_By_Status(1, 77).ToString("N2") + "</td><td align='right'>" + ExpectedRevenue_By_Status(2, 77).ToString("N2") + "</td><tr>";
                }

                //text += "<tr><td>" + lblID.LabelsName + "</td><td>" + lblID.categoty + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>";
            }
        }


        text += "</table>";
        ltlDisplay.Text = text;
    }
    #region Functions -Completed Project
    private int getCompletesProjects(int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CmplProjects_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue)?"0":ddlSubprogramme.SelectedValue),string.IsNullOrEmpty(txtFromDate.Text)?string.Format("{0:yyyy}",DateTime.Now.Date):txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue)?"0":ddlProgramme.SelectedValue),label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.ID }).ToList().FirstOrDefault();
        return getVal.Val;

    }
    private double getCompletesProjects_Variance(int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CmplProjectsVariance_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        return getVal.Val.Value;

    }

    private double getCompletesProjects_Target(int label)
    {
         string fromDate = txtFromDate.Text;
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CmplProjectsTarget_Query(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        double val=0;
        //if (getVal != null)
        //{
        //    val = getVal.Val;
        //}

        return getVal.Val.Value;
    }
    private int getCompletesProjects_PrivousYearPer(int years,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CmplProjectsPerformancePre_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue),years,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.ID }).ToList().FirstOrDefault();
        int val = 0;
        if (getVal != null)
        {
            val = getVal.Val;
        }

        return val;
    }

    #endregion

    #region Linq Query Section-Completes Projects
    //Query to get completed project-Current Performance
    private string CmplProjects_Query(int CustomerID,int SubProgramme, string  fromDate, int Programme,int label)
    {
        string q = "";
        if (label == 12)
        {
            q = " in (3)";
        }
        if (label == 1)
        {
            q = " in (6,3)";
        }
        if (label == 19)
        {
            q = " in (2)";
        }
        if (label == 13)
        {
            q = " in (1)";
        }
        fromDate = "01/01/" + fromDate;
        string sql = "";
        sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID " + q + "  and YEAR(P.ProjectEndDate)=YEAR('" + fromDate + "')";
        //+
        //      "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
        //      + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }     
      
         if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    //Get Target-Comoleted project
    private string CmplProjectsTarget_Query(string  fromDate,int label)
    {
        fromDate = "01/01/" + fromDate;
        string sql = "";
        sql = "select  ISNULL(targetvalues,0) as BuyingPrice from KPI_TargetValues where KPI_LabelID=" + label.ToString() + " and YEAR(TargetYears)=YEAR('" + string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(fromDate).ToShortDateString()) + "') ";
       

        return sql;
    }
    // Completed project-Get Variance
    private string CmplProjectsVariance_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int label)
    {
        fromDate = "01/01/" + fromDate;
        string sql = "";

        string q = "";
        if (label == 12)
        {
            q = " in (3)";
        }
        if (label == 1)
        {
            q = " in (6,3)";
        }
        if (label == 19)
        {
            q = " in (2)";
        }
        if (label == 13)
        {
            q = " in (1)";
        }
        if (label == 29)
        {
            sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=29  and YEAR(TargetYears)=YEAR('" + fromDate + "')),0)-COUNT(*) as BuyingPrice from V_ProjectDetails P where P.CustomerReference='' and P.ProjectStatusID in (1,2)  and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        }
        else
        {
            sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + fromDate + "')),0)-COUNT(*)  as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID " + q + " and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        }
       // select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        //+
        //      "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
        //      + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }

    // Get Performance Of Preivious Year
    private string CmplProjectsPerformancePre_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int years,int label)
    {
        fromDate = "01/01/" + fromDate;
        string sql = "";
        string q = "";
        if (label == 12)
        {
            q = " in (3)";
        }
        if (label == 1)
        {
            q = " in (6,3)";
        }
        if (label == 19)
        {
            q = " in (2)";
        }
        if (label == 13)
        {
            q = " in (1)";
        }
        if (label == 29)
        {
            sql = " select COUNT(*) as ID from V_ProjectDetails P where P.CustomerReference='' and P.ProjectStatusID in (1,2) and YEAR(P.ProjectEndDate)=year(dateadd(YEAR,-" + years + ",'" + fromDate + "'))";
        }
        else
        {
            sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID " + q + " and YEAR(P.ProjectEndDate)=year(dateadd(YEAR,-" + years + ",'" + fromDate + "'))";
        }
        // select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        //+
        //      "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
        //      + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }

    #endregion

    #region "Number of Projects Completed to date"
    private double NoOfProjects_Target(int label)
    {
        string fromDate = txtFromDate.Text;
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(NoOfProjectsTarget_Query1(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Val.Value;
        }

        return val;
    }
    private double NoOfProjects_PrivousYearPer(int years,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(NoOfProjects_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), years,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Val.Value;
        }

        return val;
    }
    private double NoOfProjects_Variance(int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(NoOfProjectsVariance_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue),label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        return getVal.Val.Value;

    }


    #endregion

    #region "Number of Projects Completed to date-Query Section-Lable 2nd"
    private string NoOfProjects_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int years,int label)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";
        string query = "";
        if (label == 2)
        {
            query = "convert(float,COUNT(*))";
        }
        else
        {
            //query = "convert(float,SUM(isnull(P.BudgetaryCostLevel3,0))) ";
            query = "isnull(convert(float,SUM(isnull(P.BudgetaryCostLevel3,0))),0)";
        }
        sql = "select  " + query + " as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
       // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    private string NoOfProjectsTarget_Query1( string fromDate,int label)
    {
        // = "01/01/" + fromDate;
        string sql = "";
        //sql = "select  ISNULL(targetvalues,0) as ID from KPI_TargetValues where KPI_LabelID=2  and YEAR(TargetYears)=YEAR(DATEADD(year," + years.ToString() + ", '" + Currentdate(fromDate) + "'))";
        sql = "select  ISNULL(targetvalues,0) as BuyingPrice from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + Currentdate(fromDate) + "')";



        return sql;
    }

    private string NoOfProjectsVariance_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int label)
    {

        string query = "";
        if (label == 2)
        {
            query = "convert(float,COUNT(*))";
        }
        else
        {
            //query = "convert(float,SUM(isnull(P.BudgetaryCostLevel3,0))) ";
            query = "isnull(convert(float,SUM(isnull(P.BudgetaryCostLevel3,0))),0)";
        }
        string sql = "";
        //sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=2 and YEAR(TargetYears)=YEAR(DATEADD(year," + years.ToString() + ", '" + Currentdate(fromDate) + "')),0)-COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year," + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and DATEADD(year," + years.ToString() + ",'" + Currentdate(fromDate) + "')";
        sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + Currentdate(fromDate) + "')),0)-" + query + "  as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        // select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        //+
        //      "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
        //      + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }

    private string Currentdate(string year)
    {
         int day = DateTime.Now.Day;
         int mont = DateTime.Now.Month;
         year = mont.ToString() + "/" + day.ToString() + "/" + year;
        return year;
    }
    #endregion
    #region "Number of Projects Completed this month-Functions"
    private double CompletedThisMonth_Performance(int years,int Month,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CompletedThisMonth_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), years,Month,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.ID }).ToList().FirstOrDefault();
        int val = 0;
        if (getVal != null)
        {
            val = getVal.Val;
        }

        return Convert.ToDouble(val.ToString());
    }

    private double CompletedThisMonth_Target(int month,int label)
    {
        string fromDate = txtFromDate.Text;
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>
            (CompletedThisMonthTarget_Query1(string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,month,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Val.Value;
        }

        return val;
    }
    private double CompletedThisMonth_Variance(int Month,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(CompletedThisMonthVariance_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue),Month,label)).ToList();
        var getVal = (from r in getCompletePer
                      select new { Val = r.BuyingPrice }).ToList().FirstOrDefault();
        return getVal.Val.Value;

    }
    #endregion
    #region "Number of Projects Completed this month-Query"
    private string CompletedThisMonth_Query(int CustomerID, int SubProgramme, string fromDate, int Programme, int years,int month,int label)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";
        if (label == 12)
        {
            sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (3) and P.ProjectEndDate between (select dbo.get_month_start(dateadd(month,-" + month.ToString() + ",(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))))) and (select dbo.get_month_end(dateadd(month,-" + month.ToString() + ",(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))))";
           // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.get_month_start(dateadd(month,-" + month.ToString() + ",(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))))) and (select dbo.get_month_end(dateadd(month,-" + month.ToString() + ",(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))))";
        }
        if (label == 3)
        {
            sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.get_month_start(dateadd(month,-" + month.ToString() + ",(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))))) and (select dbo.get_month_end(dateadd(month,-" + month.ToString() + ",(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))))";
        }
        if (label == 4)
        {
            sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.get_month_start(dateadd(month,-1,(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))))) and (select dbo.get_month_end(dateadd(month,-1,(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))))";
        }
        //sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.get_month_start((dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.get_month_end(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";

        
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    private string CompletedThisMonthTarget_Query1(string fromDate,int month,int label)
    {
        // = "01/01/" + fromDate;
        string sql = "";
        //sql = "select  ISNULL(targetvalues,0) as ID from KPI_TargetValues where KPI_LabelID=2  and YEAR(TargetYears)=YEAR(DATEADD(year," + years.ToString() + ", '" + Currentdate(fromDate) + "'))";
        sql = "select  ISNULL(targetvalues,0) as BuyingPrice from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "')) and MONTH(TargetYears)=MONTH(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "'))";



        return sql;
    }
    private string CompletedThisMonthVariance_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int month,int label)
    {

        string sql = "";
        if (label == 12)
        {
            sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + " and YEAR(TargetYears)=YEAR(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "')) and MONTH(TargetYears)=MONTH(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "'))),0)-COUNT(*) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (3) and P.ProjectEndDate between (select dbo.get_month_start(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "'))) and (select dbo.get_month_end(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "')))";
        }
        else
        {
            //sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=2 and YEAR(TargetYears)=YEAR(DATEADD(year," + years.ToString() + ", '" + Currentdate(fromDate) + "')),0)-COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year," + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and DATEADD(year," + years.ToString() + ",'" + Currentdate(fromDate) + "')";
            sql = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + " and YEAR(TargetYears)=YEAR(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "')) and MONTH(TargetYears)=MONTH(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "'))),0)-COUNT(*) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.get_month_start(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "'))) and (select dbo.get_month_end(DATEADD(month," + month.ToString() + ", '" + Currentdate(fromDate) + "')))";
        }
        // select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and YEAR(P.ProjectEndDate)=YEAR('" + string.Format("{0:MM/dd/yyyy}", fromDate) + "')";
        //+
        //      "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
        //      + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    #endregion

    #region "Total Invoiced to Date from Completed Jobs-Functions"
    private double InvoicedToDate_PrivousYearPer(int years,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(InvoicedToDate_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), years,label)).ToList();
        var getVal = (from r in getCompletePer
                      select r ).ToList().Sum(a=>a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }
    private double InvoicedToDate_Perf()
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(InvoicedToDateVari_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue))).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().Sum(a => a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }
    private double InvoicedToDate(double target)
    {
        double diff = 0;
        diff = target - InvoicedToDate_Perf();
        return diff;
    }
    #endregion 
    #region "Total Invoiced to Date from Completed Jobs-Query"
    private string InvoicedToDate_Query(int CustomerID, int SubProgramme, string fromDate, int Programme, int years,int label)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";

        //sql = "select  ISNULL((select sum(Value)  as BuyingPrice  from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (label == 7)
        {
            sql = "select  ISNULL((select sum(isnull(Value,0))  as BuyingPrice  from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        }
        if (label == 27)
        {
            sql = "select  ISNULL((select sum(isnull(Value,0))  as BuyingPrice  from  ProjectValuations where InvoiceStatus=3 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,2) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        }
        if (label == 26)
        {
            sql = "select  ISNULL((select sum(isnull(Value,0))  as BuyingPrice  from  ProjectValuations where ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,2) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        }
        if (label == 28)
        {
            sql = "select  ISNULL((select sum(isnull(Value,0))  as BuyingPrice  from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,2) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        }
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    private string InvoicedToDateVari_Query(int CustomerID, int SubProgramme, string fromDate, int Programme)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";
        
        //sql = "select  ISNULL((select sum(Value) from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        sql = "select  ISNULL((select sum(Value) from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }

        return sql;
    }
    #endregion
    #region "Expected Revenue from projects to date-function"
    private double ExpectedRevenue_Perform(int years)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(ExpectedRevenue_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().Sum(a => a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }
    private double ExpectedRevenue_Perf()
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(InvoicedToDateVari_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue))).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().Sum(a => a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }

    #endregion
    #region "Expected Revenue from projects to date-Query"
    private string ExpectedRevenue_Query(int CustomerID, int SubProgramme, string fromDate, int Programme, int years)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";

        sql = " select isnull(sum(forecastspend),0) as BuyingPrice  from  ForecastExpenditure where  ProjectReference   in (select ProjectReference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += " )";
        return sql;
    }

    private string ExpectedRevenueVariance_Query(int CustomerID, int SubProgramme, string fromDate, int Programme, int label)
    {

        string sql = "";

        sql = " select isnull(sum(forecastspend),0) from  ForecastExpenditure where  ProjectReference   in (select ProjectReference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += " )";
        return sql;
    }

    private double variance_RevCast(double target, double val)
    {
        double diff = 0;
        diff = target - val;
        return diff;
    }


    #endregion

    #region "Total revenue"

    private double ExpectedRevenue_By_Status(int years,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Revenue_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue),label,years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().Sum(a => a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }
    private double ExpectedRevenue_Variance(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Revenue_Query_Variance(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().Sum(a => a.BuyingPrice);
        double val = 0;
        if (getVal != null)
        {
            val = getVal.Value;
        }

        return val;
    }
    private string Revenue_Query(int CustomerID, int SubProgramme, string fromDate, int Programme, int label,int years)
    {
        string sql = "";
        if (label == 14)
        {
            sql = "select isnull(sum(isnull(P.BudgetaryCost,0)),0)  as BuyingPrice  from Projects P where P.ProjectStatusID in (1)";//pending revenue
        }
        if (label == 20)
        {
            sql = "select isnull(sum(isnull(P.BudgetaryCostLevel3,0)),0)  as BuyingPrice  from Projects P where P.ProjectStatusID in (2)";//live revenue
        }
        if (label ==77)
        {
            sql = "select isnull(sum(isnull(P.BudgetaryCostLevel3,0)),0)  as BuyingPrice  from Projects P where P.ProjectStatusID in (6)";//live revenue
        }
        sql = sql + " and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        return sql;
    }
    private string Revenue_Query_Variance(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        string sqlTarget = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + Currentdate(fromDate) + "')),0)-";
        //if (label == 14)
        //{
        //    string sqlTarget = "select isnull((select ISNULL(targetvalues,0) from KPI_TargetValues where KPI_LabelID=" + label.ToString() + "  and YEAR(TargetYears)=YEAR('" + Currentdate(fromDate) + "')),0)-";
        //}
        if (label == 14)
        {
            sql =sqlTarget+ "isnull(sum(isnull(P.BudgetaryCost,0)),0) from Projects P where P.ProjectStatusID in (1)";//pending revenue
        }
        if (label == 20)
        {
            sql = sqlTarget+"isnull(sum(isnull(P.BudgetaryCostLevel3,0)),0)  from Projects P where P.ProjectStatusID in (2)";//live revenue
        }
        sql = sql + " and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        return sql;
    }

    #endregion

    #region "BOM"
    private double Performance_BOMs(int years, int label,int status)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Performance_BOM(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years, status)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.BuyingPrice.Value;
        }

        return val;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="CustomerID"></param>
    /// <param name="SubProgramme"></param>
    /// <param name="fromDate"></param>
    /// <param name="Programme"></param>
    /// <param name="label"></param>
    /// <param name="years"></param>
    /// <param name="status"> 1-Pending,2-Live,3-Awaiting Sign-off</param>
    /// <returns></returns>
    private string Performance_BOM(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years,int status)
    {
        string sql = "";
        if (label == 15)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Labour,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        if (label == 16)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Material,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        if (label == 17)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Mics,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        if (label == 21)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Labour,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        if (label == 22)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Material,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        if (label == 23)
        {
            sql = "select isnull(sum((isnull(qty,0)*isnull(Mics,0))),0) as BuyingPrice from ProjectBOM where ProjectReference in (";
        }
        sql = sql + "(select ProjectReference from Projects P where P.ProjectStatusID in (" + status + ") and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += "))";
        return sql;
    }
    #endregion

    #region GP
    private double Gps(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(GP(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.BuyingPrice.Value;
        }

        return val;
    }
    private string GP(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        if (label == 75)
        {
            sql = "select isnull((sum(isnull(P.BuyingPrice,0))/NULLIF(sum(isnull(P.BudgetaryCost,0)),0))*100,0) as BuyingPrice  from Projects P where P.ProjectStatusID in (1)";//pending profit
        }
        if (label == 76)
        {
            sql = "select isnull((sum(isnull(P.BuyingPrice,0))/NULLIF(sum(isnull(P.BudgetaryCost,0)),0))*100,0) as BuyingPrice  from Projects P where P.ProjectStatusID in (2)";//live profit
        }
        //if (label == 20)
        //{
        //    sql = "select isnull(sum(isnull(P.BudgetaryCostLevel3,0)),0) from Projects P where P.ProjectStatusID in (2)";//live revenue
        //}
        sql = sql + " and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        return sql;
    }
    #endregion

    #region "OverDueTasks"
    private double Performance_OverDues(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Performance_OverDue(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        int val = 0;
        if (getVal != null)
        {
            val = getVal.ID;
        }

        return Convert.ToDouble(val);
    }
    private string Performance_OverDue(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        
        if (label == 32)
        {
            sql = "select isnull(COUNT(*),0) as ID from ProjectTaskItems where isMilestone=0 and RAGStatus in ('RED','AMBER') and ProjectReference in (";
        }
        if (label == 33)
        {
            sql = "select isnull(COUNT(*),0) as ID from ProjectTaskItems where isMilestone=1 and RAGStatus in ('RED','AMBER') and ProjectReference in (";
        }
        sql = sql + "(select ProjectReference from Projects P where P.ProjectStatusID in (6,2) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += "))";
        return sql;
    }
    #endregion


    #region "Issues& Risks & variation"
    private double Issues_Perf(int year,int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Issues_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue),label,year)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        int val = 0;
        if (getVal != null)
        {
            val = getVal.ID;
        }

        return Convert.ToDouble(val.ToString());
    }
    private double Issues_Perf(int year)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(Issues_Query(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), 37, year)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.BuyingPrice.Value;
        }

        return Convert.ToDouble(val.ToString());
    }
    private string Issues_Query(int CustomerID, int SubProgramme, string fromDate, int Programme,int label,int years)
    {
        //fromDate = "01/01/" + fromDate;
        string sql = "";

        //sql = "select  ISNULL((select sum(Value) from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        //sql = "select  ISNULL((select sum(Value) from  ProjectValuations where InvoiceStatus=1 and ProjectReference in (P.ProjectReference) ),0) as BuyingPrice from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year('" + Currentdate(fromDate) + "'))) and '" + Currentdate(fromDate) + "'";
        if (label == 35)
        {
            sql = "select ISNULL(count(*),0) as ID  from ProjectIssues where NextReviewDate<(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')) and Status in (1,5) and Projectreference in ( select Projectreference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";
        }

        if (label == 34)
        {
            sql = "select ISNULL(count(*),0) as ID  from AC2P_Risks where NextReviewDate<(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')) and  Reportstatus in (2,4)  and Projectreference in ( select Projectreference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";
        }
        if (label == 36)
        {
            sql = "select isnull(COUNT(*),0) as ID from DeviationReport where Approved=1   and Projectreference in ( select Projectreference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";
        }
        if (label == 37)
        {
            sql = "select  isnull(sum(DeviationValue),0) as BuyingPrice from DeviationReport where Projectreference in ( select Projectreference from V_ProjectDetails P where P.ProjectStatusID in (6,3) and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";
        }
        // sql = "select COUNT(*) as ID from V_ProjectDetails P where P.ProjectStatusID in (6,3)  and P.ProjectEndDate between (select dbo.vtStartdate(year(GETDATE()))) and GETDATE()";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        sql += ")";
        return sql;
    }
    #endregion

    #region "Finance Actulas"
    private double FinanceActuals_val(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(FinanceActuals(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        double val = 0;
        if (getVal != null)
        {
            val = getVal.BuyingPrice.Value;
        }

        return val;
    }
    private double FinanceActuals_val(int years)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(FinanceActuals(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), 24, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList().FirstOrDefault();
        int val = 0;
        if (getVal != null)
        {
            val = getVal.ID;
        }

        return Convert.ToDouble(val.ToString());
    }
    private string FinanceActuals(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        if (label == 8)
        {
            sql = "select isnull(isnull(SUM(dbo.ActualCost_FM(ProjectReference)),0)/NULLIF(sum(BudgetaryCostLevel3),0),0) as BuyingPrice from projects P where P.ProjectStatusID in (6,2)";
        }
        if (label == 25)
        {
            sql = "select isnull(sum(BuyingPrice)-sum(dbo.ActualCost_FM(ProjectReference)),0) as BuyingPrice from projects P where P.ProjectStatusID in (2)";
        }
        if (label == 24)
        {
            sql = "select  isnull(COUNT(*),0)  as ID from projects P where P.ProjectStatusID in (2) and dbo.ActualCost_FM(ProjectReference)>BuyingPrice ";
        }
        if (label == 9)
        {
            sql = "select isnull(SUM(T.TotalCost)/nullif(SUM(P.BudgetaryCostLevel3),0),0)*100  as BuyingPrice  from   TimesheetEntry T,projects P where  P.ProjectReference=T.ProjectReference and P.ProjectStatusID in (6,2)";//live profit
        }
        if (label == 10)
        {
            sql = "select isnull(SUM(PB.Material*PB.Qty)/nullif(SUM(P.BudgetaryCostLevel3),0),0)*100  as BuyingPrice  from   ProjectBOM PB,projects P where  P.ProjectReference=PB.ProjectReference and P.ProjectStatusID in (6,2)";//live profit
        }
        //if (label == 20)
        //{
        //    sql = "select isnull(sum(isnull(P.BudgetaryCostLevel3,0)),0) from Projects P where P.ProjectStatusID in (2)";//live revenue
        //}
        sql = sql + " and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        return sql;
    }
    #endregion

    #region PO Exp
    private double POExp_val(int years, int label)
    {
        var getCompletePer = projectDB.ExecuteQuery<ProjectDetails>(PO(int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue), string.IsNullOrEmpty(txtFromDate.Text) ? string.Format("{0:yyyy}", DateTime.Now.Date) : txtFromDate.Text,
             int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue), label, years)).ToList();
        var getVal = (from r in getCompletePer
                      select r).ToList();
        int val = 0;
        //if(getVal!=null)
       
        if (getVal != null)
        {
           
                var values = (from r in getVal
                              where r.BuyingPrice <= 0
                              select r).ToList();
                if (values != null)
                {
                val = values.Count();
            }
           
        }

        return Convert.ToDouble(val.ToString());
    }
    private string PO(int CustomerID, int SubProgramme, string fromDate, int Programme, int label, int years)
    {
        string sql = "";
        if (label == 31)
        {
            sql = "select isnull((select DDays from  Customer_PODatabase where PONumber=P.CustomerReference and  ProjectRef=P.ProjectReference),0) "
+ " -isnull((select dbo.POHours((select  dbo.ConvertToHours(sum(dbo.ConvertToMins(hours))))) from timesheetentry where ProjectReference=P.ProjectReference "                             
 +" and EntryType in (select HourNameType from podrain)  "
 + " and PONumber=P.CustomerReference ),0) as BuyingPrice  "
+ " from   V_ProjectDetails P where P.CustomerReference<>''";
        }
        sql = sql + " and P.ProjectEndDate between (select dbo.vtStartdate(year(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))) and (select dbo.Get_Year_EndDate(dateadd(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "')))";//(DATEADD(year,-" + years.ToString() + ",'" + Currentdate(fromDate) + "'))";
        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }

        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (SubProgramme != 0)
        {
            sql += "  and P.SubProgramme=" + SubProgramme.ToString();
        }
        return sql;
    }
    #endregion
    private string  RAG_Variance(double CurrentPerformance,double target)
    {
        string text = "";
        //if (target != 0)
        //{
        CurrentPerformance = -1 * CurrentPerformance;
            if (CurrentPerformance > target)
            {
                text = "<img alt='green' src='media/indcate_green.png' />";
                //text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_green.png' />";
            }
            else if (CurrentPerformance < target)
            {
                // text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_red.png' />";
                text = "<img alt='red' src='media/indcate_red.png'   />";
            }
            else
            {
                text = "";
            }
        //}
        //else
        //{

        //}
        return text;
    }

    private string Variance(double currentPerformance,double targetValue)
    {
      
        double val = targetValue-currentPerformance;
       
        return val.ToString("N2");
    }
    private string VarianceRAG(double currentPerformance, double targetValue)
    {
        string text = "";
        double val = targetValue - currentPerformance;
        if (currentPerformance > targetValue)
        {
            text = "<img alt='green' src='media/indcate_green.png' />";
            //text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_green.png' />";
        }
        else if (currentPerformance < targetValue)
        {
            // text = "<asp:Image runat='server' ID='Img'  ImageUrl='~/media/indcate_red.png' />";
            text = "<img alt='red' src='media/indcate_red.png'   />";
        }
        else
        {
            text = "";
        }
      return text;
    }

    protected void imgView_Click(object sender, EventArgs e)
    {
        try{
        BuildTable();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
