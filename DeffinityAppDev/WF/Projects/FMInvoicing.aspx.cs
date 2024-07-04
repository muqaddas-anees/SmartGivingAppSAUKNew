using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
public partial class CustomerInvoicing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (!IsPostBack)
        {
           //Master.PageHead = "Project Tracker";
           //div2.Visible = false;
           setProjectPrefix();
            BindCustomers();
            BindInvoice();
            BindInvoiceJournal(); 
            lblTitle.Focus();
           // grdInvoiceRaised.Focus();
        }
         }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void setProjectPrefix()
    {
        txtPrefix.Text = sessionKeys.Prefix;
        txtPrefix1.Text = sessionKeys.Prefix;
    }
    #region "Bind DropDown and Grid"
    private void BindCustomers()
    {
        PortfolioDataContext customer = new PortfolioDataContext();


        try
        {
            var portfolio = from r in customer.ProjectPortfolios
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

    private void BindInvoiceJournal() //2nd grid
    {
        
        try
        {
           
            lblFocus.Focus();
            grdInvoiceRaised.Visible = true;
            //lblTitle.Text = "";
            div1.Visible = true;
            lblTitle.Visible = true;
            lblTitle.Text = "Interim Invoices";//GetTitleForSecondGrid(ProjectRef);
            projectTaskDataContext invoice = new projectTaskDataContext();

            //List<ListItem> listStatus = RiseVal.LoadInvoiceStatus();
            //var list = RiseVal.LoadInvoiceStatus();
            var invoiceJouranl=(from r in invoice.ProjectValuations
                               // join s in list on r.InvoiceStatus equals int.Parse(s.Value)
                                join p in invoice.ProjectDetails on r.ProjectReference equals p.ProjectReference
                              
                                where p.ProjectStatusID==2 && r.InvoiceStatus==3
                               orderby r.ProjectReference
                                select new { r.InvoiceReference,r.DateRaised,r.Value,r.VATPercentage,
                                             SubTotal = ((r.VATPercentage * r.Value) / 100) + r.Value,
                                    p.ProjectReferenceWithPrefix,p.ProjectTitle ,r.ValuationID,
                                expectedate=r.DateRaised.Value.AddDays(30),r.ActualDate,r.Notes,r.ProjectReference,r.InvoiceStatus}).ToList();
            //&& s.Value.Contains("3") s.Text,
        //var invoiceJouranl=(from r in invoice.ProjectValuations
        //                    where r.ProjectReference==ProjectRef
        //                    select r).ToList();
            if (invoiceJouranl != null)
            {
                grdInvoiceRaised.DataSource = invoiceJouranl;
                grdInvoiceRaised.DataBind();
            }
     
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected string InvoiceStatus(string type)
    {
        string val = "";
        if (type == "3")
        {
            val = "Submitted";
        }
        return val;
    }
    private void  BindInvoice() //first grid
    {

        try
        {
            div2.Visible = true;
            div1.Visible = false;
            grdInvoiceRaised.Visible = false;
            projectTaskDataContext projects = new projectTaskDataContext();

            var res = projects.ExecuteQuery<ProjectDetails>(Query(int.Parse(ddlInvoiceStatus.SelectedValue),
               int.Parse(ddlCustomer.SelectedValue), ddlProject.SelectedValue, txtProjectRef.Text));

            if (res != null)
            {
                grdInvoice.DataSource = res.ToList();
                grdInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private string GetTitleForSecondGrid(int ProjectRef) //2nd grid
    {
        string titleWithRef = "";
        try
        {

            // lblTitle.Text=""+
            projectTaskDataContext invoice = new projectTaskDataContext();

            var title = (from r in invoice.ProjectDetails
                                  where r.ProjectReference == ProjectRef
                                  select new {r.ProjectTitle,r.ProjectReferenceWithPrefix}).ToList().FirstOrDefault();
            //titleWithRef = "Invoice Journal For Project  " + title.ProjectReferenceWithPrefix + ":" + title.ProjectTitle;
            titleWithRef = "Interim Invoices";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return titleWithRef;
    }

    #endregion
    #region Query

    private string Query( int Status, int CustomerID,string  ProjectRef,string ProjectRefWithPrefix)
    {


        string sql = "";
        sql = "select distinct P.ProjectReference,P.ProjectStatusName,P.ID,P.ProjectReferenceWithPrefix, P.BudgetaryCostLevel3,P.BuyingPrice,P.ProjectTitle,P.PortfolioName," +
        "  isnull((select sum(claimedTotal) from  ProjectValuationItems P1 where P1.ProjectValuationID in (select ValuationID from ProjectValuations where ProjectReference=P.ProjectReference and InvoiceStatus in (2,3))),0) as CurrentMonthAccrual ," +
 "  isnull(P.BudgetaryCostLevel3,0)-isnull((select sum(claimedTotal) from  ProjectValuationItems P1 where P1.ProjectValuationID in (select ValuationID from ProjectValuations where ProjectReference=P.ProjectReference and InvoiceStatus in (2,3))),0)as GrossProfit," +
 " P.OwnerName  from V_ProjectDetails P  where  ProjectStatusID in (6,3)  "; //where ProjectStatusID=9

        if (int.Parse(string.IsNullOrEmpty(ProjectRefWithPrefix)?"0":ProjectRefWithPrefix)!=0)
        {
            // sql += "   and   P.ProjectReferenceWithPrefix like '%" + ProjectRefWithPrefix + "%'";
            sql += " AND P.ProjectReference=" + ProjectRefWithPrefix.ToString();
        }
        else
        {
            if (CustomerID != 0)
            {
                sql += "   AND  P.Portfolio=" + CustomerID;
            }
            if (int.Parse(string.IsNullOrEmpty(ProjectRefWithPrefix) ? "0" : ProjectRefWithPrefix) != 0)
            {
                // sql += "   and   P.ProjectReferenceWithPrefix like '%" + ProjectRefWithPrefix + "%'";
                sql += " AND P.ProjectReference=" + ProjectRefWithPrefix.ToString();
            }
            if (ProjectRef != "")
            {
                sql += " AND P.ProjectReference=" + ProjectRef;
            }
        }
       
        //if (Status != 0)
        //{
        //    //sql += "  and PV.InvoiceStatus=" + Status;
        //}

        //if (ProjectRef != "" && CustomerID != 0)
        //{
        //    sql += " AND P.ProjectReference=" + ProjectRef;
        //}
        //if (ProjectRef != "" && CustomerID == 0)
        //{
        //    sql += " where  P.ProjectReference=" + ProjectRef;
        //}

        //if (ProjectRefWithPrefix != "" && CustomerID != 0 && ProjectRef != "")
        //{
        //    sql += "   and   P.ProjectReferenceWithPrefix like '%" + ProjectRefWithPrefix + "%'";
            
        //}
        //if (ProjectRefWithPrefix != "" && CustomerID!= 0 && ProjectRef == "")
        //{
        //    sql += "   and   P.ProjectReferenceWithPrefix like '%" + ProjectRefWithPrefix + "%'";
           
        //}
        //if (ProjectRefWithPrefix != "" && CustomerID == 0 && ProjectRef == "")
        //{
        //    sql += "  where   P.ProjectReferenceWithPrefix like '%" + ProjectRefWithPrefix + "%'";
          
        //}

        sql += "   order by P.ProjectReference";
        return sql;


    }
   

    #endregion
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        BindInvoice();
    }
    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
        //    Label lblValutionID = (Label)e.Row.FindControl("lblValutionID");
            
        //       // chkSelect.Attributes.Add("onclick", "MutExChkList(this)");
        //        ((CheckBox)e.Row.FindControl("chkSelect")).Attributes.Add("onclick", "javascript:MutExChkList('" + ((CheckBox)e.Row.FindControl("chkSelect")).ClientID + "')");
        //    if (chkSelect.Checked == true)
        //    {
        //       // BindInvoiceJournal(int.Parse(lblValutionID.Text));
        //    }

        //}
        try
        {
         if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                string strScript = "uncheckOthers(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).ClientID + ");";
                ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).Attributes.Add("onclick", strScript);                
            }            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }      
    }
    protected void UpdateSupplyLed(object sender, EventArgs e)
    {
        CheckBox chkSelect = sender as CheckBox;

        bool sample = chkSelect.Checked;

        GridViewRow row = chkSelect.NamingContainer as GridViewRow;
        Label lblValutionID = (Label)row.FindControl("lblValutionID");
        //TextBox txtId = row.FindControl("Id") as TextBox;
       
        //Label lblProjectReferenceWithPrefix = (Label)row.FindControl("lblProjectReferenceWithPrefix1");
        //Label lblProjectTitle = (Label)row.FindControl("lblProjectTitle1");
        //lblTitle.Text = "Invoice Journal For Project " + lblProjectReferenceWithPrefix.Text + ":" + lblProjectTitle.Text;
        lblTitle.Focus();
       
        int id = Int32.Parse(lblValutionID.Text);
        if (chkSelect.Checked == true)
        {

            hdnRef.Value = lblValutionID.Text;
            //BindInvoiceJournal(int.Parse(lblValutionID.Text));
        }
    }
    protected void grdInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         grdInvoice.PageIndex=e.NewPageIndex;
         BindInvoice();
    }
    protected void grdInvoiceRaised_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                projectTaskDataContext invoice = new projectTaskDataContext();
                //int index = grdInvoiceRaised.EditIndex;
                //GridViewRow Row = grdInvoiceRaised.Rows[index];
                //LinkButton lnkView = (LinkButton)Row.FindControl("lnkView");
                mdlPopViewInvoice.Show();
                var title = (from r in invoice.ProjectValuations
                             where r.ValuationID == (int.Parse(e.CommandArgument.ToString()))
                             select r).ToList().FirstOrDefault();

                lblRef.Text = "Invoice Reference " + title.InvoiceReference;
                var ProjectValuationItems = (from r in invoice.ProjectValuationItems
                                             where r.ProjectValuationID == (int.Parse(e.CommandArgument.ToString()))
                                             select r).ToList();

                grdViewInvoice.DataSource = ProjectValuationItems;
                grdViewInvoice.DataBind();


            }
            if (e.CommandName == "Update")
            {

                int index = grdInvoiceRaised.EditIndex;
                GridViewRow Row = grdInvoiceRaised.Rows[index];
                DropDownList ddlInvoiceStatus = (DropDownList)Row.FindControl("ddlInvoiceStatus");
                TextBox txtActualDate = (TextBox)Row.FindControl("txtActualDate");
                projectTaskDataContext invoice = new projectTaskDataContext();
                //ProjectMgt.Entity.ProjectValuation Update =
                // invoice.ProjectValuations.Single(P => P.ValuationID == int.Parse(e.CommandArgument.ToString()));
                //Update.InvoiceStatus = int.Parse(ddlInvoiceStatus.SelectedValue);

                //invoice.SubmitChanges();


                //ProjectValuation projectV=invoice.ProjectValuations.Single(P=>P.ValuationID==int.Parse(e.CommandArgument.ToString()));
                //projectV.InvoiceStatus=int.Parse(ddlInvoiceStatus.SelectedValue);
                //invoice.SubmitChanges();

                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectValuations set InvoiceStatus=@val where ValuationID=@ValuationID",
                    new SqlParameter("@val", int.Parse(ddlInvoiceStatus.SelectedValue)), new SqlParameter("@ValuationID", int.Parse(e.CommandArgument.ToString())));

                if (txtActualDate.Text != "")
                {
                    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update ProjectValuations set ActualDate=@ActualDate where ValuationID=@ValuationID",
                    new SqlParameter("@ActualDate", Convert.ToDateTime(txtActualDate.Text)), new SqlParameter("@ValuationID", int.Parse(e.CommandArgument.ToString())));
                }

                BindInvoiceJournal();

                //ProjectMgt.Entity.ProjectBOM Update =
                // InsertBOM.ProjectBOMs.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BingInvoicestatus(DropDownList ddlInvoiceStatus,int setval)
    {
        RiseValuation RiseVal = new RiseValuation();
        ddlInvoiceStatus.DataSource = RiseVal.LoadInvoiceStatus();
        ddlInvoiceStatus.DataTextField = "text";
        ddlInvoiceStatus.DataValueField = "value";
        ddlInvoiceStatus.DataBind();
        ddlInvoiceStatus.SelectedValue = setval.ToString();
    }
    protected void grdInvoiceRaised_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.FindControl("ddlInvoiceStatus") != null)
                {

                    //DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    Label lblStatusID = (Label)e.Row.FindControl("lblInvoiceStatus1");


                    //DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
                    DropDownList ddlInvoiceStatus = (DropDownList)e.Row.FindControl("ddlInvoiceStatus");
                    BingInvoicestatus(ddlInvoiceStatus, int.Parse(lblStatusID.Text));

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdInvoiceRaised_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdInvoiceRaised.EditIndex = e.NewEditIndex;
        BindInvoiceJournal();
    }
    protected void grdInvoiceRaised_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdInvoiceRaised.EditIndex = -1;
        BindInvoiceJournal(); 
    }
    protected void grdInvoiceRaised_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdInvoiceRaised.EditIndex =-1;
        BindInvoiceJournal();
    }
    protected void grdInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Archive")
        {
            int status = 5;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update Projects set ProjectStatusID=@val where ProjectReference=@ProjectReference",
                  new SqlParameter("@val", status), new SqlParameter("@ProjectReference", int.Parse(e.CommandArgument.ToString())));
            BindInvoice();
        }
    }
    protected void ImgViewLiveProjects_Click(object sender, EventArgs e)
    {

        try
        {

            if (int.Parse(string.IsNullOrEmpty(txtProjectRefInvoice.Text)?"0":txtProjectRefInvoice.Text)!=0)
            {
                GetInterimInvoice(int.Parse(txtProjectRefInvoice.Text.Trim()));
            }
            else
            {
                BindInvoiceJournal();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void GetInterimInvoice(int Ref)
    {
        try
        {

            lblFocus.Focus();
            grdInvoiceRaised.Visible = true;
            //lblTitle.Text = "";
            div1.Visible = true;
            lblTitle.Visible = true;
            lblTitle.Text = "Interim Invoices";//GetTitleForSecondGrid(ProjectRef);
            projectTaskDataContext invoice = new projectTaskDataContext();

            //List<ListItem> listStatus = RiseVal.LoadInvoiceStatus();
            //var list = RiseVal.LoadInvoiceStatus();
            var invoiceJouranl = (from r in invoice.ProjectValuations
                                  // join s in list on r.InvoiceStatus equals int.Parse(s.Value)
                                  join p in invoice.ProjectDetails on r.ProjectReference equals p.ProjectReference

                                  where p.ProjectStatusID == 2 && r.InvoiceStatus == 3 && r.ProjectReference == Ref
                                  orderby r.ProjectReference
                                  select new
                                  {
                                      r.InvoiceReference,
                                      r.DateRaised,
                                      r.Value,
                                      r.VATPercentage,
                                      SubTotal = ((r.VATPercentage * r.Value) / 100) + r.Value,
                                      p.ProjectReferenceWithPrefix,
                                      p.ProjectTitle,
                                      r.ValuationID,
                                      expectedate = r.DateRaised.Value.AddDays(30),
                                      r.Notes,
                                      r.ProjectReference,
                                      r.InvoiceStatus,
                                      r.ActualDate
                                  }).ToList();
            //&& s.Value.Contains("3") s.Text,
            //var invoiceJouranl=(from r in invoice.ProjectValuations
            //                    where r.ProjectReference==ProjectRef
            //                    select r).ToList();
            if (invoiceJouranl != null)
            {
                grdInvoiceRaised.DataSource = invoiceJouranl;
                grdInvoiceRaised.DataBind();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
