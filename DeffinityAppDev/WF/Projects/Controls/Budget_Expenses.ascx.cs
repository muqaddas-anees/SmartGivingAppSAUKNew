using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using UserMgt.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Finance.DAL;
using Finance.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class controls_Budget_Expenses : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        try
        {
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    contractorsList.Add(new UserMgt.Entity.Contractor { ID = 0, ContractorName = "" });
                    var expenseList = db.ExternalExpenses.Where(e => e.ProjectReference == QueryStringValues.Project).ToList();
                    var expenseEntryTypeList = db.Expensesentrytypes.ToList();
                    expenseEntryTypeList.Add(new Expensesentrytype { ID = 0, ExpensesentryType1 = "" });
                    var result = (from e in expenseList
                                  join t in expenseEntryTypeList on e.ExpensesentrytypeID equals t.ID
                                  join c in contractorsList on e.AssignedTo.HasValue ? e.AssignedTo : 0 equals c.ID
                                  select new ExpenseList
                                  {
                                      ID = e.ID,
                                      ContractorID = e.ContractorID.HasValue ? e.ContractorID : 0,
                                      ProjectReference = e.ProjectReference.HasValue ? e.ProjectReference : 0,
                                      Amount = e.Amount.HasValue ? e.Amount : 0,
                                      ExternalExpensesDate = e.ExternalExpensesDate,
                                      EntryTypeID = e.ExpensesentrytypeID.HasValue ? e.ExpensesentrytypeID : 0,
                                      Description = e.Description,
                                      Qty = e.Qty.HasValue ? e.Qty : 0,
                                      UnitCost = e.UnitCost.HasValue ? e.UnitCost : 0,
                                      Expensed = e.Expensed.HasValue ? e.Expensed : false,
                                      AssignedTo = e.AssignedTo.HasValue ? e.AssignedTo : 0,
                                      EntryType = t.ExpensesentryType1,
                                      ForecastValue = e.ForecastValue.HasValue ? e.ForecastValue : 0,
                                      Total = (e.Qty.HasValue ? e.Qty : 0) * (e.ForecastValue.HasValue ? e.ForecastValue : 0), // Qty * UnitCost
                                      AssignedToName = c.ContractorName

                                  }).ToList();


                    if (ddlEntryTypeFilter.SelectedValue != "0" && ddlEntryTypeFilter.SelectedValue != "")
                    {
                        result = result.Where(r => r.EntryTypeID == Convert.ToInt32(ddlEntryTypeFilter.SelectedValue)).ToList();
                    }

                    // Add empty row
                    result.Add(new ExpenseList { ID = -99, Expensed = false });

                    gvExpense.DataSource = result;
                    gvExpense.DataBind();

                    // Bind top section
                    lblForecastExpense.Text = result.Select(r => r.Total).Sum().Value.ToString("f2");
                    lblSpentToDate.Text = result.Where(r => r.Expensed == true).Select(r => r.Total).Sum().Value.ToString("f2");
                    lblRemaining.Text = result.Where(r => r.Expensed == false).Select(r => r.Total).Sum().Value.ToString("f2");
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;
                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        vis = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;
    }

    private void BindDropdown(DropDownList ddl)
    {
        try
        {
            using (UserDataContext db = new UserDataContext())
            {
                var activeUsersList = db.Contractors.Where(c => c.Status.ToLower() == "active" && c.SID != 7 && c.SID != 8).OrderBy(c => c.ContractorName).Select(c => c).ToList();
                ddl.DataSource = activeUsersList;
                ddl.DataValueField = "ID";
                ddl.DataTextField = "ContractorName";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Expense - Insert / Update

    public int InsertExternalExpenses(int projectReference, int contractorId, int entryTypeId, DateTime date, double amount, string notes, string description, double qty, double ForecastValue, int assignedTo, bool expensed)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_ExternalExpensesInsert");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, projectReference);
            db.AddInParameter(cmd, "@ContractorID", DbType.Int32, contractorId);
            db.AddInParameter(cmd, "@ExpensesentrytypeID", DbType.Int32, entryTypeId);
            db.AddInParameter(cmd, "@ExternalExpensesDate", DbType.DateTime, date);
            db.AddInParameter(cmd, "@amount", DbType.Double, amount);
            db.AddInParameter(cmd, "@Notes", DbType.String, notes);
            db.AddInParameter(cmd, "@Description", DbType.String, description);
            db.AddInParameter(cmd, "@Qty", DbType.Double, qty);
            db.AddInParameter(cmd, "@UnitCost", DbType.Double, 0);
            db.AddInParameter(cmd, "@ForecastValue", DbType.Double, ForecastValue);
            db.AddInParameter(cmd, "@AssignedTo", DbType.Int32, assignedTo);
            db.AddInParameter(cmd, "@Expensed", DbType.Boolean, expensed);

            int getVal = Convert.ToInt32(db.ExecuteNonQuery(cmd));
            cmd.Dispose();
            return getVal;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return 0;
        }
        finally
        {

        }
    }
    public void UpdateExternalExpenses(int id, int projectReference, int contractorId, int entryType, DateTime date, double amount, string notes, string description, double qty, double ForecastValue, int assignedTo, bool expensed)
    {
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("DN_ExternalExpensesupdate");
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, projectReference);
            db.AddInParameter(cmd, "@ResourceID", DbType.Int32, contractorId);
            db.AddInParameter(cmd, "@entryid", DbType.Int32, entryType);
            db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, date);
            db.AddInParameter(cmd, "@amount", DbType.Double, amount);
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            db.AddInParameter(cmd, "@notes", DbType.String, notes);
            db.AddInParameter(cmd, "@Description", DbType.String, description);
            db.AddInParameter(cmd, "@Qty", DbType.Double, qty);
         //   db.AddInParameter(cmd, "@UnitCost", DbType.Double, ForecastValue);
            db.AddInParameter(cmd, "@ForecastValue", DbType.Double, ForecastValue);
            db.AddInParameter(cmd, "@AssignedTo", DbType.Int32, assignedTo);
            db.AddInParameter(cmd, "@Expensed", DbType.Boolean, expensed);
            db.AddOutParameter(cmd, "@output", DbType.Int32, 4);
            
            db.ExecuteNonQuery(cmd);
            int GetVal = (int)db.GetParameterValue(cmd, "@output");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            //return 0;
        }
        finally
        {

        }

    }
    #endregion

    #region Grid events
    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = ((Label)e.Row.FindControl("lblID")).Text.Trim();
                if (id == "-99")
                {
                    e.Row.Visible = false;
                }
                DropDownList ddlAssignedTo = (DropDownList)e.Row.FindControl("ddlAssignTo");
                if (ddlAssignedTo != null)
                {
                    BindDropdown(ddlAssignedTo);
                    HiddenField hfAssignedTo = (HiddenField)e.Row.FindControl("hfAssignedTo");
                    ddlAssignedTo.SelectedValue = hfAssignedTo.Value;
                }
                string expensed = ((Label)e.Row.FindControl("lblExpensed")).Text.Trim();
                if (expensed == "Yes")
                {
                    ((LinkButton)e.Row.FindControl("imgDelete")).Visible = false;
                    ((LinkButton)e.Row.FindControl("LinkeditexternalExpenses")).Visible = false;
                    ((LinkButton)e.Row.FindControl("ImgSave")).Visible = false;
                }


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFooterAssignedTo = (DropDownList)e.Row.FindControl("ddlFooterAssignedTo");
                if (ddlFooterAssignedTo != null)
                {
                    BindDropdown(ddlFooterAssignedTo);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvExpense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Insert_ExternalFooter")
            {
                DateTime date = Convert.ToDateTime(((TextBox)gvExpense.FooterRow.FindControl("txtFooterDate")).Text);
                string description = ((TextBox)gvExpense.FooterRow.FindControl("txtFooterDescription")).Text.Trim();
                int entryType = Convert.ToInt32(((DropDownList)gvExpense.FooterRow.FindControl("ddlEntry_footerExternalExpenses")).SelectedValue);
                double qty = Convert.ToDouble(((TextBox)gvExpense.FooterRow.FindControl("txtFooterQty")).Text.Trim());
                double ForecastValue = Convert.ToDouble(((TextBox)gvExpense.FooterRow.FindControl("txtFooterUnitCost")).Text.Trim());
                int assignedTo = Convert.ToInt32(((DropDownList)gvExpense.FooterRow.FindControl("ddlFooterAssignedTo")).SelectedValue);
                bool expensed = Convert.ToBoolean(((CheckBox)gvExpense.FooterRow.FindControl("chkFooterExpensed")).Checked);
                double amount = qty * ForecastValue;

                InsertExternalExpenses(Convert.ToInt32(QueryStringValues.Project), sessionKeys.UID,
                     entryType, date, amount, "", description, qty, ForecastValue, assignedTo, expensed);
                BindGrid();


            }
            if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int i = gvExpense.EditIndex;
                GridViewRow Row = gvExpense.Rows[i];

                DateTime date = Convert.ToDateTime(((TextBox)Row.FindControl("txtDate")).Text);
                string description = ((TextBox)Row.FindControl("txtDescription")).Text.Trim();
                int entryType = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntryExternalExpenses")).SelectedValue);
                double qty = Convert.ToDouble(((TextBox)Row.FindControl("txtQty")).Text.Trim());
                double ForecastValue = Convert.ToDouble(((TextBox)Row.FindControl("txtUnitCost")).Text.Trim());
                int assignedTo = Convert.ToInt32(((DropDownList)Row.FindControl("ddlAssignTo")).SelectedValue);
                bool expensed = Convert.ToBoolean(((CheckBox)Row.FindControl("chkExpensed")).Checked);
                double amount = qty * ForecastValue;

                UpdateExternalExpenses(id, Convert.ToInt32(QueryStringValues.Project), sessionKeys.UID, entryType, date, amount, "", description, qty, ForecastValue, assignedTo, expensed);

            }
            if (e.CommandName == "Delete")
            {

                string id = e.CommandArgument.ToString();

                string delete = "delete from ExternalExpenses where ID='" + id + "'";

                SqlCommand cmd = new SqlCommand(delete, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            if (e.CommandName == "Admin")
            {
                Response.Redirect("~/WF/Admin/AdminDropDown.aspx?type=Finance&Projet=" + QueryStringValues.Project, false);
            }
            if (e.CommandName == "Saving")
            {
                string Expensid = e.CommandArgument.ToString();
                lblProjectExpansesId.Text = Expensid.ToString();
                lblProjectExpansesId.Visible = false;
                using (FinanceModuleDataContext Fdc = new FinanceModuleDataContext())
                {
                    using (projectTaskDataContext InsertBOM = new projectTaskDataContext())
                    {
                        var ExpenseRecord = Fdc.ExternalExpenses.Where(a => a.ID == int.Parse(Expensid)).FirstOrDefault();
                        var BOMSavingrecord = InsertBOM.GoodsReceiptSavings.Where(a => a.BOMId == ExpenseRecord.ID && a.S_type == "Expenses").FirstOrDefault();
                        lblQtyReceivedtoDate.Text = ExpenseRecord.Qty.Value.ToString();
                        txtbudgetQty.Text = ExpenseRecord.ForecastValue.Value.ToString();
                        if (BOMSavingrecord != null)
                        {
                            txtActualReq.Text = BOMSavingrecord.UnitCostSaving.Value.ToString();
                        }
                        else
                        {
                            txtActualReq.Text = string.Empty;
                        }
                    }
                }
                mdlpopupinGridToSave.Show();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GoodsReceiptSaving grSaving = null;
            using (projectTaskDataContext BOM = new projectTaskDataContext())
            {
                GoodsReceiptSaving grSaving1 = BOM.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project && a.BOMId == int.Parse(lblProjectExpansesId.Text) && a.S_type == "Expenses").FirstOrDefault();
                if (grSaving1 == null)
                {
                    grSaving = new GoodsReceiptSaving();
                    grSaving.projectRef = QueryStringValues.Project;
                    grSaving.BOMId = int.Parse(lblProjectExpansesId.Text);
                    grSaving.BudgetQty = int.Parse(txtbudgetQty.Text);
                    grSaving.UnitCostSaving =Convert.ToDouble(txtActualReq.Text);
                    grSaving.S_type = "Expenses";
                    grSaving.Userid = sessionKeys.UID;
                    grSaving.DateModified = DateTime.Now;
                    BOM.GoodsReceiptSavings.InsertOnSubmit(grSaving);
                    BOM.SubmitChanges();
                }
                else
                {
                    grSaving1.Userid = sessionKeys.UID;
                    grSaving1.DateModified = DateTime.Now;
                    grSaving1.UnitCostSaving = Convert.ToDouble(txtActualReq.Text);
                    BOM.SubmitChanges();
                }
                mdlpopupinGridToSave.Hide();
                Response.Redirect(Request.RawUrl);
                //   lblError.Text = "Updated successfully.";
                //  lblError.Visible = true;
                //  lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvExpense_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvExpense.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvExpense_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvExpense.EditIndex = -1;
        BindGrid();
    }
    protected void gvExpense_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvExpense.EditIndex = -1;
        BindGrid();
    }
    protected void gvExpense_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        gvExpense.EditIndex = -1;
        BindGrid();
    }
    #endregion

    protected void imgView_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    #region # Custom Class #
    public class ExpenseList
    {
        public int ID { get; set; }
        public int? ContractorID { get; set; }
        public int? ProjectReference { get; set; }
        public double? Amount { get; set; }
        public DateTime? ExternalExpensesDate { get; set; }
        public int? EntryTypeID { get; set; }
        public string Description { get; set; }
        public double? Qty { get; set; }
        public double? UnitCost { get; set; }
        public double? ForecastValue { get; set; }
        public bool? Expensed { get; set; }
        public int? AssignedTo { get; set; }
        public string EntryType { get; set; }
        public double? Total { get; set; }
        public string AssignedToName { get; set; }
    }
    #endregion


    private void Expense()
    {

    }

    protected void gvExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvExpense.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}