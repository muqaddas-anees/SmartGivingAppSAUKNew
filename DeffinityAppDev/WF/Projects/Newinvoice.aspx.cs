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

using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

public partial class NewValuation : System.Web.UI.Page
{
    protected string getStyle = "sec_tab1";
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    DisBindings getData = new DisBindings();
    RiseValuation RiseVal = new RiseValuation();
    int ProjectReference;
    int ContractorID;
    string ProjectValuationID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        lblMsg.Text = "";
        ProjectReference = QueryStringValues.Project;
        ContractorID = sessionKeys.UID;
       
            int type = int.Parse(QueryStringValues.Type);


            if (!Page.IsPostBack)
            {
                //Master.PageHead = "Project Management";
                BindLables();
                BindAddNew();
                BingInvoicestatus();
                BindPortfoiloContacts();
                if (Request.QueryString["ValuationID"] != null)
                {
                    if(int.Parse(Request.QueryString["ValuationID"].ToString()) >0)
                    GetInvoiceReferenceDetails(int.Parse(Request.QueryString["ValuationID"].ToString()));
                    //txtInvoiceRef.Text = getItemRefence();
                }
                else
                {
                    txtDateRaised.Text = string.Format("{0:d}",DateTime.Now);
                    BindVAT();
                }
                //BindGrid();
                BindInvoice();
                hdnRefresh.Value = "1";
                //if page is from build project section 
                if (type == 2)
                {
                    BuildProjectTabs1.Visible = true;
                    getStyle = "";
                }

            }
            lblError.Visible = false;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //set image for invoice re
        chnageImgSrc();
    }

    static DataSet ds1 = new DataSet();
    private string getItemRefence()
    {
        string stemp = "";
        try
        {
            DbCommand cmd = db.GetSqlStringCommand("Select InvoiceReference from ProjectValuations where ValuationID =" + Request.QueryString["ValuationID"].ToString());
            stemp = db.ExecuteScalar(cmd).ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return stemp;
    }
    private int getVarienceID(string InvoiceReference)
    {
        int stemp = 0;
        try
        {
            DbCommand cmd = db.GetSqlStringCommand("Select ValuationID from ProjectValuations where InvoiceReference ='" + InvoiceReference+"'");
            stemp = Convert.ToInt32(db.ExecuteScalar(cmd).ToString());
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return stemp;
    }
    //private int getMaxVarienceID(string InvoiceReference)
    //{
    //    string stemp = "";
    //    int retval =1;
    //    try
    //    {
    //        //Select top 1 ValuationID from ProjectValuations Order By 
    //        DbCommand cmd = db.GetSqlStringCommand("Select top 1 ValuationID from ProjectValuations Order By ValuationID");
    //        stemp = db.ExecuteScalar(cmd).ToString();
    //        if (!string.IsNullOrEmpty())
    //        {
    //            retval = Convert.ToInt32(stemp) + 1;
    //        }
           
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message);
    //    }
    //    return retval;
    //}
    private void BindVAT()
    {
        try
        {
            DbCommand cmd = db.GetSqlStringCommand("Select VAT from ProjectDefaults");
            txtVatper.Text = string.Format("{0:F2}", db.ExecuteScalar(cmd));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindAddNew()
    { 
        //getData.DdlBindSelect(ddlTasks, "select ID,ItemDescription from ProjectTaskItems where IndentLevel<>0 AND ProjectReference =" + QueryStringValues.Project.ToString(), "ID", "ItemDescription", false, true);
        projectTaskDataContext getTasks = new projectTaskDataContext();
        var getTask = (from r in getTasks.ProjectTaskItems
                       where r.ProjectReference == QueryStringValues.Project
                       orderby r.ItemDescription
                       select new { r.ID, r.ItemDescription }).ToList();
        if (getTask != null)
        {
            ddlTasks.DataSource = getTask;
            ddlTasks.DataTextField = "ItemDescription";
            ddlTasks.DataValueField="ID";
            ddlTasks.DataBind();
            ddlTasks.Items.Insert(0, new ListItem("Please select...", "0"));

        }
    }
    //bind grid view
    private void BindGrid()
    {
        GridView1.DataSourceID = "SqlDataSource1";
        GridView1.DataBind();
        //GridView2.DataSourceID = "SqlDataSource2";
        //GridView2.DataBind();
    }
    private void BindLables()
    {
        try
        {
            using (IDataReader dr = RiseVal.GetInvoiceTotal(QueryStringValues.Project,GetID()))
            {
                while (dr.Read())
                {
                   // lblRevisedProjectValue.Text = string.Format("{0:F2}", dr["RevisedValue"]);
                    lblTotalWithoutVat.Text = string.Format("{0:F2}", dr["TotalValue"]);
                    lblTotalInvoice.Text = string.Format("{0:F2}", dr["Total"]);
                    lblVat.Text = string.Format("{0:F2}", dr["vat"]);
                    lblVatT.Text = string.Format("{0:F2}", dr["Tvat"]);
                }
                dr.Close();
            }
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
   
    //static  DataSet ds2 = new DataSet();
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertInvoice(0,true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //send mail 
        InsertInvoice(1,true);
    }
    private void InsertInvoice(int a_sendmail, bool checkType)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtInvoiceRef.Text.Trim()))
            {
                int type = int.Parse(QueryStringValues.Type);
                int ProjectReference = QueryStringValues.Project;
                double Value = 0;
                
                DbCommand cmd = db.GetStoredProcCommand("DN_Valuation_add");
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
                db.AddInParameter(cmd, "@Status", DbType.Int32, 1);
                db.AddInParameter(cmd, "@PaymentRecieved", DbType.Int32, Value);
                db.AddInParameter(cmd, "@EmailAddress", DbType.String, txtEmailAddress.Text.Trim());
                //db.AddInParameter(cmd, "@Notes", DbType.String, "");
                db.AddInParameter(cmd, "@RaisedBy", DbType.Int32, sessionKeys.UID);
                db.AddInParameter(cmd, "@InvoiceReference", DbType.String, txtInvoiceRef.Text.Trim());
                db.AddInParameter(cmd, "@InvoiceStatus", DbType.Int32, int.Parse(ddlInvoiceStatus.SelectedValue));
                db.AddInParameter(cmd, "@ID", DbType.Int32, GetID());
                db.AddOutParameter(cmd, "@Outval", DbType.Int32, 4);
                db.AddOutParameter(cmd, "@ValuationID", DbType.Int32, 4);
                db.AddInParameter(cmd, "@DateRaised", DbType.DateTime, string.IsNullOrEmpty(txtDateRaised.Text) ? "01/01/1900" : txtDateRaised.Text);
                db.AddInParameter(cmd, "@VATF", DbType.Double, Convert.ToDouble(string.IsNullOrEmpty(txtVatper.Text) ? "0" : txtVatper.Text));
                db.AddInParameter(cmd, "@VATNumber", DbType.String, txtVatNumber.Text);
                db.AddInParameter(cmd, "@BankDetails", DbType.String, txtBankDetails.Text);
                db.ExecuteNonQuery(cmd);

                int getVal = (int)db.GetParameterValue(cmd, "Outval");

                if (getVal == 1)
                {
                    int invoiceid = (int)db.GetParameterValue(cmd, "ValuationID");
                    //send mail here..
                    if(a_sendmail == 1)
                    SendMail(invoiceid);

                    //check if page need to redirenct to previous page.
                    if (checkType)
                    {
                        if (type == 1)
                        {

                            Response.Redirect("ProjectTracker_Invoicing.aspx?Project=" + QueryStringValues.Project.ToString());
                            //Response.Redirect("ProjectValuations.aspx?Project=" + QueryStringValues.Project.ToString());
                        }
                        else if (type == 2)
                        {
                            Response.Redirect("ProjectTracker_Invoicing.aspx?Invoice=Invoice&Project=" + QueryStringValues.Project.ToString());
                            //Response.Redirect("ProjectFinancials.aspx?Invoice=Invoice&Project=" + QueryStringValues.Project.ToString());
                        }
                    }
                    else
                    {
                        lblError.Text = "";
                        Response.Redirect("Newinvoice.aspx?Project=" + QueryStringValues.Project.ToString() + "&type=" + QueryStringValues.Type.ToString() + "&ValuationID=" + invoiceid.ToString());
                    }
                }
                else if (getVal == 2)
                {
                    //lblError.Visible = true;
                    lblMsg1.Text = "Invoice reference already exists";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "There is no difference between current and previous valuations!";
                }
            }
            else
            {
                //lblMsg1.Text = "Please enter Invoice reference";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        int type = int.Parse(QueryStringValues.Type);
        int ProjectReference = QueryStringValues.Project;
        if(type == 1)
        {
            Response.Redirect("~/WF/Projects/ProjectTracker_Invoicing.aspx?Project=" + ProjectReference.ToString(), false);
            //Response.Redirect("~/WF/Projects/ProjectValuations.aspx?Project=" + ProjectReference.ToString(), false);
        }
        else if(type == 2)
        {
            Response.Redirect("~/WF/Projects/ProjectTracker_Invoicing.aspx?Invoice=Invoice&Project=" + ProjectReference.ToString(), false);
            //Response.Redirect("~/WF/Projects/ProjectFinancials.aspx?Invoice=Invoice&Project=" + ProjectReference.ToString(), false);
        }
    }   
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (GetID() > 0)
        {
            try
            {
                string des = "";
                if (!ddlTasks.Visible)
                {
                    des = txtDesc.Text.Trim();
                }
                else
                {
                    des = ddlTasks.SelectedItem.Text;
                }
                // if item reference is not exist..
                int i = 0;
                if (!string.IsNullOrEmpty(txtInvoiceRef.Text.Trim()))
                {
                    i = getVarienceID(txtInvoiceRef.Text.Trim());
                }

                RiseVal.TaskItemToInvoice(i, des, txtQty.Text.Trim(), txtSellingPrice.Text.Trim(), ddlPercent.SelectedValue, true, QueryStringValues.Project);
                lblMsg.Text = "Added successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                RequiredFieldValidator1.ErrorMessage = "";
                //BindGrid();
                BindInvoice();
                BindLables();
                ChangeVisible1();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            //clear fields
            ClearFields();
        }
        else
        {
            ValidationSummary1.Visible = false;
            lblMsg.Visible = true;
            lblMsg.Text = "Please enter invoice reference";
        }

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
    }

    private void ClearFields()
    {
        txtSellingPrice.Text = "";        
        txtQty.Text = "";
        ddlPercent.SelectedIndex = 0;
        ddlTasks.SelectedIndex = 0;
    }
    private void BindInvoice()
    {
        if (Request.QueryString["Project"] != null && Request.QueryString["ValuationID"]!=null)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_DisplayInvoice",
                new SqlParameter("@ProjectReference", int.Parse(Request.QueryString["Project"].ToString())),
                new SqlParameter("@ProjectValuationID", int.Parse(Request.QueryString["ValuationID"].ToString())),
                new SqlParameter("@Counter", int.Parse(hdnRefresh.Value))).Tables[0];
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int i = GridView1.EditIndex;
                GridViewRow Row = GridView1.Rows[i];
                string Desciption = ((TextBox)Row.FindControl("txtItemDes")).Text;
                string ID = ((HiddenField)Row.FindControl("HID1")).Value;
                string qty = ((TextBox)Row.FindControl("txtQty")).Text;
                string price = ((TextBox)Row.FindControl("txtPrice")).Text;
                string percent = ((DropDownList)Row.FindControl("ddlPercentage")).SelectedValue;
                RiseVal.TaskItemToInvoiceUpdate(ID, Desciption, qty, price, percent);
                BindInvoice();
                BindLables();

            }
            else if (e.CommandName == "Edit")
            {

            }
            if (e.CommandName == "Delete")
            {
                projectTaskDataContext task = new projectTaskDataContext();
                projectTaskDataContext task1 = new projectTaskDataContext();
                ProjectValuationItem pv = task.ProjectValuationItems.Single(P => P.ID == int.Parse(e.CommandArgument.ToString()));
                task.ProjectValuationItems.DeleteOnSubmit(pv);
                task.SubmitChanges();
                BindInvoice();
                BindLables();
                //ProjectValuation ps = task1.ProjectValuations.Single(P => P.ValuationID == int.Parse(Request.QueryString["ValuationID"].ToString()));
                //ps.Value = Convert.ToDouble(lblTotalInvoice.Text);                
                //task1.SubmitChanges();
                //POInsert.SubmitChanges();
               // update(lblTotalInvoice.Text);
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //private void update(string val)
    //{
    //    if (Request.QueryString["ValuationID"] != null)
    //    {
    //        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "UPDATE ProjectValuations set Value=@Val where ValuationID=@ValuationID",
    //            new SqlParameter("@Val", Convert.ToDouble(val)), new SqlParameter("@ValuationID", int.Parse(Request.QueryString["ValuationID"].ToString())));
    //    }

    //}
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        //SqlDataSource1.Update();
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
        BindLables();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ValidationSummary1.Visible = true;
        ChangeVisible();
    }
    private void ChangeVisible()
    {
       
        btnAdd.Visible = false;
        ddlTasks.Visible = false;
        txtDesc.Visible = true;
    }
    private void ChangeVisible1()
    {
        ValidationSummary1.Visible = false;
        btnAdd.Visible = true;
        ddlTasks.Visible = true;
        txtDesc.Visible = false;
    }
    private int GetID()
    {
        int tmp = 0;
        if (Request.QueryString["ValuationID"] != null)
        { 
            try{
            tmp = Convert.ToInt32(Request.QueryString["ValuationID"].ToString());
            }
            catch(Exception ex)
            { LogExceptions.LogException(ex.Message); }
        }
        return tmp;
    }
   

    public void SendMail(int invoiceid)
    {
        try
        {
            
            ProjectInvoiceMail1.Visible = true;
            ProjectInvoiceMail1.setdata(QueryStringValues.Project, invoiceid);

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ProjectInvoiceMail1.RenderControl(htmlWrite);
            Email ToEmail = new Email();
            ToEmail.SendingMail(txtEmailAddress.Text, "Invoice", htmlWrite.InnerWriter.ToString());

            ProjectInvoiceMail1.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            ProjectInvoiceMail1.Visible = false;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    #region Bind Invoice status dropdown
    private void BingInvoicestatus()
    {
        ddlInvoiceStatus.DataSource = RiseVal.LoadInvoiceStatus();
        ddlInvoiceStatus.DataTextField = "text";
        ddlInvoiceStatus.DataValueField = "value";
        ddlInvoiceStatus.DataBind();
    }
    #endregion

    #region Get Invoice Reference
    private void GetInvoiceReferenceDetails(int invoiceid) 
    {
        try
        {
            using (IDataReader dr = RiseVal.GetInvoiceData(invoiceid))
            {
                while (dr.Read())
                {
                    txtInvoiceRef.Text = dr["InvoiceReference"].ToString();
                    txtDateRaised.Text = string.Format("{0:d}", dr["DateRaised"]);
                    ddlInvoiceStatus.SelectedValue = dr["InvoiceStatus"].ToString();
                    txtNotes.Text = dr["Notes"].ToString();
                    txtVatper.Text = string.IsNullOrEmpty(dr["VATPercentage"].ToString()) ? "0" : dr["VATPercentage"].ToString();
                    txtBankDetails.Text = dr["BankDetails"].ToString();
                    txtVatNumber.Text = dr["VATNumber"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #endregion

    private void chnageImgSrc()
    {
        //btnAddInvoice.ImageUrl = "~/WF/media/btn_ad.gif";
        btnAddInvoice.Text = "Add";
        if (Request.QueryString["ValuationID"] != null)
        { 
            if(int.Parse(Request.QueryString["ValuationID"].ToString()) >0)
            {
                //btnAddInvoice.ImageUrl = "~/WF/media/btn_update.gif";
                btnAddInvoice.Text = "Update";
            }
        }
    }

    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {
        InsertInvoice(0,false);
        BindLables();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindInvoice();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindInvoice();
    }
    protected void ddlTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSelleingPrice();
    }
    private void BindSelleingPrice()
    {
        projectTaskDataContext task = new projectTaskDataContext();
        var tasks = (from r in task.ProjectTaskItems
                     where r.ID == int.Parse(ddlTasks.SelectedValue)
                     select r).ToList().FirstOrDefault();
        if (tasks != null)
        {
            if (tasks.Fee != null)
            {
                txtSellingPrice.Text = string.Format("{0:F2}", tasks.Fee);
            }
            else
            {
                txtSellingPrice.Text = "0.00";// string.Format("{0:F2}", tasks.Fee);
            }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindInvoice();
        BindLables();
    }
    protected void ddlCustomerContrator_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {
            txtEmailAddress.Text = "";
        PortfolioDataContext portfolioCont = new PortfolioDataContext();
        var contacts = (from r in portfolioCont.PortfolioContacts
                        where r.ID == int.Parse(ddlCustomerContrator.SelectedValue)
                        select new { r.Name, r.ID,r.Email }).ToList().FirstOrDefault();
         if (contacts != null)
            {
                txtEmailAddress.Text = contacts.Email.Trim();
        }
        }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
    }

    #region BindCustomerContracts
    private void BindPortfoiloContacts()
    {
        try
        {
            projectTaskDataContext projectRef = new projectTaskDataContext();
            var projectPortfolio = (from r in projectRef.Projects
                                    where r.ProjectReference == QueryStringValues.Project
                                    select r).FirstOrDefault();
            if (projectPortfolio != null)
            {
                PortfolioDataContext portfolioCont = new PortfolioDataContext();
                var contacts = (from r in portfolioCont.PortfolioContacts
                                where r.PortfolioID == projectPortfolio.Portfolio
                                select new { r.Name, r.ID }).ToList();
                if (contacts != null)
                {
                    ddlCustomerContrator.DataSource = contacts;
                    ddlCustomerContrator.DataTextField = "Name";
                    ddlCustomerContrator.DataValueField = "ID";
                    ddlCustomerContrator.DataBind();
                    ddlCustomerContrator.Items.Insert(0, new ListItem("Please select...", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
}
