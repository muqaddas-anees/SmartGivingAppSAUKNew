using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
public partial class FMWorkInProgress : System.Web.UI.Page
{
    decimal contractAmt = 0;
    decimal currentTotal = 0;
    decimal costToDate = 0;
    decimal billToDate = 0;
    decimal costToComplete = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Project Tracker";
                BindWIP();
                // grdInvoiceRaised.Focus();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region BindWorkinProgress
    private void BindWIP()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
            "ProjectWIP_FM").Tables[0];
        grdLiveProjects.DataSource = dt;
        grdLiveProjects.DataBind();
    }
    private DataTable BindWIPSort()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
            "ProjectWIP_FM").Tables[0];
        grdLiveProjects.DataSource = dt;
        grdLiveProjects.DataBind();
        return dt;
    }
    #endregion
    protected void grdLiveProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BudgetaryCostLevel3"));
                contractAmt = contractAmt + rowTotal;
                decimal buyingPrice = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BuyingPrice"));
                currentTotal = currentTotal + buyingPrice;
                decimal costToDateg = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ActualCost"));
                  costToDate = costToDate + costToDateg;
                  decimal billToDateg = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "invoice"));
                  billToDate = billToDate + billToDateg;
                  decimal costCompg = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "costComp"));
                  costToComplete = costToComplete + costCompg;
             
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblVariationsf = (Label)e.Row.FindControl("lblVariationsf");
                lblVariationsf.Text = string.Format("{0:#.00}", contractAmt);

                Label lblestimateTotalcostf = (Label)e.Row.FindControl("lblestimateTotalcostf");
                lblestimateTotalcostf.Text = string.Format("{0:#.00}", currentTotal);

                Label lblCostToDatef = (Label)e.Row.FindControl("lblCostToDatef");
                lblCostToDatef.Text = string.Format("{0:#.00}", costToDate);

                Label lblBillToDatef = (Label)e.Row.FindControl("lblBillToDatef");
                lblBillToDatef.Text = string.Format("{0:#.00}", billToDate);


                Label lblCosttoCompletef = (Label)e.Row.FindControl("lblCosttoCompletef");
                lblCosttoCompletef.Text = string.Format("{0:#.00}", costToComplete);
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected string UnderCal(string F, string D)
    {
        decimal val = 0;
        val = Convert.ToDecimal(F) - Convert.ToDecimal(D);

        return string.Format("{0:f2}", val);

    }

    protected string CalER(string C, string E, string D)
    {
        decimal val = 0;
        val = (Convert.ToDecimal(C) * Convert.ToDecimal(E)) * (1 / Convert.ToDecimal(D));
        return string.Format("{0:f2}", val);
    }

    protected bool visibleRedIcon(string val)
    {
        bool visibleRed = false;
        if (Convert.ToDecimal(val) >= 100)
        {
            visibleRed = true;
        }
        return visibleRed;
    }
    protected bool visibleAmberIcon(string val)
    {
        bool visibleAmber = false;
        if (Convert.ToDecimal(val) > 80 && Convert.ToDecimal(val) < 100)
        {
            visibleAmber = true;
        }
        return visibleAmber;
    }
    protected void grdLiveProjects_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        //get the pageindex of the grid.
        int pageIndex = grdLiveProjects.PageIndex;
        grdLiveProjects.DataSource = SortDataTable(BindWIPSort(), false);
        grdLiveProjects.DataBind();
        grdLiveProjects.PageIndex = pageIndex;

    }
    private void Sorting(string item)
    {
        grdLiveProjects.DataSource = SortDataTable(BindWIPSort(), false);
       
        grdLiveProjects.DataBind();
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    /// Gets or Sets the gridview sortexpression property
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    /// Returns the direction of the sorting
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
    protected void grdLiveProjects_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "sort")
        //{
        //    GridViewSortExpression = e.CommandArgument.ToString();
        //    //get the pageindex of the grid.
        //    int pageIndex = grdLiveProjects.PageIndex;
        //    grdLiveProjects.DataSource = SortDataTable(BindWIPSort(), false);
        //    grdLiveProjects.DataBind();
        //    grdLiveProjects.PageIndex = pageIndex;
        //}
    }


}
