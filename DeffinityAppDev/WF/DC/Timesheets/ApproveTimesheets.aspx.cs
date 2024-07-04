using CheckTimesheetApprovers;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet_Admin;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Timesheets
{
    public partial class ApproveTimesheets : System.Web.UI.Page
    {
        string javaScript = string.Empty;
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        static private DataView dvProducts;
        private const string ASCENDING1 = " ASC";
        private const string DESCENDING1 = " DESC";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                NewMethod();
                getFirstGrid();
            }
        }

        private static void NewMethod()
        {
            var p = TimesheetMgt.BAL.TimesheetApproverBAL.TimesheetApproverBAL_Select(sessionKeys.PortfolioID);
            if (p == null)
            {
                TimesheetMgt.BAL.TimesheetApproverBAL.TimesheetApproverBAL_Update(sessionKeys.UID, sessionKeys.PortfolioID);
            }
            else
            {

                if (p.ApproverID == 0)
                    TimesheetMgt.BAL.TimesheetApproverBAL.TimesheetApproverBAL_Update(sessionKeys.UID, sessionKeys.PortfolioID);
            }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        public void getFirstGrid()
        {
            #region Gridviewone
            TimeSheetAdminApprove DisplayGrid11 = null;
            DisplayGrid11 = new TimeSheetAdminApprove();


            dvProducts = new DataView(DisplayGrid11.displayTimesheet_SendforApproval(Convert.ToInt32(sessionKeys.UID)).Tables[0]);
            BindGridView();
            #endregion
        }
        private void BindGridView()
        {
            try
            {
                ITimesheetRepository<TimesheetMgt.Entity.TimesheetViewByWCDateResult> tRep = new TimesheetRepository<TimesheetMgt.Entity.TimesheetViewByWCDateResult>();

                GridView1.DataSource = dvProducts;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {

            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("chkAll1")).Attributes.Add("onclick", "javascript:SelectAll1('" +
                        ((CheckBox)e.Row.FindControl("chkAll1")).ClientID + "')");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                if (objList != null)
                {
                    if (e.Row.FindControl("ddlTitle") != null)
                    {
                        DropDownList ddlTitle = e.Row.FindControl("ddlTitle") as DropDownList;
                        Label ContratorID = e.Row.FindControl("lblContractorID") as Label;
                        var conID = Convert.ToInt32(ContratorID.Text);

                        ddlTitle.DataSource = GetProjectList(conID);
                        ddlTitle.DataTextField = "ProjectTitle";
                        ddlTitle.DataValueField = "ProjectReference";
                        ddlTitle.DataBind();
                        ddlTitle.SelectedValue = objList[3].ToString();

                        //DropDownList ddlProjectTasks = e.Row.FindControl("GetProjectTasks") as DropDownList;
                        ////ddlProjectTasks.DataSource = GetProjectTaskList(int.Parse(objList[3].ToString()));
                        //ddlProjectTasks.DataSource = GetProjectTaskList(int.Parse(ddlTitle.SelectedValue.ToString()));
                        //ddlProjectTasks.DataTextField = "ProjectTask";
                        //ddlProjectTasks.DataValueField = "TaskID";
                        //ddlProjectTasks.DataBind();
                        try
                        {
                            ddlTitle.SelectedValue = objList[3].ToString();

                           // ddlProjectTasks.SelectedValue = objList[4].ToString();
                        }
                        catch (Exception ex)
                        {
                            ddlTitle.SelectedIndex = 0;
                           // ddlProjectTasks.SelectedIndex = 0;
                        }

                    }
                }
            }
        }
        private DataTable GetProjectList(int conID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheet_ProjectTile", new SqlParameter("@ContractorID", conID)).Tables[0];
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                try
                {

                    int ID = Convert.ToInt32(e.CommandArgument.ToString());

                    string GetCID = string.Empty;


                    GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    GetCID = ((Label)gvrow.FindControl("lblContractorID1")).Text;

                    HiddenField4.Value = GetCID;
                    //GetContractorID = 

                    GetViewofSendforApproval.Visible = true;
                    GetIDAccept.Visible = true;
                    TextBox1.Visible = true;
                    btn_approve.Visible = true;
                    btn_declined.Visible = true;
                    ImgClose.Visible = true;
                    HiddenField1.Value = ID.ToString();
                    HiddenField2.Value = "";

                    HiddenField3.Value = "";
                    TimeSheetAdminApprove ViewTimeSheet = null;
                    ViewTimeSheet = new TimeSheetAdminApprove();
                    GridView4.DataSource = ViewTimeSheet.ViewTimeSheet_SubmittApproval(ID, 2, Convert.ToInt32(HiddenField4.Value), Convert.ToInt32(sessionKeys.UID));
                    GridView4.Columns[0].Visible = true;
                    GridView4.DataBind();
                    ModalControlExtender2.Show();

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }

            }
        }

        protected void btn_approve_Click(object sender, EventArgs e)
        {
            try
            {
                Timesheet_Approve();
                //changeStatus(4);
                DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);

                TextBox1.Text = "";

                getFirstGrid();
                ////GetSecondGrid();
                ////BindPendingGridView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btn_innerapprove_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                gv = (GridView)GridView1.FindControl("gvInnerTimeSheet");
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //GridViewRow row = GridView4.Rows[i];
                    //bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;

                    //if (isChecked)
                    //{
                    //    //get timesheet id
                    //    Label lblTimesheetid = ((Label)row.FindControl("lblID"));
                    //    //Approver by timesheet ID
                    //    ApproveByID(int.Parse(lblTimesheetid.Text), TextBox1.Text.Trim());
                    //}
                }
                //Timesheet_Approve();
                ////changeStatus(4);
                //DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);

                //TextBox1.Text = "";

                //getFirstGrid();
                ////GetSecondGrid();
                ////BindPendingGridView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btn_declined_Click(object sender, EventArgs e)
        {
            //changeStatusDenyed(3);
            DeclineTime();
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
            TextBox1.Text = "";
            getFirstGrid();
            ////GetSecondGrid();
            //// BindPendingGridView();
        }

        private void DeclineTime()
        {
            try
            {
                string TimeSheetIDs = string.Empty;
                string TIMEID = string.Empty;
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    GridViewRow row = GridView4.Rows[i];

                    bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
                    //contractorid = ((Label)row.FindControl("lblContractorID")).Text;
                    if (isChecked)
                    {
                        Label lbl1 = ((Label)row.FindControl("lblID"));
                        //contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                        //ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                        //submitids = submitids + "," + contractorid1;
                        TIMEID = ((Label)row.FindControl("lblID")).Text;
                        TimeSheetIDs = TimeSheetIDs + "," + TIMEID;

                    }
                }
                if (!string.IsNullOrEmpty(TimeSheetIDs))
                {
                    TimeSheetAdminApprove tm = new TimeSheetAdminApprove();
                    tm.Timesheet_Decline(TimeSheetIDs, sessionKeys.UID, TextBox1.Text);
                    TextBox1.Text = string.Empty;
                    //send decline mails to Users 
                    //SendDdeclineMail(TimeSheetIDs);
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                GridView4.PageIndex = e.NewPageIndex;
                DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {

            }



        }
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                try
                {
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    int i = GridView4.EditIndex;
                    GridViewRow Row = GridView4.Rows[i];
                    int ProjectReference1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddlTitle")).SelectedItem.Value);
                    int UpdateProjectTaskID = Convert.ToInt32(((DropDownList)Row.FindControl("GetProjectTasks")).SelectedItem.Value);
                    int contractorID = 0;
                    contractorID = Convert.ToInt32(((Label)Row.FindControl("lblContractorID")).Text);
                    int TimesheetentryType = 0;
                    TimesheetentryType = Convert.ToInt32(((Label)Row.FindControl("lblEntryType")).Text);
                    double Hours1 = 0;
                    if (((TextBox)Row.FindControl("txtHours")).Text != "")
                    {
                        string val = ((TextBox)Row.FindControl("txtHours")).Text;
                        char[] comm = { ':' };
                        string[] getva = val.Split(comm);

                        string newval = "";

                        newval = getva[0] + "." + getva[1];
                        Hours1 = Convert.ToDouble(newval);
                    }
                    Chekingtimesheetapprovers UpdateTimesheetDeatilsentry = new Chekingtimesheetapprovers();
                    UpdateTimesheetDeatilsentry.updatedetailsTimesheet(ID, ProjectReference1, UpdateProjectTaskID, Hours1, contractorID, TimesheetentryType);


                    DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                finally
                {
                    DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
                }

            }

            ModalControlExtender2.Show();

        }
        protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView4.EditIndex = e.NewEditIndex;
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
            ModalControlExtender2.Show();
        }
        protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView4.EditIndex = -1;
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
            ModalControlExtender2.Show();
        }
        protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView4.EditIndex = -1;
            DeatislTimesheetGridDisplay(HiddenField1.Value, HiddenField2.Value, HiddenField3.Value);
            ModalControlExtender2.Show();
        }
        public void DeatislTimesheetGridDisplay(string firstgridhiddenvalue, string secondgridhiddenvalue, string thirdgridjihhenvalue)
        {
            try
            {
                TimeSheetAdminApprove ViewTimeSheet11 = null;
                ViewTimeSheet11 = new TimeSheetAdminApprove();

                if ((firstgridhiddenvalue != "") && (secondgridhiddenvalue == "") && (thirdgridjihhenvalue == ""))
                {
                    GridView4.DataSource = ViewTimeSheet11.ViewTimeSheet_SubmittApproval(Convert.ToInt32(firstgridhiddenvalue), 2, Convert.ToInt32(HiddenField4.Value), Convert.ToInt32(sessionKeys.UID));
                    GridView4.DataBind();
                }
                else if ((secondgridhiddenvalue != "") && (firstgridhiddenvalue == "") && (thirdgridjihhenvalue == ""))
                {
                    GridView4.DataSource = ViewTimeSheet11.ViewTimeSheet_SubmittApproval(Convert.ToInt32(secondgridhiddenvalue), 3, 0, Convert.ToInt32(sessionKeys.UID));
                    GridView4.DataBind();
                }

                else if ((thirdgridjihhenvalue != "") && (secondgridhiddenvalue == "") && (firstgridhiddenvalue == ""))
                {
                    TimeSheetAdminApprove PendingTimesheet = null;
                    PendingTimesheet = new TimeSheetAdminApprove();
                    if (thirdgridjihhenvalue == "S")
                    {
                        GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1("", 1, Convert.ToInt32(sessionKeys.UID));
                        GridView4.DataBind();
                    }
                    else
                    {
                        GridView4.DataSource = PendingTimesheet.ViewTimeSheet_SubmittApproval1(thirdgridjihhenvalue, 1, Convert.ToInt32(sessionKeys.UID));
                        GridView4.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.Header)
            {

                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
            }
            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            TimeSheetAdminApprove ViewTimeSheet = null;
            ViewTimeSheet = new TimeSheetAdminApprove();
            //Find Child GridView control
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("gvInnerTimeSheet");
            if (gv.UniqueID == gvUniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;
                //Check if Sorting used


            }
            DataSet ds = ViewTimeSheet.ViewTimeSheet_SubmittApproval(Convert.ToInt32(((DataRowView)e.Row.DataItem)["WCDateID"]), 2, Convert.ToInt32(((DataRowView)e.Row.DataItem)["ContractorID"]), Convert.ToInt32(sessionKeys.UID));
            gv.DataSource = ds;
            gv.DataBind();
        }

        protected void btn_ApprovalAll_Click(object sender, EventArgs e)
        {
            try
            {
                Timesheet_ApproveAll();
                //ApprovalAll(4);
                getFirstGrid();
                BindGridView();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public string ChangePlanner(string Planner)
        {
            string GetActivity = "";

            if (Planner == "Y")
            {
                GetActivity = "Yes";
            }
            else
            {
                GetActivity = "No";
            }

            return GetActivity;
        }

        #region new implementations
        private void Timesheet_ApproveAll()
        {
            bool isSelectd = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;

                if (isChecked)
                {
                    isSelectd = true;
                    //get week commencing date id
                    Label lblWCdateID = ((Label)row.FindControl("lblWCDateID"));
                    Label lblResourceID = (Label)row.FindControl("lblContractorID1");



                    //Approver by WCdateID
                    ApproveByWCdateID(int.Parse(lblWCdateID.Text), int.Parse(lblResourceID.Text));
                    isSelectd = true;



                }
            }

            if (!isSelectd)
                lblError.Text = "Please select timesheet";
        }

        private void ApproveByWCdateID(int WCdateID, int ResourceID)
        {
            try
            {
                SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
                outval.Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntryByWcdate_Approve", new SqlParameter("@WCdateID", WCdateID), new SqlParameter("@ApproverID", sessionKeys.UID), outval);

                int retval = int.Parse(outval.Value.ToString());
                //pending primary approve 
                if (retval == 1)
                    lblError.Text = "";
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        private void Timesheet_Approve()
        {
            for (int i = 0; i < GridView4.Rows.Count; i++)
            {
                GridViewRow row = GridView4.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;

                if (isChecked)
                {
                    //get timesheet id
                    Label lblTimesheetid = ((Label)row.FindControl("lblID"));
                    //Approver by timesheet ID
                    ApproveByID(int.Parse(lblTimesheetid.Text), TextBox1.Text.Trim());
                }
            }
        }

        private void ApproveByID(int TimesheetID, string Comments)
        {
            try
            {
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    GridViewRow row = GridView4.Rows[i];
                    Label lbl1 = ((Label)row.FindControl("lblID"));
                    string contractorid1 = "";
                    int ProjectID;
                    contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;

                    ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect2")).Checked;
                    if (isChecked)
                    {
                        //TimeSheetDataContext timeSheet = new TimeSheetDataContext();

                        //var myTimesheet = from t in timeSheet.TimesheetEntries
                        //                  where t.ID == int.Parse(lbl1.Text) && t.ContractorID == int.Parse(contractorid1) // || t.PrimeApprover == int.Parse(contractorid1)
                        //                  select new {ID= t.ID, ProjectRef = t.ProjectReference };

                        //if (myTimesheet.Count() > 0)
                        //{
                        //    lblError.Visible = true;
                        //    lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                        //}
                        //else
                        //{

                        SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
                        outval.Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntry_Approve", new SqlParameter("@timesheetID", TimesheetID), new SqlParameter("@ApproverID", sessionKeys.UID), new SqlParameter("@Comments", Comments), outval);

                        int retval = int.Parse(outval.Value.ToString());
                        //pending primary approve 
                        if (retval == 0)
                        {
                            lblError.Visible = true;
                            lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                        }
                        //else
                        //{
                        //pending secondary approve
                        //if (retval == 2)
                        //    lblError.Visible = true;
                        //    lblError.Text = "Secondary timesheet approver should approve timesheet(s)";
                        //else
                        //lblError.Text = "";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        #endregion
       
        #region Sort The gird


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                dvProducts.Sort = sortExpression + DESCENDING1;
                BindGridView();
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                dvProducts.Sort = sortExpression + ASCENDING1;
                BindGridView();
            }


        }




        #endregion

        public string ChangeToHours(string GetHours)
        {
            string GetActivity = "";
            char[] comm1 = { '.' };
            string[] displayTime = GetHours.Split(comm1);


            GetActivity = displayTime[0] + ":" + displayTime[1];



            return GetActivity;
        }


        protected void gvInnerTimeSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.Header)
            {

                ((CheckBox)e.Row.FindControl("chkAll1")).Attributes.Add("onclick", "javascript:SelectAllTimeSheet('" +
                        ((CheckBox)e.Row.FindControl("chkAll1")).ClientID + "')");
            }
            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                bool isChecked = ((CheckBox)e.Row.FindControl("chkSelect2")).Checked;
                var status = ((Label)e.Row.FindControl("lblStatusName")).Text;
                if (status == "Approved")
                { e.Row.BackColor = System.Drawing.Color.LightYellow; }

            }


        }
        protected void gvInnerTimeSheet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewSignoff")
            {
                try
                {
                    string wcdateid_contractorid = e.CommandArgument.ToString();
                    string[] darray = wcdateid_contractorid.Split(',');
                    BindSignoffGrid(Convert.ToInt32(darray[0]), Convert.ToInt32(darray[1]));
                    mdlSignoff.Show();
                    getFirstGrid();
                    GridView gvTemp = (GridView)sender;
                    int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
                    gvUniqueID = gvTemp.UniqueID;
                    getFirstGrid();
                    javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
                    ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }

            if (e.CommandName == "ApproveClick")
            {
                GridView gvInnerTimeSheet = sender as GridView;
                for (int i = 0; i < gvInnerTimeSheet.Rows.Count; i++)
                {
                    bool isChecked = ((CheckBox)gvInnerTimeSheet.Rows[i].FindControl("chkSelect2")).Checked;
                    if (isChecked)
                    {
                        //txtComments
                        TextBox txtComments = ((TextBox)gvInnerTimeSheet.FooterRow.FindControl("txtComments"));
                        //get timesheet id
                        Label lblTimesheetid = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID"));
                        //Approver by timesheet ID

                        SqlParameter outval = new SqlParameter("@Outval", SqlDbType.Int);
                        outval.Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TimesheetEntry_Approve", new SqlParameter("@timesheetID", int.Parse(lblTimesheetid.Text)), new SqlParameter("@ApproverID", sessionKeys.UID), new SqlParameter("@Comments", txtComments.Text.Trim()), outval);

                        int retval = int.Parse(outval.Value.ToString());
                        //pending primary approve 
                        if (retval == 0)
                        {
                            lblError.Visible = true;
                            lblError.Text = "Sorry but you cannot approve timesheets where you are the owner/primary approver of the project";
                        }

                    }
                }

                getFirstGrid();
            }
            if (e.CommandName == "DeclineClick")
            {

                string TimeSheetIDs = string.Empty;
                string TIMEID = string.Empty;
                GridView gvInnerTimeSheet = sender as GridView;
                for (int i = 0; i < gvInnerTimeSheet.Rows.Count; i++)
                {


                    bool isChecked = ((CheckBox)gvInnerTimeSheet.Rows[i].FindControl("chkSelect2")).Checked;
                    //contractorid = ((Label)row.FindControl("lblContractorID")).Text;
                    if (isChecked)
                    {
                        Label lbl1 = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID"));
                        //contractorid1 = ((Label)row.FindControl("lblContractorID")).Text;
                        //ProjectID = Convert.ToInt32(((Label)row.FindControl("lblPref")).Text);
                        //submitids = submitids + "," + contractorid1;
                        TIMEID = ((Label)gvInnerTimeSheet.Rows[i].FindControl("lblID")).Text;
                        TimeSheetIDs = TimeSheetIDs + "," + TIMEID;

                    }
                }
                if (!string.IsNullOrEmpty(TimeSheetIDs))
                {
                    //txtComments
                    TextBox txtComments = ((TextBox)gvInnerTimeSheet.FooterRow.FindControl("txtComments"));
                    TimeSheetAdminApprove tm = new TimeSheetAdminApprove();
                    tm.Timesheet_Decline(TimeSheetIDs, sessionKeys.UID, txtComments.Text);
                    //TextBox1.Text = string.Empty;
                    //send decline mails to Users 
                    //SendDdeclineMail(TimeSheetIDs);
                }
                getFirstGrid();
            }

        }
        protected void gvInnerTimeSheet_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            getFirstGrid();
            javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);
        }
        protected void gvInnerTimeSheet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            getFirstGrid();
            javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);

        }
        protected void gvInnerTimeSheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;

            int id = Convert.ToInt32(((Label)gvTemp.Rows[e.RowIndex].FindControl("lblID")).Text);
            string comments = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtApproveComments")).Text;
            using (TimeSheetDataContext td = new TimeSheetDataContext())
            {
                TimesheetEntry timesheetEntry = td.TimesheetEntries.Where(t => t.ID == id).FirstOrDefault();
                if (timesheetEntry != null)
                {
                    timesheetEntry.ApproverComments = comments;
                    td.SubmitChanges();
                }
            }
            gvEditIndex = -1;
            int GetSubSectionID = Convert.ToInt32(gvTemp.DataKeys[0].Value);
            getFirstGrid();
            javaScript += "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + GetSubSectionID.ToString() + "','alt');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Expand", javaScript);

        }

        #region Bind Grid based on week id 
        public void BindSignoffGrid(int wcdateid, int contractorid)
        {
            try
            {
                TimeSheetAdminApprove td = new TimeSheetAdminApprove();
                DataSet ds = td.dsTimeSheet_Signoff(wcdateid, contractorid);
                gvSignoff.DataSource = ds.Tables[0];
                gvSignoff.DataBind();
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
            char[] comm1 = { '.' };
            string[] displayTime = GetHours.Split(comm1);


            GetActivity = displayTime[0] + ":" + displayTime[1];



            return GetActivity;
        }

       
    }
}