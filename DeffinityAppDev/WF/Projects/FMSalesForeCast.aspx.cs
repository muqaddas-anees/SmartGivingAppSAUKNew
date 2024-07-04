using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Finance.DAL;
using Finance.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using System.Collections;
using CustomerContract.DAL;
public partial class FMSalesForeCast : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //ProjectFM_GetExpanditure need to use
        try
        {
            if (!IsPostBack)
            {
               // Master.PageHead = "Project Tracker";
                BindCustomers();
                BindToDropdown();
                SalesForeCast();
                // grdInvoiceRaised.Focus();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindToDropdown()
    {
        projectTaskDataContext pt = new projectTaskDataContext();

        try
        {
          
        var projectstatus = from p in pt.ProjectStatus
                            select new { p.ID, p.Status };
        DropDownListStatus.DataTextField = "Status";
        DropDownListStatus.DataValueField = "ID";
        DropDownListStatus.DataSource = projectstatus;
        DropDownListStatus.DataBind();
        //DropDownListStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
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
            //ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgView_Click(object sender, EventArgs e)
    {
        SalesForeCast();
    }

    #region Sales Forecast
    private void SalesForeCast()
    {
        try
        {
            ArrayList expandi = null;
            ArrayList month = null;
            string text = string.Empty;
            //select list of months
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectFM_SalesForecast",
                new SqlParameter("@PortfolioID", int.Parse(ddlCustomer.SelectedValue)),
                new SqlParameter("@FromDate", Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text)),
                new SqlParameter("@ToDate", Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text))).Tables[0];

            //select programme for selected customers

            projectTaskDataContext projects1 = new projectTaskDataContext();
            ProgrammeDataContext programme = new ProgrammeDataContext();
            var programmes = (from r in programme.OperationsOwners
                              where r.PortfolioID == int.Parse(ddlCustomer.SelectedValue) 
                              select r).ToList();
            //if (dt.Rows.Count != 0)
            //{
            //    if (dt.Rows.Count > 12)
            //    {
            //        text += "<table width='100%' id='mainTable'  bordercolor='gray' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px'><tr><td style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</td>";
            //    }
            //    else
            //    {
            //        text += "<table  id='mainTable'  bordercolor='gray' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px'><tr><td style='align:left'>&nbsp;&nbsp;&nbsp;</td>";
            //    }
            //}
            //else
            //{
            //    text += "<table width='100%' id='mainTable'  bordercolor='gray' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px'><tr><td style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</td>";
            //}

            text += "<table class='table table-small-font table-bordered table-striped dataTable' width='100%' id='mainTable'  bordercolor='gray' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px'><tr><td style='width:25%;align:left'>&nbsp;&nbsp;&nbsp;</td>";
           
            //font-weight:bold style='width:10px' align="center"
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    text += "<td align='center'><b>" + dt.Rows[i]["Month"] + "</b></td>";
                }
                text += "</tr>";
                List<ProjectBenefitItem> list = new List<ProjectBenefitItem>();
                if (programmes != null)
                {
                    foreach (ProgrammeMgt.Entity.OperationsOwner prog in programmes)
                    {
                        //display programme list
                        text += "<tr><td style='color:gray' colspan='" + dt.Rows.Count + 1 + "'><b>Sub Programme: " + prog.OperationsOwners + "</b></td></tr>";

                        var status = new int[] { 1, 2 };
                        var projetsList = (from r in projects1.ProjectDetails
                                           where r.OwnerGroupID == prog.ID && r.Portfolio == int.Parse(ddlCustomer.SelectedValue)
                                           && r.ProjectStatusID == int.Parse(DropDownListStatus.SelectedValue)
                                           select r).ToList();

                        ProjectBenefitItem pb = new ProjectBenefitItem();
                        if (projetsList != null)
                        {
                            foreach (ProjectDetails project in projetsList)
                            {
                                   //DataTable dt1 = new DataTable();
                                   //dt1 = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectFM_GetExpanditure",
                                   //    new SqlParameter("@Portfolio", int.Parse(ddlCustomer.SelectedValue)), new SqlParameter("@ProjectReference", project.ProjectReference)).Tables[0];
                                //display projects list depending on programme id. 
                                expandi = GetExpanditure(int.Parse(ddlCustomer.SelectedValue), project.StartDate.Value, project.ProjectEndDate.Value, project.ProjectReference);
                                int monthDiff = MonthDifference(project.StartDate.Value, project.ProjectEndDate.Value);
                                month = GetMonths(int.Parse(ddlCustomer.SelectedValue), project.StartDate.Value, project.ProjectEndDate.Value);
                                text += "<tr><td> <a href='./WF/Projects/ProjectFinancials.aspx?project="+project.ProjectReference+"'>" + project.ProjectReferenceWithPrefix + "-" + project.ProjectTitle + "</a></td>";
                                if (dt.Rows.Count != 0)
                                {
                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        int ch = 0;
                                        if (month != null)
                                        {
                                            for (int k = 0; k < month.Count; k++)
                                            {
                                                pb = new ProjectBenefitItem();
                                                //if (Convert.ToDateTime(dt.Rows[j]["orgDate"]).Month == Convert.ToDateTime(month[k]).Month)
                                                if (Convert.ToDateTime(dt.Rows[j]["orgDate"]).Month == Convert.ToDateTime(month[k]).Month
                                                  && Convert.ToDateTime(dt.Rows[j]["orgDate"]).Year == Convert.ToDateTime(month[k]).Year)
                                               
                                                {
                                                    ch = 1;
                                                    //text += "<td>" + string.Format("{0:d}",month[k]) + "</td>";
                                                    if (monthDiff != 0)
                                                    {

                                                        //GetExpanditure(Convert.ToDateTime(dt.Rows[j]["orgDate"]), project.ProjectReference))
                                                        //text += "<td align='right'>" + string.Format("{0:f2}", (project.BudgetaryCost.Value / Convert.ToDouble(month.Count))) + "</td>" ;
                                                        text += "<td align='right'>" + string.Format("{0:f2}", expandi[k]) + "</td>";
                                                        pb.BenefitID = prog.ID;
                                                        pb.Achievement = Convert.ToDouble(expandi[k]); //GetExpanditure(Convert.ToDateTime(dt.Rows[j]["orgDate"]), project.ProjectReference);
                                                        //pb.Achievement = GetExpanditure(Convert.ToDateTime(month[k]), project.ProjectReference);//(project.BudgetaryCost.Value / Convert.ToDouble(month.Count));
                                                        pb.Period = Convert.ToDateTime(month[k]);
                                                        list.Add(pb);
                                                    }
                                                    else
                                                    {
                                                        // Convert.ToDouble(dt1.Rows[k]["Expenses"])
                                                        //text += "<td align='right'>" + string.Format("{0:f2}", (project.BudgetaryCost.Value)) + "</td>";
                                                        text += "<td align='right'>" + string.Format("{0:f2}", expandi[k]) + "</td>";
                                                        pb.BenefitID = prog.ID;
                                                        pb.Achievement = Convert.ToDouble(expandi[k]); //GetExpanditure(Convert.ToDateTime(dt.Rows[j]["orgDate"]), project.ProjectReference);// project.BudgetaryCost.Value;
                                                        pb.Period = Convert.ToDateTime(month[k]);
                                                        list.Add(pb);
                                                    }
                                                }
                                                //else
                                                //{
                                                //  //  text += "<td>&nbsp;</td>";
                                                //}
                                            }
                                        }
                                        if (ch == 0)
                                        {
                                            text += "<td>&nbsp;</td>";
                                        }
                                    }
                                }
                                text += "</tr>";

                            }
                        }
                        //calculate programme total here...........
                        if (dt.Rows.Count != 0)
                        {
                            text += "<tr><td style='color:gray'><b>Programme Total</b></td>";
                            for (int j1 = 0; j1 < dt.Rows.Count; j1++)
                            {
                                int ch1 = 0;
                                
                                ch1 = 1;
                                double list1 = (from r in list
                                                where r.Period.Value.Month == Convert.ToDateTime(dt.Rows[j1]["orgDate"]).Month &&
                                                 r.Period.Value.Year == Convert.ToDateTime(dt.Rows[j1]["orgDate"]).Year
                                                    && r.BenefitID == prog.ID
                                                select r).Sum(n => n.Achievement.Value);
                                text += "<td align='right'>" + string.Format("{0:f2}", list1) + "</td>";
                               
                                if (ch1 == 0)
                                {
                                    text += "<td>&nbsp;</td>";
                                }
                            }
                        }
                        text += "</tr>";
                    }
                }
                //Total month calculation goes here.............
                if (dt.Rows.Count != 0)
                {
                    text += "<tr><td  colspan='" + dt.Rows.Count + 1 + "'>&nbsp;</td></tr>";
                    text += "<tr><td><b>Total by Month</b></td>";
                    for (int j2 = 0; j2 < dt.Rows.Count; j2++)
                    {
                        int ch1 = 0;
                        
                        ch1 = 1;
                        double list1 = (from r in list
                                        where r.Period.Value.Month == Convert.ToDateTime(dt.Rows[j2]["orgDate"]).Month
                                        && r.Period.Value.Year == Convert.ToDateTime(dt.Rows[j2]["orgDate"]).Year
                                        select r).Sum(n => n.Achievement.Value);
                        text += "<td align='right'><b>" + string.Format("{0:f2}", list1) + "</b></td>";
                       
                        if (ch1 == 0)
                        {
                            text += "<td>&nbsp;</td>";
                        }
                    }
                }
                text += "</tr>";
                text += "</table>";
            }
            else
            {
                text = "No Records Found";
            }



           
            ltlSalesForecast.Text = text;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    

   
    #endregion
    #region "Return Months"
   
    private ArrayList GetMonths(int customerID,DateTime fromDate,DateTime toDate)
    {
        ArrayList ary = null;
        try
        {
            CustomerContractdbDataContext customer = new CustomerContractdbDataContext();

            //  ArrayList qtr = Deffinity.Utility.QtrlyDiff(fromDate, toDate);
            var paymentTerm = (from r in customer.Customer_Contracts
                               where r.CustomerID == customerID
                               select r).ToList().FirstOrDefault();
            if (paymentTerm != null)
            {
                //if (paymentTerm.PaymentTerms == "30 Days")
                //{
                //    ary = MonthlyDiff(fromDate, toDate);
                //}
                //if (paymentTerm..PaymentTerms == "90 Days")
                //{
                //    ary = QtrlyDiff(fromDate, toDate);
                //}
                //else if (paymentTerm.PaymentTerms == "60 Days")
                //{
                //    ary = SixtyDays(fromDate, toDate);
                //}
                //else
                //{
                    ary = MonthlyDiff(fromDate, toDate);
              //  }
            }
            else
            {
                ary = MonthlyDiff(fromDate, toDate);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ary;

    }


    private ArrayList GetExpanditure(int customerID, DateTime fromDate, DateTime toDate,int ProjectRef)
    {
        ArrayList ary = null;
        try
        {
            CustomerContractdbDataContext customer = new CustomerContractdbDataContext();

            //  ArrayList qtr = Deffinity.Utility.QtrlyDiff(fromDate, toDate);
            var paymentTerm = (from r in customer.Customer_Contracts
                               where r.CustomerID == customerID
                               select r).ToList().FirstOrDefault();
            if (paymentTerm != null)
            {
                //if (paymentTerm.PaymentTerms == "30 Days")
                //{
                //    ary = MonthlyDiff(fromDate, toDate);
                //}
                //if (paymentTerm.PaymentTerms == "90 Days")
                //{
                //    ary = QtrlyExpd(fromDate, toDate, ProjectRef);
                //}
                //else if (paymentTerm.PaymentTerms == "60 Days")
                //{
                //    ary = SixtyDaysExpand(fromDate,toDate, ProjectRef);
                //}
                //else
                //{
                    ary = MonthlyExpand(fromDate, toDate, ProjectRef);
               // }
            }
            else
            {
                ary = MonthlyExpand(fromDate, toDate, ProjectRef);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ary;

    }
    private  int MonthDifference(DateTime fromDate, DateTime toDate)
    {
       
        int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
        return M;
    }

    private ArrayList MonthlyDiff(DateTime fromDate, DateTime toDate)
    {
        ArrayList dates = new ArrayList(100);
        try
        {
            
            //DateTime[] MyArray = new DateTime[6];
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            if (M > 1)
            {
                //dates.Add(toDate.ToShortDateString());
                while (fromDate.Date < toDate.Date)
                {

                    dates.Add(fromDate.ToShortDateString());
                    fromDate = fromDate.AddMonths(1);
                }
                // dates.Add(toDate.ToShortDateString());
            }
            else
            {
                dates.Add(toDate.ToShortDateString());
            }
            //if (fromDate.Date >= toDate.Date)
            //{
            //    dates.Add(toDate.ToShortDateString());
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dates;
    }
    private ArrayList QtrlyDiff(DateTime fromDate, DateTime toDate)
    {
        ArrayList dates = new ArrayList(100);
        try
        {
            //DateTime[] MyArray = new DateTime[6];
            DateTime dateT = fromDate.AddMonths(3);
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            while (M > 3)
            {
                if (M < 2)
                {
                    dates.Add(toDate.ToShortDateString());
                }
                else
                {

                    fromDate = fromDate.AddMonths(3);
                    dates.Add(fromDate.ToShortDateString());
                }
                M = M - 3;
            }
            if (M <= 3)
            {
                dates.Add(toDate.ToShortDateString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dates;
    }

    private double GetExpanditure(DateTime fromdate,DateTime toDate, int projectRef)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        double total = 0;

        var projetsList = (from r in projectDB.ProjectDetails
                           where r.ProjectReference == projectRef
                           select new {r.ProjectEndDate }).ToList().FirstOrDefault();
        DateTime dtStart = new DateTime(fromdate.Year, fromdate.Month, 1);
        DateTime dtEnd = new DateTime(toDate.Year, toDate.Month, 1).AddMonths(1).AddDays(-1);// new DateTime(toDate.Year, toDate.Month, 1);
        if (projetsList.ProjectEndDate.Value < dtEnd)
        {
            dtEnd = projetsList.ProjectEndDate.Value;
        }
       
        var getTotal = (from r in projectDB.ForecastExpenditures
                        where r.DueDate >= dtStart && r.DueDate <= dtEnd
                        //&& (r.DueDate.Value.Year >= toDate.Year && r.DueDate.Value.Year <= toDate.Year)
                        && r.ProjectReference == projectRef
                        select r).ToList();
        if (getTotal != null)
        {
            total = (from r in getTotal
                     where r.DueDate >= dtStart && r.DueDate <= dtEnd
                       && r.ProjectReference == projectRef
                     select r).Sum(n => n.forecastspend.Value);
        }
        if (total != null)
        {
            return total;
        }
        else
        {
            return 0;
        }

    }
    private double GetExpanditureMonthly(DateTime fromdate, DateTime toDate, int projectRef)
    {
        projectTaskDataContext projectDB = new projectTaskDataContext();
        double total = 0;

        var projetsList = (from r in projectDB.ProjectDetails
                           where r.ProjectReference == projectRef
                           select new {r.ProjectEndDate }).ToList().FirstOrDefault();
        DateTime dtStart = new DateTime(fromdate.Year, fromdate.Month, 1);
        DateTime dtEnd = new DateTime(toDate.Year, toDate.Month, 1).AddMonths(1).AddDays(-1);// new DateTime(toDate.Year, toDate.Month, 1);
        if (projetsList.ProjectEndDate.Value < dtEnd)
        {
            dtEnd = projetsList.ProjectEndDate.Value;
        }
       
        var getTotal = (from r in projectDB.ForecastExpenditures
                        where r.DueDate.Value.Month == dtStart.Month && r.DueDate.Value.Year ==dtStart.Year
                        //&& (r.DueDate.Value.Year >= toDate.Year && r.DueDate.Value.Year <= toDate.Year)
                        && r.ProjectReference == projectRef
                        select r).ToList();
        if (getTotal != null)
        {
            total = (from r in getTotal
                     where r.DueDate.Value.Month == dtStart.Month && r.DueDate.Value.Year == dtStart.Year
                       && r.ProjectReference == projectRef
                     select r).Sum(n => n.forecastspend.Value);
        }
        if (total != null)
        {
            return total;
        }
        else
        {
            return 0;
        }

    }
    private ArrayList QtrlyExpd(DateTime fromDate, DateTime toDate,int ProjectRef)
    {
        ArrayList qtyExpands = new ArrayList(100);
        try
        {
            //DateTime[] MyArray = new DateTime[6];
            DateTime dateT = fromDate.AddMonths(3);
            DateTime fromDate1 = fromDate;
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            while (M > 3)
            {
                if (M < 2)
                {
                    //dates.Add(toDate.ToShortDateString());
                    qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1.ToShortDateString()), Convert.ToDateTime(toDate.ToShortDateString()), ProjectRef));
                }
                else
                {

                   
                    qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1), Convert.ToDateTime(fromDate1.AddMonths(3).ToShortDateString()), ProjectRef));
                    fromDate1 = fromDate.AddMonths(3);
                    //dates.Add(fromDate.ToShortDateString());
                }
                M = M - 3;
            }
            if (M <= 3)
            {
                qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1.AddMonths(1)), Convert.ToDateTime(toDate.ToShortDateString()), ProjectRef));
                //dates.Add(toDate.ToShortDateString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return qtyExpands;
    }

    private ArrayList SixtyDays(DateTime fromDate, DateTime toDate)
    {
        ArrayList dates = new ArrayList(100);
        try
        {
            //DateTime[] MyArray = new DateTime[6];
            DateTime dateT = fromDate.AddMonths(3);
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            while (M > 2)
            {
                if (M < 3 && M > 1)
                {
                    dates.Add(toDate.ToShortDateString());
                }
                else
                {
                    fromDate = fromDate.AddMonths(2);
                    dates.Add(fromDate.ToShortDateString());
                }
                M = M - 2;
            }
            if (M <= 2)
            {
                dates.Add(toDate.ToShortDateString());
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return dates;
    }
    private ArrayList MonthlyExpand(DateTime fromDate, DateTime toDate, int ProjectRef)
    {
        ArrayList qtyExpands = new ArrayList(100);
        try
        {

            //DateTime[] MyArray = new DateTime[6];
            DateTime fromDate1 = fromDate;
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            if (M > 1)
            {
                //dates.Add(toDate.ToShortDateString());
                while (fromDate1.Date < toDate.Date)
                {
                    qtyExpands.Add(GetExpanditureMonthly(Convert.ToDateTime(fromDate1.ToShortDateString()), Convert.ToDateTime(fromDate1.AddMonths(1).ToShortDateString()), ProjectRef));
                    //dates.Add(fromDate.ToShortDateString());
                    fromDate1 = fromDate1.AddMonths(1);
                }
                // dates.Add(toDate.ToShortDateString());
            }
            else
            {
                qtyExpands.Add(GetExpanditureMonthly(Convert.ToDateTime(fromDate1.AddMonths(1).ToShortDateString()), Convert.ToDateTime(toDate.ToShortDateString()), ProjectRef));
                ////dates.Add(toDate.ToShortDateString());
            }
            //if (fromDate.Date >= toDate.Date)
            //{
            //    dates.Add(toDate.ToShortDateString());
            //}
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return qtyExpands;
    }
    private ArrayList SixtyDaysExpand(DateTime fromDate, DateTime toDate,int ProjectRef)
    {
        ArrayList qtyExpands = new ArrayList(100);
        ArrayList dates = new ArrayList(100);
        try
        {
            //DateTime[] MyArray = new DateTime[6];
            DateTime dateT = fromDate.AddMonths(3);
            DateTime fromDate1 = fromDate;
            int M = System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(fromDate, toDate);
            while (M > 2)
            {
                if (M < 3 && M > 1)
                {
                   // dates.Add(toDate.ToShortDateString());
                    qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1.ToShortDateString()), Convert.ToDateTime(toDate.ToShortDateString()), ProjectRef));
                }
                else
                {
                    qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1), Convert.ToDateTime(fromDate1.AddMonths(2).ToShortDateString()), ProjectRef));
                    fromDate1 = fromDate1.AddMonths(2);
                }
                M = M - 2;
            }
            if (M <= 2)
            {
                qtyExpands.Add(GetExpanditure(Convert.ToDateTime(fromDate1.AddMonths(1)), Convert.ToDateTime(toDate.ToShortDateString()), ProjectRef));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return qtyExpands;
    }



    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SalesForeCast();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
