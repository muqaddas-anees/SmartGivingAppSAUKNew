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
using POMgt.DAL;
using POMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class CustomerResources : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Master.PageHead = "Finance Module";
            if (!IsPostBack)
            {
                div1.Visible = true;
                div2.Visible = true;
                DefaultBindings();
                BindToRepeater(int.Parse(ddlResource.SelectedValue));
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #region dropdownbindings
    private void DefaultBindings()
    {
        BindCustomers();
        BindResource();
        PONumbers();
    }
    private void BindResource()
    {
        try
        {
            UserDataContext user = new UserDataContext();
            var resource = from res in user.Contractors
                           where res.Status == "Active"
                           orderby res.ContractorName
                           select res;
            ddlResource.DataSource = resource;
            ddlResource.DataTextField = "ContractorName";
            ddlResource.DataValueField = "ID";
            ddlResource.DataBind();
            ddlResource.Items.Insert(0, new ListItem ("ALL...","0"));

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
    private void PONumbers()
    {
        PurchaseOrderMgtDataContext POMgt = new PurchaseOrderMgtDataContext();
        try
        {

            var poNumbers=from r in POMgt.Customer_PODatabases
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


    #endregion

    public string ChangeHoues(string GetHours)
    {

        string GetActivity = "";
        try
        {
            char[] comm1 = { '.' };
            string[] displayTime = GetHours.Split(comm1);


            GetActivity = displayTime[0] + ":" + displayTime[1];


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return GetActivity;
    }
    protected void imgbtnView_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            div2.Visible = true;
            BindToRepeater(int.Parse(ddlResource.SelectedValue));
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    private void BindGrid()
    {
        TimeSheetDataContext timesheet = new TimeSheetDataContext();
        //DateTime todate = Convert.ToDateTime("20/12/2010");
        //DateTime fromdate = Convert.ToDateTime("05/11/2009");
        string dateFrom = txtFromDate.Text;
        string dateTo = txtToDate.Text;
        if (txtFromDate.Text == "")
        {

            var from = (from r in timesheet.TimesheetEntries
                        select r.DateEntered).Min();
            if (from != null)
            {
                dateFrom = Convert.ToDateTime(from.Value).ToShortDateString();
            }
            //else
            //{
            //    txtFromDate.Text = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
            //}
        }
        if (txtToDate.Text == "")
        {

            var to = (from r in timesheet.TimesheetEntries
                      select r.DateEntered).Max();
            if (to != null)
            {
                dateTo = Convert.ToDateTime(to.Value).ToShortDateString();
            }
            //else
            //{
            //    todate = Convert.ToDateTime(txtToDate.Text);
            //}
        }
        var res = timesheet.ExecuteQuery<V_TimesheetDetail>(Query(int.Parse(ddlResource.SelectedValue),
            Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), int.Parse(RadStatus.SelectedValue), int.Parse(ddlCustomer.SelectedValue), ddlSite.SelectedValue,
            ddlProject.SelectedValue, ddlTask.SelectedValue, ddlPO.SelectedValue));
        grdTimeSheet.DataSource = res.ToList();
        grdTimeSheet.DataBind();
    }

    #region Query

    private string  Query(int ResourceID,DateTime fromDate,DateTime toDate,int Status,int CustomerID,string Site,
        string ProjectRef, string TaskID, string PONumber)
    {


        string sql = "";
        sql = "select distinct ID,ProjectReference,DateEntered,ProjectTitle,itemdescription,EntryTypeName," +
        " ApproveTypeName ,Hours,SubmittedDate, " +
        "isnull((select c.ContractorName from Contractors c where c.ID =(select ApprovedBY from TimesheetEntry te where te.ID=V_TimesheetDetails.ID)),'')as ApproverName" +
        " from V_TimesheetDetails " +
  "  where  DateEntered  BETWEEN  '" + string.Format("{0:MM/dd/yyyy}", fromDate) + "' AND   '" + string.Format("{0:MM/dd/yyyy}", toDate) + "'";

        if (CustomerID != 0)
        {
            sql += "  AND Portfolio=" + CustomerID;
        }

        if (ResourceID != 0)
        {
            sql += " AND ContractorID=" + ResourceID;
        }
        if (Status !=0)
        {
            sql += " AND  TimeSheetStatusID=" + Status;
        }
        if (Site != "")
        {
            sql += " AND  SiteID=" + Site;
        }
        if (ProjectRef != "")
        {
            sql += " AND  ProjectReference=" + ProjectRef;
        }
        if (PONumber !="0")
        {
            sql += "  and CustomerReference='" + PONumber+"'";
        }
        if (TaskID != "")
        {
            sql += "  AND ProjectTaskID=" + TaskID;
        }
        sql += "   order by DateEntered desc";
        return sql;


    }
    private void BindToRepeater(int UserID)
    {
         UserDataContext user = new UserDataContext();

         if (UserID != 0)
         {
             var resource = from res in user.Contractors
                            where res.Status == "Active" && res.ID == UserID
                            orderby res.ContractorName
                            select new { res.ID, res.ContractorName };
             rptrAllowence.DataSource = resource;
             rptrAllowence.DataBind();
         }
         else
         {
             var resource1 = from res in user.Contractors
                            where res.Status == "Active" 
                            orderby res.ContractorName
                             select new { res.ID, res.ContractorName };

             rptrAllowence.DataSource = resource1;
             rptrAllowence.DataBind();
         }
    }

    #endregion
    protected void rptrAllowence_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Contractor de = (Contractor)e.Item.DataItem;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            //LinkButton lnkBtn = (LinkButton)e.Item.FindControl("lnkBtn");
            //Panel pnlHide = (Panel)e.Item.FindControl("pnlHide");
           // lnkBtn.Attributes.Add("onclick", "showHide('" + pnlHide + "')");
            //onclick="showHide('<%=Panel1.ClientID%>');
            
            //Label lblID = (Label)e.Item.FindControl("lblID"); 
            //var gv = (GridView)e.Item.FindControl("grdAllowence");
            //gv.DataSource = SummaryBinding(int.Parse(lblID.Text));

            //    gv.DataBind();
            

        }
    }
    private DataTable SummaryBinding(int ID)
    {
        DataTable Dt_allowance=new DataTable();
        try
        {

            Dt_allowance = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "VT.RequestSummaryByResource_Mod", new SqlParameter("ResourceID", ID)).Tables[0];

            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Dt_allowance;
    }
    protected void grdTimeSheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdTimeSheet.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnRest_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            div2.Visible = true;
            DefaultBindings();
            ddlSite.SelectedIndex = 0;
            ddlSite.SelectedItem.Value = "0";
            BindToRepeater(int.Parse(ddlResource.SelectedValue));
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public string FormateDate(string value)
    {
        string mydate = "";
        if (value.Contains("01/01/1900"))
        {
            mydate = "";
        }
        else
        {
            mydate = value;
        }

        return mydate;

    }

   
}
