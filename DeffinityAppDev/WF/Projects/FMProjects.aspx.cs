using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Resources.Editor;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Shared.Events;
using Microsoft.ApplicationBlocks.Data;
public partial class CustomerProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //   Master.PageHead 
       // Master.PageHead = "Project Tracker";
        if (!IsPostBack)
        {
            try
            {
                UltraChart1.Visible = false;
                UltraChart2.Visible = false;
                DefaultBindings();
                BindGrid();
                BindChart();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    private void DefaultBindings()
    {
        BindCustomers();
        BindStatus();
        BindProjOwner();
        PONumbers();
        BindProgramme();
    }
    #region dropdownbingins
    private void PONumbers()
    {
        PurchaseOrderMgtDataContext POMgt = new PurchaseOrderMgtDataContext();
        try
        {

            var poNumbers = from r in POMgt.Customer_PODatabases
                            where r.PONumber!=""
                            orderby r.PONumber
                            select r;
            ddlPO.DataSource = poNumbers;
            ddlPO.DataTextField = "PONumber";
            ddlPO.DataValueField = "PONumber";
            ddlPO.DataBind();
            ddlPO.Items.Insert(0, new ListItem("Please select...", "0"));
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
            ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindStatus()
    {
        try
        {
           projectTaskDataContext prj = new projectTaskDataContext();
            var prjstatus = from p in prj.ProjectStatus
                            orderby p.Status
                            select p;
            ddlStatus.DataSource = prjstatus;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = "9";
            ddlStatus.Items.Insert(0, new ListItem("Please select...", "0"));


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindProjOwner()
    {
        try
        {
            UserDataContext UDContext = new UserDataContext();
            var Owner = from c in UDContext.Contractors
                        where c.SID <=3
                        orderby c.ContractorName
                        select c;
            ddlPOwner.DataSource = Owner;
            ddlPOwner.DataTextField = "ContractorName";
            ddlPOwner.DataValueField = "ID";
            ddlPOwner.DataBind();
            ddlPOwner.Items.Insert(0, new ListItem("Please select...", "0"));
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
                        where  c.Level==1
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
    #endregion
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            BindChart();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string RetUrl()
    {
        return "ExternalExpenseUpload.aspx";
    }
    #region Bind Grid and chart

    private void BindGrid()
    {
        projectTaskDataContext projets = new projectTaskDataContext();
       
        string dateFrom = txtFromDate.Text;
        string dateTo = txtToDate.Text;
        if (txtFromDate.Text == "")
        {

            var from = (from r in projets.ProjectDetails
                        select r.ProjectEndDate).Min();
            if (from != null)
            {
                dateFrom = Convert.ToDateTime(from.Value).ToShortDateString();
            }
            //else
            //{
            //    fromdate = Convert.ToDateTime(txtFromDate.Text);
            //}
        }
        if (txtToDate.Text == "")
        {

            var to = (from r in projets.ProjectDetails
                      select r.ProjectEndDate).Max();
            if (to != null)
            {
                dateTo = Convert.ToDateTime(to.Value).ToShortDateString();
            }
            //else
            //{ProjectDetails
            //    todate = Convert.ToDateTime(txtToDate.Text);
            //}
        }

        var res = projets.ExecuteQuery<ProjectMgt.Entity.ProjectDetails>(Query(int.Parse(ddlCustomer.SelectedValue),
            ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
            Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue)?"0":ddlSubprogramme.SelectedValue)));
        grdProjets.DataSource = res.ToList();
        grdProjets.DataBind();


        //var res1 = projets.ExecuteQuery<ProjectDetails>(QueryforPO(int.Parse(ddlCustomer.SelectedValue),
        //  ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
        //  Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue))).ToList().FirstOrDefault();
        //lblCount.Visible = true;
        //lblValue.Visible = true;
        //lblCount.Text = "Number Of Projects Without a PO: " + res1.SiteID.ToString();
        //lblValue.Text = "Value Of Projects Without a PO: " + res1.BudgetaryCost.ToString();
    }
    private void BindChart()
    {
        try
        {
            projectTaskDataContext projets = new projectTaskDataContext();
            string dateFrom = txtFromDate.Text;
            string dateTo = txtToDate.Text;
            if (txtFromDate.Text == "")
            {

                var from = (from r in projets.ProjectDetails
                            select r.ProjectEndDate).Min();
                if (from != null)
                {
                    dateFrom = Convert.ToDateTime(from.Value).ToShortDateString();
                }
                //else
                //{
                //    fromdate = Convert.ToDateTime(txtFromDate.Text);
                //}
            }
            if (txtToDate.Text == "")
            {

                var to = (from r in projets.ProjectDetails
                          select r.ProjectEndDate).Max();
                if (to != null)
                {
                    dateTo = Convert.ToDateTime(to.Value).ToShortDateString();
                }
                //else
                //{
                //    todate = Convert.ToDateTime(txtToDate.Text);
                //}
            }


            UltraChart1.Visible = true;
            UltraChart1.Axis.Y.Labels.Visible = true;
            UltraChart1.Axis.X.Labels.Visible = false;
            //UltraChart1.Axis.X.Extent = 100;

            UltraChart1.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;

            UltraChart2.Visible = true;
            UltraChart2.Axis.Y.Labels.Visible = true;
            UltraChart2.Axis.X.Labels.Visible = false;
            UltraChart2.Axis.X.Extent = 100;

            UltraChart2.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
            string site = "0";
            if (ddlSite.SelectedValue == "")
            {
                site = "0";
            }
            else
            {
                site = ddlSite.SelectedValue.ToString();
            }
            string project = "0";
            if (ddlProject.SelectedValue == "")
            {
                project = "0";
            }
            else
            {
                project = ddlProject.SelectedValue.ToString();
            }

            var resChart1 = projets.ExecuteQuery<ProjectDetails>(Queryfortest(int.Parse(ddlCustomer.SelectedValue),
                   ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
                    Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue),
                    int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue))).ToList();
            var resChartcnt = projets.ExecuteQuery<ProjectDetails>(Queryfortest(int.Parse(ddlCustomer.SelectedValue),
                  ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
                  Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue),
                  int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue))).ToList();
            var rs = (from r in resChart1
                      orderby r.ProjectReferenceWithPrefix
                      select new { Budget=r.BudgetaryCost,Actual=r.ActualCost, r.ProjectReferenceWithPrefix }).ToList();


            var resChart2 = projets.ExecuteQuery<ProjectDetails>(QueryforSecondChart(int.Parse(ddlCustomer.SelectedValue),
                    ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
                    Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue),
                    int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue))).ToList();
            var resChartcnt2 = projets.ExecuteQuery<ProjectDetails>(QueryforSecondChart(int.Parse(ddlCustomer.SelectedValue),
                        ddlSite.SelectedValue, ddlProject.SelectedValue, int.Parse(ddlStatus.SelectedValue), int.Parse(ddlPOwner.SelectedValue),
                       Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), ddlPO.SelectedValue, int.Parse(ddlProgramme.SelectedValue),
                       int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue))).ToList();
            var rs2 = (from r in resChart2
                       orderby r.ProjectReferenceWithPrefix
                       select new { Invoiced = r.BudgetaryCost, Actual=r.ActualCost, Outstanding = r.BuyingPrice, r.ProjectReferenceWithPrefix }).ToList();

           
            if (resChartcnt.ToList().Count == 0)
            {

                UltraChart1.Visible = false;
                UltraChart1.EmptyChartText = string.Empty;

            }
            else
            {
                UltraChart1.Visible = true;
                UltraChart1.DataSource = rs;
                // UltraChart1.DataSource = dt;
                UltraChart1.DataBind();
            }
            if (resChartcnt2.ToList().Count == 0)
            {

                UltraChart2.Visible = false;
                UltraChart2.EmptyChartText = string.Empty;

            }
            else
            {
                UltraChart2.Visible = true;
                UltraChart2.DataSource = rs2;
                UltraChart2.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
#endregion

    #region "Query"

    private string  Query(int CustomerID,string siteID,string ProjetcRef,int Status,
        int OwnerID,DateTime fromDate,DateTime ToDate,string PONumber,int Programme,int subprogramme)
    {

        //some column names are used from project Table to accomplish proper mapping of table columns to properties on  object.
        string sql = "";
        sql = "select distinct P.ID,P.ProjectReferenceWithPrefix,P.BuyingPrice as AccrualsPriorFinancial,P.ProjectReference,P.ProjectTitle,P.BudgetaryCost,"
  + " isnull((select sum(DeviationValue) from DeviationReport where ProjectReference = P.ProjectReference),0) as GrossProfit,isnull((P.BudgetaryCost-P.BuyingPrice)/NULLIF(P.BudgetaryCost, 0),0)* 100 as ProjectForecast, "
  + " isnull((select isnull(SUM(Total),0) from v_ProjectBOM where ProjectReference=P.ProjectReference),0) as BudgetaryCostLevel3 ,isnull(((P.ActualCost/NULLIF(P.BudgetaryCostLevel3, 0))*100),0) as ProjectCostForecast, "
  + " isnull((select CAST(sum(G.QtyReceived) AS float) from ProjectBOM Pb,GoodsReceipt G where Pb.ID=G.BOMID and Pb.ProjectReference=P.ProjectReference),0) as BuyingPrice, "
  +" isnull((select sum(claimedTotal) from ProjectValuationItems P1 where P1.ProjectValuationID in "
    +"  (select ValuationID from ProjectValuations where ProjectReference=P.ProjectReference and InvoiceStatus=1)),0)as ActualCost  "
+ " ,  isnull((select sum(value)from Customer_PODatabase where PONumber =P.CustomerReference),0) As AccrualsCurrentFinancial,   "
 + " P.CustomerReference As PONumber   from V_ProjectDetails P where  " +
 "  P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '" 
 + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }
        if (siteID != "")
        {
            sql += "  and P.SiteID=" + siteID;
        }
        if (ProjetcRef !="")
        {
            sql += "  and P.ProjectReference=" + ProjetcRef;
        }
        if (Status != 0)
        {
            sql += "  and P.ProjectStatusID=" + Status.ToString();
        }
        if (OwnerID != 0)
        {
            sql += "  and P.OwnerID=" + OwnerID.ToString();
        }
        if (PONumber !="0")
        {
            sql += "  and P.CustomerReference='" + PONumber+"'";
        }
        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (subprogramme != 0)
        {
            sql += "  and P.SubProgramme=" + subprogramme.ToString();
        }
        sql += "ORDER BY P.ID DESC";
        return sql;
    }


    private string QueryforPO(int CustomerID, string siteID, string ProjetcRef, int Status,
        int OwnerID, DateTime fromDate, DateTime ToDate, string PONumber, int Programme,int subprogramme)
    {

        //some column names are used from project Table to accomplish proper mapping of table columns to properties on  object.
        string sql = "";
        sql = "select distinct  COUNT(*) as siteID,isnull(sum(P.BudgetaryCost),0) as BudgetaryCost  from V_ProjectDetails P,Customer_PODatabase c   " +
"  where P.CustomerReference<>c.PONumber " +
 "  and P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
 + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }
        if (siteID != "")
        {
            sql += "  and P.SiteID=" + siteID;
        }
        if (ProjetcRef != "")
        {
            sql += "  and P.ProjectReference=" + ProjetcRef;
        }
        if (Status != 0)
        {
            sql += "  and P.ProjectStatusID=" + Status.ToString();
        }
        if (OwnerID != 0)
        {
            sql += "  and P.OwnerID=" + OwnerID.ToString();
        }
        if (PONumber !="0")
        {
            sql += "  and P.CustomerReference='" + PONumber + "'";
        }
        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (subprogramme != 0)
        {
            sql += "  and P.SubProgramme=" + subprogramme.ToString();
        }

        return sql;
    }


    private string Queryfortest(int CustomerID, string siteID, string ProjetcRef, int Status,
        int OwnerID, DateTime fromDate, DateTime ToDate, string PONumber, int Programme,int subprogramme)
    {

        //some column names are used from project Table to accomplish proper mapping of table columns to properties on  object.
        string sql = "";
        sql = "select distinct top 6 P.BudgetaryCost as BudgetaryCost ,P.ActualCost as ActualCost,"+
            "  P.ProjectReferenceWithPrefix   " +

    "from V_ProjectDetails P where " +


 "   P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
 + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }
        if (siteID != "")
        {
            sql += "  and P.SiteID=" + siteID;
        }
        if (ProjetcRef != "")
        {
            sql += "  and P.ProjectReference=" + ProjetcRef;
        }
        if (Status != 0)
        {
            sql += "  and P.ProjectStatusID=" + Status.ToString();
        }
        if (OwnerID != 0)
        {
            sql += "  and P.OwnerID=" + OwnerID.ToString();
        }
        if (PONumber != "0")
        {
            sql += "  and P.CustomerReference='" + PONumber + "'";
        }
        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (subprogramme != 0)
        {
            sql += "  and P.SubProgramme=" + subprogramme.ToString();
        }

        return sql;
    }


    private string QueryforSecondChart(int CustomerID, string siteID, string ProjetcRef, int Status,
        int OwnerID, DateTime fromDate, DateTime ToDate, string PONumber, int Programme,int subprogramme)
    {

        //some column names are used from project Table to accomplish proper mapping of table columns to properties on  object.
        string sql = "";
        sql = "select distinct top 5  ActualCost,ProjectReferenceWithPrefix, isnull((select sum(claimedTotal) from ProjectValuationItems P1 "+  
" where P1.ProjectValuationID in  (select ValuationID from ProjectValuations where ProjectReference=P.ProjectReference and InvoiceStatus=1)),0) as BudgetaryCost, "+ 
 " isnull((select sum(claimedTotal) from ProjectValuationItems P1 where P1.ProjectValuationID in  (select ValuationID from ProjectValuations where ProjectReference=P.ProjectReference and InvoiceStatus in (2,3))),0) as BuyingPrice from "+

 "  V_ProjectDetails P  " +



 "  where  P.ProjectEndDate  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '"
 + string.Format("{0:MM/dd/yyyy}", ToDate) + "'";


        if (CustomerID != 0)
        {
            sql += "  and P.Portfolio=" + CustomerID.ToString();
        }
        if (siteID != "")
        {
            sql += "  and P.SiteID=" + siteID;
        }
        if (ProjetcRef != "")
        {
            sql += "  and P.ProjectReference=" + ProjetcRef;
        }
        if (Status != 0)
        {
            sql += "  and P.ProjectStatusID=" + Status.ToString();
        }
        if (OwnerID != 0)
        {
            sql += "  and P.OwnerID=" + OwnerID.ToString();
        }
        if (PONumber != "0")
        {
            sql += "  and P.CustomerReference='" + PONumber + "'";
        }
        if (Programme != 0)
        {
            sql += "  and P.OwnerGroupID=" + Programme.ToString();
        }
        if (subprogramme != 0)
        {
            sql += "  and P.SubProgramme=" + subprogramme.ToString();
        }

        return sql;
    }
    #endregion
    protected void grdProjets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdProjets.PageIndex = e.NewPageIndex;
            BindGrid();
            BindChart();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Expense/DownloadFile");
            string fileName = "External_Expense_Template_" + DateTime.Now;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "External_Expense_Template.xlsx");
            if (fileInfo.Exists)
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
