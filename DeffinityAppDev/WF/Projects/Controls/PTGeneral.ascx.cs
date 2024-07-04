using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Finance.DAL;
using Finance.Entity;
using TimesheetMgt.DAL;
using UserMgt.DAL;
using ProjectTracker.BLL;

public partial class controls_ProjectTracker_General : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    DisBindings getData = new DisBindings();
    RiseValuation RiseVal = new RiseValuation();
    BuildProject bp = new BuildProject();
    int project;
    string invoice = "";
    int ConID = 0;
    Email mail = new Email();
    string error = "";
    StringBuilder strHtml = new StringBuilder();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectGeneralValues(QueryStringValues.Project);//QueryStringValues.Project
            CreditlblsBinding();
        }
    }
    protected void imgMinusMCD_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                Project project = db.Projects.Where(p => p.ProjectReference == QueryStringValues.Project).Select(p => p).FirstOrDefault();
                if (project != null)
                {
                    project.MinusMCD = double.Parse(txtMinusMCD.Text);
                    db.SubmitChanges();
                }
                SelectGeneralValues(QueryStringValues.Project);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SelectGeneralValues(int pref)
    {
        string c = "C";

        try
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectProject");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    txtOriginalSalesValue.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["BudgetaryCost"]);
                    txtGP.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["BudgetGP"]);
                    lblOriginalProfit.Text = (double.Parse(txtOriginalSalesValue.Text) * ((double.Parse(txtGP.Text)) / 100)).ToString(c);
                    //txtOriginalSalesValue.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", 40247.13);
                    //txtGP.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", 25);
                    //lblOriginalProfit.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", (double.Parse(txtOriginalSalesValue.Text) * ((double.Parse(txtGP.Text)) / 100)));
                    txtMinusMCD.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["MinusMCD"]);
                    if (dr["BudgetaryCost"].ToString() == "0")
                    {
                        // lblProjectbudget.Text = "0.00";

                    }
                    else
                    {
                        // lblProjectbudget.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["BudgetaryCost"]);
                    }

                    if (dr["ActualCost"].ToString() == "0")
                    {
                        lblactualProjectCosttodate.Text = "0.00";
                    }
                    else
                    {
                        lblactualProjectCosttodate.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["ActualCost"]);
                    }
                }
            }
            cmd.Dispose();
            cmd = db.GetStoredProcCommand("DN_getTotals");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    lblNewVariations.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalVariations"]);
                    lblVariations.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalVariations"]);
                    lblBonuses.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalBonusValue"]);
                    lblAbatements.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalAbatementsValue"]);
                    lblInvoiced.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["InvoicedToDate"]);
                    lblOutStanding.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["OutInvoicedToDate"]);
                    lblIndirectcost.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalIndirectcost"]);
                    lblVariationsUnapproved.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:C}", dr["TotalVariationsUnapproved"]);
                }
            }

            cmd.Dispose();
            //
            cmd = db.GetStoredProcCommand("ProjectFinanacialActualSummary");
            db.AddInParameter(cmd, "@ProjectRef", DbType.Int32, QueryStringValues.Project);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {

                while (dr.Read())
                {

                    lblactualhours.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", dr["Hours"]);

                }
            }
            cmd.Dispose();

            double totalVariation = 0;

            /************************************************************************************
             General Page Calculation
             ************************
             CURRENT SALES VALUE = BUDGET SALES VALUE + Variations
             Net Sales =   CURRENT SALES VALUE - Minus MCD
             
             Forecast Cost to Complete = Labour Tracker Cost Remaining + Material Tracker Cost Remaining + Misc Tracker Cost Remaining + PM Hours Cost Remaining + Staff Hours Cost Remaining + Expense Tracker Cost Remaining
             Spent to date = Labour Tracker Spent to Date + Material Tracker Spent to Date + Misc Tracker Spent to Date + PM Hours Auctual + Staff Hours Actual + Expense Tracker Spent to Date
              
             FORECAST SALES VALUE = CURRENT SALES VALUE
             FORECAST COST VALUE = Forecast Cost to Complete + Spent to date
             FORECAST GP (%) = (Forecast Profit / Current Sales Value) x 100
             FORECAST PROFIT = FORECAST SALES VALUE - FORECAST COST VALUE

            ************************************************************************************/
            totalVariation = ProjectTrackerBAL.GetVariationCostByProject(pref);
            lblNewVariations.Text = totalVariation.ToString(c);

            lblCurrentSalesValue.Text = (double.Parse(txtOriginalSalesValue.Text) + totalVariation).ToString(c);
            lblMinusMCD.Text = (double.Parse(lblCurrentSalesValue.Text, NumberStyles.Currency) * ((double.Parse(txtMinusMCD.Text)) / 100)).ToString(c);
            lblNetSales.Text = (double.Parse(lblCurrentSalesValue.Text, NumberStyles.Currency) - double.Parse(lblMinusMCD.Text, NumberStyles.Currency)).ToString(c);
            lblForecastSalesValues.Text = lblCurrentSalesValue.Text;

            double labourCostRemaining = 0, labourSpentToDate = 0, materialCostRemaining = 0, materialSpentToDate = 0,miscCostRemaining=0,miscSpentToDate=0,
                pmHoursCostRemaining = 0, pmHoursSpentToDate = 0, expenseCostRemaining = 0, expenseSpentToDate = 0;

            ProjectTrackerBAL.LabourCost(pref, out labourCostRemaining, out labourSpentToDate);
            ProjectTrackerBAL.MaterialCost(pref, out materialCostRemaining, out materialSpentToDate);
            ProjectTrackerBAL.MiscCost(pref, out miscCostRemaining, out miscSpentToDate);
            ProjectTrackerBAL.PMandStaffHoursCost(pref, out pmHoursCostRemaining, out pmHoursSpentToDate);
            ProjectTrackerBAL.ExpenseCost(pref, out expenseCostRemaining, out expenseSpentToDate);


            double totalCostToRemaining = (labourCostRemaining + materialCostRemaining + miscCostRemaining + pmHoursCostRemaining + expenseCostRemaining);
            double totalSpentToDate = (labourSpentToDate + materialSpentToDate + miscSpentToDate + pmHoursSpentToDate + expenseSpentToDate);

            if (totalCostToRemaining < 0)
            {
                totalCostToRemaining = 0;
            }
            lblForecastCostToComplete.Text = totalCostToRemaining.ToString(c);
            lblSpentToDate.Text = totalSpentToDate.ToString(c);

            lblForecastCostValue.Text = (totalCostToRemaining + totalSpentToDate).ToString(c);
            lblForecastProfit.Text = (double.Parse(lblForecastSalesValues.Text, NumberStyles.Currency) - (double.Parse(lblForecastCostValue.Text, NumberStyles.Currency))).ToString(c);
            double forcastGP = (double.Parse(lblForecastProfit.Text, NumberStyles.Currency) / (double.Parse(lblForecastSalesValues.Text, NumberStyles.Currency)));

            //FORECAST GP (%) = (Forecast Profit / Current Sales Value) x 100
            lblForecastGP.Text = (forcastGP * 100).ToString("F2");

            //Invoice Outstanding Amount =Current Sales Value – Invoiced to date
            lblOutStanding.Text = (double.Parse(lblCurrentSalesValue.Text, NumberStyles.Currency) - double.Parse(lblInvoiced.Text, NumberStyles.Currency)).ToString("C");

            //For some reason Spent to Date and Actual Project cost to date are different. They should be the same.
            // lblactualProjectCosttodate.Text = lblSpentToDate.Text;




        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    public void CreditlblsBinding()
    {
        try
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                var x = Pdc.Project_CreditNotes.Where(a => a.ProjectRef == QueryStringValues.Project).ToList();
                if (x != null)
                {
                    lblCreditnote.Text = string.Format(CultureInfo.GetCultureInfo("en-GB"), "{0:n}", x.Select(a => a.CreditValue.HasValue ? a.CreditValue : 0).Sum());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

}