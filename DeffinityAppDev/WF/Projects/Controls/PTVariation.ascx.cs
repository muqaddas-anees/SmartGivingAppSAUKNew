using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using Finance.DAL;
using Finance.Entity;

public partial class controls_PTVariation : System.Web.UI.UserControl
{
    double totalVariation;
    double completeToDate;
    Email mail = new Email();
    BuildProject bp = new BuildProject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CheckUserRole();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "BreakDownHours")
        //{
        //    hfVariationID.Value = e.CommandArgument.ToString();
        //    mdlBreakdownHours.Show();
        //    BindUserDropDown();
        //    BindBreakdownHoursGrid();
        //}
    }
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }
                role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }

            }
        }
    }
    //private void BindUserDropDown()
    //{
    //    using (FinanceModuleDataContext db = new FinanceModuleDataContext())
    //    {
    //        using (UserDataContext ud = new UserDataContext())
    //        {
    //            var resourceList = db.AssignedContractorsToProjects.Where(a => a.ProjectReference == int.Parse(Request.QueryString["project"].ToString())).ToList();
    //            var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
    //            var userList = (from r in resourceList
    //                            join c in contractorList
    //                                on r.ContractorID equals c.ID orderby c.ContractorName
    //                            where (c.SID == 1 || c.SID == 2 || c.SID == 3)
    //                            select new { c.ID, c.ContractorName }).ToList();
    //            ddlUser.DataSource = userList;
    //            ddlUser.DataValueField = "ID";
    //            ddlUser.DataTextField = "ContractorName";
    //            ddlUser.DataBind();
    //            ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));
    //        }
    //    }
    //}
    private void Disable()
    {
       
        btnApprove.Enabled = false;
        ImageButton7.Enabled = false;
       

    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {

        if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ManageProjectFinancials))
        {
              //Master.ErrorMsg = Resources.DeffinityRes.No_Permission;//"Sorry you do not have the required permissions to perform this task.";
              //return;
        }

        //if (txtOriginalSalesValue.Text.Trim() != "")
        //{
        Response.Redirect("~/WF/Projects/ProjectDeviationReport.aspx?Project=" + QueryStringValues.Project.ToString());
        //}
        //else
        //{
        //    lblError1.Text = Resources.DeffinityRes.Plschktheprjvalues;//"Please check the project values."; 
        //}
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), sessionKeys.UID, PermissionManager.PermissionsTo.ApproveVariations))
        {
           // Master.ErrorMsg = Resources.DeffinityRes.NoRightsToApprvVariations;//"User doesn't have rights to Approve Variations";
           // return;
        }

        int id;
        bool chk_new = false;
        try
        {


            foreach (GridViewRow row in GridView1.Rows)
            {
              
                CheckBox chkNew = (CheckBox)row.FindControl("chckApprove");
                id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);

                HiddenField hfApprove = (HiddenField)row.FindControl("hfApprove");

                if (chkNew.Checked)
                {
                    int pref = Convert.ToInt32(Request.QueryString["Project"].ToString());
                    bool approve = true;

                    if (hfApprove.Value.ToLower() == "false")
                    {
                        //db connection
                        bp.VarianceApprove(pref, id, approve);
                        if (approve)
                        {
                            mail.sendMail(pref, id, 4);
                        }
                        else
                        {
                            mail.sendMail(pref, id, 5);
                        }
                        //RiseVal.TaskItemToInvoice(id, "new1", "0", "0", "0", false, pref);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "variation approve in finance page");
        }

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
        //re-bind values
        // SelectValues(QueryStringValues.Project); 
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //Page.MaintainScrollPositionOnPostBack = true;e.Keys["ID"].ToString();
        int index = GridView1.EditIndex;
        GridViewRow Grow = GridView1.Rows[index];
        TextBox txtSDate = (TextBox)Grow.FindControl("txtstartdate");
        TextBox txtEDate = (TextBox)Grow.FindControl("txtenddate");
        TextBox txtvalue1 = (TextBox)Grow.FindControl("txtvalue");
        TextBox txtIndirectCost = (TextBox)Grow.FindControl("txtIndirectCost");
        TextBox txtvariationFC1 = (TextBox)Grow.FindControl("txtvariationFC");
        TextBox txtVariationCostFC = (TextBox)Grow.FindControl("txtvariationCostForcast");

        SqlDataSource1.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();
        SqlDataSource1.UpdateParameters["ProjectReference"].DefaultValue = QueryStringValues.Project.ToString();
        SqlDataSource1.UpdateParameters["StartDate"].DefaultValue = txtSDate.Text;
        SqlDataSource1.UpdateParameters["EndDate"].DefaultValue = txtEDate.Text;
        SqlDataSource1.UpdateParameters["VariationForcast"].DefaultValue = string.Format("{0:c}", txtvariationFC1.Text);
        SqlDataSource1.UpdateParameters["VariationCostForcast"].DefaultValue = txtVariationCostFC.Text;
        SqlDataSource1.UpdateParameters["DeviationValue"].DefaultValue = txtvalue1.Text;
        SqlDataSource1.UpdateParameters["IndirectCost"].DefaultValue = txtIndirectCost.Text;


        SqlDataSource1.Update();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double salesPrice = double.Parse(DataBinder.Eval(e.Row.DataItem, "DeviationValue").ToString());
            double percentage = double.Parse(string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "PercentageComplete").ToString()) ? "0" : DataBinder.Eval(e.Row.DataItem, "PercentageComplete").ToString());
            double totalSalesComplete = (salesPrice * (percentage / 100));

            Label lblTotalSalesComplete = (Label)e.Row.FindControl("lblTotalSalesComplete");
            lblTotalSalesComplete.Text = totalSalesComplete.ToString("C");

            //Top section binding
            totalVariation += double.Parse(DataBinder.Eval(e.Row.DataItem, "DeviationValue").ToString());
            completeToDate += totalSalesComplete;

        }
        lblTotalVariations.Text = totalVariation.ToString("C");
        lblCompleteToDate.Text = completeToDate.ToString("C");
    }
    //protected void imgAddHours_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        decimal additionalHours = 0;
    //        if (!string.IsNullOrEmpty(txtAdditionalHours.Text.Trim()))
    //        {
    //            additionalHours = Convert.ToDecimal(ChangeToDouble(txtAdditionalHours.Text.Trim()));
    //        }
    //        using (FinanceModuleDataContext db = new FinanceModuleDataContext())
    //        {
    //            DateTime dt = DateTime.Now;
            
    //            VariationBreakdownHour variationBreakdownHour = new VariationBreakdownHour();
    //            variationBreakdownHour.ProjectReference = int.Parse(Request.QueryString["project"].ToString());
    //            variationBreakdownHour.UserID = int.Parse(ddlUser.SelectedValue);
    //            variationBreakdownHour.VariationID = int.Parse(hfVariationID.Value);
    //            variationBreakdownHour.WCDate = dt.AddDays(((int)(dt.DayOfWeek) * -1) + 1);
    //            variationBreakdownHour.AdditionalHours = additionalHours;
    //            db.VariationBreakdownHours.InsertOnSubmit(variationBreakdownHour);
    //            db.SubmitChanges();
    //        }
    //        BindBreakdownHoursGrid();
    //        mdlBreakdownHours.Show();

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    public string ChangeHours(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { '.', ',' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + ":" + displayTime[1];
            return GetActivity;
        }
        else
        {
            return "0:00";
        }



    }
    public string ChangeToDouble(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { ':' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + "." + displayTime[1];
            return GetActivity;
        }
        else
        {
            return GetActivity;
        }
    }

    //private void BindBreakdownHoursGrid()
    //{
    //    try
    //    {
    //        using (UserDataContext db = new UserDataContext())
    //        {
    //            using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
    //            {
    //                var userList = db.Contractors.Where(c => c.Status.ToLower() == "active" && (c.SID ==1 || c.SID ==2|| c.SID ==3)).Select(c => c).ToList();
    //                var breakdownList = fd.VariationBreakdownHours.Where(f => f.ProjectReference == int.Parse(Request.QueryString["project"].ToString()) && f.VariationID == int.Parse(hfVariationID.Value)).Select(f=>f).ToList();

    //                var gridResult = (from b in breakdownList
    //                                  join c in userList on b.UserID equals c.ID
    //                                  select new { b.ID, b.AdditionalHours, c.ContractorName,b.WCDate }).ToList();

    //                gvBreakdownHours.DataSource = gridResult;
    //                gvBreakdownHours.DataBind();

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //protected void gvBreakdownHours_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "Delete1")
    //        {
    //            int id = int.Parse(e.CommandArgument.ToString());
    //            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
    //            {
    //                var breakdownHours = db.VariationBreakdownHours.Where(v => v.ID == id).Select(v => v).FirstOrDefault();
    //                if (breakdownHours != null)
    //                {
    //                    db.VariationBreakdownHours.DeleteOnSubmit(breakdownHours);
    //                    db.SubmitChanges();
    //                }
    //            }

    //            mdlBreakdownHours.Show();
    //            BindBreakdownHoursGrid();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
}