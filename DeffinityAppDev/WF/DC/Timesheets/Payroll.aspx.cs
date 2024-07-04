using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Timesheets
{
    public partial class Payroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
                BindGrid();
                BindExpensesGrid();
            }
        }
        private void BindUsers()
        {
            try
            {
                var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                             orderby p.ContractorName
                             select new { ID = p.ID, Text = p.ContractorName }).ToList();
                ddlUsers.DataSource = jlist;
                ddlUsers.DataTextField = "Text";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, new ListItem("All", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
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
        private void BindGrid()
        {
            try
            {
                IQueryable<TimesheetMgt.Entity.v_timesheetentry> vlist;// = new Queryable<TimesheetMgt.Entity.v_timesheetentry>();

                vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID);
                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = vlist.Where(o => o.DateEntered.Value.Year == Convert.ToDateTime(txtweekcommencedate.Text.Trim()).Year && o.DateEntered.Value.Month == Convert.ToDateTime(txtweekcommencedate.Text.Trim()).Month && o.DateEntered.Value.Day == Convert.ToDateTime(txtweekcommencedate.Text.Trim()).Day);
                if (ddlUsers.SelectedValue != "0")
                    //vlist = vlist.Where(o => Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) <= o.DateEntered && o.DateEntered >= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())));
                    if (ddlUsers.SelectedValue != "0")
                    vlist = vlist.Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue));

                //    if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                //    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) >=  o.DateEntered && o.DateEntered <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.DateEntered).ToList();
                //else if (ddlUsers.SelectedValue != "0")
                //    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.DateEntered).ToList();
                //else
                //    vlist = TimesheetMgt.BAL.TimesheetEntryBAL.TimesheetBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.PrimeApproverName).ToList();
                GridPartner.DataSource = vlist.OrderByDescending(o=>o.DateEntered).ToList();
                GridPartner.DataBind();

                lblHours.Text = string.Format("{0:N2}", vlist.ToList().Select(o => o.Hours).Sum());
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindExpensesGrid()
        {
            try
            {
                IQueryable<TimesheetMgt.Entity.v_TimeExpense> vlist;// = new Iq<TimesheetMgt.Entity.v_TimeExpense>();
                vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.Status != "Paid");

                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = vlist.Where(o => o.TimeExpensesDate.Value.Year == Convert.ToDateTime(txtweekcommencedate.Text).Year && o.TimeExpensesDate.Value.Month == Convert.ToDateTime(txtweekcommencedate.Text).Month && o.TimeExpensesDate.Value.Day == Convert.ToDateTime(txtweekcommencedate.Text).Day );
                else if (ddlUsers.SelectedValue != "0")
                    vlist = vlist.Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue));
                
                gridExpenses.DataSource = vlist.ToList().OrderByDescending(o=>o.TimeExpensesDate);
                gridExpenses.DataBind();

                lblTotalAmount.Text = string.Format("{0:N2}",vlist.ToList().Select(o => o.amount).Sum());


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btn_viewdate_Click(object sender, EventArgs e)
        {
            BindGrid();
            BindExpensesGrid();
        }


        protected void GridPartner_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    v_timesheetentry objList = e.Row.DataItem as v_timesheetentry;
                    if (objList != null)
                    {

                        Label lblGridEndTime = (Label)e.Row.FindControl("lblGridEndTime");
                        Label lblGridStartTime = (Label)e.Row.FindControl("lblGridStartTime");
                        //Button btnStop = (Button)e.Row.FindControl("btnStop");
                        //index 19,20 are StartTime and end time
                        if (objList.fromtime != null)
                        {
                            if (!string.IsNullOrEmpty(objList.totime.Value.ToString()) && !string.IsNullOrEmpty(objList.fromtime.Value.ToString()))
                            {
                                lblGridEndTime.Text = objList.totime.Value.ToString().Substring(0, 5);
                                lblGridStartTime.Text = objList.fromtime.Value.ToString().Substring(0, 5);
                                if (lblGridStartTime.Text == lblGridEndTime.Text)
                                {
                                    lblGridEndTime.Text = "";
                                    lblGridStartTime.Text = "Started at " + objList.fromtime.Value.ToString().Substring(0, 5);
                                    //btnStop.Visible = true;
                                }
                                else
                                {
                                    //btnStop.Visible = false;
                                }
                            }
                            else
                            {
                                lblGridEndTime.Text = string.Empty;
                                lblGridStartTime.Text = string.Empty;
                            }
                        }
                        else
                        {
                            lblGridEndTime.Text = string.Empty;
                            lblGridStartTime.Text = string.Empty;
                        }





                        if (objList.TimeSheetStatusName == "Approved")
                            e.Row.BackColor = System.Drawing.Color.LightYellow;
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnMarkedPaid_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvrow in gridExpenses.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    Label lblID = (Label)gvrow.FindControl("lblID");
                    if (chk != null & chk.Checked)
                    {
                        var expenseID = lblID.Text;

                        var t = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectByID(Convert.ToInt32(expenseID));
                        t.Status = "Paid";
                        //update expenses status paid
                        TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_update(t);

                        BindExpensesGrid();

                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;

                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}