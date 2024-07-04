using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Expenses
{
    public partial class ExpensesSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();

                    if (Request.QueryString["back"] != null)
                    {
                        if (Request.QueryString["pnl"] != null)
                            linkBack.NavigateUrl = Request.QueryString["back"] + "#" + Request.QueryString["pnl"].ToString();
                        else
                            linkBack.NavigateUrl = Request.QueryString["back"];
                        linkBack.Text = "<i class='fa fa-arrow-left'></i> Return to Settings";
                        linkBack.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                var mlist = TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodeBAL_Select(sessionKeys.PortfolioID);
                GridPartner.DataSource = mlist;
                GridPartner.DataBind();
                if (mlist.Count == 0)
                {
                    AddNewPopup();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridPartner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editmodule")
                {
                    huid.Value = e.CommandArgument.ToString();
                    var m = TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodeBAL_SelectByID(Convert.ToInt32(huid.Value));
                    if (m != null)
                    {
                        txtAccountingcode.Text = m.AccountingCode;
                        txtDescription.Text = m.Description;
                        mdlManageOptions.Show();
                    }

                }
                else if (e.CommandName == "del")
                {
                    var m = TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodeBAL_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var moduleid = Convert.ToInt32(huid.Value);

                if (huid.Value == "0")
                {
                   
                    var pret = TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodesBAL_Update(sessionKeys.PortfolioID,txtAccountingcode.Text.Trim(),txtDescription.Text.Trim());
                   
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    ClearFields();
                    mdlManageOptions.Hide();
                    BindGrid();
                }
                else
                {
                   
                        var pret = TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodesBAL_Update(sessionKeys.PortfolioID, txtAccountingcode.Text.Trim(), txtDescription.Text.Trim(), Convert.ToInt32(huid.Value));
                       
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        ClearFields();
                        BindGrid();
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewPopup();
        }

        private void AddNewPopup()
        {
            huid.Value = "0";
            ClearFields();
            mdlManageOptions.Show();
        }

        private void ClearFields()
        {
            txtDescription.Text = string.Empty;
            txtAccountingcode.Text = string.Empty;
           
        }
    }
}